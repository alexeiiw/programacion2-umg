namespace SistemaAlumnos.Logica;

public class RegistroNotas
{
    public RegistroNotas(Estudiante estudiante, string curso)
    {
        Estudiante = estudiante;
        Curso = curso;
    }

    public Estudiante Estudiante { get; }
    public string Curso { get; }
    public double Actividad { get; private set; }
    public double Parcial { get; private set; }
    public double Proyecto { get; private set; }

    public void IngresarNotas(double actividad, double parcial, double proyecto)
    {
        ValidarNota(actividad);
        ValidarNota(parcial);
        ValidarNota(proyecto);
        Actividad = actividad;
        Parcial = parcial;
        Proyecto = proyecto;
    }

    public double CalcularPromedio()
    {
        return (Actividad + Parcial + Proyecto) / 3;
    }

    public string ObtenerEstado()
    {
        return CalcularPromedio() >= 60 ? "Aprobado" : "Reprobado";
    }

    public void MostrarResultado()
    {
        Console.WriteLine($"{Estudiante.Nombre} | {Curso} | Promedio: {CalcularPromedio():F2} | Estado: {ObtenerEstado()}");
    }

    private static void ValidarNota(double nota)
    {
        if (nota < 0 || nota > 100)
        {
            throw new ArgumentException("Las notas deben estar entre 0 y 100.");
        }
    }
}
