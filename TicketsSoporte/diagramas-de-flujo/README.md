# Diagramas De Flujo - Tickets De Soporte

Este modulo contiene los diagramas de flujo del sistema de automatizacion de tickets de soporte tecnico corporativo.

Archivo editable:

`diagramas-de-flujo.drawio`

## Diagramas Incluidos

1. Flujo general del ciclo de vida del ticket.
2. Flujo de creacion de ticket.
3. Flujo de asignacion automatica.
4. Flujo de resolucion.
5. Flujo de control de errores.
6. Flujo de escalamiento.
7. Flujo de cierre.

## Reglas Aplicadas

1. Asignar ticket segun categoria, prioridad y disponibilidad.
2. Registrar errores y cambios de estado en bitacora.
3. Resolver solo tickets asignados.
4. Escalar tickets criticos o vencidos.
5. Generar resumen por tecnico, estado y prioridad.

## Resumen Del Flujo General

```text
[INICIO]
  -> Crear ticket
  -> Validar datos minimos
  -> Asignar tecnico disponible
  -> Registrar bitacora
  -> Atender ticket
  -> Registrar errores si existen
  -> Resolver ticket
  -> Cerrar ticket
  -> Generar resumen
[FIN]
```
