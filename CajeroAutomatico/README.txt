Proyecto BlueJ: Cajero Automatico

Curso: Programacion 2
Universidad Mariano Galvez, sede Portales

Objetivo:
Practicar programacion orientada a objetos en Java usando BlueJ.

Clases principales:
- Cliente: datos del titular.
- CuentaBancaria: saldo, deposito, retiro, transferencia e historial.
- Tarjeta: autenticacion por PIN y bloqueo por intentos fallidos.
- Banco: administra cuentas, tarjetas y facturas.
- CajeroAutomatico: operaciones disponibles para el usuario autenticado.
- Transaccion: clase abstracta base.
- Deposito, Retiro, Transferencia y PagoServicio: herencia y polimorfismo.
- Factura: saldo pendiente de servicio con soporte de pago parcial.
- Recibo: comprobante de una operacion.
- Simulador: clase con main para probar el sistema.

POO aplicada:
- Encapsulamiento: atributos privados y metodos publicos.
- Abstraccion: clase abstracta Transaccion.
- Herencia: Deposito, Retiro, Transferencia y PagoServicio heredan de Transaccion.
- Polimorfismo: el cajero ejecuta distintos tipos de Transaccion.

Pago de servicios:
- Factura permite pagos parciales o totales.
- No permite sobrepago: monto no puede superar el saldo pendiente.
- Al llegar a saldo cero la factura queda marcada como pagada.
- PagoServicio recibe cuenta, factura y monto especifico a pagar.
- CajeroAutomatico expone: pagarServicio(tipo, identificador, monto).

Para probar en BlueJ:
1. Abrir esta carpeta como proyecto BlueJ.
2. Presionar Compile.
3. Clic derecho sobre Simulador.
4. Ejecutar void main(String[] args).
5. Presionar OK.
6. Revisar la salida en la terminal.

Para probar desde PowerShell:
1. Entrar a la carpeta del repositorio.
2. Ejecutar cd CajeroAutomatico.
3. Ejecutar javac *.java.
4. Ejecutar java Simulador.

Como leer el codigo:
1. Abrir Simulador para ver el flujo general.
2. Revisar CajeroAutomatico para ver todas las operaciones.
3. Revisar CuentaBancaria para entender saldo y movimientos.
4. Revisar Factura para entender el pago parcial.
5. Revisar PagoServicio para entender la transaccion de pago.
6. Revisar Transaccion y sus hijas para entender herencia y polimorfismo.

Nota:
Los archivos .class aparecen al compilar pero no son codigo fuente.

Documentacion adicional:
- README.md: guia completa para GitHub.
- diagramas.md: diagramas de clases, casos de uso, actividades, flujo y entidad-relacion.
