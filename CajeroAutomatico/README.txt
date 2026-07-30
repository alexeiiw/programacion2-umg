Proyecto BlueJ: Cajero Automatico

Curso: Programacion 2
Universidad Mariano Galvez, sede Portales

Objetivo:
Practicar programacion orientada a objetos en Java usando BlueJ.

Clases principales:
- Cliente: datos del titular.
- CuentaBancaria: saldo, deposito, retiro, transferencia e historial.
- Tarjeta: autenticacion por PIN y bloqueo por intentos fallidos.
- Banco: administra cuentas y tarjetas.
- CajeroAutomatico: operaciones disponibles para el usuario autenticado.
- Transaccion: clase abstracta base.
- Deposito, Retiro y Transferencia: herencia y polimorfismo.
- Recibo: representa el comprobante de una operacion.
- Simulador: clase con main para probar el sistema.

POO aplicada:
- Encapsulamiento: atributos privados y metodos publicos.
- Abstraccion: clase abstracta Transaccion.
- Herencia: Deposito, Retiro y Transferencia heredan de Transaccion.
- Polimorfismo: el cajero ejecuta diferentes transacciones usando la clase base.

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
1. Abrir primero Simulador para ver el flujo general.
2. Revisar CajeroAutomatico para ver las operaciones disponibles.
3. Revisar CuentaBancaria para entender saldo, retiro, deposito y transferencia.
4. Revisar Transaccion y sus hijas para entender herencia y polimorfismo.

Nota:
Los archivos .class aparecen al compilar, pero no forman parte del codigo fuente.

Documentacion adicional:
- README.md: guia completa del proyecto para GitHub.
- diagramas.md: diagrama de clases, casos de uso, actividades con carriles, flujo y modelo entidad-relacion conceptual.
