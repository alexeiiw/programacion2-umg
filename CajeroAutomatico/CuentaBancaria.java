import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class CuentaBancaria {
    private final String numero;
    private final Cliente titular;
    private double saldo;
    private final List<Transaccion> historial;

    public CuentaBancaria(String numero, Cliente titular, double saldoInicial) {
        if (saldoInicial < 0) {
            throw new IllegalArgumentException("El saldo inicial no puede ser negativo");
        }

        this.numero = numero;
        this.titular = titular;
        this.saldo = saldoInicial;
        this.historial = new ArrayList<Transaccion>();
    }

    public String getNumero() {
        return numero;
    }

    public Cliente getTitular() {
        return titular;
    }

    public double getSaldo() {
        return saldo;
    }

    public List<Transaccion> getHistorial() {
        return Collections.unmodifiableList(historial);
    }

    public void depositar(double monto) {
        validarMonto(monto);
        saldo += monto;
    }

    public boolean retirar(double monto) {
        validarMonto(monto);

        if (monto > saldo) {
            return false;
        }

        saldo -= monto;
        return true;
    }

    public boolean transferirA(CuentaBancaria destino, double monto) {
        if (retirar(monto)) {
            destino.depositar(monto);
            return true;
        }

        return false;
    }

    public void registrarTransaccion(Transaccion transaccion) {
        historial.add(transaccion);
    }

    private void validarMonto(double monto) {
        if (monto <= 0) {
            throw new IllegalArgumentException("El monto debe ser mayor que cero");
        }
    }

    @Override
    public String toString() {
        return "Cuenta " + numero + " - " + titular.getNombre() + " - Saldo: " + saldo;
    }
}
