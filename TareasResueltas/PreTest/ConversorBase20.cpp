#include <iostream>
#include <string>
#include <algorithm>
using namespace std;

string convertirBase20(int numero) {
    if (numero == 0) {
        return "0";
    }

    string digitos = "0123456789ABCDEFGHIJ";
    string resultado = "";

    while (numero > 0) {
        int residuo = numero % 20;
        resultado += digitos[residuo];
        numero = numero / 20;
    }

    reverse(resultado.begin(), resultado.end());
    return resultado;
}

int main() {
    int numero;

    cout << "Ingrese un numero entero positivo en base 10: ";
    cin >> numero;

    if (numero < 0) {
        cout << "El numero debe ser positivo." << endl;
        return 0;
    }

    cout << "Base 20: " << convertirBase20(numero) << endl;
    return 0;
}
