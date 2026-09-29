namespace GestionAcademica.Dominio;

public sealed class Curso
{
    public Curso(string codigo, string nombre, int creditos, int cupo)
    {
        Codigo = string.IsNullOrWhiteSpace(codigo) ? throw new ArgumentException("El codigo del curso es obligatorio.") : codigo.Trim();
        Nombre = string.IsNullOrWhiteSpace(nombre) ? throw new ArgumentException("El nombre del curso es obligatorio.") : nombre.Trim();
        if (creditos <= 0) throw new ArgumentException("Los creditos deben ser mayores que cero.");
        if (cupo <= 0) throw new ArgumentException("El cupo debe ser mayor que cero.");
        Creditos = creditos;
        Cupo = cupo;
    }

    public string Codigo { get; }
    public string Nombre { get; }
    public int Creditos { get; }
    public int Cupo { get; }
}
