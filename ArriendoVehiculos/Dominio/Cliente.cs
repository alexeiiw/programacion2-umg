namespace ArriendoVehiculos.Dominio;

public sealed class Cliente : Persona
{
    public Cliente(string codigo, string nombre, string documento)
        : base(codigo, nombre)
    {
        Documento = string.IsNullOrWhiteSpace(documento)
            ? throw new ArgumentException("El documento del cliente es obligatorio.")
            : documento.Trim();
    }

    public string Documento { get; }

    public override string ObtenerTipo() => "Cliente";

    public override string ObtenerInformacion() => $"{base.ObtenerInformacion()} | Documento: {Documento}";
}
