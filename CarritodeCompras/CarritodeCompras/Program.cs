using System;
using System.Collections.Generic;
using CarritodeCompras.logica;

namespace CarritodeCompras
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Inicialización del Catálogo de Productos en memoria
            List<Producto> lstCatalogo = new List<Producto>()
            {
                new Producto("P01", "Laptop HP Core i5", 5200.00, 5),
                new Producto("P02", "Mouse Inalámbrico", 125.50, 15),
                new Producto("P03", "Teclado Mecánico RGB", 450.00, 8),
                new Producto("P04", "Monitor 24'' Full HD", 1100.00, 4),
                new Producto("P05", "Audífonos Bluetooth", 320.00, 10)
            };

            // 2. Instancia del Carrito de Compras
            CarritodeCompras.logica.CarritodeCompras objCarrito = new CarritodeCompras.logica.CarritodeCompras();

            bool blnContinuar = true;

            while (blnContinuar)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n=======================================================");
                Console.WriteLine("        SISTEMA DE CARRITO DE COMPRAS - E-COMMERCE     ");
                Console.WriteLine("=======================================================");
                Console.ResetColor();
                Console.WriteLine(" 1. Ver Catálogo de Productos");
                Console.WriteLine(" 2. Agregar Producto al Carrito");
                Console.WriteLine(" 3. Ver Carrito de Compras");
                Console.WriteLine(" 4. Vaciar Carrito");
                Console.WriteLine(" 5. Pagar con Tarjeta de Crédito (Checkout)");
                Console.WriteLine(" 6. Salir");
                Console.Write("\n Seleccione una opción (1-6): ");

                string strOpcion = Console.ReadLine()?.Trim();

                switch (strOpcion)
                {
                    case "1":
                        mostrarCatalogo(lstCatalogo);
                        break;

                    case "2":
                        agregarProductoAlCarrito(lstCatalogo, objCarrito);
                        break;

                    case "3":
                        objCarrito.mostrarCarrito();
                        break;

                    case "4":
                        objCarrito.vaciarCarrito();
                        break;

                    case "5":
                        procesarPagoConTarjeta(objCarrito);
                        break;

                    case "6":
                        blnContinuar = false;
                        Console.WriteLine("\nGracias por utilizar el sistema de compras. ¡Hasta pronto!");
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida. Por favor ingrese un número del 1 al 6.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        /// <summary>
        /// Muestra el listado del catálogo de productos disponibles y su stock.
        /// </summary>
        static void mostrarCatalogo(List<Producto> lstCatalogo)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n---------------- CATÁLOGO DE PRODUCTOS ----------------");
            Console.ResetColor();
            foreach (var prod in lstCatalogo)
            {
                prod.mostrarProducto();
            }
            Console.WriteLine("-------------------------------------------------------\n");
        }

        /// <summary>
        /// Solicita el código del producto y cantidad para agregarlo al carrito.
        /// </summary>
        static void agregarProductoAlCarrito(List<Producto> lstCatalogo, CarritodeCompras.logica.CarritodeCompras objCarrito)
        {
            mostrarCatalogo(lstCatalogo);

            Console.Write("Ingrese el código del producto a comprar: ");
            string strCodigo = Console.ReadLine()?.Trim().ToUpper();

            Producto objProd = lstCatalogo.Find(p => p.strCodigo.ToUpper() == strCodigo);
            if (objProd == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: El código ingresado no existe en el catálogo.");
                Console.ResetColor();
                return;
            }

            Console.Write($"Ingrese la cantidad deseada de '{objProd.strNombre}': ");
            if (int.TryParse(Console.ReadLine(), out int intCantidad) && intCantidad > 0)
            {
                objCarrito.agregarProducto(objProd, intCantidad);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Debe ingresar una cantidad numérica entera mayor a 0.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Simula el proceso de pago exclusivo con Tarjeta de Crédito.
        /// </summary>
        static void procesarPagoConTarjeta(CarritodeCompras.logica.CarritodeCompras objCarrito)
        {
            if (objCarrito.lstDetalles.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo puede realizar el checkout porque el carrito está vacío.");
                Console.ResetColor();
                return;
            }

            objCarrito.mostrarCarrito();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("================= PASARELA DE PAGOS =================");
            Console.WriteLine(" [REGLA]: El único método admitido es Tarjeta de Crédito");
            Console.ResetColor();

            Console.Write(" Ingrese Número de Tarjeta (16 dígitos): ");
            string strNumTarjeta = Console.ReadLine()?.Trim().Replace(" ", "");

            if (string.IsNullOrEmpty(strNumTarjeta) || strNumTarjeta.Length < 13)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Error: Número de tarjeta no válido. Pago rechazado.");
                Console.ResetColor();
                return;
            }

            Console.Write(" Ingrese Nombre del Titular: ");
            string strTitular = Console.ReadLine()?.Trim();

            Console.Write(" Ingrese Fecha de Expiración (MM/AA): ");
            string strExpiracion = Console.ReadLine()?.Trim();

            Console.Write(" Ingrese CVV (3 dígitos): ");
            string strCVV = Console.ReadLine()?.Trim();

            // Simulación de validación bancaria
            string strUltimos4 = strNumTarjeta.Length >= 4 ? strNumTarjeta.Substring(strNumTarjeta.Length - 4) : "****";
            double dblTotal = objCarrito.calcularTotal();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n >>> ¡PAGO AUTORIZADO CON ÉXITO! <<<");
            Console.WriteLine($" Monto debitado: Q{dblTotal:F2}");
            Console.WriteLine($" Tarjeta: **** **** **** {strUltimos4} | Titular: {strTitular}");
            Console.WriteLine(" Se han descontado los productos del inventario.");
            Console.ResetColor();

            // Descontar inventario y vaciar carrito
            objCarrito.confirmarCompra();
        }
    }
}
