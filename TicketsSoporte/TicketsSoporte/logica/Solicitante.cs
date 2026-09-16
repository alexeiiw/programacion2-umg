namespace TicketsSoporte.logica
{
    public class Solicitante : Usuario
    {
        public string strDepartamento { get; set; }
        public string strExtension { get; set; }

        public Solicitante(string strCodigo, string strNombre, string strCorreo, string strDepartamento, string strExtension)
            : base(strCodigo, strNombre, strCorreo)
        {
            this.strDepartamento = strDepartamento;
            this.strExtension = strExtension;
        }

        public override string obtenerRol()
        {
            return "Solicitante";
        }

        public override void mostrarInformacion()
        {
            base.mostrarInformacion();
            Console.WriteLine($" Departamento: {strDepartamento} | Extension: {strExtension}");
        }
    }
}
