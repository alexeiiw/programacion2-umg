PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS Clientes (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Codigo TEXT NOT NULL UNIQUE,
    Nombre TEXT NOT NULL,
    Documento TEXT NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS Vehiculos (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Placa TEXT NOT NULL UNIQUE,
    Modelo TEXT NOT NULL,
    TarifaDiaria INTEGER NOT NULL CHECK (TarifaDiaria > 0)
);

CREATE TABLE IF NOT EXISTS Arriendos (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    ClienteId INTEGER NOT NULL,
    VehiculoId INTEGER NOT NULL,
    FechaInicio TEXT NOT NULL,
    FechaFin TEXT NOT NULL,
    FechaDevolucion TEXT NULL,
    Total INTEGER NULL,
    CHECK (date(FechaFin) > date(FechaInicio)),
    CHECK (FechaDevolucion IS NULL OR date(FechaDevolucion) >= date(FechaInicio)),
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id) ON DELETE CASCADE,
    FOREIGN KEY (VehiculoId) REFERENCES Vehiculos(Id) ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS IX_Arriendos_ClienteId ON Arriendos(ClienteId);
CREATE INDEX IF NOT EXISTS IX_Arriendos_VehiculoId_Estado ON Arriendos(VehiculoId, FechaDevolucion);
