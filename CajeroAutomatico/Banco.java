import java.util.HashMap;
import java.util.Map;

public class Banco {
    private final String nombre;
    private final Map<String, Tarjeta> tarjetas;
    private final Map<String, CuentaBancaria> cuentas;

    public Banco(String nombre) {
        this.nombre = nombre;
        this.tarjetas = new HashMap<String, Tarjeta>();
        this.cuentas = new HashMap<String, CuentaBancaria>();
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
}
