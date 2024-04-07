CREATE TABLE IF NOT EXISTS threat_types (
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS objects (
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS tech_groups (
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS threats (
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL,
    description TEXT,
    threat_type INTEGER NOT NULL,
    object INTEGER NOT NULL,
    tech_group INTEGER NOT NULL,

    FOREIGN KEY (threat_type) REFERENCES threat_types(id),
    FOREIGN KEY (object) REFERENCES objects(id),
    FOREIGN KEY (tech_group) REFERENCES tech_groups(id)
);

CREATE TABLE IF NOT EXISTS def_groups (
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS technics (
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS components (
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS potentials (
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS defenses (
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS threats_def_groups (
    threat_id INTEGER NOT NULL,
    def_group_id INTEGER NOT NULL,

    FOREIGN KEY (threat_id) REFERENCES threats(id),
    FOREIGN KEY (def_group_id) REFERENCES def_groups(id)
);

CREATE TABLE IF NOT EXISTS threats_technics (
    threat_id INTEGER NOT NULL,
    technics_id INTEGER NOT NULL,

    FOREIGN KEY (threat_id) REFERENCES threats(id),
    FOREIGN KEY (technics_id) REFERENCES technics(id)
);

CREATE TABLE IF NOT EXISTS threats_components (
    threat_id INTEGER NOT NULL,
    components_id INTEGER NOT NULL,

    FOREIGN KEY (threat_id) REFERENCES threats(id),
    FOREIGN KEY (components_id) REFERENCES components(id)
);

CREATE TABLE IF NOT EXISTS threats_potentials (
    threat_id INTEGER NOT NULL,
    potentials_id INTEGER NOT NULL,

    FOREIGN KEY (threat_id) REFERENCES threats(id),
    FOREIGN KEY (potentials_id) REFERENCES potentials(id)
);

CREATE TABLE IF NOT EXISTS threats_defenses (
    threat_id INTEGER NOT NULL,
    defenses_id INTEGER NOT NULL,

    FOREIGN KEY (threat_id) REFERENCES threats(id),
    FOREIGN KEY (defenses_id) REFERENCES defenses(id)
);
