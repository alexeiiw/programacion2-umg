namespace GestionAcademica.Dominio;

public sealed class Estudiante : Persona
{
    public Estudiante(string codigo, string nombre, string correo, string carrera)
        : base(codigo, nombre, correo)
    {
        Carrera = ValidarTexto(carrera, "La carrera es obligatoria.");
    }

    public string Carrera { get; }
    public override string ObtenerRol() => "Estudiante";

    public override string ObtenerInformacion() =>
        $"{base.ObtenerInformacion()} | Carrera: {Carrera}";
}
