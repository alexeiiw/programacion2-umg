public class Simulador {
    public static void main(String[] args) {
        Persona personaAprobada = new Estudiante("E001", "Ana Lopez", "Ingenieria");
        Persona personaReprobada = new Estudiante("E002", "Luis Perez", "Ingenieria");

        RegistroNotas aprobado = new RegistroNotas((Estudiante) personaAprobada, "Programacion II");
        RegistroNotas reprobado = new RegistroNotas((Estudiante) personaReprobada, "Programacion II");

        try {
            aprobado.ingresarNotas(80, 70, 90);
            reprobado.ingresarNotas(40, 50, 45);
            aprobado.mostrarResultado();
            reprobado.mostrarResultado();
        } catch (IllegalArgumentException error) {
            System.out.println(error.getMessage());
        }

        Persona[] personas = { personaAprobada, personaReprobada };
        for (Persona persona : personas) {
            persona.mostrarInformacion();
        }
    }
}
