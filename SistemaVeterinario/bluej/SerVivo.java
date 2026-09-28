public abstract class SerVivo {
    protected String codigo;
    protected String nombre;

    public SerVivo(String codigo, String nombre) {
        this.codigo = codigo;
        this.nombre = nombre;
    }

    public abstract void mostrarInformacion();
}
