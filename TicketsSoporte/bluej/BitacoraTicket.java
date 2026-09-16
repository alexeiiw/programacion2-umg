import java.util.ArrayList;

/**
 * Clase BitacoraTicket.
 * Guarda eventos del ciclo de vida del ticket.
 */
public class BitacoraTicket {
    private ArrayList<String> eventos;

    public BitacoraTicket() {
        eventos = new ArrayList<String>();
    }

    public void registrarEvento(String evento) {
        eventos.add(evento);
    }

    public void mostrarEventos() {
        System.out.println("--- Bitacora del Ticket ---");
        for (String evento : eventos) {
            System.out.println("- " + evento);
        }
    }
}
