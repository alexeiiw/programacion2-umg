# Caso de Uso: Sistema de Ingreso de Notas

## Actor

- Docente.

## Caso de uso principal

**Registrar y consultar notas del estudiante**

## Flujo principal

1. El docente selecciona o registra un estudiante.
2. El docente ingresa actividad, parcial y proyecto.
3. El sistema valida que las notas estén entre 0 y 100.
4. El sistema calcula el promedio.
5. El sistema determina `Aprobado` o `Reprobado`.
6. El sistema muestra el resultado.

## Excepción

Si una nota está fuera del rango permitido, el sistema informa el error y no registra las notas inválidas.
