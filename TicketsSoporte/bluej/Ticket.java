import java.util.ArrayList;

/**
 * Clase Ticket.
 * Modela el incidente reportado por un solicitante.
 */
public class Ticket {
    private int numero;
    private String titulo;
    private String descripcion;
    private Categoria categoria;
    private Prioridad prioridad;
    private Solicitante solicitante;
    private Tecnico tecnicoAsignado;
    private String estado;
    private boolean escalado;
    private ArrayList<ErrorSoporte> errores;
    private BitacoraTicket bitacora;

    public Ticket(int numero, String titulo, String descripcion, Categoria categoria, Prioridad prioridad, Solicitante solicitante) {
        this.numero = numero;
        this.titulo = titulo;
        this.descripcion = descripcion;
        this.categoria = categoria;
        this.prioridad = prioridad;
        this.solicitante = solicitante;
        this.estado = "Abierto";
        this.escalado = false;
        this.errores = new ArrayList<ErrorSoporte>();
        this.bitacora = new BitacoraTicket();
        this.bitacora.registrarEvento("Ticket creado: " + titulo);
    }

    public int getNumero() {
        return numero;
    }

    public Categoria getCategoria() {
        return categoria;
    }

    public Prioridad getPrioridad() {
        return prioridad;
    }

    public Tecnico getTecnicoAsignado() {
        return tecnicoAsignado;
    }

    public String getEstado() {
        return estado;
    }

    public void asignarTecnico(Tecnico tecnico) {
        if (tecnico != null && tecnico.puedeAtender(categoria.getNombre())) {
            this.tecnicoAsignado = tecnico;
            this.estado = "Asignado";
            tecnico.aumentarCarga();
            bitacora.registrarEvento("Asignado a tecnico: " + tecnico.getNombre());
        }
    }

    public void registrarError(ErrorSoporte error) {
        errores.add(error);
        bitacora.registrarEvento("Error registrado: " + error.resumen());
        if (error.esCritico()) {
            escalar("Error de alto impacto");
        }
    }

    public void resolver(String solucion) {
        if (tecnicoAsignado != null && estado.equals("Asignado")) {
            estado = "Resuelto";
            bitacora.registrarEvento("Solucion registrada: " + solucion);
        }
    }

    public void cerrar() {
        if (estado.equals("Resuelto")) {
            estado = "Cerrado";
            if (tecnicoAsignado != null) {
                tecnicoAsignado.liberarCarga();
            }
            bitacora.registrarEvento("Ticket cerrado");
        }
    }

    public void escalar(String motivo) {
        escalado = true;
        bitacora.registrarEvento("Ticket escalado: " + motivo);
    }

    public void mostrarResumen() {
        System.out.println("Ticket #" + numero + " - " + titulo);
        System.out.println("Estado: " + estado + " | Prioridad: " + prioridad.getNombre() + " | Categoria: " + categoria.getNombre());
        System.out.println("Solicitante: " + solicitante.getNombre());
        System.out.println("Tecnico: " + (tecnicoAsignado != null ? tecnicoAsignado.getNombre() : "Sin asignar"));
        System.out.println("Escalado: " + escalado);
    }

    public void mostrarBitacora() {
        bitacora.mostrarEventos();
    }
}
