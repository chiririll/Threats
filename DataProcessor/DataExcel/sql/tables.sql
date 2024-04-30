CREATE TABLE IF NOT EXISTS threats (
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL,
    description TEXT,
    
    violations INTEGER NOT NULL,
    add_date TEXT,
    update_date TEXT
);

CREATE TABLE IF NOT EXISTS intruders (
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL,
    type INTEGER NOT NULL,
    potential INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS objects (
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS threats_intruders (
    threat_id INTEGER NOT NULL,
    intruder_id INTEGER NOT NULL,

    FOREIGN KEY (threat_id) REFERENCES threats(id),
    FOREIGN KEY (intruder_id) REFERENCES intruders(id)
);

CREATE TABLE IF NOT EXISTS threats_objects (
    threat_id INTEGER NOT NULL,
    object_id INTEGER NOT NULL,

    FOREIGN KEY (threat_id) REFERENCES threats(id),
    FOREIGN KEY (object_id) REFERENCES objects(id)
);

CREATE TABLE IF NOT EXISTS negative_types(
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS negatives(
    id INTEGER PRIMARY KEY,
    type INTEGER NOT NULL,
    name TEXT NOT NULL,
    
    FOREIGN KEY (type) REFERENCES negative_types(id)
);
