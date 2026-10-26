using System.Data;
using Microsoft.Data.Sqlite;

namespace ArriendoVehiculos.Persistencia;

internal sealed class RepositorioArriendoVehiculos
{
    private readonly string _cadenaConexion;

    public RepositorioArriendoVehiculos(string cadenaConexion) => _cadenaConexion = cadenaConexion;

    // Clientes
    public DataTable ObtenerClientes() => Consultar(
        "SELECT Id, Codigo, Nombre, Documento FROM Clientes ORDER BY Nombre;");

    public void AgregarCliente(string codigo, string nombre, string documento)
    {
        using var conexion = AbrirConexion();
        using var comando = conexion.CreateCommand();
        comando.CommandText = "SELECT COUNT(*) FROM Clientes WHERE Codigo = $codigo OR Documento = $documento;";
        comando.Parameters.AddWithValue("$codigo", codigo);
        comando.Parameters.AddWithValue("$documento", documento);
        if (Convert.ToInt32(comando.ExecuteScalar()) > 0)
            throw new InvalidOperationException("Ya existe un cliente con ese Codigo o Documento.");
        comando.CommandText = "INSERT INTO Clientes (Codigo, Nombre, Documento) VALUES ($codigo, $nombre, $documento);";
        comando.Parameters.AddWithValue("$nombre", nombre);
        comando.ExecuteNonQuery();
    }

    public void EliminarCliente(int id) => Ejecutar("DELETE FROM Clientes WHERE Id = $id;", ("$id", id));

