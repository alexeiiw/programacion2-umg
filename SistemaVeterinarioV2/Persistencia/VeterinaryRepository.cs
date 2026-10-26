using System.Data;
using Microsoft.Data.Sqlite;
using SistemaVeterinarioV2.Dominio;

namespace SistemaVeterinarioV2;

internal sealed class VeterinaryRepository
{
    private readonly string _connectionString;

    public VeterinaryRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DataTable ObtenerMascotas()
    {
        return Query("SELECT Id, Codigo, Nombre, Especie, Propietario FROM Mascotas ORDER BY Nombre;");
    }

    public DataTable ObtenerMascotasParaSeleccion()
    {
        return Query("SELECT Id, Codigo || ' - ' || Nombre AS Mascota FROM Mascotas ORDER BY Nombre;");
    }

    public void CrearMascota(Mascota mascota) => Execute(
        "INSERT INTO Mascotas (Codigo, Nombre, Especie, Propietario) VALUES ($codigo, $nombre, $especie, $propietario);",
        ("$codigo", mascota.Codigo), ("$nombre", mascota.Nombre),
        ("$especie", mascota.Especie), ("$propietario", mascota.Propietario));

    public void ActualizarMascota(int id, Mascota mascota) => Execute(
        "UPDATE Mascotas SET Codigo = $codigo, Nombre = $nombre, Especie = $especie, Propietario = $propietario WHERE Id = $id;",
        ("$codigo", mascota.Codigo), ("$nombre", mascota.Nombre),
        ("$especie", mascota.Especie), ("$propietario", mascota.Propietario), ("$id", id));

    public void EliminarMascota(int id) => Execute(
        "DELETE FROM Mascotas WHERE Id = $id;", ("$id", id));

    public DataTable ObtenerConsultas()
    {
        return Query(@"SELECT c.Id, c.MascotaId, m.Codigo AS CodigoMascota, m.Nombre AS Mascota,
                              c.Servicio, c.Peso, c.Temperatura, c.Costo, c.Fecha,
                              CASE WHEN c.Temperatura >= 39.5 THEN 'Requiere observacion' ELSE 'Estable' END AS EstadoClinico
                       FROM ConsultasVeterinarias c
                       INNER JOIN Mascotas m ON m.Id = c.MascotaId
                       ORDER BY c.Fecha DESC, c.Id DESC;");
    }

    public void CrearConsulta(ConsultaVeterinaria consulta) => Execute(
        "INSERT INTO ConsultasVeterinarias (MascotaId, Servicio, Peso, Temperatura, Costo, Fecha) VALUES ($mascotaId, $servicio, $peso, $temperatura, $costo, $fecha);",
        ("$mascotaId", consulta.MascotaId), ("$servicio", consulta.Servicio),
        ("$peso", consulta.Peso), ("$temperatura", consulta.Temperatura),
        ("$costo", consulta.Costo), ("$fecha", consulta.Fecha.ToString("O")));

    public void ActualizarConsulta(int id, ConsultaVeterinaria consulta) => Execute(
        "UPDATE ConsultasVeterinarias SET MascotaId = $mascotaId, Servicio = $servicio, Peso = $peso, Temperatura = $temperatura, Costo = $costo, Fecha = $fecha WHERE Id = $id;",
        ("$mascotaId", consulta.MascotaId), ("$servicio", consulta.Servicio),
        ("$peso", consulta.Peso), ("$temperatura", consulta.Temperatura),
        ("$costo", consulta.Costo), ("$fecha", consulta.Fecha.ToString("O")), ("$id", id));

    public void EliminarConsulta(int id) => Execute(
        "DELETE FROM ConsultasVeterinarias WHERE Id = $id;", ("$id", id));

    private DataTable Query(string sql)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        using var reader = command.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    private void Execute(string sql, params (string Name, object Value)[] parameters)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        foreach (var (name, value) in parameters)
            command.Parameters.AddWithValue(name, value ?? DBNull.Value);
        command.ExecuteNonQuery();
    }
}
