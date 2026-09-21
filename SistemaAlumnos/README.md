# SistemaAlumnos

Solución de referencia del Pre Test 2 de Programación 2: sistema de ingreso y consulta de notas de estudiantes.

## Estructura

```text
SistemaAlumnos/
├── casos-de-uso/       -> Caso de uso del sistema
├── diagramas-de-flujo/ -> Flujo de validación y cálculo de notas
├── bluej/              -> Solución Java para BlueJ
└── SistemaAlumnos/     -> Solución C# de consola
```

## Reglas de negocio

- Cada estudiante registra actividad, parcial y proyecto.
- Las notas válidas están entre 0 y 100.
- El promedio es `(actividad + parcial + proyecto) / 3`.
- Un promedio mayor o igual a 60 produce estado `Aprobado`.
- Un promedio menor que 60 produce estado `Reprobado`.
- Los errores de validación se controlan con `try-catch`.

## POO aplicada

- `Persona` es una clase abstracta.
- `Estudiante` hereda de `Persona`.
- `RegistroNotas` relaciona un estudiante con un curso y sus notas.
- `Persona persona = new Estudiante(...)` demuestra polimorfismo.
- Los atributos se protegen mediante encapsulamiento.

## Probar C#

```powershell
cd SistemaAlumnos\SistemaAlumnos
dotnet build
dotnet run
```

## Probar Java en BlueJ

1. Abrir BlueJ.
2. Seleccionar `Project` -> `Open Project...`.
3. Elegir `SistemaAlumnos\bluej`.
4. Presionar `Compile`.
5. Ejecutar `Simulador.main()`.
