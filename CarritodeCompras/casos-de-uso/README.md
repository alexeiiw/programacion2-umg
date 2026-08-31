# Especificación de Casos de Uso - Sistema Carrito de Compras

Este módulo contiene el modelado de casos de uso para el sistema de comercio electrónico y carrito de compras.

El archivo principal editable se encuentra en:
👉 `casos-de-uso.drawio` *(compatible con Draw.io, app.diagrams.net y extensiones de VS Code)*

---

## 1. Actores del Sistema

| Actor | Tipo | Descripción |
| :--- | :--- | :--- |
| **Visitante** | Primario | Usuario no autenticado que navega el catálogo, consulta productos, solicita cotizaciones o se registra. |
| **Cliente Registrado** | Primario (Hereda de Visitante) | Usuario autenticado con credenciales activas, datos personales y tarjeta de crédito vinculada. Puede realizar el proceso de compra (Checkout). |
| **Pasarela de Pagos / Banco** | Secundario / Externo | Sistema externo encargado de la validación de la tarjeta de crédito, verificación de fondos/cupo disponible y procesamiento de la transacción financiera. |

---

## 2. Matriz de Casos de Uso

| Código | Caso de Uso | Actor(es) | Descripción | Relaciones |
| :--- | :--- | :--- | :--- | :--- |
| **CU-01** | **Registrar Usuario** | Visitante | Permite a un nuevo usuario crear una cuenta en el sistema ingresando nombre, documento de identidad, correo y contraseña. | - |
| **CU-02** | **Consultar Catálogo** | Visitante, Cliente | Permite explorar la lista de productos disponibles, visualizando código, nombre, precio unitario y existencias (stock). | - |
| **CU-03** | **Cotizar Productos** | Visitante, Cliente | Permite seleccionar productos y cantidades para calcular el subtotal, el impuesto (IVA 12%) y el total proyectado sin descontar stock ni requerir pago. | - |
| **CU-04** | **Comprar (Checkout)** | Cliente Registrado | Proceso transaccional completo para consolidar el carrito, validar inventario, cobrar mediante tarjeta de crédito y emitir comprobante. | `«include»` Validar Inventario<br>`«include»` Pagar con Tarjeta<br>`«include»` Verificar Autenticación<br>`«include»` Emitir Comprobante |
| **CU-05** | **Pagar con Tarjeta de Crédito** | Cliente Registrado, Pasarela de Pagos | Valida la tarjeta ingresada (número, fecha de expiración, CVV, límite de crédito) y procesa el cargo total de la orden. | `«included by»` CU-04 |

---

## 3. Reglas de Negocio Incorporadas

1. **RN-01 (Autenticación Obligatoria para Compra)**: Ningún visitante puede concretar una compra sin haber iniciado sesión como cliente registrado.
2. **RN-02 (Validación Estricta de Inventario)**: Solo se permite comprar productos con stock suficiente (`stock >= cantidad`). Si el stock es insuficiente, se bloquea la orden y se notifica al usuario.
3. **RN-03 (Exclusividad de Pago con Tarjeta de Crédito)**: El único medio de pago aceptado para procesar la transacción es Tarjeta de Crédito.
4. **RN-04 (No Reserva en Cotización)**: Las cotizaciones son informativas y no congelan ni descuentan inventario.
