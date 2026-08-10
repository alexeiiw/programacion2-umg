public class CajeroAutomatico {
    private final Banco banco;
    private Tarjeta tarjetaActual;

    public CajeroAutomatico(Banco banco) {
        this.banco = banco;
    }

    public boolean autenticar(String numeroTarjeta, String pin) {
        Tarjeta tarjeta = banco.buscarTarjeta(numeroTarjeta);
        if (tarjeta != null && tarjeta.validarPin(pin)) {
            tarjetaActual = tarjeta;
            return true;
        }
        tarjetaActual = null;
        return false;
    }

    public void cerrarSesion() {
        tarjetaActual = null;
    }

    public double consultarSaldo() {
        return obtenerCuentaAutenticada().getSaldo();
    }

    public Recibo depositar(double monto) {
        Transaccion transaccion = new Deposito(obtenerCuentaAutenticada(), monto);
        boolean exitosa = transaccion.ejecutar();
        return transaccion.generarRecibo(exitosa);
    }

    public Recibo retirar(double monto) {
        Transaccion transaccion = new Retiro(obtenerCuentaAutenticada(), monto);
        boolean exitosa = transaccion.ejecutar();
        return transaccion.generarRecibo(exitosa);
    }

    public Recibo transferir(String numeroCuentaDestino, double monto) {
        CuentaBancaria destino = banco.buscarCuenta(numeroCuentaDestino);
        if (destino == null) {
            throw new IllegalArgumentException("La cuenta destino no existe");
        }
        Transaccion transaccion = new Transferencia(obtenerCuentaAutenticada(), destino, monto);
        boolean exitosa = transaccion.ejecutar();
        return transaccion.generarRecibo(exitosa);
    }

    public Recibo pagarServicio(String tipo, String identificador, double monto) {
        Factura factura = banco.buscarFactura(tipo, identificador);
        if (factura == null) {
            throw new IllegalArgumentException("La factura no existe: " + tipo + " / " + identificador);
        }
        Transaccion transaccion = new PagoServicio(obtenerCuentaAutenticada(), factura, monto);
        boolean exitosa = transaccion.ejecutar();
        return transaccion.generarRecibo(exitosa);
    }

    private CuentaBancaria obtenerCuentaAutenticada() {
        if (tarjetaActual == null) {
            throw new IllegalStateException("Debe autenticarse primero");
        }
        return tarjetaActual.getCuenta();
    }
}
