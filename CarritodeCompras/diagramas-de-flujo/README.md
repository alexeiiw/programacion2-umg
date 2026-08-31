# Diagramas de Flujo - Sistema Carrito de Compras

Este módulo contiene el diagrama de flujo detallado del proceso de compra en comercio electrónico aplicando rigurosamente las reglas de negocio del curso.

El archivo principal editable se encuentra en:
👉 `diagramas-de-flujo.drawio` *(compatible con Draw.io, app.diagrams.net y extensiones de VS Code)*

---

## 1. Reglas de Negocio Implementadas

1. **Regla 1 (Autenticación Obligatoria de Cliente)**:
   * Antes de procesar el pago o la orden, el sistema evalúa si el usuario es un `Cliente` registrado y con sesión activa.
   * Si no está registrado, se deriva al flujo de Registro / Inicio de Sesión antes de permitir el cobro.

2. **Regla 2 (Validación de Inventario / Stock)**:
   * Al seleccionar un producto y cantidad $N$, el sistema valida `Stock >= N && Stock > 0`.
   * Si no hay existencias suficientes, se emite una alerta de inventario insuficiente y se impide agregar el ítem al carrito o comprarlo.
   * Al concretarse el pago, las existencias se descuentan en tiempo real (`Stock = Stock - Cantidad`).

3. **Regla 3 (Exclusividad de Pago con Tarjeta de Crédito)**:
   * La compra solo puede realizarse mediante `Tarjeta de Crédito`.
   * Si el usuario selecciona cualquier otro método (efectivo, cheque, etc.), el sistema rechaza la operación.
   * Se valida el número, fecha de vencimiento, CVV y cupo/límite disponible contra el total con impuestos.

---

## 2. Estructura de Pasos del Flujo de Compra

```
[ INICIO ]
    │
    ▼
[ Consultar catálogo de productos ]
    │
    ▼
[ Seleccionar producto y cantidad N ]
    │
    ▼
{ ¿Stock >= N y Stock > 0? } ──(NO)──► [ Alerta: Stock insuficiente ] ──► (Reintentar)
    │ (SÍ)
    ▼
[ Agregar al Carrito de Compras ]
    │
    ▼
{ ¿Desea agregar otro producto? } ──(SÍ)──► (Volver a Catálogo)
    │ (NO)
    ▼
[ Calcular Subtotal, IVA (12%) y Total ]
    │
    ▼
{ ¿Cliente registrado y autenticado? } ──(NO)──► [ Registrar / Iniciar Sesión ]
    │ (SÍ)
    ▼
[ Seleccionar Método de Pago ]
    │
    ▼
{ ¿Es Tarjeta de Crédito? } ──(NO)──► [ Rechazo: Solo Tarjeta de Crédito ]
    │ (SÍ)
    ▼
[ Capturar datos de Tarjeta ]
    │
    ▼
{ ¿Tarjeta válida y cupo >= Total? } ──(NO)──► [ Alerta: Tarjeta rechazada / Fondos ]
    │ (SÍ)
    ▼
[ Procesar cobro en Tarjeta ]
    │
    ▼
[ Descontar inventario en tiempo real ]
    │
    ▼
[ Generar Comprobante de Compra ]
    │
    ▼
[ Vaciar Carrito ]
    │
    ▼
[ FIN ]
```
