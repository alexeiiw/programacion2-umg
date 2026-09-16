import java.util.ArrayList;

/**
 * Clase FlujoTicket.
 * Controla los estados validos del ciclo de vida de un ticket.
 */
public class FlujoTicket {
    private ArrayList<String> estados;

    public FlujoTicket() {
        estados = new ArrayList<String>();
        estados.add("Abierto");
        estados.add("Asignado");
        estados.add("Resuelto");
        estados.add("Cerrado");
    }

    public boolean puedeCambiarEstado(String estadoActual, String estadoNuevo) {
        if (estadoActual.equals("Abierto") && estadoNuevo.equals("Asignado")) return true;
        if (estadoActual.equals("Asignado") && estadoNuevo.equals("Resuelto")) return true;
        if (estadoActual.equals("Resuelto") && estadoNuevo.equals("Cerrado")) return true;

        return false;
    }

    public void mostrarFlujo() {
        System.out.println("--- Flujo del Ticket ---");
        System.out.println("Abierto  -> ticket creado por el solicitante");
        System.out.println("Asignado -> ticket asignado a un tecnico");
        System.out.println("Resuelto -> tecnico registra la solucion");
        System.out.println("Cerrado  -> se confirma el cierre del ticket");
    }
}
