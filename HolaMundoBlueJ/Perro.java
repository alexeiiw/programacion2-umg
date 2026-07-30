public class Perro extends Animal {
    private final String raza;

    public Perro(String nombre, String raza) {
        super(nombre);
        this.raza = raza;
    }

    public String getRaza() {
        return raza;
    }

    @Override
    public String hacerSonido() {
        return "Guau guau";
    }
}
