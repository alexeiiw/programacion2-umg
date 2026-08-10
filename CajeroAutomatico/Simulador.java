public class Simulador {
    public static void main(String[] args) {

        // --- Banco ---
        Banco banco = new Banco("POO Guatemala");

        // --- Clientes ---
        Cliente cliente1 = new Cliente("Mario Cifuentes", "2001");
        Cliente cliente2 = new Cliente("Sofia Reyes", "2002");
        Cliente cliente3 = new Cliente("Pedro Molina", "2003");

        // --- Cuentas ---
        CuentaBancaria cuenta1 = new CuentaBancaria("CTA-101", cliente1, 2500.0);
        CuentaBancaria cuenta2 = new CuentaBancaria("CTA-102", cliente2, 800.0);
        CuentaBancaria cuenta3 = new CuentaBancaria("CTA-103", cliente3, 150.0);

        // --- Tarjetas (CTA-103 no tiene tarjeta) ---
        Tarjeta tarjeta1 = new Tarjeta("TAR-201", "4321", cuenta1);
        Tarjeta tarjeta2 = new Tarjeta("TAR-202", "8765", cuenta2);

        // --- Facturas ---
        Factura facturaTelefono = new Factura("TELEFONO", "55667788", 450.0);
        Factura facturaEnergia  = new Factura("ENERGIA",  "CORR-11223", 600.0);

        // --- Registrar en banco ---
        banco.registrarTarjeta(tarjeta1);
        banco.registrarTarjeta(tarjeta2);
        banco.registrarCuenta(cuenta3);
        banco.registrarFactura(facturaTelefono);
        banco.registrarFactura(facturaEnergia);

        // --- Cajero ---
        CajeroAutomatico cajero = new CajeroAutomatico(banco);

        // ============================================================
        // SESION: Mario Cifuentes - TAR-201
        // ============================================================
        System.out.println("=== Sesion Mario Cifuentes ===");
        System.out.println("Autenticacion incorrecta: " + cajero.autenticar("TAR-201", "0000"));

        if (cajero.autenticar("TAR-201", "4321")) {
            System.out.println("Saldo inicial CTA-101: " + cajero.consultarSaldo());

            // Retiro aprobado
            System.out.println(cajero.retirar(500.0));

            // Deposito
            System.out.println(cajero.depositar(300.0));

            // Transferencia a CTA-102
            System.out.println(cajero.transferir("CTA-102", 400.0));

            // Pago parcial de telefono
            System.out.println(cajero.pagarServicio("TELEFONO", "55667788", 200.0));
            System.out.println("Saldo pendiente factura telefono: " + facturaTelefono.getSaldo());

            // Segundo pago parcial de telefono
            System.out.println(cajero.pagarServicio("TELEFONO", "55667788", 250.0));
            System.out.println("Factura telefono pagada: " + facturaTelefono.estaPagada());

            // Intento de sobrepago (rechazado, factura ya pagada)
            System.out.println(cajero.pagarServicio("TELEFONO", "55667788", 50.0));

            // Retiro rechazado por saldo insuficiente
            System.out.println(cajero.retirar(99999.0));

            System.out.println("Saldo final CTA-101: " + cajero.consultarSaldo());
            mostrarHistorial(cuenta1);
        }

        cajero.cerrarSesion();

        // ============================================================
        // SESION: Sofia Reyes - TAR-202
        // ============================================================
        System.out.println("\n=== Sesion Sofia Reyes ===");

        if (cajero.autenticar("TAR-202", "8765")) {
            System.out.println("Saldo inicial CTA-102: " + cajero.consultarSaldo());

            // Pago parcial de energia
            System.out.println(cajero.pagarServicio("ENERGIA", "CORR-11223", 300.0));
            System.out.println("Saldo pendiente factura energia: " + facturaEnergia.getSaldo());

            // Intento de pago mayor al saldo pendiente (rechazado)
            System.out.println(cajero.pagarServicio("ENERGIA", "CORR-11223", 400.0));

            // Retiro rechazado: CTA-102 solo tiene 500 tras recibir transferencia y pagar
            System.out.println(cajero.retirar(1000.0));

            System.out.println("Saldo final CTA-102: " + cajero.consultarSaldo());
            mostrarHistorial(cuenta2);
        }

        cajero.cerrarSesion();

        // ============================================================
        // RESUMEN FACTURAS
        // ============================================================
        System.out.println("\n=== Estado de facturas ===");
        System.out.println(facturaTelefono);
        System.out.println(facturaEnergia);
    }

    private static void mostrarHistorial(CuentaBancaria cuenta) {
        System.out.println("Historial " + cuenta.getNumero() + ":");
        for (Transaccion t : cuenta.getHistorial()) {
            System.out.println("  - " + t.getTipo() + " Q" + t.getMonto() + " el " + t.getFecha());
        }
    }
}
