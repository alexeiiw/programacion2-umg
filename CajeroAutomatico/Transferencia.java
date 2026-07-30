public class Transferencia extends Transaccion {
    private final CuentaBancaria cuentaDestino;

    public Transferencia(CuentaBancaria cuentaOrigen, CuentaBancaria cuentaDestino, double monto) {
        super(cuentaOrigen, monto);
        this.cuentaDestino = cuentaDestino;
    }

    public CuentaBancaria getCuentaDestino() {
        return cuentaDestino;
    }

    @Override
    public boolean ejecutar() {
        boolean exitosa = getCuentaOrigen().transferirA(cuentaDestino, getMonto());

        if (exitosa) {
            getCuentaOrigen().registrarTransaccion(this);
            cuentaDestino.registrarTransaccion(this);
        }

        return exitosa;
    }

    @Override
    public String getTipo() {
        return "Transferencia";
    }
}
