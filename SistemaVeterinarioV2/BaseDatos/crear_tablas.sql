PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS Mascotas (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Codigo TEXT NOT NULL UNIQUE,
    Nombre TEXT NOT NULL,
    Especie TEXT NOT NULL,
    Propietario TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS ConsultasVeterinarias (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    MascotaId INTEGER NOT NULL,
    Servicio TEXT NOT NULL,
    Peso REAL NOT NULL CHECK (Peso > 0),
    Temperatura REAL NOT NULL CHECK (Temperatura BETWEEN 35 AND 43),
    Costo NUMERIC NOT NULL CHECK (Costo > 0),
    Fecha TEXT NOT NULL,
    FOREIGN KEY (MascotaId) REFERENCES Mascotas(Id) ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS IX_ConsultasVeterinarias_MascotaId
    ON ConsultasVeterinarias(MascotaId);
