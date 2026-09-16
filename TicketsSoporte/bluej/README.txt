------------------------------------------------------------------------
PROYECTO: Tickets de Soporte Tecnico Corporativo
CURSO: Programacion II - UMG Portales
VERSION: 1.0
------------------------------------------------------------------------

DESCRIPCION:
Banco de objetos para modelar la automatizacion de asignacion, resolucion
y control de errores en tickets de soporte tecnico.

CLASES:
1. Usuario: clase base para usuarios del sistema.
2. Tecnico: hereda de Usuario y atiende tickets.
3. Solicitante: hereda de Usuario y reporta problemas.
4. Categoria: tipo de problema reportado.
5. Prioridad: urgencia y tiempo maximo esperado.
6. ErrorSoporte: error detectado durante la atencion.
7. BitacoraTicket: historial de eventos del ticket.
8. Ticket: incidente con solicitante, tecnico, estado y bitacora.
9. FlujoTicket: controla estados validos del ticket.
10. GestorTickets: administra tickets, tecnicos y asignacion automatica.
------------------------------------------------------------------------
