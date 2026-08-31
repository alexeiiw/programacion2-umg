/**
 * Clase Usuario.
 * Modela las credenciales de acceso y estado de cuenta de un usuario en la plataforma.
 * Hereda de Persona.
 * 
 * @author Programacion II - UMG Portales
 * @version 1.0
 */
public class Usuario extends Persona {
    private String username;
    private String password;
    private boolean activo;
    private boolean sesionIniciada;

    /**
     * Constructor por defecto.
     */
    public Usuario() {
        super();
        this.username = "";
        this.password = "";
        this.activo = true;
        this.sesionIniciada = false;
    }

    /**
     * Constructor con datos personales y credenciales.
     * 
     * @param id Identificador unico.
     * @param nombre Nombre de la persona.
     * @param apellido Apellido de la persona.
     * @param username Nombre de usuario.
     * @param password Clave de acceso.
     */
    public Usuario(String id, String nombre, String apellido, String username, String password) {
        super(id, nombre, apellido);
        this.username = username;
        this.password = password;
        this.activo = true;
        this.sesionIniciada = false;
    }

    /**
     * Constructor completo.
     * 
     * @param id Identificador unico.
     * @param nombre Nombre.
     * @param apellido Apellido.
     * @param email Correo.
     * @param telefono Telefono.
     * @param username Nombre de usuario.
     * @param password Clave.
     */
    public Usuario(String id, String nombre, String apellido, String email, String telefono, String username, String password) {
        super(id, nombre, apellido, email, telefono);
        this.username = username;
        this.password = password;
        this.activo = true;
        this.sesionIniciada = false;
    }

    // Getters y Setters
    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public boolean isActivo() {
        return activo;
    }

    public void setActivo(boolean activo) {
        this.activo = activo;
    }

    public boolean isSesionIniciada() {
        return sesionIniciada;
    }

    /**
     * Autentica el usuario contra las credenciales proporcionadas.
     * 
     * @param user Usuario ingresado.
     * @param pass Password ingresado.
     * @return true si la autenticacion es exitosa, false de lo contrario.
     */
    public boolean autenticar(String user, String pass) {
        if (!activo) {
            System.out.println("Error: El usuario esta desactivado.");
            this.sesionIniciada = false;
            return false;
        }

        if (this.username.equals(user) && this.password.equals(pass)) {
            this.sesionIniciada = true;
            return true;
        }

        this.sesionIniciada = false;
        return false;
    }

    /**
     * Cierra la sesion actual del usuario.
     */
    public void cerrarSesion() {
        this.sesionIniciada = false;
    }

    /**
     * Permite cambiar la contrasena previa confirmacion de la clave anterior.
     * 
     * @param oldPass Clave actual.
     * @param newPass Nueva clave.
     * @return true si se actualizo correctamente.
     */
    public boolean cambiarPassword(String oldPass, String newPass) {
        if (this.password.equals(oldPass) && newPass != null && !newPass.isEmpty()) {
            this.password = newPass;
            return true;
        }
        return false;
    }

    @Override
    public String toString() {
        return "Usuario [username=" + username + ", nombre=" + getNombreCompleto() + ", activo=" + activo + "]";
    }
}
