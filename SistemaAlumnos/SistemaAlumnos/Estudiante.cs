namespace SistemaAlumnos.Logica;

public class Estudiante : Persona
{
    public Estudiante(string codigo, string nombre, string carrera)
        : base(codigo, nombre)
    {
        Carrera = carrera;
    }

    public string Carrera { get; }

    public override void MostrarInformacion()
    {
        Console.WriteLine($"{Codigo} - {Nombre} - {Carrera}");
    }
}
