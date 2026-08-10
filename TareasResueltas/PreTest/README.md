# Pre Test

Esta carpeta contiene soluciones de apoyo para los ejercicios del pre test de Programacion 2.

## Contenido

- `CajeroAutomatico/Simulador.java`: solucion del ejercicio del cajero automatico con pago de servicios.
- `ConversorBase20.cpp`: solucion del conversor de base 10 a base 20 en C++.
- `ConversorBase20.cs`: solucion del conversor de base 10 a base 20 en C#.

## Uso De La Solucion Del Cajero

El archivo `CajeroAutomatico/Simulador.java` esta pensado para reemplazar o comparar la clase `Simulador` del proyecto `CajeroAutomatico`.

Para probarlo, copie temporalmente el archivo dentro del proyecto `CajeroAutomatico` y compile todas las clases Java.

```powershell
cd CajeroAutomatico
javac *.java
java Simulador
```

## Uso Del Conversor

La solucion esta disponible en C++ y C# porque el enunciado permite cualquiera de los dos lenguajes.

Ambas versiones usan divisiones sucesivas entre 20 y convierten residuos de 10 a 19 usando letras de `A` a `J`.
