public abstract class Persona {
    protected String codigo;
    protected String nombre;

    public Persona(String codigo, String nombre) {
        this.codigo = codigo;
        this.nombre = nombre;
    }

    public abstract void mostrarInformacion();
}
