import java.util.ArrayList;

/**
 * Clase GestorTickets.
 * Administra tecnicos, tickets y reglas de asignacion.
 */
public class GestorTickets {
    private ArrayList<Tecnico> tecnicos;
    private ArrayList<Ticket> tickets;

    public GestorTickets() {
        tecnicos = new ArrayList<Tecnico>();
        tickets = new ArrayList<Ticket>();
    }

    public void agregarTecnico(Tecnico tecnico) {
        tecnicos.add(tecnico);
    }

    public void crearTicket(Ticket ticket) {
        tickets.add(ticket);
        asignarAutomaticamente(ticket);
    }

    public boolean asignarAutomaticamente(Ticket ticket) {
        Tecnico mejorTecnico = null;

        for (Tecnico tecnico : tecnicos) {
            if (tecnico.puedeAtender(ticket.getCategoria().getNombre())) {
                if (mejorTecnico == null || tecnico.getCargaActual() < mejorTecnico.getCargaActual()) {
                    mejorTecnico = tecnico;
                }
            }
        }

        if (mejorTecnico != null) {
            ticket.asignarTecnico(mejorTecnico);
            return true;
        }

        ticket.escalar("No hay tecnicos disponibles");
        return false;
    }

    public void mostrarTickets() {
        for (Ticket ticket : tickets) {
            ticket.mostrarResumen();
            System.out.println();
        }
    }
}
