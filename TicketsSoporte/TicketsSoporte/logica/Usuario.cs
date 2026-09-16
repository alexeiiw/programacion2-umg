namespace TicketsSoporte.logica
{
    /// <summary>
    /// Clase abstracta base para demostrar herencia y polimorfismo.
    /// </summary>
    public abstract class Usuario
    {
        public string strCodigo { get; set; }
        public string strNombre { get; set; }
        public string strCorreo { get; set; }
        public bool blnActivo { get; set; }

        protected Usuario(string strCodigo, string strNombre, string strCorreo)
        {
            this.strCodigo = strCodigo;
            this.strNombre = strNombre;
            this.strCorreo = strCorreo;
            this.blnActivo = true;
        }

        public abstract string obtenerRol();

        public virtual void mostrarInformacion()
        {
            Console.WriteLine($"[{obtenerRol()}] {strCodigo} - {strNombre} | {strCorreo} | Activo: {blnActivo}");
        }
    }
}
