using SistemaAlumnos.Logica;

public class Program
{
    public static void Main(string[] args)
    {
        Persona personaAprobada = new Estudiante("E001", "Ana Lopez", "Ingenieria");
        Persona personaReprobada = new Estudiante("E002", "Luis Perez", "Ingenieria");

        RegistroNotas registroAprobado = new RegistroNotas((Estudiante)personaAprobada, "Programacion II");
        RegistroNotas registroReprobado = new RegistroNotas((Estudiante)personaReprobada, "Programacion II");

        try
        {
            registroAprobado.IngresarNotas(80, 70, 90);
            registroReprobado.IngresarNotas(40, 50, 45);
            registroAprobado.MostrarResultado();
            registroReprobado.MostrarResultado();
        }
        catch (ArgumentException error)
        {
            Console.WriteLine(error.Message);
        }

        Persona[] personas = { personaAprobada, personaReprobada };
        foreach (Persona persona in personas)
        {
            persona.MostrarInformacion();
        }
    }
}
