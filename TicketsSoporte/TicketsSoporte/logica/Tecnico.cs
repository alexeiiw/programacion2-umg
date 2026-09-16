namespace TicketsSoporte.logica
{
    public class Tecnico : Usuario
    {
        public string strEspecialidad { get; set; }
        public int intCargaActual { get; set; }
        public int intCapacidadMaxima { get; set; }

        public Tecnico(string strCodigo, string strNombre, string strCorreo, string strEspecialidad, int intCapacidadMaxima)
            : base(strCodigo, strNombre, strCorreo)
        {
            this.strEspecialidad = strEspecialidad;
            this.intCargaActual = 0;
            this.intCapacidadMaxima = intCapacidadMaxima;
        }

        public override string obtenerRol()
        {
            return "Tecnico";
        }

        public bool estaDisponible()
        {
            return blnActivo && intCargaActual < intCapacidadMaxima;
        }

        public bool puedeAtender(string strCategoria)
        {
            return estaDisponible() &&
                   (strEspecialidad.Equals(strCategoria, StringComparison.OrdinalIgnoreCase) ||
                    strEspecialidad.Equals("General", StringComparison.OrdinalIgnoreCase));
        }

        public void aumentarCarga()
        {
            if (intCargaActual < intCapacidadMaxima)
            {
                intCargaActual++;
            }
        }

        public void liberarCarga()
        {
            if (intCargaActual > 0)
            {
                intCargaActual--;
            }
        }

        public override void mostrarInformacion()
        {
            base.mostrarInformacion();
            Console.WriteLine($" Especialidad: {strEspecialidad} | Carga: {intCargaActual}/{intCapacidadMaxima}");
        }
    }
}
