using System;

class ConversorMaya
{
    const int BASE_MAYA = 20;
    const int MAX_NIVELES = 16;

    /// <summary>
    /// Solicita y valida un numero entero decimal no negativo.
    /// </summary>
    static long LeerNumeroDecimal()
    {
        while (true)
        {
            Console.Write("Ingrese un numero decimal entero no negativo: ");
            string? entrada = Console.ReadLine();

            if (long.TryParse(entrada, out long numero) && numero >= 0)
            {
                return numero;
            }

            Console.WriteLine("Entrada invalida. Intente nuevamente.");
        }
    }

    /// <summary>
    /// Convierte un numero decimal a base 20 usando divisiones sucesivas.
    /// Los residuos se almacenan desde el nivel de menor peso al de mayor peso.
    /// </summary>
    /// <returns>Cantidad de niveles utilizados.</returns>
    static int ConvertirDecimalAVigesimal(long numero, int[] niveles)
    {
        if (numero == 0)
        {
            niveles[0] = 0;
            return 1;
        }

        int cantidadNiveles = 0;

        while (numero > 0 && cantidadNiveles < niveles.Length)
        {
            niveles[cantidadNiveles] = (int)(numero % BASE_MAYA);
            numero /= BASE_MAYA;
            cantidadNiveles++;
        }

        return cantidadNiveles;
    }

    /// <summary>
    /// Muestra la representacion vigesimal desde el nivel de mayor peso
    /// hasta el nivel de menor peso.
    /// </summary>
    static void MostrarNumeroVigesimal(int[] niveles, int cantidadNiveles)
    {
        Console.Write("Representacion vigesimal Maya: ");

        for (int i = cantidadNiveles - 1; i >= 0; i--)
        {
            Console.Write($"[{niveles[i]}]");

            if (i > 0)
            {
                Console.Write(" ");
            }
        }

        Console.WriteLine();
    }

    static void Main()
    {
        int[] niveles = new int[MAX_NIVELES];

        Console.WriteLine("========================================");
        Console.WriteLine(" CONVERSOR DECIMAL A VIGESIMAL MAYA");
        Console.WriteLine("========================================");

        long numeroDecimal = LeerNumeroDecimal();
        int cantidadNiveles = ConvertirDecimalAVigesimal(numeroDecimal, niveles);

        MostrarNumeroVigesimal(niveles, cantidadNiveles);
    }
}
