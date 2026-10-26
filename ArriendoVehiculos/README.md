# Sistema de Arriendo de Vehiculos

Solucion de referencia del Pre Test Final (Programacion 2, UMG Portales). Ejemplo de consola en C# que recorre el circuito completo de bases de datos con SQLite: creacion de tablas, clase de inicializacion, CRUD de clientes y vehiculos, y flujo de arriendos con devolucion y total.

## Requisitos y ejecucion

- .NET 6 SDK o posterior.
- Conexion a Internet al restaurar por primera vez el paquete NuGet.

Desde esta carpeta:

```powershell
dotnet restore
dotnet run
```

`Microsoft.Data.Sqlite` esta declarado en `ArriendoVehiculos.csproj`; NuGet descarga automaticamente el proveedor y las bibliotecas necesarias. No se deben copiar DLLs manualmente. Al iniciar, el programa crea la base y sus tablas si no existen.

## Tablas y relaciones

- `Clientes`: personas que pueden arrendar (Codigo y Documento unicos).
- `Vehiculos`: flota con placas unicas y tarifa diaria (CHECK mayor que cero).
- `Arriendos`: rango de fechas por cliente y vehiculo, con fecha de devolucion y total calculados al devolver.
- Claves foraneas con eliminacion en cascada y `CHECK` de coherencia de fechas.

Revise el esquema completo en `BaseDatos/crear_tablas.sql` (recurso embebido en el ensamblado).

## Clase de inicializacion

`Persistencia/InicializadorBaseDatos.cs` expone los 4 miembros del pre test: `RutaBaseDatos`, `CadenaConexion`, `Inicializar()` y `LeerScriptTablas()`. `Inicializar()` crea el directorio, abre la conexion con claves foraneas activas y ejecuta el script embebido sin importar cuantas veces se repita (`CREATE TABLE IF NOT EXISTS`).

La base se crea en `%LOCALAPPDATA%\UMG\ArriendoVehiculos\arriendos.db`; la variable de entorno `ARRIENDO_VEHICULOS_DB` permite apuntar a otra ruta para pruebas. Para reiniciar el ejemplo, cierre el programa y elimine el archivo `.db`.

## CRUD y reglas

- Clientes y vehiculos: agregar, listar y eliminar con consultas parametrizadas y control de duplicados (codigo/documento/placa).
- Arriendos: crear (valida fechas y comprueba que no exista un arriendo activo del vehiculo cuyo rango se solape), listar con estado (`Arrendado`, `Vencido`, `Devuelto`), devolver (guarda la fecha y calcula `Total = dias x TarifaDiaria`) y eliminar.
- El total se calcula al momento de la devolucion con la tarifa del vehiculo arrendado.

Las operaciones estan separadas en `Persistencia/RepositorioArriendoVehiculos.cs`; el menu interactivo esta en `Aplicacion/MenuArriendoVehiculos.cs`.

## POO y manejo de errores

- `Persona` es abstracta y `Cliente` hereda de ella; `ObtenerInformacion()` muestra polimorfismo (opcion 10 del menu).
- `Vehiculo` encapsula y valida tarifa, placa y modelo.
- `try-catch` en el menu controla fechas invalidas, duplicados, solapamientos y restricciones SQLite sin cerrar la aplicacion.

## Recorrido sugerido para clase

1. Leer `BaseDatos/crear_tablas.sql` y ubicar llaves foraneas y checks.
2. Agregar un cliente y un vehiculo con tarifa.
3. Crear un arriendo y listar: el vehiculo pasa a `Arrendado`.
4. Intentar otro arriendo del mismo vehiculo en fechas que se solapen y observar el error controlado.
5. Devolver el arriendo y verificar el `Total` calculado y la disponibilidad.
6. Probar la opcion 10 para observar el polimorfismo.
