namespace TicketsSoporte.logica
{
    public class Ticket
    {
        public int intNumero { get; set; }
        public string strTitulo { get; set; }
        public string strDescripcion { get; set; }
        public string strCategoria { get; set; }
        public string strPrioridad { get; set; }
        public string strEstado { get; set; }
        public bool blnEscalado { get; set; }
        public Solicitante objSolicitante { get; set; }
        public Tecnico objTecnicoAsignado { get; set; }
        public List<string> lstBitacora { get; set; }
        public List<string> lstErrores { get; set; }

        public Ticket(int intNumero, string strTitulo, string strDescripcion, string strCategoria, string strPrioridad, Solicitante objSolicitante)
        {
            this.intNumero = intNumero;
            this.strTitulo = strTitulo;
            this.strDescripcion = strDescripcion;
            this.strCategoria = strCategoria;
            this.strPrioridad = strPrioridad;
            this.objSolicitante = objSolicitante;
            this.strEstado = "Abierto";
            this.blnEscalado = false;
            this.lstBitacora = new List<string>();
            this.lstErrores = new List<string>();
            registrarBitacora("Ticket creado");
        }

        public void registrarBitacora(string strEvento)
        {
            lstBitacora.Add($"{DateTime.Now:dd/MM/yyyy HH:mm} - {strEvento}");
        }

        public void asignarTecnico(Tecnico objTecnico)
        {
            if (objTecnico == null)
            {
                throw new InvalidOperationException("No se puede asignar un tecnico nulo.");
            }

            if (!objTecnico.puedeAtender(strCategoria))
            {
                throw new InvalidOperationException("El tecnico no esta disponible o no atiende esta categoria.");
            }

            objTecnicoAsignado = objTecnico;
            objTecnico.aumentarCarga();
            strEstado = "Asignado";
            registrarBitacora($"Asignado a {objTecnico.strNombre}");
        }

        public void registrarError(string strTipo, string strDescripcion, string strImpacto)
        {
            string strError = $"{strTipo}: {strDescripcion} | Impacto: {strImpacto}";
            lstErrores.Add(strError);
            registrarBitacora("Error registrado - " + strError);

            if (strImpacto.Equals("Alto", StringComparison.OrdinalIgnoreCase) ||
                strImpacto.Equals("Critico", StringComparison.OrdinalIgnoreCase))
            {
                escalar("Error de alto impacto");
            }
        }

        public void resolver(string strSolucion)
        {
            if (objTecnicoAsignado == null || strEstado != "Asignado")
            {
                throw new InvalidOperationException("Solo se puede resolver un ticket asignado.");
            }

            strEstado = "Resuelto";
            registrarBitacora("Solucion registrada: " + strSolucion);
        }

        public void cerrar()
        {
            if (strEstado != "Resuelto")
            {
                throw new InvalidOperationException("Solo se puede cerrar un ticket resuelto.");
            }

            strEstado = "Cerrado";
            objTecnicoAsignado?.liberarCarga();
            registrarBitacora("Ticket cerrado");
        }

        public void escalar(string strMotivo)
        {
            blnEscalado = true;
            if (!strPrioridad.Equals("Critica", StringComparison.OrdinalIgnoreCase))
            {
                strPrioridad = "Critica";
            }
            registrarBitacora("Ticket escalado: " + strMotivo);
        }

        public void mostrarResumen()
        {
            Console.WriteLine($"#{intNumero} | {strTitulo} | Estado: {strEstado} | Prioridad: {strPrioridad} | Categoria: {strCategoria}");
            Console.WriteLine($" Solicitante: {objSolicitante.strNombre}");
            Console.WriteLine($" Tecnico: {(objTecnicoAsignado != null ? objTecnicoAsignado.strNombre : "Sin asignar")} | Escalado: {blnEscalado}");
        }

        public void mostrarBitacora()
        {
            Console.WriteLine($"\nBitacora del ticket #{intNumero}");
            foreach (string strEvento in lstBitacora)
            {
                Console.WriteLine("- " + strEvento);
            }
        }
    }
}
