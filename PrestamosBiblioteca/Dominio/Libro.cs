namespace PrestamosBiblioteca.Dominio;

public sealed class Libro
{
    public Libro(string codigo, string titulo, string autor, int ejemplares)
    {
        Codigo = string.IsNullOrWhiteSpace(codigo) ? throw new ArgumentException("El codigo del libro es obligatorio.") : codigo.Trim();
        Titulo = string.IsNullOrWhiteSpace(titulo) ? throw new ArgumentException("El titulo es obligatorio.") : titulo.Trim();
        Autor = string.IsNullOrWhiteSpace(autor) ? throw new ArgumentException("El autor es obligatorio.") : autor.Trim();
        if (ejemplares < 0) throw new ArgumentException("La cantidad de ejemplares no puede ser negativa.");
        Ejemplares = ejemplares;
    }

    public string Codigo { get; }
    public string Titulo { get; }
    public string Autor { get; }
    public int Ejemplares { get; }
}
