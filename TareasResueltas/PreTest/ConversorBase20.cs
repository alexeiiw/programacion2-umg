using System;

public class ConversorBase20
{
    public static string ConvertirBase20(int numero)
    {
        if (numero == 0)
        {
            return "0";
        }

        string digitos = "0123456789ABCDEFGHIJ";
        string resultado = "";

        while (numero > 0)
        {
            int residuo = numero % 20;
            resultado = digitos[residuo] + resultado;
            numero = numero / 20;
        }

        return resultado;
    }

    public static void Main(string[] args)
    {
        Console.Write("Ingrese un numero entero positivo en base 10: ");
        int numero = int.Parse(Console.ReadLine() ?? "0");

        if (numero < 0)
        {
            Console.WriteLine("El numero debe ser positivo.");
            return;
        }

        Console.WriteLine("Base 20: " + ConvertirBase20(numero));
    }
}
