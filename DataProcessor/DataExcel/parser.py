import openpyxl
import sqlite3

from os import path


BASE_DIR = path.dirname(__file__)
SQL_DIR = "sql"
TABLES_SCRIPT = path.join(BASE_DIR, SQL_DIR, "tables.sql")


class Negative:
    def __init__(self, id: int, name: str, neg_type: int):
        self.id = id
        self.name = name
        self.type = neg_type


class Threat:
    def __init__(
        self,
        id: int,
        name: str,
        desc: str,
        intruders: list[int],
        objects: list[int],
        violation: int,
        add_date: str,
        upd_date: str,
    ):
        self.id = id
        self.name = name
        self.desc = desc

        self.intruders = intruders
        self.objects = objects

        self.violation = violation

        self.add_date = add_date
        self.upd_date = upd_date


class Intruder:
    def __init__(self, id: int, name: str):
        self.id = id
        self.name = name

        self.type = self.get_type(name.lower())
        self.potential = self.get_potential(name.lower())
        pass

    def get_potential(self, name: str) -> int:
        if "низк" in name:
            return 1
        if "средн" in name:
            return 2
        if "высок" in name:
            return 3

    def get_type(self, name: str) -> int:
        if "внутр" in name:
            return 1
        if "внешн" in name:
            return 2


def parse_negatives(negatives_file: str, cursor):
    with open(negatives_file, 'r', encoding="utf-8") as f:
        lines = f.readlines()

    negative_types = []
    negatives = []
    
    has_type = False
    for line in lines:
        line = line.replace('\r', '').replace('\r', '').strip()

        if not line:
            has_type = False
            continue

        if not has_type:
            has_type = True
            negative_types.append(line)
            continue

        negatives.append(Negative(len(negatives) + 1, line, len(negative_types)))

    for i, neg_type in enumerate(negative_types):
        sql = """
            INSERT INTO negative_types(id, name)
            VALUES (?, ?);
            """
        cursor.execute(sql, (i + 1, neg_type))

    for negative in negatives:
        sql = """
            INSERT INTO negatives(id, type, name)
            VALUES (?, ?, ?);
            """
        cursor.execute(sql, (negative.id, negative.type, negative.name))


def parse_objects(objects_str: str) -> list[str]:
    

    return map(
        lambda s: s[0].title() + s[1:],
        map(lambda s: s.strip(), objects_str.split(separator)),
    )


def parse_threats(excel_file: str, cursor):    
    wb = openpyxl.load_workbook(excel_file, True)

    intruders_names = []
    objects_names = []
    threats = []

    threats_sheet = wb.worksheets[0]

    for i, row in enumerate(threats_sheet):
        if i < 2 or len(row[1].value.strip()) < 1:
            continue

        intruders_ids = []
        objects_ids = []

        included_intruders = map(
            lambda s: s[0].title() + s[1:] if len(s) > 0 else s,
            map(lambda s: s.strip(), row[3].value.split(";")),
        )
        for intruder_name in included_intruders:
            if len(intruder_name) < 1:
                continue
            if intruder_name not in intruders_names:
                intruders_names.append(intruder_name)
            intruders_ids.append(intruders_names.index(intruder_name) + 1)

        included_objects = map(
            lambda s: s[0].title() + s[1:],
            map(lambda s: s.strip(), row[4].value.split(';')),
        )
        for object_name in included_objects:
            if object_name not in objects_names:
                objects_names.append(object_name)
            objects_ids.append(objects_names.index(object_name) + 1)

        violations = sum(
            map(lambda x: int(x[1].value) << (2 - x[0]), enumerate(row[5:8]))
        )

        threats.append(
            Threat(
                int(row[0].value),
                row[1].value,
                row[2].value,
                intruders_ids,
                objects_ids,
                violations,
                row[8].value.strftime("%Y-%m-%d"),
                row[9].value.strftime("%Y-%m-%d"),
            )
        )

    for i, intruder_name in enumerate(intruders_names):
        intruder = Intruder(i + 1, intruder_name)
        sql = """
            INSERT OR IGNORE INTO intruders (id, name, type, potential) 
            VALUES (?, ?, ?, ?);
            """
        cursor.execute(sql, (intruder.id, intruder.name, intruder.type, intruder.potential))

    for i, obj in enumerate(objects_names):
        sql = """
            INSERT OR IGNORE INTO objects (id, name) 
            VALUES (?, ?);
            """
        cursor.execute(sql, (i + 1, obj))

    for threat in threats:
        sql = """
            INSERT OR IGNORE INTO threats (id, name, description, violations, add_date, update_date) 
            VALUES (?, ?, ?, ?, ?, ?);
            """

        cursor.execute(
            sql,
            (
                threat.id,
                threat.name,
                threat.desc,
                threat.violation,
                threat.add_date,
                threat.upd_date,
            ),
        )

        for intruder in threat.intruders:
            sql = """
            INSERT OR IGNORE INTO threats_intruders (threat_id, intruder_id) 
            VALUES (?, ?);
            """
            cursor.execute(sql, (threat.id, intruder))

        for obj in threat.objects:
            sql = """
            INSERT OR IGNORE INTO threats_objects (threat_id, object_id) 
            VALUES (?, ?);
            """
            cursor.execute(sql, (threat.id, obj))


def main(excel_file: str, negatives_file: str, database: str):
    connection = sqlite3.connect(database)
    cursor = connection.cursor()

    with open(TABLES_SCRIPT, "r", encoding="UTF-8") as f:
        tables_command = f.read()

    cursor.executescript(tables_command)
    connection.commit()

    parse_negatives(negatives_file, cursor)
    connection.commit()

    parse_threats(excel_file, cursor)
    connection.commit()

    connection.close()


if __name__ == "__main__":
    
    input_folder = "Input"
    
    excel_file = path.join(input_folder, "thrlist.xlsx")
    negatives_file = path.join(input_folder, "negatives.txt")

    main(excel_file, negatives_file, "threats.db")
