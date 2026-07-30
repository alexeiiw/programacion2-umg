import java.time.LocalDateTime;

public abstract class Transaccion {
    private static int siguienteCodigo = 1;

    private final int codigo;
    private final LocalDateTime fecha;
    private final CuentaBancaria cuentaOrigen;
    private final double monto;

    public Transaccion(CuentaBancaria cuentaOrigen, double monto) {
        this.codigo = siguienteCodigo++;
        this.fecha = LocalDateTime.now();
        this.cuentaOrigen = cuentaOrigen;
        this.monto = monto;
    }

    public int getCodigo() {
        return codigo;
    }

    public LocalDateTime getFecha() {
        return fecha;
    }

    public CuentaBancaria getCuentaOrigen() {
        return cuentaOrigen;
    }

    public double getMonto() {
        return monto;
    }

    public abstract boolean ejecutar();

    public abstract String getTipo();

    public Recibo generarRecibo(boolean exitosa) {
        return new Recibo(this, exitosa);
    }
}
