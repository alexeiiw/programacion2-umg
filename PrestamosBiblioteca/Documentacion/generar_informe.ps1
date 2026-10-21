$ErrorActionPreference = 'Stop'

$raiz = Resolve-Path (Join-Path $PSScriptRoot '..')
$salidaHtml = Join-Path $PSScriptRoot 'Informe_PrestamosBiblioteca.html'
$salidaPdf = Join-Path $PSScriptRoot 'Informe_PrestamosBiblioteca.pdf'

function ConvertTo-HtmlCode([string] $texto) {
    $t = $texto -replace '&', '&amp;'
    $t = $t -replace '<', '&lt;'
    $t = $t -replace '>', '&gt;'
    return $t
}

function New-CodeBlock([string] $rutaRelativa) {
    $rutaCompleta = Join-Path $raiz $rutaRelativa
    $contenido = Get-Content -LiteralPath $rutaCompleta -Raw
    $lineas = ($contenido -replace "`r`n", "`n") -split "`n"
    if ($lineas.Length -gt 0 -and $lineas[-1] -eq '') {
        $lineas = $lineas[0..($lineas.Length - 2)]
    }

    $sb = New-Object System.Text.StringBuilder
    $i = 0
    foreach ($linea in $lineas) {
        $i++
        $esc = ConvertTo-HtmlCode $linea
        if ($esc -match '//') {
            $esc = $esc -replace '(//.*)$', '<span class="cm">$1</span>'
        }
        [void]$sb.AppendLine("<div class=`"line`"><span class=`"ln`">$i</span><span class=`"lc`">$esc</span></div>")
    }
    return $sb.ToString().TrimEnd("`r", "`n")
}

$archivos = @(
    @{ Ruta = 'PrestamosBiblioteca.csproj'; Capa = 'Configuracion del proyecto'; Desc = 'Proyecto .NET 6 de consola. Declara el paquete NuGet Microsoft.Data.Sqlite 6.0.36 (que aporta el proveedor SQLite y sus dependencias) y embebe el script de tablas como recurso.' },
    @{ Ruta = 'Program.cs'; Capa = 'Punto de entrada'; Desc = 'Inicia la aplicacion: crea la base de datos, construye el repositorio y arranca el menu. Envuelve el arranque en try-catch.' },
    @{ Ruta = 'Dominio/Persona.cs'; Capa = 'Dominio (POO)'; Desc = 'Clase abstracta base con codigo, nombre y correo. Define el metodo abstracto ObtenerTipo y la validacion de texto comun.' },
    @{ Ruta = 'Dominio/Lector.cs'; Capa = 'Dominio (POO)'; Desc = 'Hereda de Persona. Agrega el telefono y sobrescribe ObtenerInformacion para demostrar herencia y polimorfismo.' },
    @{ Ruta = 'Dominio/Libro.cs'; Capa = 'Dominio (POO)'; Desc = 'Entidad del catalogo: codigo, titulo, autor y cantidad de ejemplares. Encapsula y valida sus datos.' },
    @{ Ruta = 'Dominio/Prestamo.cs'; Capa = 'Dominio (POO)'; Desc = 'Representa la relacion lector-libro con fechas. Valida que las fechas sean coherentes y expone el estado del prestamo.' },
    @{ Ruta = 'Persistencia/InicializadorBaseDatos.cs'; Capa = 'Persistencia (SQLite)'; Desc = 'Determina la ubicacion del archivo .db, abre la conexion con claves foraneas activas y ejecuta el script SQL de creacion de tablas.' },
    @{ Ruta = 'Persistencia/RepositorioBiblioteca.cs'; Capa = 'Persistencia (SQLite)'; Desc = 'Contiene las operaciones CRUD de lectores, libros y prestamos, la validacion de disponibilidad, el registro de devoluciones y el uso de transacciones. Todas las consultas usan parametros.' },
    @{ Ruta = 'Aplicacion/MenuBiblioteca.cs'; Capa = 'Aplicacion (consola)'; Desc = 'Menu interactivo: captura datos, invoca al repositorio, presenta tablas y controla errores con try-catch.' },
    @{ Ruta = 'BaseDatos/crear_tablas.sql'; Capa = 'Base de datos'; Desc = 'Script SQL con las tablas Lectores, Libros y Prestamos, claves foraneas, restricciones CHECK e indices.' },
    @{ Ruta = 'README.md'; Capa = 'Documentacion'; Desc = 'Instrucciones de instalacion, ejecucion, descripcion de tablas, reglas de negocio y recorrido sugerido para clase.' }
)

