namespace PrestamosBiblioteca.Dominio;

public sealed class Prestamo
{
    public Prestamo(int lectorId, int libroId, DateTime fechaPrestamo, DateTime fechaLimite, DateTime? fechaDevolucion = null)
    {
        if (lectorId <= 0) throw new ArgumentException("Debe seleccionar un lector.");
        if (libroId <= 0) throw new ArgumentException("Debe seleccionar un libro.");
        if (fechaLimite.Date < fechaPrestamo.Date) throw new ArgumentException("La fecha limite no puede ser anterior al prestamo.");
        if (fechaDevolucion.HasValue && fechaDevolucion.Value.Date < fechaPrestamo.Date)
            throw new ArgumentException("La devolucion no puede ser anterior al prestamo.");

        LectorId = lectorId;
        LibroId = libroId;
        FechaPrestamo = fechaPrestamo;
        FechaLimite = fechaLimite;
        FechaDevolucion = fechaDevolucion;
    }

    public int Id { get; set; }
    public int LectorId { get; }
    public int LibroId { get; }
    public DateTime FechaPrestamo { get; }
    public DateTime FechaLimite { get; }
    public DateTime? FechaDevolucion { get; }
    public bool EstaActivo => !FechaDevolucion.HasValue;
    public bool EstaVencido(DateTime hoy) => EstaActivo && FechaLimite.Date < hoy.Date;
}
