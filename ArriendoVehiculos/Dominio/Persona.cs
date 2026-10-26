namespace ArriendoVehiculos.Dominio;

public abstract class Persona
{
    protected Persona(string codigo, string nombre)
    {
        Codigo = ValidarTexto(codigo, "El codigo es obligatorio.");
        Nombre = ValidarTexto(nombre, "El nombre es obligatorio.");
    }

    public string Codigo { get; }
    public string Nombre { get; }

    public abstract string ObtenerTipo();
    public virtual string ObtenerInformacion() => $"{ObtenerTipo()}: {Codigo} - {Nombre}";

    protected static string ValidarTexto(string valor, string mensaje) =>
        string.IsNullOrWhiteSpace(valor) ? throw new ArgumentException(mensaje) : valor.Trim();
}
