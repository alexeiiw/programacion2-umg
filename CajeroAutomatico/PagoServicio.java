public class PagoServicio extends Transaccion {
    private final Factura factura;

    public PagoServicio(CuentaBancaria cuentaOrigen, Factura factura, double monto) {
        super(cuentaOrigen, monto);
        this.factura = factura;
    }

    public Factura getFactura() {
        return factura;
    }

    @Override
    public boolean ejecutar() {
        if (factura.estaPagada()) {
            return false;
        }
        if (getMonto() > factura.getSaldo()) {
            return false;
        }
        boolean exitosa = getCuentaOrigen().retirar(getMonto());
        if (exitosa) {
            factura.pagar(getMonto());
            getCuentaOrigen().registrarTransaccion(this);
        }
        return exitosa;
    }

    @Override
    public String getTipo() {
        return "Pago de Servicio";
    }
}
