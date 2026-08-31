/**
 * Clase Producto.
 * Modela un articulo del catalogo comercial con gestion de precio unitario e inventario (stock).
 * 
 * @author Programacion II - UMG Portales
 * @version 1.0
 */
public class Producto {
    private String codigo;
    private String nombre;
    private String descripcion;
    private double precioUnitario;
    private int stock;

    /**
     * Constructor por defecto.
     */
    public Producto() {
        this.codigo = "";
        this.nombre = "";
        this.descripcion = "";
        this.precioUnitario = 0.0;
        this.stock = 0;
    }

    /**
     * Constructor con atributos principales.
     * 
     * @param codigo Codigo identificador del producto (SKU).
     * @param nombre Nombre comercial.
     * @param precioUnitario Precio en Quetzales.
     * @param stock Cantidad de unidades disponibles en inventario.
     */
    public Producto(String codigo, String nombre, double precioUnitario, int stock) {
        this.codigo = codigo;
        this.nombre = nombre;
        this.descripcion = "";
        this.precioUnitario = Math.max(0.0, precioUnitario);
        this.stock = Math.max(0, stock);
    }

    /**
     * Constructor completo.
     * 
     * @param codigo Codigo identificador.
     * @param nombre Nombre comercial.
     * @param descripcion Descripcion detallada.
     * @param precioUnitario Precio en Quetzales.
     * @param stock Cantidad en inventario.
     */
    public Producto(String codigo, String nombre, String descripcion, double precioUnitario, int stock) {
        this.codigo = codigo;
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.precioUnitario = Math.max(0.0, precioUnitario);
        this.stock = Math.max(0, stock);
    }

    // Getters y Setters
    public String getCodigo() {
        return codigo;
    }

    public void setCodigo(String codigo) {
        this.codigo = codigo;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }

    public double getPrecioUnitario() {
        return precioUnitario;
    }

    public void setPrecioUnitario(double precioUnitario) {
        this.precioUnitario = Math.max(0.0, precioUnitario);
    }

    public int getStock() {
        return stock;
    }

    public void setStock(int stock) {
        this.stock = Math.max(0, stock);
    }

    /**
     * Valida si existe inventario suficiente para la cantidad solicitada (Regla de negocio).
     * 
     * @param cantidad Cantidad requerida.
     * @return true si el stock actual cubre la cantidad (stock >= cantidad y cantidad > 0).
     */
    public boolean hayStock(int cantidad) {
        return cantidad > 0 && this.stock >= cantidad;
    }

    /**
     * Descuenta del inventario la cantidad comprada.
     * 
     * @param cantidad Cantidad a descontar.
     * @return true si se efectuo el descuento exitosamente, false si no habia suficiente stock.
     */
    public boolean descontarStock(int cantidad) {
        if (!hayStock(cantidad)) {
            System.out.println("Error: No hay suficiente stock de " + nombre + " (Disponible: " + stock + ", Solicitado: " + cantidad + ")");
            return false;
        }

        this.stock -= cantidad;
        return true;
    }

    /**
     * Incrementa el inventario por reabastecimiento o devolucion.
     * 
     * @param cantidad Cantidad a anadir.
     */
    public void reabastecer(int cantidad) {
        if (cantidad > 0) {
            this.stock += cantidad;
        }
    }

    @Override
    public String toString() {
        return "[" + codigo + "] " + nombre + " - Q" + String.format("%.2f", precioUnitario) + " (Stock: " + stock + ")";
    }
}
