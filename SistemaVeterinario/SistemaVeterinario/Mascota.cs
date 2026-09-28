public class Mascota : SerVivo
{
    public Mascota(string codigo, string nombre, string especie, string propietario)
        : base(codigo, nombre)
    {
        Especie = especie;
        Propietario = propietario;
    }

    public string Especie { get; }
    public string Propietario { get; }

    public override void MostrarInformacion()
    {
        Console.WriteLine($"{Codigo} - {Nombre} - {Especie} - Propietario: {Propietario}");
    }
}
