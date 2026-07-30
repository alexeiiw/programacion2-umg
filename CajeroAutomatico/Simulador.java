public class Simulador {
    public static void main(String[] args) {
        Banco banco = new Banco("Banco POO");

        Cliente cliente1 = new Cliente("Ana Perez", "1001");
        Cliente cliente2 = new Cliente("Luis Gomez", "1002");

        CuentaBancaria cuenta1 = new CuentaBancaria("CTA-001", cliente1, 1000.0);
        CuentaBancaria cuenta2 = new CuentaBancaria("CTA-002", cliente2, 500.0);

        Tarjeta tarjeta1 = new Tarjeta("TAR-001", "1234", cuenta1);

        banco.registrarTarjeta(tarjeta1);
        banco.registrarCuenta(cuenta2);

        CajeroAutomatico cajero = new CajeroAutomatico(banco);

        if (cajero.autenticar("TAR-001", "1234")) {
            System.out.println("Saldo inicial: " + cajero.consultarSaldo());
            System.out.println(cajero.retirar(200.0));
            System.out.println(cajero.depositar(150.0));
            System.out.println(cajero.transferir("CTA-002", 300.0));
            System.out.println("Saldo final: " + cajero.consultarSaldo());
        } else {
            System.out.println("No se pudo autenticar la tarjeta");
        }
    }
}
