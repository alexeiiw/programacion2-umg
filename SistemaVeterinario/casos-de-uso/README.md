# Caso de Uso: Sistema de Control Veterinario

## Actor

- Personal de la clinica veterinaria.

## Caso de uso principal

**Registrar y consultar consulta veterinaria**

## Flujo principal

1. El personal selecciona o registra una mascota.
2. El personal indica el tipo de servicio.
3. El personal ingresa peso, temperatura y costo.
4. El sistema valida los datos clinicos y el costo.
5. El sistema calcula el total a pagar.
6. El sistema determina si la mascota esta estable o requiere observacion.
7. El sistema muestra el resultado de la consulta.

## Excepcion

Si el peso, la temperatura o el costo no cumplen las reglas, el sistema informa el error y no registra datos invalidos.
