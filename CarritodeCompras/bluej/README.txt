------------------------------------------------------------------------
PROYECTO: Carrito de Compras (Banco de Objetos)
CURSO: Programacion II - UMG Portales
AUTOR: Catedra de Programacion II
VERSION: 1.0 (Modulo 2)
------------------------------------------------------------------------

DESCRIPCION DEL PROYECTO:
Banco de objetos base para el modelado y ejecucion de un sistema de comercio
electronico (E-Commerce) con carrito de compras y pasarela de tarjeta de credito.

OBJETOS INCLUIDOS:
1. Persona: Superclase base con datos personales e identificacion.
2. Usuario: Subclase de Persona para credenciales y autenticacion.
3. Cliente: Subclase de Persona con direccion, documento y tarjeta asociada.
4. Documento: Modela identificacion legal (DPI, NIT, Pasaporte).
5. TarjetaCredito: Modela la tarjeta y validaciones de limite y cobro.
6. Producto: Modela articulos con control estricto de inventario (stock).
7. CarritoCompras: Modela la sesion del carrito del cliente.

EJERCICIO PRACTICO PARA ESTUDIANTES:
Crear la clase 'DetalleCarrito' para establecer la relacion de composicion
(1 a muchos) con 'CarritoCompras' y la asociacion por referencia hacia 'Producto'.
------------------------------------------------------------------------
