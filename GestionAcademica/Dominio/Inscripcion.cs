namespace GestionAcademica.Dominio;

public sealed class Inscripcion
{
    public Inscripcion(int estudianteId, int cursoId, DateTime fecha)
    {
        if (estudianteId <= 0) throw new ArgumentException("Debe seleccionar un estudiante.");
        if (cursoId <= 0) throw new ArgumentException("Debe seleccionar un curso.");
        EstudianteId = estudianteId;
        CursoId = cursoId;
        Fecha = fecha;
    }

    public int Id { get; set; }
    public int EstudianteId { get; }
    public int CursoId { get; }
    public DateTime Fecha { get; }
}
