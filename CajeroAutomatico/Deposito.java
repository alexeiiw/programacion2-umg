public class Deposito extends Transaccion {
    public Deposito(CuentaBancaria cuentaOrigen, double monto) {
        super(cuentaOrigen, monto);
    }

    @Override
    public boolean ejecutar() {
        getCuentaOrigen().depositar(getMonto());
        getCuentaOrigen().registrarTransaccion(this);
        return true;
    }

    @Override
    public String getTipo() {
        return "Deposito";
    }
}
