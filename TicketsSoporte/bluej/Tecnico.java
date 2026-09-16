/**
 * Clase Tecnico.
 * Hereda de Usuario y atiende tickets asignados.
 */
public class Tecnico extends Usuario {
    private String especialidad;
    private int cargaActual;
    private int capacidadMaxima;

    public Tecnico() {
        super();
        this.especialidad = "General";
        this.cargaActual = 0;
        this.capacidadMaxima = 3;
    }

    public Tecnico(String codigo, String nombre, String correo, String especialidad, int capacidadMaxima) {
        super(codigo, nombre, correo);
        this.especialidad = especialidad;
        this.cargaActual = 0;
        this.capacidadMaxima = capacidadMaxima;
    }

    public String getEspecialidad() {
        return especialidad;
    }

    public void setEspecialidad(String especialidad) {
        this.especialidad = especialidad;
    }

    public int getCargaActual() {
        return cargaActual;
    }

    public int getCapacidadMaxima() {
        return capacidadMaxima;
    }

    public boolean estaDisponible() {
        return isActivo() && cargaActual < capacidadMaxima;
    }

    public boolean puedeAtender(String categoria) {
        return estaDisponible() && (especialidad.equalsIgnoreCase(categoria) || especialidad.equalsIgnoreCase("General"));
    }

    public void aumentarCarga() {
        if (cargaActual < capacidadMaxima) {
            cargaActual++;
        }
    }

    public void liberarCarga() {
        if (cargaActual > 0) {
            cargaActual--;
        }
    }

    @Override
    public String obtenerRol() {
        return "Tecnico";
    }
}
