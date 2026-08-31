/**
 * Clase Cliente.
 * Representa a un cliente registrado en la tienda virtual.
 * Hereda de la superclase Persona y gestiona su identificacion legal y tarjeta de credito.
 * 
 * @author Programacion II - UMG Portales
 * @version 1.0
 */
public class Cliente extends Persona {
    private String codigoCliente;
    private Documento documento;
    private String direccionEntrega;
    private TarjetaCredito tarjetaCredito;
    private boolean registrado;

    /**
     * Constructor por defecto.
     */
    public Cliente() {
        super();
        this.codigoCliente = "";
        this.documento = new Documento();
        this.direccionEntrega = "";
        this.tarjetaCredito = null;
        this.registrado = true;
    }

    /**
     * Constructor con datos personales basicos y codigo de cliente.
     * 
     * @param id Identificador personal.
     * @param nombre Nombre.
     * @param apellido Apellido.
     * @param codigoCliente Codigo de cliente en el sistema.
     */
    public Cliente(String id, String nombre, String apellido, String codigoCliente) {
        super(id, nombre, apellido);
        this.codigoCliente = codigoCliente;
        this.documento = new Documento();
        this.direccionEntrega = "";
        this.tarjetaCredito = null;
        this.registrado = true;
    }

    /**
     * Constructor completo.
     * 
     * @param id Identificador personal.
     * @param nombre Nombre.
     * @param apellido Apellido.
     * @param email Correo electronico.
     * @param telefono Telefono de contacto.
     * @param codigoCliente Codigo del cliente.
     * @param documento Documento de identificacion.
     * @param direccionEntrega Direccion fisica de envio.
     */
    public Cliente(String id, String nombre, String apellido, String email, String telefono,
                   String codigoCliente, Documento documento, String direccionEntrega) {
        super(id, nombre, apellido, email, telefono);
        this.codigoCliente = codigoCliente;
        this.documento = documento != null ? documento : new Documento();
        this.direccionEntrega = direccionEntrega;
        this.tarjetaCredito = null;
        this.registrado = true;
    }

    // Getters y Setters
    public String getCodigoCliente() {
        return codigoCliente;
    }

    public void setCodigoCliente(String codigoCliente) {
        this.codigoCliente = codigoCliente;
    }

    public Documento getDocumento() {
        return documento;
    }

    public void setDocumento(Documento documento) {
        this.documento = documento;
    }

    public String getDireccionEntrega() {
        return direccionEntrega;
    }

    public void setDireccionEntrega(String direccionEntrega) {
        this.direccionEntrega = direccionEntrega;
    }

    public TarjetaCredito getTarjetaCredito() {
        return tarjetaCredito;
    }

    public void setTarjetaCredito(TarjetaCredito tarjetaCredito) {
        this.tarjetaCredito = tarjetaCredito;
    }

    public boolean isRegistrado() {
        return registrado;
    }

    public void setRegistrado(boolean registrado) {
        this.registrado = registrado;
    }

    /**
     * Verifica si el cliente cuenta con una tarjeta de credito vinculada y valida.
     * 
     * @return true si tiene tarjeta de credito asociada y activa.
     */
    public boolean tieneTarjetaCreditoValida() {
        return tarjetaCredito != null && tarjetaCredito.validar();
    }

    @Override
    public void mostrarInformacion() {
        super.mostrarInformacion();
        System.out.println("Codigo Cliente: " + codigoCliente);
        if (documento != null) {
            System.out.println("Documento: " + documento.toString());
        }
        System.out.println("Direccion Entrega: " + direccionEntrega);
        if (tarjetaCredito != null) {
            System.out.println("Tarjeta Vinculada: " + tarjetaCredito.toString());
        } else {
            System.out.println("Tarjeta Vinculada: Ninguna");
        }
    }

    @Override
    public String toString() {
        return "Cliente [codigo=" + codigoCliente + ", nombre=" + getNombreCompleto() 
                + ", email=" + getEmail() + ", tieneTC=" + (tarjetaCredito != null) + "]";
    }
}
