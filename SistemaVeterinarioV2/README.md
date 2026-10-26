# Sistema de Control Clínico Veterinario — Versión 2

Evolución del sistema veterinario del Proyecto 1. Conserva el registro de mascotas y consultas, las validaciones clínicas y la regla de estado (`Temperatura >= 39.5`: requiere observación; de lo contrario: estable). En esta versión, la información se guarda en SQLite y se administra desde formularios Windows Forms.

## Alcance

- Aplicación de escritorio desarrollada únicamente en C#.
- Formularios para administrar mascotas y consultas veterinarias.
- Operaciones CRUD (crear, consultar, actualizar y eliminar) para ambas tablas.
- Persistencia local con SQLite.
- Clave foránea desde consultas hacia mascotas; al eliminar una mascota, se eliminan sus consultas.
- Validación de peso mayor que cero, temperatura de 35 a 43 °C y costo mayor que cero.
- Estado clínico derivado de la temperatura.

## Estructura

```text
SistemaVeterinarioV2/
├── BaseDatos/crear_tablas.sql       -> Esquema SQL de las tablas
├── Dominio/                         -> SerVivo, Mascota y ConsultaVeterinaria
├── Persistencia/                    -> Inicialización SQLite y métodos CRUD
├── Presentacion/                    -> Formularios Windows Forms
├── Program.cs
└── SistemaVeterinarioV2.csproj      -> Dependencia Microsoft.Data.Sqlite
```

## Requisitos y ejecución

- Windows con .NET 6 SDK y runtime de escritorio.
- Conexión a Internet durante la primera restauración de paquetes NuGet.

Desde esta carpeta:

```powershell
dotnet restore
dotnet run
```

La referencia `Microsoft.Data.Sqlite` del archivo `.csproj` permite a NuGet descargar la biblioteca y sus dependencias automáticamente. No hace falta descargar DLLs manualmente. Al iniciar, la aplicación crea el directorio local, la base de datos `veterinaria.db` y las tablas definidas en `BaseDatos/crear_tablas.sql` si aún no existen.

Ubicación de la base de datos:

```text
%LOCALAPPDATA%\UMG\SistemaVeterinarioV2\veterinaria.db
```

El script SQL está incluido como recurso integrado y también disponible como archivo legible del esquema.
