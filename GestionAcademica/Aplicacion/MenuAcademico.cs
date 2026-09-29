using GestionAcademica.Dominio;
using GestionAcademica.Persistencia;

namespace GestionAcademica.Aplicacion;

internal sealed class MenuAcademico
{
    private readonly RepositorioAcademico _repositorio;
    private bool _continuar = true;

    public MenuAcademico(RepositorioAcademico repositorio)
    {
        _repositorio = repositorio;
    }

    public void Ejecutar()
    {
        while (_continuar)
        {
            MostrarMenu();
            try
            {
                var entrada = Console.ReadLine();
                if (entrada is null)
                {
                    _continuar = false;
                    continue;
                }

                switch (entrada.Trim())
                {
                    case "1": CrearEstudiante(); break;
                    case "2": ListarEstudiantes(); break;
                    case "3": ActualizarEstudiante(); break;
                    case "4": EliminarEstudiante(); break;
                    case "5": CrearCurso(); break;
                    case "6": ListarCursos(); break;
                    case "7": ActualizarCurso(); break;
                    case "8": EliminarCurso(); break;
                    case "9": CrearInscripcion(); break;
                    case "10": ListarInscripciones(); break;
                    case "11": ActualizarInscripcion(); break;
                    case "12": EliminarInscripcion(); break;
                    case "13": DemostrarPolimorfismo(); break;
                    case "0": _continuar = false; break;
                    default: Console.WriteLine("Seleccione una opcion valida del menu."); break;
                }
            }
            catch (Exception error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Operacion controlada con try-catch: {error.Message}");
                Console.ResetColor();
            }

            if (_continuar) Pausar();
        }
    }

    private static void MostrarMenu()
    {
        if (!Console.IsOutputRedirected) Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("====================================================");
        Console.WriteLine("       SISTEMA DE GESTION ACADEMICA - SQLITE");
        Console.WriteLine("====================================================");
        Console.ResetColor();
        Console.WriteLine("ESTUDIANTES");
        Console.WriteLine(" 1. Crear estudiante   2. Listar estudiantes");
        Console.WriteLine(" 3. Actualizar estudiante   4. Eliminar estudiante");
        Console.WriteLine("CURSOS");
        Console.WriteLine(" 5. Crear curso   6. Listar cursos");
        Console.WriteLine(" 7. Actualizar curso   8. Eliminar curso");
        Console.WriteLine("INSCRIPCIONES");
        Console.WriteLine(" 9. Inscribir estudiante   10. Listar inscripciones");
        Console.WriteLine("11. Actualizar inscripcion   12. Eliminar inscripcion");
        Console.WriteLine("POO");
        Console.WriteLine("13. Demostrar polimorfismo");
        Console.WriteLine(" 0. Salir");
        Console.Write("Opcion: ");
    }

    private void CrearEstudiante()
    {
        _repositorio.CrearEstudiante(LeerEstudiante());
        Console.WriteLine("Estudiante creado correctamente.");
    }

    private void ListarEstudiantes() => MostrarTabla(_repositorio.ObtenerEstudiantes(), "ESTUDIANTES");

    private void ActualizarEstudiante()
    {
        _repositorio.ActualizarEstudiante(LeerId("Id del estudiante a actualizar"), LeerEstudiante());
        Console.WriteLine("Estudiante actualizado correctamente (si el Id existe).");
    }

    private void EliminarEstudiante()
    {
        _repositorio.EliminarEstudiante(LeerId("Id del estudiante a eliminar"));
        Console.WriteLine("Estudiante eliminado. Sus inscripciones relacionadas tambien se eliminan.");
    }

    private void CrearCurso()
    {
        var codigo = LeerTexto("Codigo");
        var nombre = LeerTexto("Nombre");
        var creditos = LeerEntero("Creditos");
        var cupo = LeerEntero("Cupo maximo");
        _repositorio.CrearCurso(new Curso(codigo, nombre, creditos, cupo));
        Console.WriteLine("Curso creado correctamente.");
    }

    private void ListarCursos() => MostrarTabla(_repositorio.ObtenerCursos(), "CURSOS");

