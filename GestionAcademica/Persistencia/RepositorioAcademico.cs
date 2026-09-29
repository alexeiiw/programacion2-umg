using System.Data;
using Microsoft.Data.Sqlite;
using GestionAcademica.Dominio;

namespace GestionAcademica.Persistencia;

internal sealed class RepositorioAcademico
{
    private readonly string _cadenaConexion;

    public RepositorioAcademico(string cadenaConexion)
    {
        _cadenaConexion = cadenaConexion;
    }

    // CRUD de estudiantes
    public DataTable ObtenerEstudiantes() => Consultar(
        "SELECT Id, Codigo, Nombre, Correo, Carrera FROM Estudiantes ORDER BY Nombre;");

    public DataTable ObtenerEstudiantesParaSeleccion() => Consultar(
        "SELECT Id, Codigo || ' - ' || Nombre AS Estudiante FROM Estudiantes ORDER BY Nombre;");

    public void CrearEstudiante(Estudiante estudiante) => Ejecutar(
        "INSERT INTO Estudiantes (Codigo, Nombre, Correo, Carrera) VALUES ($codigo, $nombre, $correo, $carrera);",
        ("$codigo", estudiante.Codigo), ("$nombre", estudiante.Nombre),
        ("$correo", estudiante.Correo), ("$carrera", estudiante.Carrera));

    public void ActualizarEstudiante(int id, Estudiante estudiante) => Ejecutar(
        "UPDATE Estudiantes SET Codigo = $codigo, Nombre = $nombre, Correo = $correo, Carrera = $carrera WHERE Id = $id;",
        ("$codigo", estudiante.Codigo), ("$nombre", estudiante.Nombre),
        ("$correo", estudiante.Correo), ("$carrera", estudiante.Carrera), ("$id", id));

    public void EliminarEstudiante(int id) => Ejecutar(
        "DELETE FROM Estudiantes WHERE Id = $id;", ("$id", id));

    // CRUD de cursos
    public DataTable ObtenerCursos() => Consultar(
        "SELECT Id, Codigo, Nombre, Creditos, Cupo FROM Cursos ORDER BY Nombre;");

    public DataTable ObtenerCursosParaSeleccion() => Consultar(
        "SELECT Id, Codigo || ' - ' || Nombre AS Curso FROM Cursos ORDER BY Nombre;");

    public void CrearCurso(Curso curso) => Ejecutar(
        "INSERT INTO Cursos (Codigo, Nombre, Creditos, Cupo) VALUES ($codigo, $nombre, $creditos, $cupo);",
        ("$codigo", curso.Codigo), ("$nombre", curso.Nombre),
        ("$creditos", curso.Creditos), ("$cupo", curso.Cupo));

    public void ActualizarCurso(int id, Curso curso) => Ejecutar(
        "UPDATE Cursos SET Codigo = $codigo, Nombre = $nombre, Creditos = $creditos, Cupo = $cupo WHERE Id = $id;",
        ("$codigo", curso.Codigo), ("$nombre", curso.Nombre),
        ("$creditos", curso.Creditos), ("$cupo", curso.Cupo), ("$id", id));

    public void EliminarCurso(int id) => Ejecutar(
        "DELETE FROM Cursos WHERE Id = $id;", ("$id", id));

