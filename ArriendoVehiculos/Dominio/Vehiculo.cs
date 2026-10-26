namespace ArriendoVehiculos.Dominio;

public sealed class Vehiculo
{
    public Vehiculo(string placa, string modelo, int tarifaDiaria)
    {
        Placa = string.IsNullOrWhiteSpace(placa)
            ? throw new ArgumentException("La placa del vehiculo es obligatoria.")
            : placa.Trim().ToUpperInvariant();
        Modelo = string.IsNullOrWhiteSpace(modelo)
            ? throw new ArgumentException("El modelo del vehiculo es obligatorio.")
            : modelo.Trim();
        if (tarifaDiaria <= 0) throw new ArgumentException("La tarifa diaria debe ser mayor que cero.");
        TarifaDiaria = tarifaDiaria;
    }

    public string Placa { get; }
    public string Modelo { get; }
    public int TarifaDiaria { get; }
}
