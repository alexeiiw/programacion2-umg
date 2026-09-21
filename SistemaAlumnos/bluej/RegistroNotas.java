public class RegistroNotas {
    private Estudiante estudiante;
    private String curso;
    private double actividad;
    private double parcial;
    private double proyecto;

    public RegistroNotas(Estudiante estudiante, String curso) {
        this.estudiante = estudiante;
        this.curso = curso;
    }

    public void ingresarNotas(double actividad, double parcial, double proyecto) {
        validarNota(actividad);
        validarNota(parcial);
        validarNota(proyecto);
        this.actividad = actividad;
        this.parcial = parcial;
        this.proyecto = proyecto;
    }

    public double calcularPromedio() {
        return (actividad + parcial + proyecto) / 3;
    }

    public String obtenerEstado() {
        return calcularPromedio() >= 60 ? "Aprobado" : "Reprobado";
    }

    public void mostrarResultado() {
        System.out.println(estudiante.nombre + " | " + curso + " | Promedio: "
                + String.format("%.2f", calcularPromedio()) + " | Estado: " + obtenerEstado());
    }

    private void validarNota(double nota) {
        if (nota < 0 || nota > 100) {
            throw new IllegalArgumentException("Las notas deben estar entre 0 y 100.");
        }
    }
}
