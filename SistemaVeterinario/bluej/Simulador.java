public class Simulador {
    public static void main(String[] args) {
        SerVivo mascotaEstable = new Mascota("M001", "Luna", "Perro", "Carlos Mendez");
        SerVivo mascotaObservacion = new Mascota("M002", "Milo", "Gato", "Andrea Lopez");

        ConsultaVeterinaria consultaEstable = new ConsultaVeterinaria((Mascota) mascotaEstable, "Consulta General");
        ConsultaVeterinaria consultaObservacion = new ConsultaVeterinaria((Mascota) mascotaObservacion, "Emergencia");

        try {
            consultaEstable.registrarDatos(18.5, 38.4, 125.00);
            consultaObservacion.registrarDatos(6.2, 40.1, 275.00);
        } catch (IllegalArgumentException error) {
            System.out.println(error.getMessage());
        }

        mascotaEstable.mostrarInformacion();
        consultaEstable.mostrarResultado();
        mascotaObservacion.mostrarInformacion();
        consultaObservacion.mostrarResultado();
    }
}
