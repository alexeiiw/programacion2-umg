# Cajero Automatico

Proyecto BlueJ para practicar programacion orientada a objetos en Java.

Curso: Programacion 2  
Universidad Mariano Galvez, sede Portales

## Objetivo

Simular un cajero automatico con operaciones basicas usando clases, objetos, encapsulamiento, abstraccion, herencia y polimorfismo.

## Clases Del Proyecto

- `Cliente`: representa al titular de una cuenta.
- `CuentaBancaria`: administra saldo, depositos, retiros, transferencias e historial.
- `Tarjeta`: valida PIN y bloquea la tarjeta despues de intentos fallidos.
- `Banco`: registra y busca cuentas y tarjetas.
- `CajeroAutomatico`: coordina autenticacion y operaciones del usuario.
- `Transaccion`: clase abstracta base para operaciones bancarias.
- `Deposito`: transaccion concreta para depositar dinero.
- `Retiro`: transaccion concreta para retirar dinero.
- `Transferencia`: transaccion concreta para mover dinero entre cuentas.
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

Desde la raiz del repositorio:

```powershell
cd CajeroAutomatico
javac *.java
java Simulador
```

Para regresar a la raiz:

```powershell
cd ..
```

## Salida Esperada

El simulador muestra:

- Intento de autenticacion incorrecto.
- Autenticacion correcta.
- Consulta de saldo inicial.
- Retiro aprobado.
- Deposito aprobado.
- Transferencia aprobada.
- Retiro rechazado por saldo insuficiente.
- Historial de transacciones.
- Saldo final.

## POO Aplicada

- Encapsulamiento: atributos privados y metodos publicos.
- Abstraccion: `Transaccion` define comportamiento comun.
- Herencia: `Deposito`, `Retiro` y `Transferencia` heredan de `Transaccion`.
- Polimorfismo: el cajero trabaja con distintos tipos de `Transaccion`.

## Diagramas

Los diagramas del proyecto estan en:

```text
diagramas.md
```

Incluye diagrama de clases, casos de uso, actividades con carriles, flujo principal y modelo entidad-relacion conceptual.

## Nota Sobre Archivos Compilados

Los archivos `.class` se generan al compilar. No forman parte del codigo fuente y no deben subirse al repositorio.
