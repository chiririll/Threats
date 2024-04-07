import json
import sqlite3

TABLES_SCRIPT = "sql/tables.sql"
ID_BYTES = 8


def parse_id(id_string: str) -> int:
    return sum(
        map(
            lambda x: x[1] << (x[0] * ID_BYTES),
            enumerate(map(int, id_string.split(".")[1:])),
        )
    )


def restore_id(id: int) -> str:
    mask = 2**ID_BYTES - 1
    ids = []
    while id != 0:
        ids.append(str(mask & id))
        id = id >> ID_BYTES
    return ".".join(ids)


def parse_node(cursor: sqlite3.Cursor, node: dict, table_name: str) -> id:
    id = parse_id(node["idf"])
    name = node["name"]

    sql = f"""
        INSERT OR IGNORE INTO {table_name} (id, name)
        VALUES (?, ?);
        """
    cursor.execute(sql, (id, name))

    return id


def parse_collection(
    cursor: sqlite3.Cursor,
    threat_id: int,
    array: list[dict],
    match_table_name: str,
    math_table_column: str,
    obj_table_name: str,
):
    for node in array:
        id = parse_node(cursor, node, obj_table_name)

        sql = f"""
            INSERT INTO {match_table_name} (threat_id, {math_table_column})
            VALUES (?, ?);
            """

        cursor.execute(sql, (threat_id, id))


def main(filename: str, database: str):
    connection = sqlite3.connect(database)
    cursor = connection.cursor()

    with open(TABLES_SCRIPT, "r", encoding="UTF-8") as f:
        tables_command = f.read()

    cursor.executescript(tables_command)
    connection.commit()

    with open(filename, "r", encoding="UTF-8") as f:
        threats = json.load(f)

    for category in threats:
        for threat in category["threats"]:

            id = parse_id(threat["idf"])
            name = threat["name"]
            description = threat["description"]

            type_id = parse_node(cursor, threat["threatType"], "threat_types")
            object_id = parse_node(cursor, threat["object"], "objects")
            tech_group_id = parse_node(cursor, threat["techGroup"], "tech_groups")

            sql = f"""
                INSERT INTO threats(id, name, description, threat_type, object, tech_group)
                VALUES (?, ?, ?, ?, ?, ?);
                """
            cursor.execute(
                sql, (id, name, description, type_id, object_id, tech_group_id)
            )

            parse_collection(cursor, id, threat["defGroups"], "threats_def_groups", "def_group_id", "def_groups")
            parse_collection(cursor, id, threat["technics"], "threats_technics", "technics_id", "technics")
            parse_collection(cursor, id, threat["components"], "threats_components", "components_id", "components")
            parse_collection(cursor, id, threat["potentials"], "threats_potentials", "potentials_id", "potentials")
            parse_collection(cursor, id, threat["defenses"], "threats_defenses", "defenses_id", "defenses")

    connection.commit()
    connection.close()


if __name__ == "__main__":
    main("data/Перечень сформированных угроз.json", "data/threats.db")