    private void ActualizarCurso()
    {
        var id = LeerId("Id del curso a actualizar");
        var curso = new Curso(LeerTexto("Codigo"), LeerTexto("Nombre"), LeerEntero("Creditos"), LeerEntero("Cupo maximo"));
        _repositorio.ActualizarCurso(id, curso);
        Console.WriteLine("Curso actualizado correctamente (si el Id existe).");
    }

    private void EliminarCurso()
    {
        _repositorio.EliminarCurso(LeerId("Id del curso a eliminar"));
        Console.WriteLine("Curso eliminado. Sus inscripciones relacionadas tambien se eliminan.");
    }

    private void CrearInscripcion()
    {
        MostrarTabla(_repositorio.ObtenerEstudiantes(), "ESTUDIANTES DISPONIBLES");
        MostrarTabla(_repositorio.ObtenerCursos(), "CURSOS DISPONIBLES");
        var inscripcion = new Inscripcion(LeerId("Id del estudiante"), LeerId("Id del curso"), DateTime.Now);
        _repositorio.CrearInscripcion(inscripcion);
        Console.WriteLine("Inscripcion creada correctamente.");
    }

    private void ListarInscripciones() => MostrarTabla(_repositorio.ObtenerInscripciones(), "INSCRIPCIONES");

    private void ActualizarInscripcion()
    {
        MostrarTabla(_repositorio.ObtenerEstudiantes(), "ESTUDIANTES DISPONIBLES");
        MostrarTabla(_repositorio.ObtenerCursos(), "CURSOS DISPONIBLES");
        var id = LeerId("Id de la inscripcion a actualizar");
        var inscripcion = new Inscripcion(LeerId("Nuevo Id del estudiante"), LeerId("Nuevo Id del curso"), DateTime.Now);
        _repositorio.ActualizarInscripcion(id, inscripcion);
        Console.WriteLine("Inscripcion actualizada correctamente (si el Id existe).");
    }

    private void EliminarInscripcion()
    {
        _repositorio.EliminarInscripcion(LeerId("Id de la inscripcion a eliminar"));
        Console.WriteLine("Inscripcion eliminada.");
    }

    private void DemostrarPolimorfismo()
    {
        Persona persona = new Estudiante("EJEMPLO", "Ana Lopez", "ana@umg.edu.gt", "Ingenieria en Sistemas");
        Console.WriteLine("Una referencia de tipo Persona contiene un objeto Estudiante:");
        Console.WriteLine(persona.ObtenerInformacion());
    }

    private static Estudiante LeerEstudiante() => new(
        LeerTexto("Codigo"), LeerTexto("Nombre completo"), LeerTexto("Correo electronico"), LeerTexto("Carrera"));

    private static int LeerId(string mensaje) => LeerEntero(mensaje);

    private static int LeerEntero(string mensaje)
    {
        Console.Write($"{mensaje}: ");
        if (!int.TryParse(Console.ReadLine(), out var valor))
            throw new ArgumentException("Ingrese un numero entero valido.");
        return valor;
    }

    private static string LeerTexto(string mensaje)
    {
        Console.Write($"{mensaje}: ");
        var valor = Console.ReadLine();
        return string.IsNullOrWhiteSpace(valor)
            ? throw new ArgumentException($"El campo '{mensaje}' no puede quedar vacio.")
            : valor.Trim();
    }

    private static void MostrarTabla(System.Data.DataTable tabla, string titulo)
    {
        Console.WriteLine($"\n=== {titulo} ===");
        if (tabla.Rows.Count == 0)
        {
            Console.WriteLine("No hay registros.");
            return;
        }

        foreach (System.Data.DataColumn columna in tabla.Columns)
            Console.Write($"{columna.ColumnName,-24}");
        Console.WriteLine();
        foreach (System.Data.DataRow fila in tabla.Rows)
        {
            foreach (var valor in fila.ItemArray)
                Console.Write($"{valor,-24}");
            Console.WriteLine();
        }
    }

    private static void Pausar()
    {
        Console.WriteLine("\nPresione Enter para volver al menu...");
        Console.ReadLine();
    }
}
