namespace CarritodeCompras.logica
{
    /// <summary>
    /// Clase Producto: Representa los artículos disponibles en el catálogo.
    /// </summary>
    public class Producto
    {
        // Propiedades con prefijo por tipo de dato
        public string strCodigo { get; set; }
        public string strNombre { get; set; }
        public double dblPrecio { get; set; }
        public int intStock { get; set; }

        // Constructor
        public Producto(string strCodigo, string strNombre, double dblPrecio, int intStock)
        {
            this.strCodigo = strCodigo;
            this.strNombre = strNombre;
            this.dblPrecio = dblPrecio;
            this.intStock = intStock;
        }

        /// <summary>
        /// Valida si hay suficientes existencias en bodega para la cantidad solicitada.
        /// </summary>
        public bool verificarStock(int intCantidadDeseada)
        {
            return intCantidadDeseada > 0 && this.intStock >= intCantidadDeseada;
        }

        /// <summary>
        /// Reduce el inventario tras confirmarse la compra.
        /// </summary>
        public void reducirStock(int intCantidad)
        {
            if (verificarStock(intCantidad))
            {
                this.intStock -= intCantidad;
            }
        }

        /// <summary>
        /// Muestra la ficha básica del producto.
        /// </summary>
        public void mostrarProducto()
        {
            Console.WriteLine($"[{strCodigo}] {strNombre} - Precio: Q{dblPrecio:F2} | Stock: {intStock}");
        }
    }
}
