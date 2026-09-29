PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS Estudiantes (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Codigo TEXT NOT NULL UNIQUE,
    Nombre TEXT NOT NULL,
    Correo TEXT NOT NULL UNIQUE,
    Carrera TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Cursos (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Codigo TEXT NOT NULL UNIQUE,
    Nombre TEXT NOT NULL,
    Creditos INTEGER NOT NULL CHECK (Creditos > 0),
    Cupo INTEGER NOT NULL CHECK (Cupo > 0)
);

CREATE TABLE IF NOT EXISTS Inscripciones (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    EstudianteId INTEGER NOT NULL,
    CursoId INTEGER NOT NULL,
    Fecha TEXT NOT NULL,
    UNIQUE (EstudianteId, CursoId),
    FOREIGN KEY (EstudianteId) REFERENCES Estudiantes(Id) ON DELETE CASCADE,
    FOREIGN KEY (CursoId) REFERENCES Cursos(Id) ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS IX_Inscripciones_EstudianteId
    ON Inscripciones(EstudianteId);

CREATE INDEX IF NOT EXISTS IX_Inscripciones_CursoId
    ON Inscripciones(CursoId);
