public abstract class SerVivo
{
    protected SerVivo(string codigo, string nombre)
    {
        Codigo = codigo;
        Nombre = nombre;
    }

    public string Codigo { get; }
    public string Nombre { get; }

    public abstract void MostrarInformacion();
}
