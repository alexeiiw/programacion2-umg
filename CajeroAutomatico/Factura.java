public class Factura {
    private final String tipo;
    private final String identificador;
    private double saldo;
    private boolean pagada;

    public Factura(String tipo, String identificador, double saldo) {
        if (saldo <= 0) {
            throw new IllegalArgumentException("El saldo de la factura debe ser mayor que cero");
        }
        this.tipo = tipo;
        this.identificador = identificador;
        this.saldo = saldo;
        this.pagada = false;
    }

    public String getTipo() {
        return tipo;
    }

    public String getIdentificador() {
        return identificador;
    }

    public double getSaldo() {
        return saldo;
    }

    public boolean estaPagada() {
        return pagada;
    }

    public String getLlave() {
        return tipo.toUpperCase() + ":" + identificador.toUpperCase();
    }

    public boolean pagar(double monto) {
        if (pagada) {
            return false;
        }
        if (monto <= 0) {
            return false;
        }
        if (monto > saldo) {
            return false;
        }
        saldo -= monto;
        if (saldo == 0) {
            pagada = true;
        }
        return true;
    }

    @Override
    public String toString() {
        String estado = pagada ? "Pagada" : "Pendiente Q" + saldo;
        return tipo + " - " + identificador + " - " + estado;
    }
}
