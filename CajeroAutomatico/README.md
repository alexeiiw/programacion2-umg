# Cajero Automatico

Proyecto BlueJ para practicar programacion orientada a objetos en Java.

Curso: Programacion 2
Universidad Mariano Galvez, sede Portales

## Objetivo

Simular un cajero automatico con operaciones basicas y pago de servicios usando clases,
objetos, encapsulamiento, abstraccion, herencia y polimorfismo.

## Clases Del Proyecto

- `Cliente`: representa al titular de una cuenta.
- `CuentaBancaria`: administra saldo, depositos, retiros, transferencias e historial.
- `Tarjeta`: valida PIN y bloquea la tarjeta despues de intentos fallidos.
- `Banco`: registra y busca cuentas, tarjetas y facturas.
- `CajeroAutomatico`: coordina autenticacion y operaciones del usuario.
- `Transaccion`: clase abstracta base para operaciones bancarias.
- `Deposito`: transaccion concreta para depositar dinero.
- `Retiro`: transaccion concreta para retirar dinero.
- `Transferencia`: transaccion concreta para mover dinero entre cuentas.
- `Factura`: representa una factura de servicio con saldo pendiente y soporte de pago parcial.
- `PagoServicio`: transaccion concreta para pagar parcial o totalmente una factura registrada.
- `Recibo`: genera el comprobante de una operacion.
- `Simulador`: contiene el metodo `main` para probar el sistema.

## Como Probar En BlueJ

1. Abrir BlueJ.
2. Ir a `Project`.
3. Seleccionar `Open Project...`.
4. Elegir la carpeta `CajeroAutomatico`.
5. Presionar `Compile`.
6. Hacer clic derecho sobre la clase `Simulador`.
7. Seleccionar `void main(String[] args)`.
8. Presionar `OK`.
9. Revisar la salida en la terminal de BlueJ.

## Como Probar Desde PowerShell

```powershell
cd CajeroAutomatico
javac *.java
java Simulador
```

## Salida Esperada

El simulador muestra:

- Intento de autenticacion incorrecto.
- Autenticacion correcta con dos sesiones distintas.
- Retiro aprobado.
- Deposito aprobado.
- Transferencia aprobada.
- Pago parcial de telefono aprobado, saldo pendiente actualizado.
- Segundo pago que completa la factura de telefono.
- Intento de pago sobre factura ya pagada, rechazado.
- Retiro rechazado por saldo insuficiente.
- Pago parcial de energia aprobado.
- Intento de pago mayor al saldo pendiente, rechazado.
- Historial de transacciones por cuenta.
- Estado final de facturas.

## Pago De Servicios

El pago de servicios permite pagos parciales o totales sobre una factura registrada en el banco.

### Factura

- Guarda tipo, identificador, saldo pendiente y estado pagada/pendiente.
- Metodo `pagar(double monto)`: descuenta el monto si es valido.
- Validaciones en `pagar`:
  - Factura ya pagada completamente: rechaza.
  - Monto mayor al saldo pendiente: rechaza, no permite sobrepago.
  - Monto valido: descuenta y marca como pagada si el saldo llega a cero.

### PagoServicio

- Hereda de `Transaccion`.
- Recibe `CuentaBancaria`, `Factura` y `monto`.
- `ejecutar()` valida el saldo de la factura, descuenta de la cuenta y llama a `factura.pagar(monto)`.

### Metodo en CajeroAutomatico

```java
pagarServicio(String tipo, String identificador, double monto)
```

### Servicios incluidos en el simulador

- Telefono: identificado por numero de linea fija.
- Energia electrica: identificado por numero de correlativo.

## POO Aplicada

- Encapsulamiento: atributos privados y metodos publicos.
- Abstraccion: `Transaccion` define comportamiento comun.
- Herencia: `Deposito`, `Retiro`, `Transferencia` y `PagoServicio` heredan de `Transaccion`.
- Polimorfismo: el cajero trabaja con distintos tipos de `Transaccion`.

## Diagramas

Los diagramas del proyecto estan en `diagramas.md`.
Incluye diagrama de clases, casos de uso, actividades con carriles,
flujo principal y modelo entidad-relacion conceptual.

## Nota Sobre Archivos Compilados

Los archivos `.class` se generan al compilar. No forman parte del codigo fuente
y no deben subirse al repositorio.
