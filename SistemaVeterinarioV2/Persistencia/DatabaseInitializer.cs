using System.Reflection;
using Microsoft.Data.Sqlite;

namespace SistemaVeterinarioV2;

internal sealed class DatabaseInitializer
{
    public string DatabasePath { get; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "UMG",
        "SistemaVeterinarioV2",
        "veterinaria.db");

    public string ConnectionString => new SqliteConnectionStringBuilder
    {
        DataSource = DatabasePath,
        ForeignKeys = true
    }.ToString();

    public void Initialize()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(DatabasePath)!);

        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        using (var foreignKeys = connection.CreateCommand())
        {
            foreignKeys.CommandText = "PRAGMA foreign_keys = ON;";
            foreignKeys.ExecuteNonQuery();
        }

        using var command = connection.CreateCommand();
        command.CommandText = ReadSchemaScript();
        command.ExecuteNonQuery();
    }

    private static string ReadSchemaScript()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = assembly.GetManifestResourceNames()
            .Single(name => name.EndsWith("crear_tablas.sql", StringComparison.OrdinalIgnoreCase));
        using var stream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException("No se encontro el script de tablas de SQLite.");
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}
