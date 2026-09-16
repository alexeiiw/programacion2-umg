/**
 * Clase Prioridad.
 * Modela el nivel de urgencia del ticket.
 */
public class Prioridad {
    private String nombre;
    private int nivel;
    private int horasMaximas;

    public Prioridad() {
        this.nombre = "Media";
        this.nivel = 2;
        this.horasMaximas = 24;
    }

    public Prioridad(String nombre, int nivel, int horasMaximas) {
        this.nombre = nombre;
        this.nivel = nivel;
        this.horasMaximas = horasMaximas;
    }

    public String getNombre() {
        return nombre;
    }

    public int getNivel() {
        return nivel;
    }

    public int getHorasMaximas() {
        return horasMaximas;
    }

    public boolean esCritica() {
        return nivel >= 4 || nombre.equalsIgnoreCase("Critica");
    }
}