$totalArchivos = $archivos.Count
$totalLineas = 0
foreach ($a in $archivos) {
    $rutaCompleta = Join-Path $raiz $a.Ruta
    $totalLineas += ((Get-Content -LiteralPath $rutaCompleta).Count)
}

$indiceItems = New-Object System.Text.StringBuilder
$secciones = New-Object System.Text.StringBuilder
$n = 0
foreach ($a in $archivos) {
    $n++
    $ancla = "archivo-$n"
    [void]$indiceItems.AppendLine("<li><span class=`"toc-num`">$n.</span> <span class=`"toc-name`">$($a.Ruta)</span> <span class=`"toc-layer`">[$($a.Capa)]</span></li>")
    $codigo = New-CodeBlock $a.Ruta
    [void]$secciones.AppendLine(@"
<section class="file" id="$ancla">
  <div class="file-head">
    <span class="file-num">Archivo $n de $totalArchivos</span>
    <h3>$($a.Ruta)</h3>
    <span class="file-layer">$($a.Capa)</span>
  </div>
  <p class="file-desc">$($a.Desc)</p>
  <div class="code">$codigo</div>
</section>
"@)
}

$css = @'
@page { size: Letter; margin: 16mm 14mm; }
* { box-sizing: border-box; }
body { font-family: "Segoe UI", Arial, Helvetica, sans-serif; color: #1c2733; font-size: 10.5pt; line-height: 1.5; margin: 0; }
h1, h2, h3, h4 { color: #0b2a4a; margin: 0 0 6px 0; }
h2 { font-size: 15pt; border-bottom: 2px solid #0b2a4a; padding-bottom: 4px; margin-top: 6px; }
h3 { font-size: 12.5pt; }
p { margin: 0 0 8px 0; text-align: justify; }
.cover { height: 245mm; display: flex; flex-direction: column; justify-content: space-between; page-break-after: always; }
.cover-top { text-align: center; border-bottom: 3px solid #0b2a4a; padding-bottom: 14px; }
.cover-top .uni { font-size: 17pt; font-weight: 700; letter-spacing: 1px; color: #0b2a4a; }
.cover-top .sede { font-size: 11pt; color: #33506e; margin-top: 4px; }
.cover-mid { text-align: center; margin-top: 30mm; }
.cover-mid .kicker { text-transform: uppercase; letter-spacing: 3px; color: #8a5a00; font-size: 10pt; }
.cover-mid .titulo { font-size: 27pt; font-weight: 800; color: #0b2a4a; margin: 10px 0; line-height: 1.15; }
.cover-mid .subtitulo { font-size: 13pt; color: #33506e; }
.cover-mid .badge { display: inline-block; margin-top: 18px; border: 2px solid #0b2a4a; color: #0b2a4a; border-radius: 6px; padding: 6px 16px; font-weight: 700; font-size: 11pt; }
.cover-data { margin: 0 auto; width: 78%; border: 1px solid #c9d4e0; border-radius: 8px; overflow: hidden; }
.cover-data table { width: 100%; border-collapse: collapse; font-size: 10.5pt; }
.cover-data td { padding: 7px 12px; border-bottom: 1px solid #e2e8ef; }
.cover-data tr:last-child td { border-bottom: 0; }
.cover-data td.k { width: 38%; background: #f2f6fa; color: #33506e; font-weight: 600; }
.cover-foot { text-align: center; color: #6b7c90; font-size: 9.5pt; border-top: 1px solid #d7e0ea; padding-top: 10px; }
.section { page-break-before: always; }
.content > section:first-of-type { page-break-before: avoid; }
ul, ol { margin: 4px 0 10px 0; padding-left: 20px; }
li { margin-bottom: 3px; }
.toc { list-style: none; padding: 0; }
.toc li { display: flex; gap: 8px; padding: 5px 0; border-bottom: 1px dotted #cdd8e3; font-size: 10.5pt; }
.toc-num { color: #8a5a00; font-weight: 700; width: 26px; }
.toc-name { font-family: Consolas, "Courier New", monospace; }
.toc-layer { margin-left: auto; color: #6b7c90; font-size: 9pt; }
.grid2 { display: grid; grid-template-columns: 1fr 1fr; gap: 10px; }
.card { border: 1px solid #c9d4e0; border-radius: 8px; padding: 10px 12px; background: #fbfdff; }
.card h4 { margin-bottom: 4px; color: #0b2a4a; }
.tag { display: inline-block; background: #0b2a4a; color: #fff; border-radius: 4px; padding: 1px 8px; font-size: 9pt; margin-right: 4px; }
table.data { width: 100%; border-collapse: collapse; margin: 6px 0 12px 0; font-size: 10pt; }
table.data th { background: #0b2a4a; color: #fff; text-align: left; padding: 6px 9px; }
table.data td { border: 1px solid #d7e0ea; padding: 6px 9px; vertical-align: top; }
table.data tr:nth-child(even) td { background: #f5f8fb; }
.kpi { display: flex; gap: 10px; margin: 10px 0 14px 0; }
.kpi .box { flex: 1; border: 1px solid #c9d4e0; border-top: 3px solid #8a5a00; border-radius: 8px; padding: 10px; text-align: center; }
.kpi .num { font-size: 20pt; font-weight: 800; color: #0b2a4a; }
.kpi .lbl { font-size: 9pt; color: #6b7c90; text-transform: uppercase; letter-spacing: 1px; }
.file { page-break-before: always; }
.file-head { border-left: 6px solid #8a5a00; background: #f2f6fa; padding: 8px 12px; border-radius: 0 8px 8px 0; margin-bottom: 6px; }
.file-head h3 { font-family: Consolas, "Courier New", monospace; margin: 2px 0; }
.file-num { font-size: 8.5pt; text-transform: uppercase; letter-spacing: 1px; color: #8a5a00; font-weight: 700; }
.file-layer { font-size: 9pt; color: #33506e; }
.file-desc { font-size: 9.8pt; color: #33506e; margin-bottom: 8px; }
.code { font-family: Consolas, "Courier New", monospace; font-size: 8.4pt; line-height: 1.32; border: 1px solid #cfd9e4; border-radius: 6px; background: #fbfdff; padding: 8px 6px 8px 0; }
.line { display: flex; min-height: 1.32em; }
.line:nth-child(even) { background: #f4f8fc; }
.ln { flex: 0 0 34px; text-align: right; padding-right: 8px; color: #9aa8b8; user-select: none; }
.lc { flex: 1 1 auto; white-space: pre-wrap; word-break: break-word; padding-right: 6px; }
.cm { color: #1a7f37; font-style: italic; }
.note { border: 1px solid #e0c98a; background: #fdf8ec; border-radius: 6px; padding: 8px 12px; font-size: 10pt; }
code { font-family: Consolas, "Courier New", monospace; background: #eef3f8; padding: 1px 5px; border-radius: 4px; }
'@

$fecha = '09 de octubre de 2026'

$html = @"
<!DOCTYPE html>
<html lang="es">
<head>
<meta charset="UTF-8">
<title>Informe - Sistema de Prestamos de Biblioteca</title>
<style>
$css
</style>
</head>
<body>

<div class="cover">
  <div class="cover-top">
    <div class="uni">UNIVERSIDAD MARIANO GALVEZ DE GUATEMALA</div>
    <div class="sede">Centro Universitario Portales Domingo, Guatemala &middot; Ingenieria en Sistemas</div>
  </div>
  <div class="cover-mid">
    <div class="kicker">Programacion 2 &middot; Documentacion Tecnica</div>
    <div class="titulo">Sistema de Prestamos<br>de Biblioteca</div>
    <div class="subtitulo">Informe academico y anexo de codigo fuente en C# con SQLite</div>
    <div class="badge">Circuito completo de base de datos</div>
  </div>
  <div class="cover-data">
    <table>
      <tr><td class="k">Proyecto</td><td>Sistema de Prestamos de Biblioteca</td></tr>
      <tr><td class="k">Lenguaje / Plataforma</td><td>C# / .NET 6 (aplicacion de consola)</td></tr>
      <tr><td class="k">Motor de base de datos</td><td>SQLite mediante Microsoft.Data.Sqlite 6.0.36</td></tr>
      <tr><td class="k">Alcance</td><td>Tablas, CRUD, prestamos y devoluciones</td></tr>
      <tr><td class="k">Asignatura</td><td>Programacion 2</td></tr>
      <tr><td class="k">Fecha</td><td>$fecha</td></tr>
    </table>
  </div>
  <div class="cover-foot">
    Documento generado automaticamente a partir del codigo fuente del proyecto. Incluye la totalidad de los archivos.
  </div>
</div>

<div class="content">

<section>
  <h2>1. Resumen Ejecutivo</h2>
  <p>El presente informe documenta el proyecto <b>Sistema de Prestamos de Biblioteca</b>, desarrollado en lenguaje C# sobre la plataforma .NET 6 y con persistencia en una base de datos SQLite. Su proposito didactico es recorrer de forma completa el circuito de una base de datos: identificar entidades y relaciones, modelar las tablas, conectarse al motor desde el codigo, implementar las operaciones CRUD (crear, consultar, actualizar y eliminar) y aplicar reglas de negocio con manejo de errores controlado.</p>
  <p>El sistema administra tres entidades relacionadas: <b>Lectores</b>, <b>Libros</b> y <b>Prestamos</b>. Un prestamo asocia un lector con un libro e incluye las fechas de inicio, limite y devolucion. El sistema controla la disponibilidad de ejemplares, impide prestamos sin existencias y calcula el estado de cada prestamo (Prestado, Vencido o Devuelto).</p>

  <div class="kpi">
    <div class="box"><div class="num">$totalArchivos</div><div class="lbl">Archivos</div></div>
    <div class="box"><div class="num">3</div><div class="lbl">Tablas</div></div>
    <div class="box"><div class="num">4</div><div class="lbl">Capas</div></div>
    <div class="box"><div class="num">$totalLineas</div><div class="lbl">Lineas de codigo</div></div>
  </div>
</section>

<section>
  <h2>2. Objetivos</h2>
  <ul>
    <li>Modelar un caso de uso real mediante tablas relacionadas con claves foraneas.</li>
    <li>Implementar tecnicas de Programacion Orientada a Objetos: abstraccion, herencia, polimorfismo y encapsulamiento.</li>
    <li>Aplicar el patron de separacion por capas: dominio, persistencia y aplicacion.</li>
    <li>Ejecutar operaciones CRUD seguras usando consultas parametrizadas y transacciones.</li>
    <li>Controlar errores de entrada y de negocio mediante <code>try-catch</code> sin cerrar la aplicacion.</li>
    <li>Distribuir el proyecto de forma portable en SQLite, sin requerir un servidor de base de datos.</li>
  </ul>
</section>

<section>
  <h2>3. Arquitectura y Estructura</h2>
  <p>El proyecto se organiza en cuatro capas que separan las responsabilidades y facilitan el mantenimiento:</p>
  <div class="grid2">
    <div class="card"><h4>Dominio</h4><p>Clases de negocio: <code>Persona</code>, <code>Lector</code>, <code>Libro</code> y <code>Prestamo</code>. Contienen las reglas y validaciones.</p></div>
    <div class="card"><h4>Persistencia</h4><p><code>InicializadorBaseDatos</code> crea la base y las tablas; <code>RepositorioBiblioteca</code> ejecuta el CRUD sobre SQLite.</p></div>
    <div class="card"><h4>Aplicacion</h4><p><code>MenuBiblioteca</code> presenta las opciones, captura datos y muestra resultados en consola.</p></div>
    <div class="card"><h4>Configuracion</h4><p><code>Program.cs</code> y el archivo <code>.csproj</code> arrancan la aplicacion y declaran la dependencia de SQLite.</p></div>
  </div>

  <h3 style="margin-top:14px;">Inventario de archivos del codigo</h3>
  <table class="data">
    <tr><th style="width:52%;">Archivo</th><th>Capa / Rol</th></tr>
    $(($archivos | ForEach-Object { "<tr><td><code>$($_.Ruta)</code></td><td>$($_.Capa)</td></tr>" }) -join "`n    ")
  </table>
</section>

<section>
  <h2>4. Diseno de la Base de Datos</h2>
  <p>La base de datos SQLite <code>biblioteca.db</code> se crea automaticamente al iniciar la aplicacion y se ubica en <code>%LOCALAPPDATA%\UMG\PrestamosBiblioteca\</code>. El esquema se compone de tres tablas.</p>

  <table class="data">
    <tr><th>Tabla</th><th>Campos principales</th><th>Clave / Restricciones</th></tr>
    <tr><td><b>Lectores</b></td><td>Id, Codigo, Nombre, Correo, Telefono</td><td>Clave primaria Id; Codigo y Correo unicos.</td></tr>
    <tr><td><b>Libros</b></td><td>Id, Codigo, Titulo, Autor, Ejemplares</td><td>Clave primaria Id; Codigo unico; Ejemplares &gt;= 0.</td></tr>
    <tr><td><b>Prestamos</b></td><td>Id, LectorId, LibroId, FechaPrestamo, FechaLimite, FechaDevolucion</td><td>Claves foraneas a Lectores y Libros; validacion de fechas; borrado en cascada.</td></tr>
  </table>

  <h4>Relaciones</h4>
  <ul>
    <li><b>Lector 1 --- N Prestamo:</b> un lector puede tener varios prestamos.</li>
    <li><b>Libro 1 --- N Prestamo:</b> un libro puede registrarse en varios prestamos a lo largo del tiempo.</li>
    <li>Los ejemplares <b>disponibles</b> de un libro equivalen a sus <code>Ejemplares</code> totales menos los prestamos activos (sin fecha de devolucion).</li>
  </ul>

  <div class="note">La opcion <b>ON DELETE CASCADE</b> elimina automaticamente los prestamos asociados cuando se borra un lector o un libro, preservando la integridad referencial. Las consultas usan parametros (<code>`$nombre</code>) para evitar la inyeccion SQL.</div>
</section>

<section>
  <h2>5. Reglas de Negocio y Flujo</h2>
  <ul>
    <li>No se registra un prestamo si el libro no tiene ejemplares disponibles.</li>
    <li>La fecha limite no puede ser anterior a la fecha del prestamo.</li>
    <li>La devolucion no puede ser anterior a la fecha del prestamo.</li>
    <li>Un ejemplar vuelve a estar disponible al registrar su devolucion.</li>
    <li>Los ejemplares de un libro no pueden reducirse por debajo de los prestamos activos.</li>
    <li>El estado del prestamo se determina como <code>Devuelto</code>, <code>Vencido</code> o <code>Prestado</code>.</li>
  </ul>
  <h4>Flujo principal</h4>
  <p>Registrar lector y libro &rarr; crear prestamo (valida disponibilidad) &rarr; consultar prestamos &rarr; registrar devolucion (libera el ejemplar) &rarr; consultar disponibilidad. Todo error se captura con <code>try-catch</code> y se informa al usuario.</p>
</section>

<section>
  <h2>6. Verificacion Realizada</h2>
  <ul>
    <li>Compilacion con <code>dotnet build</code>: 0 advertencias y 0 errores.</li>
    <li>Restauracion automatica de <code>Microsoft.Data.Sqlite</code> desde NuGet al ejecutar <code>dotnet restore</code>.</li>
    <li>Pruebas de CRUD de lectores, libros y prestamos.</li>
    <li>Rechazo controlado del prestamo cuando no hay ejemplares disponibles.</li>
    <li>Registro de devolucion y liberacion del ejemplar (estado <code>Devuelto</code>).</li>
    <li>Actualizaciones y eliminaciones con integridad referencial.</li>
    <li>Demostracion de herencia y polimorfismo mediante <code>Persona = new Lector(...)</code>.</li>
  </ul>
</section>

<section>
  <h2>7. Indice de Codigo Fuente</h2>
  <p>Cada archivo se presenta a continuacion numerado y con la totalidad de su contenido. Se incluyen los archivos de configuracion, las clases de dominio, la capa de persistencia, la capa de aplicacion, el script SQL y la documentacion.</p>
  <ul class="toc">
$($indiceItems.ToString().TrimEnd())
  </ul>
</section>

<section>
  <h2>8. Codigo Fuente Completo</h2>
  <p>Las secciones siguientes contienen el codigo fuente integro del proyecto, organizado en el mismo orden del indice.</p>
</section>

$($secciones.ToString())

<section>
  <h2>9. Conclusiones</h2>
  <p>El Sistema de Prestamos de Biblioteca cubre el circuito completo de una base de datos aplicada a un caso de uso real: desde el diseno del esquema relacional hasta la ejecucion de operaciones CRUD y reglas de negocio con manejo de errores. La separacion por capas, el uso de consultas parametrizadas y las transacciones constituyen buenas practicas que el estudiante puede reutilizar en proyectos posteriores.</p>
</section>

</div>
</body>
</html>
"@

[System.IO.File]::WriteAllText($salidaHtml, $html, (New-Object System.Text.UTF8Encoding($false)))
Write-Output "HTML generado: $salidaHtml"

$navegadores = @(
    'C:\Program Files\Google\Chrome\Application\chrome.exe',
    'C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe'
)
$navegador = $navegadores | Where-Object { Test-Path -LiteralPath $_ } | Select-Object -First 1
if (-not $navegador) { throw 'No se encontro Chrome ni Edge para generar el PDF.' }

$uri = ([System.Uri]$salidaHtml).AbsoluteUri
$perfil = Join-Path ([System.IO.Path]::GetTempPath()) ('informe-chrome-' + [guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Force -Path $perfil | Out-Null
$log = Join-Path $perfil 'chrome.log'
$estadoAnterior = $ErrorActionPreference
$ErrorActionPreference = 'Continue'
& $navegador --headless --disable-gpu --no-first-run --no-default-browser-check --user-data-dir="$perfil" --no-pdf-header-footer --print-to-pdf="$salidaPdf" $uri *> $log
$ErrorActionPreference = $estadoAnterior
Start-Sleep -Seconds 3
Remove-Item -LiteralPath $perfil -Recurse -Force -ErrorAction SilentlyContinue

if (Test-Path -LiteralPath $salidaPdf) {
    $info = Get-Item -LiteralPath $salidaPdf
    Write-Output "PDF generado: $salidaPdf ($([math]::Round($info.Length/1KB,1)) KB)"
} else {
    throw 'No se genero el PDF.'
}
