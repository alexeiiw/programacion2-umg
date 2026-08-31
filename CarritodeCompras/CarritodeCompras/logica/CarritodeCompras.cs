using System;
using System.Collections.Generic;

namespace CarritodeCompras.logica
{
    /// <summary>
    /// Clase CarritodeCompras: Gestiona la lista de detalles y los cálculos financieros del carrito.
    /// </summary>
    public class CarritodeCompras
    {
        // Propiedades con prefijo por tipo de dato
        public List<DetalleCarritodeCompras> lstDetalles { get; set; }

        // Constructor
        public CarritodeCompras()
        {
            this.lstDetalles = new List<DetalleCarritodeCompras>();
        }

        /// <summary>
        /// Agrega un producto al carrito validando previamente el stock disponible.
        /// </summary>
        public bool agregarProducto(Producto objProducto, int intCantidad)
        {
            if (objProducto == null)
            {
                Console.WriteLine("Error: Producto no válido.");
                return false;
            }

            // Validar stock antes de agregar
            if (!objProducto.verificarStock(intCantidad))
            {
                Console.WriteLine($"Error: Stock insuficiente para '{objProducto.strNombre}'. Stock actual: {objProducto.intStock}, Solicitado: {intCantidad}");
                return false;
            }

            // Si el producto ya existe en el carrito, se suma la cantidad validando el stock total
            var objDetalleExistente = lstDetalles.Find(d => d.objProducto.strCodigo == objProducto.strCodigo);
            if (objDetalleExistente != null)
            {
                int intNuevaCantidadTotal = objDetalleExistente.intCantidad + intCantidad;
                if (!objProducto.verificarStock(intNuevaCantidadTotal))
                {
                    Console.WriteLine($"Error: La cantidad total en carrito ({intNuevaCantidadTotal}) supera el stock disponible ({objProducto.intStock}).");
                    return false;
                }
                objDetalleExistente.intCantidad = intNuevaCantidadTotal;
            }
            else
            {
                lstDetalles.Add(new DetalleCarritodeCompras(objProducto, intCantidad));
            }

            Console.WriteLine($"¡'{objProducto.strNombre}' agregado exitosamente ({intCantidad} unidades)!");
            return true;
        }

        /// <summary>
        /// Calcula el subtotal sumando los subtotales de todos los items.
        /// </summary>
        public double calcularSubtotal()
        {
            double dblSubtotal = 0.0;
            foreach (var item in lstDetalles)
            {
                dblSubtotal += item.calcularSubtotal();
            }
            return dblSubtotal;
        }

        /// <summary>
        /// Calcula el IVA (12% estándar).
        /// </summary>
        public double calcularImpuesto(double dblPorcentajeIva = 0.12)
        {
            return calcularSubtotal() * dblPorcentajeIva;
        }

        /// <summary>
        /// Calcula el monto total a pagar (Subtotal + IVA).
        /// </summary>
        public double calcularTotal()
        {
            return calcularSubtotal() + calcularImpuesto();
        }

        /// <summary>
        /// Muestra el desglose detallado del carrito en consola.
        /// </summary>
        public void mostrarCarrito()
        {
            Console.WriteLine("\n================== DETALLE DEL CARRITO ==================");
            if (lstDetalles.Count == 0)
            {
                Console.WriteLine(" El carrito de compras se encuentra actualmente vacío.");
                Console.WriteLine("=========================================================\n");
                return;
            }

            foreach (var item in lstDetalles)
            {
                item.mostrarDetalle();
            }

            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine($" Subtotal:       Q{calcularSubtotal(),10:F2}");
            Console.WriteLine($" IVA (12%):      Q{calcularImpuesto(),10:F2}");
            Console.WriteLine($" TOTAL A PAGAR:  Q{calcularTotal(),10:F2}");
            Console.WriteLine("=========================================================\n");
        }

        /// <summary>
        /// Confirma la compra: descuenta stock real de cada producto y vacía el carrito.
        /// </summary>
        public void confirmarCompra()
        {
            foreach (var item in lstDetalles)
            {
                item.objProducto.reducirStock(item.intCantidad);
            }
            lstDetalles.Clear();
        }

        /// <summary>
        /// Vacía todos los elementos del carrito.
        /// </summary>
        public void vaciarCarrito()
        {
            lstDetalles.Clear();
            Console.WriteLine("Se ha vaciado el carrito de compras.");
        }
    }
}
