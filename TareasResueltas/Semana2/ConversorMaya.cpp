#include <iostream>
#include <limits>

const int BASE_MAYA = 20;
const int MAX_NIVELES = 16;

/**
 * Solicita y valida un numero entero decimal no negativo.
 */
long long leerNumeroDecimal()
{
    long long numero;

    while (true)
    {
        std::cout << "Ingrese un numero decimal entero no negativo: ";

        if (std::cin >> numero && numero >= 0)
        {
            return numero;
        }

        std::cout << "Entrada invalida. Intente nuevamente.\n";
        std::cin.clear();
        std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
    }
}

/**
 * Convierte un numero decimal a base 20 usando divisiones sucesivas.
 * Los residuos se almacenan desde el nivel de menor peso al de mayor peso.
 *
 * Retorna la cantidad de niveles utilizados.
 */
int convertirDecimalAVigesimal(long long numero, int niveles[])
{
    if (numero == 0)
    {
        niveles[0] = 0;
        return 1;
    }

    int cantidadNiveles = 0;

    while (numero > 0 && cantidadNiveles < MAX_NIVELES)
    {
        niveles[cantidadNiveles] = static_cast<int>(numero % BASE_MAYA);
        numero /= BASE_MAYA;
        cantidadNiveles++;
    }

    return cantidadNiveles;
}

/**
 * Muestra la representacion vigesimal desde el nivel de mayor peso
 * hasta el nivel de menor peso.
 */
void mostrarNumeroVigesimal(const int niveles[], int cantidadNiveles)
{
    std::cout << "Representacion vigesimal Maya: ";

    for (int i = cantidadNiveles - 1; i >= 0; i--)
    {
        std::cout << "[" << niveles[i] << "]";

        if (i > 0)
        {
            std::cout << " ";
        }
    }

    std::cout << "\n";
}

int main()
{
    int niveles[MAX_NIVELES] = {0};

    std::cout << "========================================\n";
    std::cout << " CONVERSOR DECIMAL A VIGESIMAL MAYA\n";
    std::cout << "========================================\n";

    long long numeroDecimal = leerNumeroDecimal();
    int cantidadNiveles = convertirDecimalAVigesimal(numeroDecimal, niveles);

    mostrarNumeroVigesimal(niveles, cantidadNiveles);

    return 0;
}
