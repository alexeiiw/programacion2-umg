using GestionAcademica.Aplicacion;
using GestionAcademica.Dominio;
using GestionAcademica.Persistencia;

namespace GestionAcademica;

internal static class Program
{
    private static void Main()
    {
        try
        {
            var inicializador = new InicializadorBaseDatos();
            inicializador.Inicializar();
            var repositorio = new RepositorioAcademico(inicializador.CadenaConexion);
            new MenuAcademico(repositorio).Ejecutar();
        }
        catch (Exception error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"No fue posible iniciar el sistema: {error.Message}");
            Console.ResetColor();
        }
    }
}
