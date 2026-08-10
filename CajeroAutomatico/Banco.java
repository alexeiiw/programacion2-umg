import java.util.HashMap;
import java.util.Map;

public class Banco {
    private final String nombre;
    private final Map<String, Tarjeta> tarjetas;
    private final Map<String, CuentaBancaria> cuentas;
    private final Map<String, Factura> facturas;

    public Banco(String nombre) {
        this.nombre = nombre;
        this.tarjetas = new HashMap<String, Tarjeta>();
        this.cuentas = new HashMap<String, CuentaBancaria>();
        this.facturas = new HashMap<String, Factura>();
    }

    public String getNombre() {
        return nombre;
    }

    public void registrarCuenta(CuentaBancaria cuenta) {
        cuentas.put(cuenta.getNumero(), cuenta);
    }

    public void registrarTarjeta(Tarjeta tarjeta) {
        tarjetas.put(tarjeta.getNumero(), tarjeta);
        registrarCuenta(tarjeta.getCuenta());
    }

    public Tarjeta buscarTarjeta(String numeroTarjeta) {
        return tarjetas.get(numeroTarjeta);
    }

    public CuentaBancaria buscarCuenta(String numeroCuenta) {
        return cuentas.get(numeroCuenta);
    }

    public void registrarFactura(Factura factura) {
        facturas.put(factura.getLlave(), factura);
    }

    public Factura buscarFactura(String tipo, String identificador) {
        String llave = tipo.toUpperCase() + ":" + identificador.toUpperCase();
        return facturas.get(llave);
    }
}
