import java.time.LocalDateTime;

/**
 * Clase CarritoCompras.
 * Representa la sesion y cabecera del carrito de compras de un cliente.
 * 
 * NOTA PEDAGOGICA:
 * La coleccion de items o relacion de composicion hacia 'DetalleCarrito' 
 * constituye el ejercicio practico a desarrollar por los estudiantes.
 * 
 * @author Programacion II - UMG Portales
 * @version 1.0
 */
public class CarritoCompras {
    private String idCarrito;
    private Cliente cliente;
    private LocalDateTime fechaCreacion;
    private String estado; // ACTIVO, COTIZACION, PAGADO, CANCELADO
    private double totalEstimado;

    /**
     * Constructor por defecto.
     */
    public CarritoCompras() {
        this.idCarrito = "CART-" + System.currentTimeMillis();
        this.cliente = null;
        this.fechaCreacion = LocalDateTime.now();
        this.estado = "ACTIVO";
        this.totalEstimado = 0.0;
    }

    /**
     * Constructor asociado a un cliente.
     * 
     * @param cliente Cliente titular del carrito.
     */
    public CarritoCompras(Cliente cliente) {
        this.idCarrito = "CART-" + System.currentTimeMillis();
        this.cliente = cliente;
        this.fechaCreacion = LocalDateTime.now();
        this.estado = "ACTIVO";
        this.totalEstimado = 0.0;
    }

    /**
     * Constructor completo.
     * 
     * @param idCarrito Identificador unico del carrito.
     * @param cliente Cliente titular.
     */
    public CarritoCompras(String idCarrito, Cliente cliente) {
        this.idCarrito = idCarrito;
        this.cliente = cliente;
        this.fechaCreacion = LocalDateTime.now();
        this.estado = "ACTIVO";
        this.totalEstimado = 0.0;
    }

    // Getters y Setters
    public String getIdCarrito() {
        return idCarrito;
    }

    public void setIdCarrito(String idCarrito) {
        this.idCarrito = idCarrito;
    }

    public Cliente getCliente() {
        return cliente;
    }

    public void setCliente(Cliente cliente) {
        this.cliente = cliente;
    }

    public LocalDateTime getFechaCreacion() {
        return fechaCreacion;
    }

    public void setFechaCreacion(LocalDateTime fechaCreacion) {
        this.fechaCreacion = fechaCreacion;
    }

    public String getEstado() {
        return estado;
    }

    public void setEstado(String estado) {
        this.estado = estado;
    }

    public double getTotalEstimado() {
        return totalEstimado;
    }

    public void setTotalEstimado(double totalEstimado) {
        this.totalEstimado = Math.max(0.0, totalEstimado);
    }

    /**
     * Valida si el carrito esta asociado a un cliente registrado (Regla de negocio).
     * 
     * @return true si tiene un cliente no nulo y registrado.
     */
    public boolean tieneClienteRegistrado() {
        return this.cliente != null && this.cliente.isRegistrado();
    }

    /**
     * Valida si el cliente del carrito tiene configurada su tarjeta de credito para pagar.
     * 
     * @return true si el cliente y su tarjeta son validos.
     */
    public boolean listoParaPagoConTarjeta() {
        return tieneClienteRegistrado() && this.cliente.tieneTarjetaCreditoValida();
    }

    /**
     * Vacia el estado del carrito.
     */
    public void vaciar() {
        this.totalEstimado = 0.0;
        this.estado = "ACTIVO";
        System.out.println("Carrito " + idCarrito + " vaciado.");
    }

    @Override
    public String toString() {
        String nombreTitular = cliente != null ? cliente.getNombreCompleto() : "Anonimo / Visitante";
        return "CarritoCompras [ID=" + idCarrito + ", Cliente=" + nombreTitular 
                + ", Estado=" + estado + ", TotalEstimado=Q" + String.format("%.2f", totalEstimado) + "]";
    }
}
