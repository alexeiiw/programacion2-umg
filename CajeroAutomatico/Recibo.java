public class Recibo {
    private final Transaccion transaccion;
    private final boolean exitosa;

    public Recibo(Transaccion transaccion, boolean exitosa) {
        this.transaccion = transaccion;
        this.exitosa = exitosa;
    }

    public String imprimir() {
        String estado = exitosa ? "APROBADA" : "RECHAZADA";
        String texto = "Recibo #" + transaccion.getCodigo() + "\n"
                + "Tipo: " + transaccion.getTipo() + "\n"
                + "Fecha: " + transaccion.getFecha() + "\n"
                + "Cuenta: " + transaccion.getCuentaOrigen().getNumero() + "\n"
                + "Monto: " + transaccion.getMonto() + "\n"
                + "Estado: " + estado;

        if (transaccion instanceof Transferencia) {
            Transferencia transferencia = (Transferencia) transaccion;
            texto += "\nCuenta destino: " + transferencia.getCuentaDestino().getNumero();
        }

        return texto;
    }

    @Override
    public String toString() {
        return imprimir();
    }
}
