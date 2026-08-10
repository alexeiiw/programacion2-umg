public class Simulador {
    public static void main(String[] args) {
        Banco banco = new Banco("POO Guatemala");

        Cliente mario = new Cliente("Mario Cifuentes", "2001");
        Cliente sofia = new Cliente("Sofia Reyes", "2002");
        Cliente pedro = new Cliente("Pedro Molina", "2003");

        CuentaBancaria cuentaMario = new CuentaBancaria("CTA-101", mario, 2500.0);
        CuentaBancaria cuentaSofia = new CuentaBancaria("CTA-102", sofia, 800.0);
        CuentaBancaria cuentaPedro = new CuentaBancaria("CTA-103", pedro, 150.0);

        Tarjeta tarjetaMario = new Tarjeta("TAR-201", "4321", cuentaMario);
        Tarjeta tarjetaSofia = new Tarjeta("TAR-202", "8765", cuentaSofia);

        Factura facturaTelefono = new Factura("TELEFONO", "55667788", 450.0);
        Factura facturaEnergia = new Factura("ENERGIA", "CORR-11223", 600.0);

        banco.registrarTarjeta(tarjetaMario);
        banco.registrarTarjeta(tarjetaSofia);
        banco.registrarCuenta(cuentaPedro);
        banco.registrarFactura(facturaTelefono);
        banco.registrarFactura(facturaEnergia);

        CajeroAutomatico cajero = new CajeroAutomatico(banco);

        System.out.println("=== Sesion Mario Cifuentes ===");
        System.out.println("Autenticacion incorrecta: " + cajero.autenticar("TAR-201", "0000"));

        if (cajero.autenticar("TAR-201", "4321")) {
            System.out.println("Saldo inicial CTA-101: " + cajero.consultarSaldo());
            System.out.println(cajero.retirar(500.0));
            System.out.println(cajero.depositar(300.0));
            System.out.println(cajero.transferir("CTA-102", 400.0));
            System.out.println(cajero.pagarServicio("TELEFONO", "55667788", 200.0));
            System.out.println("Saldo pendiente factura telefono: " + facturaTelefono.getSaldo());
            System.out.println(cajero.pagarServicio("TELEFONO", "55667788", 250.0));
            System.out.println("Factura telefono pagada: " + facturaTelefono.estaPagada());
            System.out.println(cajero.pagarServicio("TELEFONO", "55667788", 50.0));
            System.out.println(cajero.retirar(99999.0));
            System.out.println("Saldo final CTA-101: " + cajero.consultarSaldo());
        }

        cajero.cerrarSesion();

        System.out.println("\n=== Sesion Sofia Reyes ===");

        if (cajero.autenticar("TAR-202", "8765")) {
            System.out.println("Saldo inicial CTA-102: " + cajero.consultarSaldo());
            System.out.println(cajero.pagarServicio("ENERGIA", "CORR-11223", 300.0));
            System.out.println("Saldo pendiente factura energia: " + facturaEnergia.getSaldo());
            System.out.println(cajero.pagarServicio("ENERGIA", "CORR-11223", 400.0));
            System.out.println("Saldo final CTA-102: " + cajero.consultarSaldo());
        }

        cajero.cerrarSesion();

        System.out.println("\n=== Estado de facturas ===");
        System.out.println(facturaTelefono);
        System.out.println(facturaEnergia);
    }
}
