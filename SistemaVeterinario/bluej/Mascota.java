public class Mascota extends SerVivo {
    private String especie;
    private String propietario;

    public Mascota(String codigo, String nombre, String especie, String propietario) {
        super(codigo, nombre);
        this.especie = especie;
        this.propietario = propietario;
    }

    @Override
    public void mostrarInformacion() {
        System.out.println(codigo + " - " + nombre + " - " + especie + " - Propietario: " + propietario);
    }
}