    // Vehiculos
    public DataTable ObtenerVehiculos() => Consultar(@"SELECT v.Id, v.Placa, v.Modelo, v.TarifaDiaria,
        CASE WHEN EXISTS (SELECT 1 FROM Arriendos a WHERE a.VehiculoId = v.Id AND a.FechaDevolucion IS NULL)
             THEN 'Arrendado' ELSE 'Disponible' END AS Estado
        FROM Vehiculos v ORDER BY v.Placa;");

    public void AgregarVehiculo(string placa, string modelo, int tarifaDiaria)
    {
        using var conexion = AbrirConexion();
        using var comando = conexion.CreateCommand();
        comando.CommandText = "SELECT COUNT(*) FROM Vehiculos WHERE Placa = $placa;";
        comando.Parameters.AddWithValue("$placa", placa);
        if (Convert.ToInt32(comando.ExecuteScalar()) > 0)
            throw new InvalidOperationException("Ya existe un vehiculo con esa placa.");
        comando.CommandText = "INSERT INTO Vehiculos (Placa, Modelo, TarifaDiaria) VALUES ($placa, $modelo, $tarifa);";
        comando.Parameters.AddWithValue("$modelo", modelo);
        comando.Parameters.AddWithValue("$tarifa", tarifaDiaria);
        comando.ExecuteNonQuery();
    }

    public void EliminarVehiculo(int id) => Ejecutar("DELETE FROM Vehiculos WHERE Id = $id;", ("$id", id));

    // Arriendos
    public DataTable ObtenerArriendos() => Consultar(@"SELECT a.Id, a.ClienteId, a.VehiculoId,
        c.Nombre AS Cliente, v.Placa AS Vehiculo,
        a.FechaInicio, a.FechaFin, a.FechaDevolucion, a.Total,
        CASE WHEN a.FechaDevolucion IS NOT NULL THEN 'Devuelto'
             WHEN date(a.FechaFin) < date('now', 'localtime') THEN 'Vencido'
             ELSE 'Arrendado' END AS Estado
        FROM Arriendos a
        INNER JOIN Clientes c ON c.Id = a.ClienteId
        INNER JOIN Vehiculos v ON v.Id = a.VehiculoId
        ORDER BY a.FechaInicio DESC, a.Id DESC;");

    public void CrearArriendo(int clienteId, int vehiculoId, string fechaInicio, string fechaFin)
    {
        ValidarFecha(fechaInicio, "fecha de inicio");
        ValidarFecha(fechaFin, "fecha de fin");
        var inicio = DateTime.Parse(fechaInicio);
        var fin = DateTime.Parse(fechaFin);
        if (fin.Date <= inicio.Date)
            throw new InvalidOperationException("La fecha de fin debe ser posterior a la fecha de inicio.");

        using var conexion = AbrirConexion();
        using var transaccion = conexion.BeginTransaction();
        ValidarExistencia(conexion, transaccion, clienteId, vehiculoId);
        ValidarDisponibilidad(conexion, transaccion, vehiculoId, fechaInicio, fechaFin);
        using var comando = conexion.CreateCommand();
        comando.Transaction = transaccion;
        comando.CommandText = @"INSERT INTO Arriendos (ClienteId, VehiculoId, FechaInicio, FechaFin, FechaDevolucion, Total)
            VALUES ($cliente, $vehiculo, $inicio, $fin, NULL, NULL);";
        comando.Parameters.AddWithValue("$cliente", clienteId);
        comando.Parameters.AddWithValue("$vehiculo", vehiculoId);
        comando.Parameters.AddWithValue("$inicio", fechaInicio);
        comando.Parameters.AddWithValue("$fin", fechaFin);
        comando.ExecuteNonQuery();
        transaccion.Commit();
    }

    public void DevolverArriendo(int arriendoId, string fechaDevolucion)
    {
        ValidarFecha(fechaDevolucion, "fecha de devolucion");
        using var conexion = AbrirConexion();
        using var comando = conexion.CreateCommand();
        comando.CommandText = @"SELECT a.Id, a.FechaInicio, v.TarifaDiaria
            FROM Arriendos a
            INNER JOIN Vehiculos v ON v.Id = a.VehiculoId
            WHERE a.Id = $id AND a.FechaDevolucion IS NULL;";
        comando.Parameters.AddWithValue("$id", arriendoId);
        using var lector = comando.ExecuteReader();
        if (!lector.Read())
            throw new InvalidOperationException("No existe un arriendo con ese Id o ya fue devuelto.");
        var inicio = lector.GetDateTime(lector.GetOrdinal("FechaInicio"));
        var tarifa = lector.GetInt32(lector.GetOrdinal("TarifaDiaria"));
        lector.Close();
        if (DateTime.Parse(fechaDevolucion).Date < inicio.Date)
            throw new InvalidOperationException("La fecha de devolucion no puede ser anterior al inicio del arriendo.");
        var dias = (DateTime.Parse(fechaDevolucion).Date - inicio.Date).Days;
        using var actualizacion = conexion.CreateCommand();
        actualizacion.CommandText = "UPDATE Arriendos SET FechaDevolucion = $devolucion, Total = $total WHERE Id = $id AND FechaDevolucion IS NULL;";
        actualizacion.Parameters.AddWithValue("$devolucion", fechaDevolucion);
        actualizacion.Parameters.AddWithValue("$total", dias * tarifa);
        actualizacion.Parameters.AddWithValue("$id", arriendoId);
        if (actualizacion.ExecuteNonQuery() == 0)
            throw new InvalidOperationException("No fue posible registrar la devolucion del arriendo.");
    }

    public void EliminarArriendo(int id) => Ejecutar("DELETE FROM Arriendos WHERE Id = $id;", ("$id", id));

    private static void ValidarFecha(string valor, string nombreCampo)
    {
        if (string.IsNullOrWhiteSpace(valor) || !DateTime.TryParse(valor, out _))
            throw new ArgumentException($"La {nombreCampo} es invalida. Use el formato aaaa-mm-dd.");
    }

    private static void ValidarExistencia(SqliteConnection conexion, SqliteTransaction transaccion, int clienteId, int vehiculoId)
    {
        using var comando = conexion.CreateCommand();
        comando.Transaction = transaccion;
        comando.CommandText = "SELECT COUNT(*) FROM Clientes WHERE Id = $cliente;";
        comando.Parameters.AddWithValue("$cliente", clienteId);
        if (Convert.ToInt32(comando.ExecuteScalar()) == 0)
            throw new InvalidOperationException("El cliente seleccionado no existe.");
        comando.CommandText = "SELECT COUNT(*) FROM Vehiculos WHERE Id = $vehiculo;";
        comando.Parameters.AddWithValue("$vehiculo", vehiculoId);
        if (Convert.ToInt32(comando.ExecuteScalar()) == 0)
            throw new InvalidOperationException("El vehiculo seleccionado no existe.");
    }

    private static void ValidarDisponibilidad(SqliteConnection conexion, SqliteTransaction transaccion, int vehiculoId, string fechaInicio, string fechaFin)
    {
        using var comando = conexion.CreateCommand();
        comando.Transaction = transaccion;
        comando.CommandText = @"SELECT COUNT(*) FROM Arriendos
            WHERE VehiculoId = $vehiculo AND FechaDevolucion IS NULL
              AND date(FechaInicio) < date($fin) AND date(FechaFin) > date($inicio);";
        comando.Parameters.AddWithValue("$vehiculo", vehiculoId);
        comando.Parameters.AddWithValue("$inicio", fechaInicio);
        comando.Parameters.AddWithValue("$fin", fechaFin);
        if (Convert.ToInt32(comando.ExecuteScalar()) > 0)
            throw new InvalidOperationException("El vehiculo ya tiene un arriendo activo que se solapa con el rango solicitado.");
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
