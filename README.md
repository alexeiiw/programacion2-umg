# Programacion 2 UMG Portales

Ultima actualizacion: 30 de julio de 2026

Bienvenidos al repositorio de apoyo para el curso de Programacion 2 de la Universidad Mariano Galvez, sede Portales.

El objetivo principal de este repositorio es practicar programacion orientada a objetos en Java usando BlueJ.

## Objetivos Del Repositorio

- Comprender clases, objetos, atributos y metodos.
- Aplicar encapsulamiento, abstraccion, herencia y polimorfismo.
- Organizar ejercicios por carpetas independientes.
- Compartir ejemplos base para que cada estudiante pueda analizarlos, ejecutarlos y mejorarlos.

## Estructura

Cada proyecto debe vivir en su propia carpeta.

Ejemplos:

```text
CajeroAutomatico/
HolaMundo/
Calculadora/
SistemaNotas/
```

El proyecto actual disponible es:

```text
CajeroAutomatico/
HolaMundoBlueJ/
```

## Como Usar BlueJ

1. Abrir BlueJ.
2. Ir a `Project`.
3. Seleccionar `Open Project...`.
4. Elegir la carpeta del proyecto, por ejemplo `CajeroAutomatico`.
5. Presionar `Compile` para compilar todas las clases.
6. Si una clase tiene `main`, hacer clic derecho sobre esa clase.
7. Seleccionar `void main(String[] args)`.
8. Presionar `OK`.
9. Revisar la salida en la terminal de BlueJ.

## Como Ver El Codigo

1. Abrir el proyecto en BlueJ.
2. Dar doble clic sobre cualquier clase.
3. Revisar atributos, constructores y metodos.
4. Identificar como se relacionan las clases.
5. Modificar el codigo y volver a compilar.

## Como Ver La Interfaz De BlueJ

BlueJ muestra las clases como cajas dentro del proyecto.

- Las flechas de herencia muestran relaciones `extends`.
- Las flechas de uso muestran dependencias entre clases.
- El boton `Compile` valida si el codigo tiene errores.
- La terminal muestra los resultados de `System.out.println`.

## Como Usar El Repositorio

1. Instalar Git si aun no esta instalado.
2. Abrir PowerShell o Git Bash.
3. Ir a la carpeta donde se desea guardar el repositorio.
4. Clonar el repositorio.
5. Abrir cada carpeta de proyecto desde BlueJ.
6. No subir archivos `.class`, porque son generados al compilar.
7. No modificar archivos de otros proyectos sin indicacion del docente.
8. Crear nuevas carpetas para nuevos ejercicios.
9. Usar nombres claros para clases, metodos y variables.

Comandos basicos:

```powershell
git clone https://github.com/alexeiiw/programacion2-umg.git
cd programacion2-umg
dir
```

Para actualizar el repositorio local con cambios nuevos:

```powershell
git pull
```

Para ver el estado de sus cambios:

```powershell
git status
```

Para probar un proyecto desde PowerShell:

```powershell
cd CajeroAutomatico
javac *.java
java Simulador
```

## Recomendaciones Para Los Estudiantes

- Compilen frecuentemente.
- Prueben los cambios con ejemplos simples.
- Lean primero las clases principales antes de modificar.
- Eviten copiar codigo sin entenderlo.
- Documenten decisiones importantes con comentarios breves.

## Proyecto CajeroAutomatico

Este proyecto simula operaciones basicas de un cajero automatico:

- Autenticacion con tarjeta y PIN.
- Consulta de saldo.
- Retiro.
- Deposito.
- Transferencia.
- Generacion de recibos.

Tambien sirve como ejemplo de POO usando encapsulamiento, abstraccion, herencia y polimorfismo.

Documentacion especifica:

- `CajeroAutomatico/README.md`: guia completa del proyecto.
- `CajeroAutomatico/README.txt`: resumen visible desde BlueJ.
- `CajeroAutomatico/diagramas.md`: diagramas de clases, casos de uso, actividades con carriles, flujo y modelo entidad-relacion conceptual.

## Proyecto HolaMundoBlueJ

Este proyecto es un ejemplo inicial muy simple para abrir, compilar y ejecutar en BlueJ.

Incluye:

- `Animal`: clase base.
- `Perro`: clase hija que hereda de `Animal`.
- `Simulador`: crea un objeto `Perro` y muestra datos en consola.

Para probarlo desde PowerShell:

```powershell
cd HolaMundoBlueJ
javac *.java
java Simulador
```
