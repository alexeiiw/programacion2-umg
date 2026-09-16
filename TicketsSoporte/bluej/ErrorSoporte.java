/**
 * Clase ErrorSoporte.
 * Registra errores detectados durante la atencion del ticket.
 */
public class ErrorSoporte {
    private String tipo;
    private String descripcion;
    private String impacto;

    public ErrorSoporte() {
        this.tipo = "General";
        this.descripcion = "";
        this.impacto = "Bajo";
    }

    public ErrorSoporte(String tipo, String descripcion, String impacto) {
        this.tipo = tipo;
        this.descripcion = descripcion;
        this.impacto = impacto;
    }

    public String getTipo() {
        return tipo;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public String getImpacto() {
        return impacto;
    }

    public boolean esCritico() {
        return impacto.equalsIgnoreCase("Critico") || impacto.equalsIgnoreCase("Alto");
    }

    public String resumen() {
        return tipo + " - " + descripcion + " (Impacto: " + impacto + ")";
    }
}
