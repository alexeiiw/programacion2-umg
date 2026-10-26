namespace SistemaVeterinarioV2;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        var database = new DatabaseInitializer();
        database.Initialize();
        Application.Run(new MainForm(new VeterinaryRepository(database.ConnectionString)));
    }
}
