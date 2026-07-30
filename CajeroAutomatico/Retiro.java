public class Retiro extends Transaccion {
    public Retiro(CuentaBancaria cuentaOrigen, double monto) {
        super(cuentaOrigen, monto);
    }

    @Override
    public boolean ejecutar() {
        boolean exitosa = getCuentaOrigen().retirar(getMonto());

        if (exitosa) {
            getCuentaOrigen().registrarTransaccion(this);
        }

        return exitosa;
    }

    @Override
    public String getTipo() {
        return "Retiro";
    }
}
