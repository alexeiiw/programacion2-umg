using System.Data;
using Microsoft.Data.Sqlite;
using PrestamosBiblioteca.Dominio;

namespace PrestamosBiblioteca.Persistencia;

internal sealed class RepositorioBiblioteca
{
    private readonly string _cadenaConexion;

    public RepositorioBiblioteca(string cadenaConexion) => _cadenaConexion = cadenaConexion;

    // CRUD de lectores
    public DataTable ObtenerLectores() => Consultar(
        "SELECT Id, Codigo, Nombre, Correo, Telefono FROM Lectores ORDER BY Nombre;");

    public void CrearLector(Lector lector) => Ejecutar(
        "INSERT INTO Lectores (Codigo, Nombre, Correo, Telefono) VALUES ($codigo, $nombre, $correo, $telefono);",
        ("$codigo", lector.Codigo), ("$nombre", lector.Nombre),
        ("$correo", lector.Correo), ("$telefono", lector.Telefono));

    public void ActualizarLector(int id, Lector lector) => Ejecutar(
        "UPDATE Lectores SET Codigo = $codigo, Nombre = $nombre, Correo = $correo, Telefono = $telefono WHERE Id = $id;",
        ("$codigo", lector.Codigo), ("$nombre", lector.Nombre), ("$correo", lector.Correo),
        ("$telefono", lector.Telefono), ("$id", id));

    public void EliminarLector(int id) => Ejecutar("DELETE FROM Lectores WHERE Id = $id;", ("$id", id));