    // CRUD de inscripciones
    public DataTable ObtenerInscripciones() => Consultar(@"SELECT i.Id, i.EstudianteId, i.CursoId,
        e.Codigo AS CodigoEstudiante, e.Nombre AS Estudiante,
        c.Codigo AS CodigoCurso, c.Nombre AS Curso, i.Fecha
        FROM Inscripciones i
        INNER JOIN Estudiantes e ON e.Id = i.EstudianteId
        INNER JOIN Cursos c ON c.Id = i.CursoId
        ORDER BY i.Fecha DESC, i.Id DESC;");

    public void CrearInscripcion(Inscripcion inscripcion)
    {
        using var conexion = AbrirConexion();
        using var transaccion = conexion.BeginTransaction();

        using (var duplicada = conexion.CreateCommand())
        {
            duplicada.Transaction = transaccion;
            duplicada.CommandText = "SELECT COUNT(*) FROM Inscripciones WHERE EstudianteId = $estudiante AND CursoId = $curso;";
            duplicada.Parameters.AddWithValue("$estudiante", inscripcion.EstudianteId);
            duplicada.Parameters.AddWithValue("$curso", inscripcion.CursoId);
            if (Convert.ToInt32(duplicada.ExecuteScalar()) > 0)
                throw new InvalidOperationException("El estudiante ya esta inscrito en ese curso.");
        }

        using (var cupo = conexion.CreateCommand())
        {
            cupo.Transaction = transaccion;
            cupo.CommandText = @"SELECT c.Cupo - COUNT(i.Id)
                FROM Cursos c LEFT JOIN Inscripciones i ON i.CursoId = c.Id
                WHERE c.Id = $curso GROUP BY c.Id, c.Cupo;";
            cupo.Parameters.AddWithValue("$curso", inscripcion.CursoId);
            var disponibles = cupo.ExecuteScalar();
            if (disponibles is null || disponibles is DBNull)
                throw new InvalidOperationException("El curso seleccionado no existe.");
            if (Convert.ToInt32(disponibles) <= 0)
                throw new InvalidOperationException("El curso ya alcanzo su cupo maximo.");
        }

        using (var comando = conexion.CreateCommand())
        {
            comando.Transaction = transaccion;
            comando.CommandText = "INSERT INTO Inscripciones (EstudianteId, CursoId, Fecha) VALUES ($estudiante, $curso, $fecha);";
            comando.Parameters.AddWithValue("$estudiante", inscripcion.EstudianteId);
            comando.Parameters.AddWithValue("$curso", inscripcion.CursoId);
            comando.Parameters.AddWithValue("$fecha", inscripcion.Fecha.ToString("O"));
            comando.ExecuteNonQuery();
        }

        transaccion.Commit();
    }

    public void ActualizarInscripcion(int id, Inscripcion inscripcion)
    {
        using var conexion = AbrirConexion();
        using var transaccion = conexion.BeginTransaction();
        ValidarInscripcion(conexion, transaccion, inscripcion, id);

        using var comando = conexion.CreateCommand();
        comando.Transaction = transaccion;
        comando.CommandText = "UPDATE Inscripciones SET EstudianteId = $estudiante, CursoId = $curso, Fecha = $fecha WHERE Id = $id;";
        comando.Parameters.AddWithValue("$estudiante", inscripcion.EstudianteId);
        comando.Parameters.AddWithValue("$curso", inscripcion.CursoId);
        comando.Parameters.AddWithValue("$fecha", inscripcion.Fecha.ToString("O"));
        comando.Parameters.AddWithValue("$id", id);
        comando.ExecuteNonQuery();
        transaccion.Commit();
    }

    public void EliminarInscripcion(int id) => Ejecutar(
        "DELETE FROM Inscripciones WHERE Id = $id;", ("$id", id));

    private static void ValidarInscripcion(
        SqliteConnection conexion,
        SqliteTransaction transaccion,
        Inscripcion inscripcion,
        int? inscripcionActualId)
    {
        using (var duplicada = conexion.CreateCommand())
        {
            duplicada.Transaction = transaccion;
            duplicada.CommandText = @"SELECT COUNT(*) FROM Inscripciones
                WHERE EstudianteId = $estudiante AND CursoId = $curso
                AND ($id IS NULL OR Id <> $id);";
            duplicada.Parameters.AddWithValue("$estudiante", inscripcion.EstudianteId);
            duplicada.Parameters.AddWithValue("$curso", inscripcion.CursoId);
            duplicada.Parameters.AddWithValue("$id", inscripcionActualId is null ? DBNull.Value : inscripcionActualId.Value);
            if (Convert.ToInt32(duplicada.ExecuteScalar()) > 0)
                throw new InvalidOperationException("El estudiante ya esta inscrito en ese curso.");
        }

        using (var cupo = conexion.CreateCommand())
        {
            cupo.Transaction = transaccion;
            cupo.CommandText = @"SELECT c.Cupo - (
                    SELECT COUNT(*) FROM Inscripciones i
                    WHERE i.CursoId = c.Id AND ($id IS NULL OR i.Id <> $id)
                )
                FROM Cursos c WHERE c.Id = $curso;";
            cupo.Parameters.AddWithValue("$curso", inscripcion.CursoId);
            cupo.Parameters.AddWithValue("$id", inscripcionActualId is null ? DBNull.Value : inscripcionActualId.Value);
            var disponibles = cupo.ExecuteScalar();
            if (disponibles is null || disponibles is DBNull)
                throw new InvalidOperationException("El curso seleccionado no existe.");
            if (Convert.ToInt32(disponibles) <= 0)
                throw new InvalidOperationException("El curso ya alcanzo su cupo maximo.");
        }
    }

    private DataTable Consultar(string sql)
    {
        using var conexion = AbrirConexion();
        using var comando = conexion.CreateCommand();
        comando.CommandText = sql;
        using var lector = comando.ExecuteReader();
        var tabla = new DataTable();
        tabla.Load(lector);
        return tabla;
    }

    private void Ejecutar(string sql, params (string Nombre, object Valor)[] parametros)
    {
        using var conexion = AbrirConexion();
        using var comando = conexion.CreateCommand();
        comando.CommandText = sql;
        foreach (var (nombre, valor) in parametros)
            comando.Parameters.AddWithValue(nombre, valor ?? DBNull.Value);
        comando.ExecuteNonQuery();
    }

    private SqliteConnection AbrirConexion()
    {
        var conexion = new SqliteConnection(_cadenaConexion);
        conexion.Open();
        return conexion;
    }
}
