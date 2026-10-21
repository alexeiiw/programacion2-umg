using System.Data;
using PrestamosBiblioteca.Dominio;
using PrestamosBiblioteca.Persistencia;

namespace PrestamosBiblioteca.Aplicacion;

internal sealed class MenuBiblioteca
{
    private readonly RepositorioBiblioteca _repositorio;
    private bool _continuar = true;

    public MenuBiblioteca(RepositorioBiblioteca repositorio) => _repositorio = repositorio;

    public void Ejecutar()
    {
        while (_continuar)
        {
            MostrarMenu();
            try
            {
                var entrada = Console.ReadLine();
                if (entrada is null) break;
                switch (entrada.Trim())
                {
                    case "1": CrearLector(); break;
                    case "2": ListarLectores(); break;
                    case "3": ActualizarLector(); break;
                    case "4": EliminarLector(); break;
                    case "5": CrearLibro(); break;
                    case "6": ListarLibros(); break;
                    case "7": ActualizarLibro(); break;
                    case "8": EliminarLibro(); break;
                    case "9": CrearPrestamo(); break;
                    case "10": ListarPrestamos(); break;
                    case "11": ActualizarPrestamo(); break;
                    case "12": RegistrarDevolucion(); break;
                    case "13": EliminarPrestamo(); break;
                    case "14": DemostrarPolimorfismo(); break;
                    case "0": _continuar = false; break;
                    default: Console.WriteLine("Seleccione una opcion valida."); break;
                }
            }
            catch (Exception error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error controlado con try-catch: {error.Message}");
                Console.ResetColor();
            }

            if (_continuar) Pausar();
        }
    }

    private static void MostrarMenu()
    {
        if (!Console.IsOutputRedirected) Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("===============================================");
        Console.WriteLine("       SISTEMA DE PRESTAMOS DE BIBLIOTECA");
        Console.WriteLine("===============================================");
        Console.ResetColor();
        Console.WriteLine("LECTORES: 1 Crear | 2 Listar | 3 Actualizar | 4 Eliminar");
        Console.WriteLine("LIBROS:   5 Crear | 6 Listar | 7 Actualizar | 8 Eliminar");
        Console.WriteLine("PRESTAMOS: 9 Crear | 10 Listar | 11 Actualizar | 12 Devolver | 13 Eliminar");
        Console.WriteLine("POO: 14 Demostrar polimorfismo | 0 Salir");
        Console.Write("Opcion: ");
    }

    private void CrearLector()
    {
        _repositorio.CrearLector(LeerLector());
        Console.WriteLine("Lector creado correctamente.");
    }

    private void ListarLectores() => MostrarTabla(_repositorio.ObtenerLectores(), "LECTORES");

    private void ActualizarLector()
    {
        _repositorio.ActualizarLector(LeerId("Id del lector a actualizar"), LeerLector());
        Console.WriteLine("Lector actualizado correctamente (si el Id existe).");
    }

    private void EliminarLector()
    {
        _repositorio.EliminarLector(LeerId("Id del lector a eliminar"));
        Console.WriteLine("Lector eliminado; sus prestamos tambien se eliminan.");
    }

    private void CrearLibro()
    {
        _repositorio.CrearLibro(LeerLibro());
        Console.WriteLine("Libro creado correctamente.");
    }

    private void ListarLibros() => MostrarTabla(_repositorio.ObtenerLibros(), "LIBROS Y EJEMPLARES DISPONIBLES");

    private void ActualizarLibro()
    {
        _repositorio.ActualizarLibro(LeerId("Id del libro a actualizar"), LeerLibro());
        Console.WriteLine("Libro actualizado correctamente.");
    }

    private void EliminarLibro()
    {
        _repositorio.EliminarLibro(LeerId("Id del libro a eliminar"));
        Console.WriteLine("Libro eliminado; sus prestamos tambien se eliminan.");
    }

