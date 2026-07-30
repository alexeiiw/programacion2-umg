public class Simulador {
    public static void main(String[] args) {
        Perro perro = new Perro("Firulais", "Labrador");

        System.out.println("Hola Mundo BlueJ");
        System.out.println("Nombre del perro: " + perro.getNombre());
        System.out.println("Raza: " + perro.getRaza());
        System.out.println("Sonido: " + perro.hacerSonido());
    }
}
