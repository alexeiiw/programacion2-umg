# Sistema de Gestion Academica

Ejemplo didactico en C# para administrar estudiantes, cursos e inscripciones usando operaciones CRUD y una base de datos SQLite local. El proyecto muestra clases, encapsulamiento, abstraccion, herencia, polimorfismo, relaciones entre entidades y manejo de errores con `try-catch`.

## Requisitos

- .NET 6 SDK o posterior.
- Conexion a Internet durante la primera restauracion del paquete NuGet.

## Descargar y ejecutar

Clonar el repositorio:

```powershell
git clone https://github.com/alexeiiw/programacion2-umg.git
cd programacion2-umg\GestionAcademica
dotnet restore
dotnet run
```

Tambien puede abrirse `GestionAcademica.csproj` desde Visual Studio y ejecutar el proyecto.

`Microsoft.Data.Sqlite` esta declarado en el `.csproj`. `dotnet restore` descarga SQLite y sus dependencias automaticamente; no se requiere descargar o copiar DLLs manualmente. Al iniciar, el programa crea las tablas si no existen.

La base de datos se crea en:

```text
%LOCALAPPDATA%\UMG\GestionAcademica\academico.db
```

Para ejecutar pruebas con una base independiente, se puede definir la variable de entorno `GESTION_ACADEMICA_DB` con la ruta completa a un archivo `.db`; si no se define, se utiliza la ubicación predeterminada anterior.

Para reiniciar los datos de practica, cierre el programa y elimine ese archivo.

## Estructura del proyecto

```text
GestionAcademica/
├── Aplicacion/MenuAcademico.cs          -> Menu y captura de datos
├── BaseDatos/crear_tablas.sql            -> Tablas, claves, restricciones e indices
├── Dominio/Persona.cs                    -> Clase abstracta
├── Dominio/Estudiante.cs                 -> Herencia y polimorfismo
├── Dominio/Curso.cs                      -> Entidad y validaciones
├── Dominio/Inscripcion.cs                -> Relacion estudiante-curso
├── Persistencia/InicializadorBaseDatos.cs -> Creacion de base y tablas
├── Persistencia/RepositorioAcademico.cs   -> Consultas SQL y CRUD parametrizados
├── Program.cs
└── GestionAcademica.csproj                -> Referencia NuGet de SQLite
```

## Tablas y relaciones

- `Estudiantes`: codigo, nombre, correo y carrera.
- `Cursos`: codigo, nombre, creditos y cupo.
- `Inscripciones`: relacion entre estudiante y curso con fecha.
- Las claves foraneas impiden inscripciones invalidas; al borrar un estudiante o curso se borran sus inscripciones.
- La combinacion estudiante-curso es unica para impedir inscripciones repetidas.
- La aplicacion valida que no se exceda el cupo del curso.

El script completo y comentable esta en `BaseDatos/crear_tablas.sql`.

## Operaciones CRUD

El menu permite crear, listar, actualizar y eliminar estudiantes, cursos e inscripciones. Los metodos SQL correspondientes estan agrupados en `RepositorioAcademico.cs` y usan parametros para evitar concatenar entradas del usuario en las consultas.

## POO y manejo de errores

- **Abstraccion:** `Persona` declara el rol y la informacion que deben proporcionar sus clases derivadas.
- **Herencia:** `Estudiante` hereda codigo, nombre y correo de `Persona`.
- **Polimorfismo:** `Persona` puede referirse a un objeto `Estudiante`; la opcion 13 demuestra la llamada sobrescrita.
- **Encapsulamiento:** las entidades validan sus datos al construirse y exponen propiedades de solo lectura.
- **Asociacion:** `Inscripcion` relaciona un estudiante con un curso; SQLite mantiene la relacion persistente.
- **`try-catch`:** el menu captura errores de entrada, reglas de negocio y base de datos sin terminar inesperadamente.

## Pasos sugeridos para explorar

1. Revise `BaseDatos/crear_tablas.sql` y dibuje las relaciones.
2. Ejecute `dotnet run` y cree un estudiante y dos cursos.
3. Liste los datos para identificar sus Id.
4. Inscriba al estudiante en un curso y liste las inscripciones.
5. Intente repetir la inscripcion o superar el cupo para ver las validaciones y el `try-catch`.
6. Pruebe actualizar y eliminar registros.
7. Elija la opcion 13 para observar el polimorfismo.
