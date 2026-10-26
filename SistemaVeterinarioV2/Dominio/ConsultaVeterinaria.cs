namespace SistemaVeterinarioV2.Dominio;

public sealed class ConsultaVeterinaria
{
    public ConsultaVeterinaria(
        int mascotaId,
        string servicio,
        double peso,
        double temperatura,
        decimal costo,
        DateTime fecha)
    {
        if (mascotaId <= 0)
            throw new ArgumentException("Debe seleccionar una mascota.", nameof(mascotaId));
        if (string.IsNullOrWhiteSpace(servicio))
            throw new ArgumentException("El servicio es obligatorio.", nameof(servicio));
        if (peso <= 0)
            throw new ArgumentException("El peso debe ser mayor que 0.", nameof(peso));
        if (temperatura < 35 || temperatura > 43)
            throw new ArgumentException("La temperatura debe estar entre 35 y 43 grados.", nameof(temperatura));
        if (costo <= 0)
            throw new ArgumentException("El costo debe ser mayor que 0.", nameof(costo));

        MascotaId = mascotaId;
        Servicio = servicio.Trim();
        Peso = peso;
        Temperatura = temperatura;
        Costo = costo;
        Fecha = fecha;
    }

    public int Id { get; set; }
    public int MascotaId { get; }
    public string Servicio { get; }
    public double Peso { get; }
    public double Temperatura { get; }
    public decimal Costo { get; }
    public DateTime Fecha { get; }
    public decimal CalcularTotal() => Costo;
    public string ObtenerEstadoClinico() => Temperatura >= 39.5 ? "Requiere observacion" : "Estable";
}
