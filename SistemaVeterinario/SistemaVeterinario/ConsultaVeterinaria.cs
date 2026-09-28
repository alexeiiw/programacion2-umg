public class ConsultaVeterinaria
{
    public ConsultaVeterinaria(Mascota mascota, string servicio)
    {
        Mascota = mascota;
        Servicio = servicio;
    }

    public Mascota Mascota { get; }
    public string Servicio { get; }
    public double Peso { get; private set; }
    public double Temperatura { get; private set; }
    public double Costo { get; private set; }

    public void RegistrarDatos(double peso, double temperatura, double costo)
    {
        ValidarPeso(peso);
        ValidarTemperatura(temperatura);
        ValidarCosto(costo);
        Peso = peso;
        Temperatura = temperatura;
        Costo = costo;
    }

    public double CalcularTotal()
    {
        return Costo;
    }

    public string ObtenerEstadoClinico()
    {
        return Temperatura >= 39.5 ? "Requiere observacion" : "Estable";
    }

    public void MostrarResultado()
    {
        Console.WriteLine($"{Mascota.Nombre} | {Servicio} | Peso: {Peso:F1} kg | Temperatura: {Temperatura:F1} C | Total: Q{CalcularTotal():F2} | Estado: {ObtenerEstadoClinico()}");
    }

    private static void ValidarPeso(double peso)
    {
        if (peso <= 0)
        {
            throw new ArgumentException("El peso debe ser mayor que 0.");
        }
    }

    private static void ValidarTemperatura(double temperatura)
    {
        if (temperatura < 35 || temperatura > 43)
        {
            throw new ArgumentException("La temperatura debe estar entre 35 y 43 grados.");
        }
    }

    private static void ValidarCosto(double costo)
    {
        if (costo <= 0)
        {
            throw new ArgumentException("El costo debe ser mayor que 0.");
        }
    }
}
