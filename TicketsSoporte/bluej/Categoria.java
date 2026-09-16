/**
 * Clase Categoria.
 * Define el tipo de problema reportado en un ticket.
 */
public class Categoria {
    private int id;
    private String nombre;
    private String descripcion;

    public Categoria() {
        this.id = 0;
        this.nombre = "General";
        this.descripcion = "Soporte general";
    }

    public Categoria(int id, String nombre, String descripcion) {
        this.id = id;
        this.nombre = nombre;
        this.descripcion = descripcion;
    }

    public int getId() {
        return id;
    }

    public String getNombre() {
        return nombre;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public void mostrarCategoria() {
        System.out.println(id + " - " + nombre + ": " + descripcion);
    }
}
