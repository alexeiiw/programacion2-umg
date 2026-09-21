public class Estudiante extends Persona {
    private String carrera;

    public Estudiante(String codigo, String nombre, String carrera) {
        super(codigo, nombre);
        this.carrera = carrera;
    }

    @Override
    public void mostrarInformacion() {
        System.out.println(codigo + " - " + nombre + " - " + carrera);
    }
}
