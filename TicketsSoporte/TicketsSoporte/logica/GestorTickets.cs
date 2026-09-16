namespace TicketsSoporte.logica
{
    public class GestorTickets
    {
        public List<Tecnico> lstTecnicos { get; set; }
        public List<Solicitante> lstSolicitantes { get; set; }
        public List<Ticket> lstTickets { get; set; }
        private int intCorrelativoTicket;

        public GestorTickets()
        {
            lstTecnicos = new List<Tecnico>();
            lstSolicitantes = new List<Solicitante>();
            lstTickets = new List<Ticket>();
            intCorrelativoTicket = 1;
        }

        public void cargarDatosBase()
        {
            lstTecnicos.Add(new Tecnico("T01", "Ana Lopez", "ana@empresa.com", "Software", 2));
            lstTecnicos.Add(new Tecnico("T02", "Carlos Mendez", "carlos@empresa.com", "Hardware", 2));
            lstTecnicos.Add(new Tecnico("T03", "Maria Perez", "maria@empresa.com", "General", 3));

            lstSolicitantes.Add(new Solicitante("S01", "Luis Ramirez", "luis@empresa.com", "Contabilidad", "1201"));
            lstSolicitantes.Add(new Solicitante("S02", "Karla Gomez", "karla@empresa.com", "Ventas", "1305"));
        }

        public Ticket crearTicket(string strTitulo, string strDescripcion, string strCategoria, string strPrioridad, Solicitante objSolicitante)
        {
            if (string.IsNullOrWhiteSpace(strTitulo) || string.IsNullOrWhiteSpace(strDescripcion))
            {
                throw new ArgumentException("El titulo y la descripcion son obligatorios.");
            }

            Ticket objTicket = new Ticket(intCorrelativoTicket, strTitulo, strDescripcion, strCategoria, strPrioridad, objSolicitante);
            intCorrelativoTicket++;
            lstTickets.Add(objTicket);
            asignarAutomaticamente(objTicket);
            return objTicket;
        }

        public bool asignarAutomaticamente(Ticket objTicket)
        {
            Tecnico objMejorTecnico = null;

            foreach (Tecnico objTecnico in lstTecnicos)
            {
                if (objTecnico.puedeAtender(objTicket.strCategoria))
                {
                    if (objMejorTecnico == null || objTecnico.intCargaActual < objMejorTecnico.intCargaActual)
                    {
                        objMejorTecnico = objTecnico;
                    }
                }
            }

            if (objMejorTecnico == null)
            {
                objTicket.escalar("No hay tecnicos disponibles para la categoria");
                return false;
            }

            objTicket.asignarTecnico(objMejorTecnico);
            return true;
        }

        public Ticket buscarTicket(int intNumero)
        {
            Ticket objTicket = lstTickets.Find(t => t.intNumero == intNumero);
            if (objTicket == null)
            {
                throw new InvalidOperationException("No existe un ticket con ese numero.");
            }
            return objTicket;
        }

        public void mostrarUsuariosPolimorfismo()
        {
            List<Usuario> lstUsuarios = new List<Usuario>();
            lstUsuarios.AddRange(lstSolicitantes);
            lstUsuarios.AddRange(lstTecnicos);

            Console.WriteLine("\n=== USUARIOS DEL SISTEMA (POLIMORFISMO) ===");
            foreach (Usuario objUsuario in lstUsuarios)
            {
                objUsuario.mostrarInformacion();
                Console.WriteLine();
            }
        }

        public void mostrarTickets()
        {
            Console.WriteLine("\n=== TICKETS REGISTRADOS ===");
            if (lstTickets.Count == 0)
            {
                Console.WriteLine("No hay tickets registrados.");
                return;
            }

            foreach (Ticket objTicket in lstTickets)
            {
                objTicket.mostrarResumen();
                Console.WriteLine();
            }
        }

        public void generarResumenControl()
        {
            Console.WriteLine("\n=== RESUMEN DE CONTROL ===");
            Console.WriteLine($"Total tickets: {lstTickets.Count}");
            Console.WriteLine($"Abiertos: {lstTickets.Count(t => t.strEstado == "Abierto")}");
            Console.WriteLine($"Asignados: {lstTickets.Count(t => t.strEstado == "Asignado")}");
            Console.WriteLine($"Resueltos: {lstTickets.Count(t => t.strEstado == "Resuelto")}");
            Console.WriteLine($"Cerrados: {lstTickets.Count(t => t.strEstado == "Cerrado")}");
            Console.WriteLine($"Criticos: {lstTickets.Count(t => t.strPrioridad == "Critica")}");
            Console.WriteLine($"Escalados: {lstTickets.Count(t => t.blnEscalado)}");
        }
    }
}
