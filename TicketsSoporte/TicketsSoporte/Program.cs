using TicketsSoporte.logica;

namespace TicketsSoporte
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Herencia: Tecnico y Solicitante son tipos especializados de Usuario.
            Tecnico objTecnicoSoftware = new Tecnico("T01", "Ana Lopez", "ana@empresa.com", "Software", 2);
            Tecnico objTecnicoHardware = new Tecnico("T02", "Carlos Mendez", "carlos@empresa.com", "Hardware", 2);
            Tecnico objTecnicoGeneral = new Tecnico("T03", "Maria Perez", "maria@empresa.com", "General", 3);

            Solicitante objSolicitanteContabilidad = new Solicitante("S01", "Luis Ramirez", "luis@empresa.com", "Contabilidad", "1201");
            Solicitante objSolicitanteVentas = new Solicitante("S02", "Karla Gomez", "karla@empresa.com", "Ventas", "1305");

            // 2. Polimorfismo: una lista de Usuario puede contener tecnicos y solicitantes.
            List<Usuario> lstUsuarios = new List<Usuario>()
            {
                objTecnicoSoftware,
                objTecnicoHardware,
                objTecnicoGeneral,
                objSolicitanteContabilidad,
                objSolicitanteVentas
            };

            // 3. Gestion centralizada: el gestor administra tecnicos, solicitantes y tickets.
            GestorTickets objGestor = new GestorTickets();
            objGestor.lstTecnicos.Add(objTecnicoSoftware);
            objGestor.lstTecnicos.Add(objTecnicoHardware);
            objGestor.lstTecnicos.Add(objTecnicoGeneral);
            objGestor.lstSolicitantes.Add(objSolicitanteContabilidad);
            objGestor.lstSolicitantes.Add(objSolicitanteVentas);

            // 4. Flujo del ticket: define el ciclo Abierto -> Asignado -> Resuelto -> Cerrado.
            FlujoTicket objFlujoTicket = new FlujoTicket();

            bool blnContinuar = true;

            while (blnContinuar)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n========================================================");
                Console.WriteLine(" SISTEMA DE TICKETS DE SOPORTE TECNICO CORPORATIVO");
                Console.WriteLine("========================================================");
                Console.ResetColor();
                Console.WriteLine(" 1. Ver usuarios del sistema (polimorfismo)");
                Console.WriteLine(" 2. Ver flujo de estados del ticket");
                Console.WriteLine(" 3. Crear ticket y asignar automaticamente");
                Console.WriteLine(" 4. Ver tickets");
                Console.WriteLine(" 5. Registrar error en ticket");
                Console.WriteLine(" 6. Resolver ticket");
                Console.WriteLine(" 7. Cerrar ticket");
                Console.WriteLine(" 8. Generar resumen de control");
                Console.WriteLine(" 9. Salir");
                Console.Write("\n Seleccione una opcion (1-9): ");

                try
                {
                    string strOpcion = Console.ReadLine()?.Trim();

                    switch (strOpcion)
                    {
                        case "1":
                            mostrarUsuariosPolimorfismo(lstUsuarios);
                            break;

                        case "2":
                            objFlujoTicket.mostrarFlujo();
                            break;

                        case "3":
                            crearTicket(objGestor);
                            break;

                        case "4":
                            objGestor.mostrarTickets();
                            break;

                        case "5":
                            registrarError(objGestor);
                            break;

                        case "6":
                            resolverTicket(objGestor, objFlujoTicket);
                            break;

                        case "7":
                            cerrarTicket(objGestor, objFlujoTicket);
                            break;

                        case "8":
                            objGestor.generarResumenControl();
                            break;

                        case "9":
                            blnContinuar = false;
                            Console.WriteLine("\nGracias por utilizar el sistema de soporte.");
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Opcion no valida. Ingrese un numero del 1 al 9.");
                            Console.ResetColor();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error controlado por try/catch: " + ex.Message);
                    Console.ResetColor();
                }
            }
        }

        /// <summary>
        /// Demuestra polimorfismo: cada objeto ejecuta su propia version de mostrarInformacion().
        /// </summary>
        static void mostrarUsuariosPolimorfismo(List<Usuario> lstUsuarios)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== USUARIOS DEL SISTEMA ===");
            Console.ResetColor();

            foreach (Usuario objUsuario in lstUsuarios)
            {
                objUsuario.mostrarInformacion();
                Console.WriteLine();
            }
        }

        static void crearTicket(GestorTickets objGestor)
        {
            Console.WriteLine("\nSolicitantes disponibles:");
            for (int i = 0; i < objGestor.lstSolicitantes.Count; i++)
            {
                Console.WriteLine($" {i + 1}. {objGestor.lstSolicitantes[i].strNombre} - {objGestor.lstSolicitantes[i].strDepartamento}");
            }

            Console.Write("Seleccione solicitante: ");
            int intIndice = int.Parse(Console.ReadLine() ?? "0") - 1;

            if (intIndice < 0 || intIndice >= objGestor.lstSolicitantes.Count)
            {
                throw new ArgumentOutOfRangeException("Solicitante", "Seleccion fuera de rango.");
            }

            Console.Write("Titulo del problema: ");
            string strTitulo = Console.ReadLine();

            Console.Write("Descripcion: ");
            string strDescripcion = Console.ReadLine();

            Console.Write("Categoria (Software/Hardware/Red/General): ");
            string strCategoria = Console.ReadLine();

            Console.Write("Prioridad (Baja/Media/Alta/Critica): ");
            string strPrioridad = Console.ReadLine();

            Ticket objTicket = objGestor.crearTicket(strTitulo, strDescripcion, strCategoria, strPrioridad, objGestor.lstSolicitantes[intIndice]);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nTicket creado correctamente.");
            Console.ResetColor();
            objTicket.mostrarResumen();
        }

        static void registrarError(GestorTickets objGestor)
        {
            Ticket objTicket = solicitarTicket(objGestor);

            Console.Write("Tipo de error (Software/Hardware/Red/Usuario): ");
            string strTipo = Console.ReadLine();

            Console.Write("Descripcion del error: ");
            string strDescripcion = Console.ReadLine();

            Console.Write("Impacto (Bajo/Medio/Alto/Critico): ");
            string strImpacto = Console.ReadLine();

            objTicket.registrarError(strTipo, strDescripcion, strImpacto);
            Console.WriteLine("Error registrado correctamente.");
            objTicket.mostrarBitacora();
        }

        static void resolverTicket(GestorTickets objGestor, FlujoTicket objFlujoTicket)
        {
            Ticket objTicket = solicitarTicket(objGestor);

            if (!objFlujoTicket.puedeCambiarEstado(objTicket.strEstado, "Resuelto"))
            {
                throw new InvalidOperationException("El flujo no permite resolver el ticket desde el estado actual.");
            }

            Console.Write("Solucion aplicada: ");
            string strSolucion = Console.ReadLine();

            objTicket.resolver(strSolucion);
            Console.WriteLine("Ticket resuelto correctamente.");
            objTicket.mostrarBitacora();
        }

        static void cerrarTicket(GestorTickets objGestor, FlujoTicket objFlujoTicket)
        {
            Ticket objTicket = solicitarTicket(objGestor);

            if (!objFlujoTicket.puedeCambiarEstado(objTicket.strEstado, "Cerrado"))
            {
                throw new InvalidOperationException("El flujo no permite cerrar el ticket desde el estado actual.");
            }

            objTicket.cerrar();
            Console.WriteLine("Ticket cerrado correctamente.");
            objTicket.mostrarBitacora();
        }

        static Ticket solicitarTicket(GestorTickets objGestor)
        {
            Console.Write("Ingrese numero de ticket: ");
            int intNumero = int.Parse(Console.ReadLine() ?? "0");
            return objGestor.buscarTicket(intNumero);
        }
    }
}
