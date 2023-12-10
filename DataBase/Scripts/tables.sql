-- Table: Personne
CREATE TABLE IF NOT EXISTS Personne (
    PersonId INTEGER PRIMARY KEY AUTOINCREMENT,
    FirstName TEXT,
    LastName TEXT,
    AddressNumber INTEGER,
    AddressStreet TEXT,
    PostalCode TEXT,
    City TEXT
);

-- Table: Denonciation
CREATE TABLE IF NOT EXISTS Denonciation (
    DenonciationId INTEGER PRIMARY KEY AUTOINCREMENT,
    Timestamp DATETIME,
    InformateurId INTEGER,
    SuspectId INTEGER,
    Delit TEXT,
    PaysEvasion TEXT
);

-- Table: Reponse
CREATE TABLE IF NOT EXISTS Reponse (
    ReponseId INTEGER PRIMARY KEY AUTOINCREMENT,
    DenonciationId INTEGER,
    ResponseType TEXT,
    RetributionAmount REAL,
    FOREIGN KEY(DenonciationId) REFERENCES Denonciation(DenonciationId)
);

-- Table: VIP
CREATE TABLE IF NOT EXISTS VIP (
    VIPId INTEGER PRIMARY KEY AUTOINCREMENT,
    PersonId INTEGER,
    FOREIGN KEY(PersonId) REFERENCES Personne(PersonId)
);

-- Table: Calomniateur
CREATE TABLE IF NOT EXISTS Calomniateur (
    CalomniateurId INTEGER PRIMARY KEY AUTOINCREMENT,
    PersonId INTEGER,
    InformateurId INTEGER,
    CountRejet INTEGER DEFAULT 0,
    FOREIGN KEY(PersonId) REFERENCES Personne(PersonId),
    FOREIGN KEY(InformateurId) REFERENCES Personne(PersonId)
);
