CREATE TABLE IF NOT EXISTS threats (
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL,
    description TEXT,
    
    violations INTEGER NOT NULL,
    add_date TEXT,
    update_date TEXT
);

CREATE TABLE IF NOT EXISTS sources (
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL,
    type INTEGER NOT NULL,
    potential INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS objects (
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS threats_sources (
    threat_id INTEGER NOT NULL,
    source_id INTEGER NOT NULL,

    FOREIGN KEY (threat_id) REFERENCES threats(id),
    FOREIGN KEY (source_id) REFERENCES sources(id)
);

CREATE TABLE IF NOT EXISTS threats_objects (
    threat_id INTEGER NOT NULL,
    object_id INTEGER NOT NULL,

    FOREIGN KEY (threat_id) REFERENCES threats(id),
    FOREIGN KEY (object_id) REFERENCES objects(id)
);
