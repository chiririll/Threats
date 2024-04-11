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
        sources: list[int],
        objects: list[int],
        violation: int,
        add_date: str,
        upd_date: str,
    ):
        self.id = id
        self.name = name
        self.desc = desc

        self.sources = sources
        self.objects = objects

        self.violation = violation

        self.add_date = add_date
        self.upd_date = upd_date


class Source:
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


def main(excel_file: str, database: str):

    connection = sqlite3.connect(database)
    cursor = connection.cursor()

    with open(TABLES_SCRIPT, "r", encoding="UTF-8") as f:
        tables_command = f.read()

    cursor.executescript(tables_command)
    connection.commit()

    wb = openpyxl.load_workbook(excel_file, True)

    sources_names = []
    objects_names = []
    threats = []

    threats_sheet = wb.worksheets[0]
    negatives_sheet = wb.worksheets[1]

    negative_types = []
    negatives = []
    for row in negatives_sheet:
        neg_type = row[0].value
        if neg_type not in negative_types:
            negative_types.append(neg_type)
        negatives.append(
            Negative(len(negatives) + 1, row[1].value, negative_types.index(neg_type))
        )

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

    connection.commit()

    for i, row in enumerate(threats_sheet):
        if i < 2 or len(row[1].value.strip()) < 1:
            continue

        sources_ids = []
        objects_ids = []

        included_sources = map(
            lambda s: s[0].title() + s[1:] if len(s) > 0 else s,
            map(lambda s: s.strip(), row[3].value.split(";")),
        )
        for source_name in included_sources:
            if len(source_name) < 1:
                continue
            if source_name not in sources_names:
                sources_names.append(source_name)
            sources_ids.append(sources_names.index(source_name) + 1)

        included_objects = map(
            lambda s: s[0].title() + s[1:],
            map(lambda s: s.strip(), row[4].value.split(";")),
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
                sources_ids,
                objects_ids,
                violations,
                row[8].value.strftime("%Y-%m-%d"),
                row[9].value.strftime("%Y-%m-%d"),
            )
        )

    for i, source_name in enumerate(sources_names):
        source = Source(i + 1, source_name)
        sql = """
            INSERT OR IGNORE INTO sources (id, name, type, potential) 
            VALUES (?, ?, ?, ?);
            """
        cursor.execute(sql, (source.id, source.name, source.type, source.potential))

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

        for source in threat.sources:
            sql = """
            INSERT OR IGNORE INTO threats_sources (threat_id, source_id) 
            VALUES (?, ?);
            """
            cursor.execute(sql, (threat.id, source))

        for obj in threat.objects:
            sql = """
            INSERT OR IGNORE INTO threats_objects (threat_id, object_id) 
            VALUES (?, ?);
            """
            cursor.execute(sql, (threat.id, obj))

    connection.commit()


if __name__ == "__main__":

    main(path.join("Input", "thrlist.xlsx"), "threats.db")
