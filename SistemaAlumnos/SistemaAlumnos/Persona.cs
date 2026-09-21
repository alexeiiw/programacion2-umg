namespace SistemaAlumnos.Logica;

public abstract class Persona
{
    protected Persona(string codigo, string nombre)
    {
        Codigo = codigo;
        Nombre = nombre;
    }

    public string Codigo { get; }
    public string Nombre { get; }

    public abstract void MostrarInformacion();
}
