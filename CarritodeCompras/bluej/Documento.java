/**
 * Clase Documento.
 * Representa un documento de identificacion legal (DPI, NIT, Pasaporte, etc.).
 * 
 * @author Programacion II - UMG Portales
 * @version 1.0
 */
public class Documento {
    private String tipo;      // DPI, NIT, PASAPORTE, OTRO
    private String numero;
    private String paisEmision;

    /**
     * Constructor por defecto.
     */
    public Documento() {
        this.tipo = "DPI";
        this.numero = "";
        this.paisEmision = "Guatemala";
    }

    /**
     * Constructor con tipo y numero.
     * 
     * @param tipo Tipo de documento (DPI, NIT, PASAPORTE).
     * @param numero Numero identificador del documento.
     */
    public Documento(String tipo, String numero) {
        this.tipo = tipo != null ? tipo.toUpperCase() : "DPI";
        this.numero = numero != null ? numero.trim() : "";
        this.paisEmision = "Guatemala";
    }

    /**
     * Constructor completo.
     * 
     * @param tipo Tipo de documento.
     * @param numero Numero del documento.
     * @param paisEmision Pais de emision.
     */
    public Documento(String tipo, String numero, String paisEmision) {
        this.tipo = tipo != null ? tipo.toUpperCase() : "DPI";
        this.numero = numero != null ? numero.trim() : "";
        this.paisEmision = paisEmision != null ? paisEmision : "Guatemala";
    }

    // Getters y Setters
    public String getTipo() {
        return tipo;
    }

    public void setTipo(String tipo) {
        this.tipo = tipo != null ? tipo.toUpperCase() : "DPI";
    }

    public String getNumero() {
        return numero;
    }

    public void setNumero(String numero) {
        this.numero = numero != null ? numero.trim() : "";
    }

    public String getPaisEmision() {
        return paisEmision;
    }

    public void setPaisEmision(String paisEmision) {
        this.paisEmision = paisEmision;
    }

    /**
     * Valida que el documento contenga datos minimos validos.
     * 
     * @return true si tiene datos no vacios, false en caso contrario.
     */
    public boolean esValido() {
        return tipo != null && !tipo.isEmpty() && numero != null && !numero.isEmpty();
    }

    @Override
    public String toString() {
        return tipo + ": " + numero + " (" + paisEmision + ")";
    }
}
