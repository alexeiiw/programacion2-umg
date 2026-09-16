/**
 * Clase Solicitante.
 * Hereda de Usuario y representa al empleado que reporta problemas.
 */
public class Solicitante extends Usuario {
    private String departamento;
    private String extension;

    public Solicitante() {
        super();
        this.departamento = "";
        this.extension = "";
    }

    public Solicitante(String codigo, String nombre, String correo, String departamento, String extension) {
        super(codigo, nombre, correo);
        this.departamento = departamento;
        this.extension = extension;
    }

    public String getDepartamento() {
        return departamento;
    }

    public void setDepartamento(String departamento) {
        this.departamento = departamento;
    }

    public String getExtension() {
        return extension;
    }

    public void setExtension(String extension) {
        this.extension = extension;
    }

    @Override
    public String obtenerRol() {
        return "Solicitante";
    }
}
