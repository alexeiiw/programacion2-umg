/**
 * Clase base Usuario.
 * Modela datos comunes para usuarios del sistema de tickets.
 *
 * @author Programacion II - UMG Portales
 * @version 1.0
 */
public class Usuario {
    private String codigo;
    private String nombre;
    private String correo;
    private boolean activo;

    public Usuario() {
        this.codigo = "";
        this.nombre = "";
        this.correo = "";
        this.activo = true;
    }

    public Usuario(String codigo, String nombre, String correo) {
        this.codigo = codigo;
        this.nombre = nombre;
        this.correo = correo;
        this.activo = true;
    }

    public String getCodigo() {
        return codigo;
    }

    public void setCodigo(String codigo) {
        this.codigo = codigo;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getCorreo() {
        return correo;
    }

    public void setCorreo(String correo) {
        this.correo = correo;
    }

    public boolean isActivo() {
        return activo;
    }

    public void setActivo(boolean activo) {
        this.activo = activo;
    }

    public String obtenerRol() {
        return "Usuario";
    }

    public void mostrarInformacion() {
        System.out.println("Codigo: " + codigo);
        System.out.println("Nombre: " + nombre);
        System.out.println("Correo: " + correo);
        System.out.println("Rol: " + obtenerRol());
        System.out.println("Activo: " + activo);
    }
}
