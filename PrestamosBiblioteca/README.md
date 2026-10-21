# Sistema de Prestamos de Biblioteca

Ejemplo didactico de consola en C# para registrar lectores, libros y prestamos en SQLite. Recorre el flujo desde las tablas y sus relaciones hasta las operaciones CRUD, las reglas de disponibilidad y la devolucion de ejemplares.

## Requisitos y ejecucion

- .NET 6 SDK o posterior.
- Conexion a Internet al restaurar por primera vez el paquete NuGet.

Desde esta carpeta:

```powershell
dotnet restore
dotnet run
```

`Microsoft.Data.Sqlite` esta declarado en `PrestamosBiblioteca.csproj`; NuGet descarga automaticamente el proveedor y las bibliotecas necesarias. No se deben copiar DLLs manualmente. Al iniciar, el programa crea la base de datos y sus tablas si no existen.

La base se crea en `%LOCALAPPDATA%\UMG\PrestamosBiblioteca\biblioteca.db`. Para pruebas, puede establecerse la variable de entorno `PRESTAMOS_BIBLIOTECA_DB` con otra ruta completa. Para reiniciar el ejemplo, cierre el programa y elimine el archivo `.db` correspondiente.

## Tablas y relaciones

- `Lectores`: personas que pueden solicitar libros.
- `Libros`: catalogo con cantidad total de ejemplares.
- `Prestamos`: relacion de un lector con un libro, fechas del prestamo y eventual devolucion.
- Las claves foraneas relacionan prestamos con lectores y libros.
- Los ejemplares disponibles se calculan restando los prestamos activos del total.

Revise el esquema completo en `BaseDatos/crear_tablas.sql`.

## CRUD y reglas

El menu permite crear, listar, actualizar y eliminar lectores, libros y prestamos, ademas de registrar devoluciones. No se permite prestar cuando no hay ejemplares disponibles. Un ejemplar vuelve a estar disponible al registrar su devolucion. El listado informa si cada prestamo esta `Prestado`, `Vencido` o `Devuelto`.

Las consultas estan separadas en `Persistencia/RepositorioBiblioteca.cs` y usan parametros SQL. El menu interactivo esta en `Aplicacion/MenuBiblioteca.cs`.

## POO y manejo de errores

- `Persona` es abstracta y `Lector` hereda de ella.
- `Persona persona = new Lector(...)` y la opcion 14 muestran polimorfismo.
- `Libro` y `Prestamo` encapsulan sus datos y validan reglas de negocio.
- La capa de persistencia separa las entidades de la base de datos.
- `try-catch` en el menu controla datos incorrectos, restricciones SQLite y reglas del sistema sin cerrar la aplicacion.

## Recorrido sugerido para clase

1. Leer `BaseDatos/crear_tablas.sql` y ubicar las claves foraneas.
2. Crear un lector y un libro con un ejemplar.
3. Crear un prestamo y consultar el listado: los ejemplares disponibles deben pasar a cero.
4. Intentar otro prestamo del mismo libro y observar el error controlado.
5. Registrar la devolucion y consultar nuevamente la disponibilidad.
6. Probar actualizar/eliminar datos y la opcion 14 para observar polimorfismo.
