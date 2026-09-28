# Diagrama de Flujo: Sistema de Control Veterinario

```text
[INICIO]
    |
    v
[Crear mascota y consulta]
    |
    v
[Ingresar peso, temperatura y costo]
    |
    v
¿Datos validos?
    | SI                                      | NO
    v                                         v
[Calcular total]                       [Capturar error]
    |                                         |
    v                                         v
¿Temperatura >= 39.5?                  [Mostrar error]
    | SI               | NO                   |
    v                  v                      v
[Requiere observacion] [Estable]            [FIN]
    |                  |
    +--------v---------+
          [Mostrar resultado]
                 |
                 v
                [FIN]
```
