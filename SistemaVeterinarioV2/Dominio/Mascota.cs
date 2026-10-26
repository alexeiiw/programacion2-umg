namespace SistemaVeterinarioV2.Dominio;

public sealed class Mascota : SerVivo
{
    public Mascota(string codigo, string nombre, string especie, string propietario)
        : base(codigo, nombre)
    {
        Especie = especie;
        Propietario = propietario;
    }

    public string Especie { get; }
    public string Propietario { get; }
}
