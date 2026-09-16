namespace TicketsSoporte.logica
{
    public class FlujoTicket
    {
        public List<string> lstEstados { get; set; }

        public FlujoTicket()
        {
            lstEstados = new List<string>()
            {
                "Abierto",
                "Asignado",
                "Resuelto",
                "Cerrado"
            };
        }

        public bool puedeCambiarEstado(string strEstadoActual, string strEstadoNuevo)
        {
            if (strEstadoActual == "Abierto" && strEstadoNuevo == "Asignado") return true;
            if (strEstadoActual == "Asignado" && strEstadoNuevo == "Resuelto") return true;
            if (strEstadoActual == "Resuelto" && strEstadoNuevo == "Cerrado") return true;

            return false;
        }

        public void mostrarFlujo()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== FLUJO DEL TICKET ===");
            Console.ResetColor();
            Console.WriteLine("Abierto  -> ticket creado por el solicitante");
            Console.WriteLine("Asignado -> ticket asignado automaticamente a un tecnico");
            Console.WriteLine("Resuelto -> tecnico registra la solucion");
            Console.WriteLine("Cerrado  -> solicitante o coordinador confirma el cierre");
        }
    }
}