    private void CrearPrestamo()
    {
        MostrarTabla(_repositorio.ObtenerLectores(), "LECTORES");
        MostrarTabla(_repositorio.ObtenerLibros(), "LIBROS");
        var inicio = DateTime.Today;
        var limite = LeerFecha("Fecha limite (AAAA-MM-DD)");
        var prestamo = new Prestamo(LeerId("Id del lector"), LeerId("Id del libro"), inicio, limite);
        _repositorio.CrearPrestamo(prestamo);
        Console.WriteLine("Prestamo registrado; el ejemplar queda ocupado hasta su devolucion.");
    }

    private void ListarPrestamos() => MostrarTabla(_repositorio.ObtenerPrestamos(), "PRESTAMOS Y ESTADO");

    private void ActualizarPrestamo()
    {
        MostrarTabla(_repositorio.ObtenerLectores(), "LECTORES");
        MostrarTabla(_repositorio.ObtenerLibros(), "LIBROS");
        var id = LeerId("Id del prestamo a actualizar");
        var lectorId = LeerId("Id del lector");
        var libroId = LeerId("Id del libro");
        var inicio = LeerFecha("Fecha de prestamo (AAAA-MM-DD)");
        var limite = LeerFecha("Fecha limite (AAAA-MM-DD)");
        _repositorio.ActualizarPrestamo(id, new Prestamo(lectorId, libroId, inicio, limite));
        Console.WriteLine("Prestamo actualizado correctamente.");
    }

    private void RegistrarDevolucion()
    {
        _repositorio.RegistrarDevolucion(LeerId("Id del prestamo a devolver"), DateTime.Today);
        Console.WriteLine("Devolucion registrada; el ejemplar vuelve a estar disponible.");
    }

    private void EliminarPrestamo()
    {
        _repositorio.EliminarPrestamo(LeerId("Id del prestamo a eliminar"));
        Console.WriteLine("Prestamo eliminado.");
    }

    private static void DemostrarPolimorfismo()
    {
        Persona persona = new Lector("L-DEMO", "Ana Lopez", "ana@umg.edu.gt", "5555-0101");
        Console.WriteLine("Referencia Persona que contiene una instancia de Lector:");
        Console.WriteLine(persona.ObtenerInformacion());
    }

    private static Lector LeerLector() => new(
        LeerTexto("Codigo"), LeerTexto("Nombre"), LeerTexto("Correo"), LeerTexto("Telefono"));

    private static Libro LeerLibro() => new(
        LeerTexto("Codigo"), LeerTexto("Titulo"), LeerTexto("Autor"), LeerEntero("Cantidad de ejemplares"));

    private static int LeerId(string mensaje) => LeerEntero(mensaje);

    private static int LeerEntero(string mensaje)
    {
        Console.Write($"{mensaje}: ");
        if (!int.TryParse(Console.ReadLine(), out var valor)) throw new ArgumentException("Ingrese un numero entero valido.");
        return valor;
    }

    private static DateTime LeerFecha(string mensaje)
    {
        Console.Write($"{mensaje}: ");
        if (!DateTime.TryParse(Console.ReadLine(), out var fecha)) throw new ArgumentException("Ingrese una fecha valida.");
        return fecha.Date;
    }

    private static string LeerTexto(string mensaje)
    {
        Console.Write($"{mensaje}: ");
        var texto = Console.ReadLine();
        return string.IsNullOrWhiteSpace(texto)
            ? throw new ArgumentException($"El campo '{mensaje}' es obligatorio.")
            : texto.Trim();
    }

    private static void MostrarTabla(DataTable tabla, string titulo)
    {
        Console.WriteLine($"\n=== {titulo} ===");
        if (tabla.Rows.Count == 0)
        {
            Console.WriteLine("No hay registros.");
            return;
        }

        foreach (DataColumn columna in tabla.Columns) Console.Write($"{columna.ColumnName,-22}");
        Console.WriteLine();
        foreach (DataRow fila in tabla.Rows)
        {
            foreach (var valor in fila.ItemArray) Console.Write($"{valor,-22}");
            Console.WriteLine();
        }
    }

    private static void Pausar()
    {
        if (Console.IsInputRedirected) return;
        Console.WriteLine("\nPresione Enter para continuar...");
        Console.ReadLine();
    }
}
