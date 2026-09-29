namespace GestionAcademica.Dominio;

public abstract class Persona
{
    protected Persona(string codigo, string nombre, string correo)
    {
        Codigo = ValidarTexto(codigo, "El codigo es obligatorio.");
        Nombre = ValidarTexto(nombre, "El nombre es obligatorio.");
        Correo = ValidarTexto(correo, "El correo es obligatorio.");
    }

    public string Codigo { get; }
    public string Nombre { get; }
    public string Correo { get; }

    public abstract string ObtenerRol();

    public virtual string ObtenerInformacion() => $"{ObtenerRol()}: {Codigo} - {Nombre} | {Correo}";

    protected static string ValidarTexto(string valor, string mensaje) =>
        string.IsNullOrWhiteSpace(valor) ? throw new ArgumentException(mensaje) : valor.Trim();
}
