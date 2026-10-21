using System.Reflection;
using Microsoft.Data.Sqlite;

namespace PrestamosBiblioteca.Persistencia;

internal sealed class InicializadorBaseDatos
{
    public string RutaBaseDatos { get; } = Environment.GetEnvironmentVariable("PRESTAMOS_BIBLIOTECA_DB")
        ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "UMG",
            "PrestamosBiblioteca",
            "biblioteca.db");

    public string CadenaConexion => new SqliteConnectionStringBuilder
    {
        DataSource = RutaBaseDatos,
        ForeignKeys = true
    }.ToString();

    public void Inicializar()
    {
        var directorio = Path.GetDirectoryName(RutaBaseDatos);
        if (!string.IsNullOrEmpty(directorio)) Directory.CreateDirectory(directorio);
        using var conexion = new SqliteConnection(CadenaConexion);
        conexion.Open();
        using var comando = conexion.CreateCommand();
        comando.CommandText = LeerScriptTablas();
        comando.ExecuteNonQuery();
    }

    private static string LeerScriptTablas()
    {
        var ensamblado = Assembly.GetExecutingAssembly();
        var recurso = ensamblado.GetManifestResourceNames()
            .Single(nombre => nombre.EndsWith("crear_tablas.sql", StringComparison.OrdinalIgnoreCase));
        using var stream = ensamblado.GetManifestResourceStream(recurso)
            ?? throw new InvalidOperationException("No se encontro el script SQL de las tablas.");
        using var lector = new StreamReader(stream);
        return lector.ReadToEnd();
    }
}
