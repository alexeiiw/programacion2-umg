public class Animal {
    private final String nombre;

    public Animal(String nombre) {
        this.nombre = nombre;
    }

    public String getNombre() {
        return nombre;
    }

    public String hacerSonido() {
        return "El animal hace un sonido";
    }
}
