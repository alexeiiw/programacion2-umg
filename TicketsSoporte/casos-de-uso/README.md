# Especificacion De Casos De Uso - Tickets De Soporte

Este modulo contiene los casos de uso del sistema de automatizacion de tickets de soporte tecnico corporativo.

Archivo editable:

`casos-de-uso.drawio`

## Actores

| Actor | Tipo | Descripcion |
|---|---|---|
| Solicitante | Primario | Usuario corporativo que reporta un problema tecnico. |
| Tecnico | Primario | Responsable de atender, documentar y resolver tickets asignados. |
| Coordinador de Soporte | Primario | Supervisa carga de trabajo, escalamiento y control de errores. |
| Sistema de Tickets | Secundario | Automatiza asignacion, bitacora, validaciones y resumen de control. |

## Matriz De Casos De Uso

| Codigo | Caso de uso | Actor principal | Descripcion |
|---|---|---|---|
| CU-01 | Registrar solicitante | Coordinador | Crea o actualiza los datos del empleado solicitante. |
| CU-02 | Crear ticket | Solicitante | Registra problema, categoria, prioridad y descripcion. |
| CU-03 | Consultar tickets | Solicitante / Tecnico / Coordinador | Lista tickets por estado, prioridad o responsable. |
| CU-04 | Asignar ticket a tecnico | Sistema / Coordinador | Selecciona tecnico activo con disponibilidad y categoria compatible. |
| CU-05 | Cambiar prioridad | Coordinador | Modifica prioridad cuando cambia el impacto del problema. |
| CU-06 | Registrar error detectado | Tecnico | Documenta causa, mensaje, impacto y accion aplicada. |
| CU-07 | Resolver ticket | Tecnico | Marca el ticket como resuelto y registra solucion. |
| CU-08 | Cerrar ticket | Solicitante / Coordinador | Confirma cierre despues de la resolucion. |
| CU-09 | Escalar ticket critico o vencido | Sistema / Coordinador | Eleva prioridad o notifica supervision por riesgo operativo. |
| CU-10 | Generar resumen de control | Coordinador | Resume tickets por tecnico, estado y prioridad. |

## Relaciones Principales

- `Crear ticket` incluye `Asignar ticket a tecnico`.
- `Resolver ticket` incluye `Registrar error detectado` cuando se identifica causa tecnica.
- `Asignar ticket a tecnico` incluye validar categoria, prioridad y disponibilidad.
- `Escalar ticket critico o vencido` extiende `Consultar tickets` y `Resolver ticket`.
- `Cerrar ticket` requiere ticket resuelto.
