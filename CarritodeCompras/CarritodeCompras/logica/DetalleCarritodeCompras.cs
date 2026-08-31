namespace CarritodeCompras.logica
{
    /// <summary>
    /// Clase DetalleCarritodeCompras: Asocia un producto con la cantidad seleccionada.
    /// </summary>
    public class DetalleCarritodeCompras
    {
        // Propiedades con prefijo por tipo de dato
        public Producto objProducto { get; set; }
        public int intCantidad { get; set; }

        // Constructor
        public DetalleCarritodeCompras(Producto objProducto, int intCantidad)
        {
            this.objProducto = objProducto;
            this.intCantidad = intCantidad;
        }

        /// <summary>
        /// Calcula el subtotal multiplicando precio unitario por cantidad.
        /// </summary>
        public double calcularSubtotal()
        {
            if (objProducto == null) return 0.0;
            return objProducto.dblPrecio * intCantidad;
        }

        /// <summary>
        /// Muestra en consola la fila del detalle.
        /// </summary>
        public void mostrarDetalle()
        {
            if (objProducto != null)
            {
                double dblSubtotal = calcularSubtotal();
                Console.WriteLine($" - {objProducto.strNombre,-20} Cantidad: {intCantidad,-3} Unitario: Q{objProducto.dblPrecio,7:F2} | Subtotal: Q{dblSubtotal,8:F2}");
            }
        }
    }
}
