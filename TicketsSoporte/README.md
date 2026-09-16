# Tickets Soporte

Modulo practico para el curso de Programacion 2.

El proyecto modela la automatizacion de la asignacion, resolucion y control de errores en tickets de soporte tecnico corporativo.

## Objetivo

Aplicar programacion orientada a objetos para resolver un caso corporativo realista: recepcion de tickets, asignacion automatica a tecnicos, registro de errores, resolucion, cierre y control por estado/prioridad.

## Estructura

```text
TicketsSoporte/
├── casos-de-uso/        -> Diagrama UML en Draw.io y documentacion de casos de uso
├── diagramas-de-flujo/  -> Diagramas de flujo del ciclo del ticket y reglas de negocio
├── bluej/               -> Banco de objetos Java para BlueJ
└── TicketsSoporte/      -> Proyecto de consola C# .NET 6 para clase
```

## Reglas De Negocio

1. **Asignacion automatica**: El ticket se asigna segun categoria, prioridad y disponibilidad del tecnico.
2. **Control de errores**: Todo error detectado debe registrarse en la bitacora del ticket.
3. **Resolucion controlada**: Solo se puede resolver un ticket asignado a un tecnico activo.
4. **Escalamiento**: Los tickets criticos o vencidos se escalan para atencion prioritaria.
5. **Resumen de control**: El sistema permite consultar tickets por tecnico, estado y prioridad.

## Actores Principales

- `Solicitante`: empleado que reporta un problema.
- `Tecnico`: colaborador de soporte que atiende tickets.
- `Coordinador de Soporte`: supervisa asignaciones, errores y escalamiento.
- `Sistema de Tickets`: automatiza reglas de asignacion y control.

## Como Probar El Proyecto C#

El proyecto C# incluye:

- Clase abstracta `Usuario`.
- Herencia con `Tecnico` y `Solicitante`.
- Polimorfismo al mostrar usuarios desde una lista de `Usuario`.
- Manejo de errores con `try/catch` en el menu principal.

```powershell
cd TicketsSoporte\TicketsSoporte
dotnet build
dotnet run
```

## Como Probar El Proyecto BlueJ

1. Abrir BlueJ.
2. Ir a `Project` -> `Open Project...`.
3. Elegir la carpeta `TicketsSoporte\bluej`.
4. Presionar `Compile`.
5. Instanciar objetos o ejecutar metodos desde el banco de objetos.