    // CRUD de libros
    public DataTable ObtenerLibros() => Consultar(@"SELECT l.Id, l.Codigo, l.Titulo, l.Autor, l.Ejemplares,
        l.Ejemplares - (SELECT COUNT(*) FROM Prestamos p WHERE p.LibroId = l.Id AND p.FechaDevolucion IS NULL) AS Disponibles
        FROM Libros l ORDER BY l.Titulo;");

    public void CrearLibro(Libro libro) => Ejecutar(
        "INSERT INTO Libros (Codigo, Titulo, Autor, Ejemplares) VALUES ($codigo, $titulo, $autor, $ejemplares);",
        ("$codigo", libro.Codigo), ("$titulo", libro.Titulo),
        ("$autor", libro.Autor), ("$ejemplares", libro.Ejemplares));

    public void ActualizarLibro(int id, Libro libro)
    {
        using var conexion = AbrirConexion();
        using var comando = conexion.CreateCommand();
        comando.CommandText = @"UPDATE Libros SET Codigo = $codigo, Titulo = $titulo, Autor = $autor, Ejemplares = $ejemplares
            WHERE Id = $id AND $ejemplares >= (SELECT COUNT(*) FROM Prestamos WHERE LibroId = $id AND FechaDevolucion IS NULL);";
        comando.Parameters.AddWithValue("$codigo", libro.Codigo);
        comando.Parameters.AddWithValue("$titulo", libro.Titulo);
        comando.Parameters.AddWithValue("$autor", libro.Autor);
        comando.Parameters.AddWithValue("$ejemplares", libro.Ejemplares);
        comando.Parameters.AddWithValue("$id", id);
        if (comando.ExecuteNonQuery() == 0)
            throw new InvalidOperationException("No existe el libro o los ejemplares no pueden ser menos que los prestamos activos.");
    }

    public void EliminarLibro(int id) => Ejecutar("DELETE FROM Libros WHERE Id = $id;", ("$id", id));

    // CRUD y flujo de préstamos
    public DataTable ObtenerPrestamos() => Consultar(@"SELECT p.Id, p.LectorId, p.LibroId,
        l.Nombre AS Lector, b.Titulo AS Libro, p.FechaPrestamo, p.FechaLimite, p.FechaDevolucion,
        CASE WHEN p.FechaDevolucion IS NOT NULL THEN 'Devuelto'
             WHEN date(p.FechaLimite) < date('now', 'localtime') THEN 'Vencido'
             ELSE 'Prestado' END AS Estado
        FROM Prestamos p
        INNER JOIN Lectores l ON l.Id = p.LectorId
        INNER JOIN Libros b ON b.Id = p.LibroId
        ORDER BY p.FechaPrestamo DESC, p.Id DESC;");

    public void CrearPrestamo(Prestamo prestamo)
    {
        using var conexion = AbrirConexion();
        using var transaccion = conexion.BeginTransaction();
        ValidarDisponibilidad(conexion, transaccion, prestamo.LibroId, null);
        using var comando = conexion.CreateCommand();
        comando.Transaction = transaccion;
        comando.CommandText = "INSERT INTO Prestamos (LectorId, LibroId, FechaPrestamo, FechaLimite, FechaDevolucion) VALUES ($lector, $libro, $inicio, $limite, $devolucion);";
        AgregarParametrosPrestamo(comando, prestamo);
        comando.ExecuteNonQuery();
        transaccion.Commit();
    }

    public void ActualizarPrestamo(int id, Prestamo prestamo)
    {
        using var conexion = AbrirConexion();
        using var transaccion = conexion.BeginTransaction();
        if (prestamo.EstaActivo) ValidarDisponibilidad(conexion, transaccion, prestamo.LibroId, id);
        using var comando = conexion.CreateCommand();
        comando.Transaction = transaccion;
        comando.CommandText = @"UPDATE Prestamos SET LectorId = $lector, LibroId = $libro,
            FechaPrestamo = $inicio, FechaLimite = $limite, FechaDevolucion = $devolucion WHERE Id = $id;";
        AgregarParametrosPrestamo(comando, prestamo);
        comando.Parameters.AddWithValue("$id", id);
        if (comando.ExecuteNonQuery() == 0) throw new InvalidOperationException("No existe un prestamo con ese Id.");
        transaccion.Commit();
    }

    public void RegistrarDevolucion(int id, DateTime fechaDevolucion)
    {
        using var conexion = AbrirConexion();
        using var comando = conexion.CreateCommand();
        comando.CommandText = @"UPDATE Prestamos SET FechaDevolucion = $fecha
            WHERE Id = $id AND FechaDevolucion IS NULL AND date($fecha) >= date(FechaPrestamo);";
        comando.Parameters.AddWithValue("$fecha", fechaDevolucion.ToString("yyyy-MM-dd"));
        comando.Parameters.AddWithValue("$id", id);
        if (comando.ExecuteNonQuery() == 0)
            throw new InvalidOperationException("El prestamo no existe, ya fue devuelto o la fecha no es valida.");
    }

    public void EliminarPrestamo(int id) => Ejecutar("DELETE FROM Prestamos WHERE Id = $id;", ("$id", id));

    private static void ValidarDisponibilidad(SqliteConnection conexion, SqliteTransaction transaccion, int libroId, int? prestamoActualId)
    {
        using var comando = conexion.CreateCommand();
        comando.Transaction = transaccion;
        comando.CommandText = @"SELECT b.Ejemplares - (
            SELECT COUNT(*) FROM Prestamos p WHERE p.LibroId = b.Id AND p.FechaDevolucion IS NULL
            AND ($prestamoId IS NULL OR p.Id <> $prestamoId))
            FROM Libros b WHERE b.Id = $libroId;";
        comando.Parameters.AddWithValue("$libroId", libroId);
        comando.Parameters.AddWithValue("$prestamoId", prestamoActualId.HasValue ? prestamoActualId.Value : DBNull.Value);
        var disponibles = comando.ExecuteScalar();
        if (disponibles is null || disponibles is DBNull)
            throw new InvalidOperationException("El libro seleccionado no existe.");
        if (Convert.ToInt32(disponibles) <= 0)
            throw new InvalidOperationException("No hay ejemplares disponibles de ese libro.");
    }

    private static void AgregarParametrosPrestamo(SqliteCommand comando, Prestamo prestamo)
    {
        comando.Parameters.AddWithValue("$lector", prestamo.LectorId);
        comando.Parameters.AddWithValue("$libro", prestamo.LibroId);
        comando.Parameters.AddWithValue("$inicio", prestamo.FechaPrestamo.ToString("yyyy-MM-dd"));
        comando.Parameters.AddWithValue("$limite", prestamo.FechaLimite.ToString("yyyy-MM-dd"));
        comando.Parameters.AddWithValue("$devolucion", prestamo.FechaDevolucion.HasValue
            ? prestamo.FechaDevolucion.Value.ToString("yyyy-MM-dd")
            : DBNull.Value);
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
        foreach (var (nombre, valor) in parametros) comando.Parameters.AddWithValue(nombre, valor ?? DBNull.Value);
        comando.ExecuteNonQuery();
    }

    private SqliteConnection AbrirConexion()
    {
        var conexion = new SqliteConnection(_cadenaConexion);
        conexion.Open();
        return conexion;
    }
}
