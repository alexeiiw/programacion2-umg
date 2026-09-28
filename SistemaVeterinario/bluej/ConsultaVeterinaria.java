public class ConsultaVeterinaria {
    private Mascota mascota;
    private String servicio;
    private double peso;
    private double temperatura;
    private double costo;

    public ConsultaVeterinaria(Mascota mascota, String servicio) {
        this.mascota = mascota;
        this.servicio = servicio;
    }

    public void registrarDatos(double peso, double temperatura, double costo) {
        validarPeso(peso);
        validarTemperatura(temperatura);
        validarCosto(costo);
        this.peso = peso;
        this.temperatura = temperatura;
        this.costo = costo;
    }

    public double calcularTotal() {
        return costo;
    }

    public String obtenerEstadoClinico() {
        return temperatura >= 39.5 ? "Requiere observacion" : "Estable";
    }

    public void mostrarResultado() {
        System.out.println(mascota.nombre + " | " + servicio + " | Peso: "
                + String.format("%.1f", peso) + " kg | Temperatura: "
                + String.format("%.1f", temperatura) + " C | Total: Q"
                + String.format("%.2f", calcularTotal()) + " | Estado: " + obtenerEstadoClinico());
    }

    private void validarPeso(double peso) {
        if (peso <= 0) {
            throw new IllegalArgumentException("El peso debe ser mayor que 0.");
        }
    }

    private void validarTemperatura(double temperatura) {
        if (temperatura < 35 || temperatura > 43) {
            throw new IllegalArgumentException("La temperatura debe estar entre 35 y 43 grados.");
        }
    }

    private void validarCosto(double costo) {
        if (costo <= 0) {
            throw new IllegalArgumentException("El costo debe ser mayor que 0.");
        }
    }
}
