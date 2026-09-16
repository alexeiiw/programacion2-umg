# Programacion 2 UMG Portales

Ultima actualizacion: 31 de agosto de 2026

Bienvenidos al repositorio de apoyo para el curso de Programacion 2 de la Universidad Mariano Galvez, sede Portales.

El objetivo principal de este repositorio es practicar programacion orientada a objetos en Java usando BlueJ y la transicion estructurada a C# (.NET).

## Alcance Del Repositorio

Este repositorio contiene el material que se comparte con los alumnos: ejemplos, proyectos base, tareas resueltas y documentacion tecnica necesaria para las practicas del curso.

El material interno de preparacion docente, instaladores, archivos compilados y documentacion privada no forma parte del repositorio publico.

## Objetivos Del Repositorio

- Comprender clases, objetos, atributos y metodos.
- Aplicar encapsulamiento, abstraccion, herencia y polimorfismo.
- Organizar ejercicios y proyectos por carpetas independientes.
- Modelar soluciones mediante diagramas de Casos de Uso y Flujo (Draw.io).
- Transicionar de modelos POO en BlueJ a implementaciones en C# (.NET).
- Compartir ejemplos base para que cada estudiante pueda analizarlos, ejecutarlos y mejorarlos.

## Estructura

Cada proyecto vive en su propia carpeta independiente:

```text
CajeroAutomatico/      -> Simulador ATM con pago de servicios (POO en Java)
CarritodeCompras/      -> Modulo integral E-Commerce (Casos de uso, flujo, BlueJ y C#)
HolaMundoBlueJ/        -> Proyecto introductorio de POO y herencia (Animal -> Perro)
TicketsSoporte/        -> Automatizacion de tickets de soporte (BlueJ y C#)
TareasResueltas/       -> Soluciones de tareas y ejercicios de apoyo
```

---

## Modulo CarritodeCompras (Semanas 7 y 8)

Modulo metodologico completo que cubre el ciclo de vida del software desde los diagramas hasta la ejecucion en codigo:

```text
CarritodeCompras/
├── casos-de-uso/        -> Diagrama UML en Draw.io y documentacion de casos de uso
├── diagramas-de-flujo/  -> Diagrama de flujo detallado en Draw.io con reglas de negocio
├── bluej/               -> Banco de objetos base en Java para BlueJ (Persona, Usuario, Cliente, Documento, TarjetaCredito, Producto, CarritoCompras)
└── CarritodeCompras/    -> Proyecto de consola en C# (.NET 6) con logica transaccional
```

### Reglas de Negocio del Sistema:
1. **Autenticacion**: Solo clientes registrados y autenticados pueden comprar.
2. **Inventario**: Solo se pueden comprar articulos con existencias en stock (`Stock >= Cantidad`).
3. **Metodo de Pago**: Exclusividad de cobro con **Tarjeta de Credito**.

### Como probar el proyecto en C#:
```powershell
cd CarritodeCompras\CarritodeCompras
dotnet build
dotnet run
```

---

## Modulo TicketsSoporte

Modulo practico para automatizar asignacion, resolucion y control de errores en tickets de soporte tecnico corporativo.

Incluye casos de uso, diagramas de flujo, clases Java para BlueJ y proyecto de consola C# .NET 6 con clase abstracta, herencia, polimorfismo y manejo de errores con try/catch.

Para probar el proyecto en C#:

```powershell
cd TicketsSoporte\TicketsSoporte
dotnet build
dotnet run
```

---

## Proyecto CajeroAutomatico

Este proyecto simula operaciones de un cajero automatico:

- Autenticacion con tarjeta y PIN.
- Consulta de saldo.
- Retiro.
- Deposito.
- Transferencia.
- Pago de servicios (Facturas de Telefono y Energia Electrica con pago parcial/total).
- Generacion de recibos.

Para compilar y probar:
```powershell
cd CajeroAutomatico
javac *.java
java Simulador
```

---

## Proyecto HolaMundoBlueJ

Proyecto inicial para abrir, compilar y ejecutar en BlueJ:
- `Animal`: clase base.
- `Perro`: clase hija que hereda de `Animal`.
- `Simulador`: crea un objeto `Perro` y muestra datos en consola.

Para probarlo desde PowerShell:
```powershell
cd HolaMundoBlueJ
javac *.java
java Simulador
```

---

## Carpeta TareasResueltas

Codigo de tareas resueltas y ejemplos adicionales:
- `TareasResueltas/Semana2/ConversorMaya.cpp` y `.cs`
- `TareasResueltas/PreTest/CajeroAutomatico/Simulador.java`
- `TareasResueltas/PreTest/ConversorBase20.cpp` y `.cs`

---

## Como Usar BlueJ

1. Abrir BlueJ.
2. Ir a `Project` -> `Open Project...`.
3. Elegir la carpeta del proyecto (por ejemplo `CajeroAutomatico` o `CarritodeCompras\bluej`).
4. Presionar `Compile` para compilar las clases.
5. Hacer clic derecho sobre la clase con `main` o instanciar objetos directamente en el banco de objetos.

## Como Usar el Repositorio con Git

```powershell
git clone https://github.com/alexeiiw/programacion2-umg.git
cd programacion2-umg
git status
git pull
```
