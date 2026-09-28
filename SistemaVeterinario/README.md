# SistemaVeterinario

Solucion de referencia del Segundo Parcial de Programacion 2: sistema de registro y consulta de consultas veterinarias.

## Estructura

```text
SistemaVeterinario/
├── casos-de-uso/       -> Caso de uso del sistema
├── diagramas-de-flujo/ -> Flujo de validacion y estado clinico
├── bluej/              -> Solucion Java para BlueJ
└── SistemaVeterinario/ -> Solucion C# de consola
```

## Reglas de negocio

- Cada consulta registra peso, temperatura y costo del servicio.
- El peso debe ser mayor que 0.
- La temperatura debe estar entre 35 y 43 grados.
- El costo debe ser mayor que 0.
- Una temperatura mayor o igual a 39.5 produce el estado `Requiere observacion`.
- Una temperatura menor que 39.5 produce el estado `Estable`.
- Los errores de validacion se controlan con `try-catch`.

## POO aplicada

- `SerVivo` es una clase abstracta.
- `Mascota` hereda de `SerVivo`.
- `ConsultaVeterinaria` relaciona una mascota con el servicio y sus datos clinicos.
- `SerVivo serVivo = new Mascota(...)` demuestra polimorfismo.
- Los atributos se protegen mediante encapsulamiento.

## Probar C#

```powershell
cd SistemaVeterinario\SistemaVeterinario
dotnet build
dotnet run
```

## Probar Java en BlueJ

1. Abrir BlueJ.
2. Seleccionar `Project` -> `Open Project...`.
3. Elegir `SistemaVeterinario\bluej`.
4. Presionar `Compile`.
5. Ejecutar `Simulador.main()`.
