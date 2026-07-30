# Hola Mundo BlueJ

Proyecto BlueJ muy simple para iniciar con programacion orientada a objetos en Java.

Curso: Programacion 2  
Universidad Mariano Galvez, sede Portales

## Objetivo

Mostrar un ejemplo minimo de clase, objeto, herencia y sobrescritura de metodos.

## Clases Del Proyecto

- `Animal`: clase base con nombre y metodo `hacerSonido`.
- `Perro`: clase hija que hereda de `Animal` y cambia el sonido.
- `Simulador`: clase con `main` que crea un objeto `Perro` y muestra datos en consola.

## Como Probar En BlueJ

1. Abrir BlueJ.
2. Ir a `Project`.
3. Seleccionar `Open Project...`.
4. Elegir la carpeta `HolaMundoBlueJ`.
5. Presionar `Compile`.
6. Clic derecho sobre `Simulador`.
7. Ejecutar `void main(String[] args)`.
8. Presionar `OK`.
9. Revisar la salida en la terminal de BlueJ.

## Como Probar Desde PowerShell

Desde la raiz del repositorio:

```powershell
cd HolaMundoBlueJ
javac *.java
java Simulador
```

## Salida Esperada

```text
Hola Mundo BlueJ
Nombre del perro: Firulais
Raza: Labrador
Sonido: Guau guau
```

## Conceptos POO

- Clase: `Animal` y `Perro`.
- Objeto: `perro` creado en `Simulador`.
- Herencia: `Perro extends Animal`.
- Sobrescritura: `Perro` redefine `hacerSonido`.
