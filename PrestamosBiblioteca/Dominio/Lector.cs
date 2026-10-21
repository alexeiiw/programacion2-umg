namespace PrestamosBiblioteca.Dominio;

public sealed class Lector : Persona
{
    public Lector(string codigo, string nombre, string correo, string telefono)
        : base(codigo, nombre, correo)
    {
        Telefono = string.IsNullOrWhiteSpace(telefono)
            ? throw new ArgumentException("El telefono es obligatorio.")
            : telefono.Trim();
    }

    public string Telefono { get; }
    public override string ObtenerTipo() => "Lector";

    public override string ObtenerInformacion() => $"{base.ObtenerInformacion()} | Tel: {Telefono}";
}
