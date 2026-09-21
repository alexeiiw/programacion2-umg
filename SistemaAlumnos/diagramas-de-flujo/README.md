# Diagrama de Flujo: Sistema de Ingreso de Notas

```text
[INICIO]
    |
    v
[Crear estudiante y registro]
    |
    v
[Ingresar actividad, parcial y proyecto]
    |
    v
¿Todas las notas están entre 0 y 100?
    | SI                         | NO
    v                            v
[Calcular promedio]       [Capturar error]
    |                            |
    v                            v
¿Promedio >= 60?             [Mostrar error]
    | SI          | NO            |
    v             v              v
[Aprobado]  [Reprobado]       [FIN]
    |             |
    +------v------+
       [Mostrar resultado]
              |
              v
             [FIN]
```
