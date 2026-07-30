public class Tarjeta {
    private final String numero;
    private String pin;
    private boolean bloqueada;
    private int intentosFallidos;
    private final CuentaBancaria cuenta;

    public Tarjeta(String numero, String pin, CuentaBancaria cuenta) {
        this.numero = numero;
        this.pin = pin;
        this.cuenta = cuenta;
        this.bloqueada = false;
        this.intentosFallidos = 0;
    }

    public String getNumero() {
        return numero;
    }

    public CuentaBancaria getCuenta() {
        return cuenta;
    }

    public boolean estaBloqueada() {
        return bloqueada;
    }

    public boolean validarPin(String pinIngresado) {
        if (bloqueada) {
            return false;
        }

        if (pin.equals(pinIngresado)) {
            intentosFallidos = 0;
            return true;
        }

        intentosFallidos++;
        if (intentosFallidos >= 3) {
            bloqueada = true;
        }

        return false;
    }

    public void cambiarPin(String pinActual, String pinNuevo) {
        if (!validarPin(pinActual)) {
            throw new IllegalArgumentException("PIN actual incorrecto");
        }

        pin = pinNuevo;
    }
}
