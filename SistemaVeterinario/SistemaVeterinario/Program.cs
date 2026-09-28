using System;

public class Program
{
    public static void Main(string[] args)
    {
        SerVivo mascotaEstable = new Mascota("M001", "Luna", "Perro", "Carlos Mendez");
        SerVivo mascotaObservacion = new Mascota("M002", "Milo", "Gato", "Andrea Lopez");

        ConsultaVeterinaria consultaEstable = new ConsultaVeterinaria((Mascota)mascotaEstable, "Consulta General");
        ConsultaVeterinaria consultaObservacion = new ConsultaVeterinaria((Mascota)mascotaObservacion, "Emergencia");

        try
        {
            consultaEstable.RegistrarDatos(18.5, 38.4, 125.00);
            consultaObservacion.RegistrarDatos(6.2, 40.1, 275.00);
        }
        catch (ArgumentException error)
        {
            Console.WriteLine(error.Message);
        }

        mascotaEstable.MostrarInformacion();
        consultaEstable.MostrarResultado();
        mascotaObservacion.MostrarInformacion();
        consultaObservacion.MostrarResultado();
    }
}
