/**
 * Clase TarjetaCredito.
 * Modela una tarjeta de credito para el procesamiento exclusivo de pagos de compras.
 * 
 * @author Programacion II - UMG Portales
 * @version 1.0
 */
public class TarjetaCredito {
    private String numeroTarjeta;
    private String titular;
    private String fechaExpiracion; // Formato MM/AA
    private String cvv;
    private double limiteCredito;
    private double saldoUtilizado;
    private boolean bloqueada;

    /**
     * Constructor por defecto.
     */
    public TarjetaCredito() {
        this.numeroTarjeta = "";
        this.titular = "";
        this.fechaExpiracion = "";
        this.cvv = "";
        this.limiteCredito = 0.0;
        this.saldoUtilizado = 0.0;
        this.bloqueada = false;
    }

    /**
     * Constructor con datos principales.
     * 
     * @param numeroTarjeta Numero de 16 digitos de la tarjeta.
     * @param titular Nombre del titular.
     * @param fechaExpiracion Fecha de vencimiento (MM/AA).
     * @param cvv Codigo de seguridad (3 digitos).
     * @param limiteCredito Limite de credito aprobado.
     */
    public TarjetaCredito(String numeroTarjeta, String titular, String fechaExpiracion, String cvv, double limiteCredito) {
        this.numeroTarjeta = numeroTarjeta != null ? numeroTarjeta.replaceAll("\\s+", "") : "";
        this.titular = titular;
        this.fechaExpiracion = fechaExpiracion;
        this.cvv = cvv;
        this.limiteCredito = Math.max(0.0, limiteCredito);
        this.saldoUtilizado = 0.0;
        this.bloqueada = false;
    }

    // Getters y Setters
    public String getNumeroTarjeta() {
        return numeroTarjeta;
    }

    public void setNumeroTarjeta(String numeroTarjeta) {
        this.numeroTarjeta = numeroTarjeta != null ? numeroTarjeta.replaceAll("\\s+", "") : "";
    }

    public String getTitular() {
        return titular;
    }

    public void setTitular(String titular) {
        this.titular = titular;
    }

    public String getFechaExpiracion() {
        return fechaExpiracion;
    }

    public void setFechaExpiracion(String fechaExpiracion) {
        this.fechaExpiracion = fechaExpiracion;
    }

    public String getCvv() {
        return cvv;
    }

    public void setCvv(String cvv) {
        this.cvv = cvv;
    }

    public double getLimiteCredito() {
        return limiteCredito;
    }

    public void setLimiteCredito(double limiteCredito) {
        this.limiteCredito = Math.max(0.0, limiteCredito);
    }

    public double getSaldoUtilizado() {
        return saldoUtilizado;
    }

    public boolean isBloqueada() {
        return bloqueada;
    }

    public void setBloqueada(boolean bloqueada) {
        this.bloqueada = bloqueada;
    }

    /**
     * Calcula el cupo disponible actual de la tarjeta.
     * 
     * @return double con el cupo restante.
     */
    public double getCupoDisponible() {
        return Math.max(0.0, this.limiteCredito - this.saldoUtilizado);
    }

    /**
     * Valida si los datos estructurales de la tarjeta son consistentes.
     * 
     * @return true si los campos cumplen el formato minimo.
     */
    public boolean validar() {
        if (bloqueada) return false;
        if (numeroTarjeta == null || numeroTarjeta.length() < 13) return false;
        if (cvv == null || (cvv.length() != 3 && cvv.length() != 4)) return false;
        if (fechaExpiracion == null || !fechaExpiracion.contains("/")) return false;
        return true;
    }

    /**
     * Valida si la tarjeta tiene suficiente cupo disponible para un monto.
     * 
     * @param monto Monto total a cobrar.
     * @return true si hay cupo suficiente y no esta bloqueada.
     */
    public boolean tieneCupoDisponible(double monto) {
        if (!validar() || monto <= 0) return false;
        return getCupoDisponible() >= monto;
    }

    /**
     * Procesa un cobro cargandolo al saldo utilizado de la tarjeta.
     * 
     * @param monto Monto a debitar del limite.
     * @return true si el cobro fue autorizado y procesado con exito.
     */
    public boolean procesarCobro(double monto) {
        if (!tieneCupoDisponible(monto)) {
            System.out.println("Error: Cobro rechazado. Cupo insuficiente o tarjeta invalida.");
            return false;
        }

        this.saldoUtilizado += monto;
        System.out.println("Cobro autorizado por Q" + String.format("%.2f", monto) + " en tarjeta terminada en " + getUltimos4Digitos());
        return true;
    }

    /**
     * Realiza un abono o pago a la tarjeta, liberando cupo.
     * 
     * @param monto Monto a abonar.
     */
    public void abonarSaldo(double monto) {
        if (monto > 0) {
            this.saldoUtilizado = Math.max(0.0, this.saldoUtilizado - monto);
        }
    }

    /**
     * Retorna los ultimos 4 digitos para impresion segura.
     * 
     * @return String con los ultimos 4 digitos o vacio.
     */
    public String getUltimos4Digitos() {
        if (numeroTarjeta == null || numeroTarjeta.length() < 4) return "****";
        return numeroTarjeta.substring(numeroTarjeta.length() - 4);
    }

    @Override
    public String toString() {
        return "TarjetaCredito [Terminacion=****" + getUltimos4Digitos() + ", Titular=" + titular 
                + ", Limite=Q" + limiteCredito + ", CupoDisponible=Q" + getCupoDisponible() + "]";
    }
}
