/**
 * Superclase Persona.
 * Modela los atributos base y comportamiento general de una persona en el sistema.
 * 
 * @author Programacion II - UMG Portales
 * @version 1.0
 */
public class Persona {
    private String id;
    private String nombre;
    private String apellido;
    private String email;
    private String telefono;

    /**
     * Constructor por defecto.
     */
    public Persona() {
        this.id = "";
        this.nombre = "";
        this.apellido = "";
        this.email = "";
        this.telefono = "";
    }

    /**
     * Constructor con parametros basicos.
     * 
     * @param id Identificador unico de la persona.
     * @param nombre Nombre de la persona.
     * @param apellido Apellido de la persona.
     */
    public Persona(String id, String nombre, String apellido) {
        this.id = id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.email = "";
        this.telefono = "";
    }

    /**
     * Constructor completo.
     * 
     * @param id Identificador unico.
     * @param nombre Nombre de la persona.
     * @param apellido Apellido de la persona.
     * @param email Correo electronico.
     * @param telefono Numero telefonico.
     */
    public Persona(String id, String nombre, String apellido, String email, String telefono) {
        this.id = id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.email = email;
        this.telefono = telefono;
    }

    // Getters y Setters
    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getApellido() {
        return apellido;
    }

    public void setApellido(String apellido) {
        this.apellido = apellido;
    }

    public String getNombreCompleto() {
        return this.nombre + " " + this.apellido;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getTelefono() {
        return telefono;
    }

    public void setTelefono(String telefono) {
        this.telefono = telefono;
    }

    /**
     * Muestra la informacion general de la persona.
     */
    public void mostrarInformacion() {
        System.out.println("ID: " + id);
        System.out.println("Nombre: " + getNombreCompleto());
        System.out.println("Email: " + email);
        System.out.println("Telefono: " + telefono);
    }

    @Override
    public String toString() {
        return "Persona [id=" + id + ", nombreCompleto=" + getNombreCompleto() + ", email=" + email + "]";
    }
}
