using System.Data;
using ArriendoVehiculos.Dominio;
using ArriendoVehiculos.Persistencia;

namespace ArriendoVehiculos.Aplicacion;

internal sealed class MenuArriendoVehiculos
{
    private readonly RepositorioArriendoVehiculos _repositorio;
    private bool _continuar = true;

    public MenuArriendoVehiculos(RepositorioArriendoVehiculos repositorio) => _repositorio = repositorio;

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
                    case "1": AgregarCliente(); break;
                    case "2": ListarClientes(); break;
                    case "3": EliminarCliente(); break;
                    case "4": AgregarVehiculo(); break;
                    case "5": ListarVehiculos(); break;
                    case "6": EliminarVehiculo(); break;
                    case "7": CrearArriendo(); break;
                    case "8": ListarArriendos(); break;
                    case "9": DevolverArriendo(); break;
                    case "10": DemostrarPolimorfismo(); break;
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
        Console.WriteLine("================================================");
        Console.WriteLine("      SISTEMA DE ARRIENDO DE VEHICULOS");
        Console.WriteLine("================================================");
        Console.ResetColor();
        Console.WriteLine("CLIENTES:  1 Agregar | 2 Listar | 3 Eliminar");
        Console.WriteLine("VEHICULOS: 4 Agregar | 5 Listar | 6 Eliminar");
        Console.WriteLine("ARRIENDOS: 7 Crear | 8 Listar | 9 Devolver");
        Console.WriteLine("POO: 10 Demostrar polimorfismo | 0 Salir");
        Console.Write("Opcion: ");
    }

    private void AgregarCliente()
    {
        _repositorio.AgregarCliente(LeerTexto("Codigo"), LeerTexto("Nombre"), LeerTexto("Documento"));
        Console.WriteLine("Cliente agregado correctamente.");
    }

    private void ListarClientes() => MostrarTabla(_repositorio.ObtenerClientes(), "CLIENTES");

    private void EliminarCliente()
    {
        _repositorio.EliminarCliente(LeerId("Id del cliente a eliminar"));
        Console.WriteLine("Cliente eliminado; sus arriendos tambien se eliminan.");
    }

    private void AgregarVehiculo()
    {
        _repositorio.AgregarVehiculo(LeerTexto("Placa"), LeerTexto("Modelo"), LeerEntero("Tarifa diaria"));
        Console.WriteLine("Vehiculo agregado correctamente.");
    }

    private void ListarVehiculos() => MostrarTabla(_repositorio.ObtenerVehiculos(), "VEHICULOS Y DISPONIBILIDAD");

    private void EliminarVehiculo()
    {
        _repositorio.EliminarVehiculo(LeerId("Id del vehiculo a eliminar"));
        Console.WriteLine("Vehiculo eliminado; sus arriendos tambien se eliminan.");
    }

    private void CrearArriendo()
    {
        MostrarTabla(_repositorio.ObtenerClientes(), "CLIENTES");
        MostrarTabla(_repositorio.ObtenerVehiculos(), "VEHICULOS");
        _repositorio.CrearArriendo(
            LeerId("Id del cliente"),
            LeerId("Id del vehiculo"),
            LeerFecha("Fecha de inicio (AAAA-MM-DD)").ToString("yyyy-MM-dd"),
            LeerFecha("Fecha de fin (AAAA-MM-DD)").ToString("yyyy-MM-dd"));
        Console.WriteLine("Arriendo registrado; el vehiculo queda ocupado durante el rango solicitado.");
    }

    private void ListarArriendos() => MostrarTabla(_repositorio.ObtenerArriendos(), "ARRIENDOS Y ESTADO");

    private void DevolverArriendo()
    {
        _repositorio.DevolverArriendo(LeerId("Id del arriendo a devolver"), LeerFecha("Fecha de devolucion (AAAA-MM-DD)").ToString("yyyy-MM-dd"));
        Console.WriteLine("Devolucion registrada; el total quedo calculado y el vehiculo vuelve a estar disponible.");
    }

    private static void DemostrarPolimorfismo()
    {
        Persona persona = new Cliente("C-DEMO", "Ana Lopez", "123456789");
        Console.WriteLine("Referencia Persona que contiene una instancia de Cliente:");
        Console.WriteLine(persona.ObtenerInformacion());
    }

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
        if (!DateTime.TryParse(Console.ReadLine(), out var fecha)) throw new ArgumentException("Ingrese una fecha valida (AAAA-MM-DD).");
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

        foreach (DataColumn columna in tabla.Columns) Console.Write($"{columna.ColumnName,-20}");
        Console.WriteLine();
        foreach (DataRow fila in tabla.Rows)
        {
            foreach (var valor in fila.ItemArray) Console.Write($"{valor,-20}");
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
