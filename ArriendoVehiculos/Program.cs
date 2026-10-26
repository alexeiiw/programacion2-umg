using ArriendoVehiculos.Aplicacion;
using ArriendoVehiculos.Persistencia;

namespace ArriendoVehiculos;

internal static class Program
{
    private static void Main()
    {
        try
        {
            var inicializador = new InicializadorBaseDatos();
            inicializador.Inicializar();
            var repositorio = new RepositorioArriendoVehiculos(inicializador.CadenaConexion);
            new MenuArriendoVehiculos(repositorio).Ejecutar();
        }
        catch (Exception error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"No fue posible iniciar el sistema: {error.Message}");
            Console.ResetColor();
        }
    }
}
