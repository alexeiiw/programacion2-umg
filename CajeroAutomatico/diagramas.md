# Diagramas Del Proyecto Cajero Automatico

Estos diagramas documentan el analisis y diseno del proyecto. GitHub puede mostrar los diagramas Mermaid directamente desde este archivo.

## Diagrama De Clases

```mermaid
classDiagram
    class Cliente {
        -String nombre
        -String documento
        +getNombre() String
        +getDocumento() String
    }

    class CuentaBancaria {
        -String numero
        -Cliente titular
        -double saldo
        -List~Transaccion~ historial
        +depositar(double monto)
        +retirar(double monto) boolean
        +transferirA(CuentaBancaria destino, double monto) boolean
        +registrarTransaccion(Transaccion transaccion)
    }

    class Tarjeta {
        -String numero
        -String pin
        -boolean bloqueada
        -int intentosFallidos
        +validarPin(String pinIngresado) boolean
        +cambiarPin(String pinActual, String pinNuevo)
    }

    class Banco {
        -String nombre
        -Map tarjetas
        -Map cuentas
        +registrarCuenta(CuentaBancaria cuenta)
        +registrarTarjeta(Tarjeta tarjeta)
        +buscarTarjeta(String numeroTarjeta) Tarjeta
        +buscarCuenta(String numeroCuenta) CuentaBancaria
    }

    class CajeroAutomatico {
        -Banco banco
        -Tarjeta tarjetaActual
        +autenticar(String numeroTarjeta, String pin) boolean
        +consultarSaldo() double
        +depositar(double monto) Recibo
        +retirar(double monto) Recibo
        +transferir(String numeroCuentaDestino, double monto) Recibo
    }

    class Transaccion {
        <<abstract>>
        -int codigo
        -LocalDateTime fecha
        -CuentaBancaria cuentaOrigen
        -double monto
        +ejecutar() boolean
        +getTipo() String
        +generarRecibo(boolean exitosa) Recibo
    }

    class Deposito
    class Retiro
    class Transferencia {
        -CuentaBancaria cuentaDestino
    }
    class Recibo
    class Simulador

    CuentaBancaria --> Cliente
    CuentaBancaria --> Transaccion
    Tarjeta --> CuentaBancaria
    Banco --> Tarjeta
    Banco --> CuentaBancaria
    CajeroAutomatico --> Banco
    CajeroAutomatico --> Tarjeta
    Transaccion --> CuentaBancaria
    Transaccion <|-- Deposito
    Transaccion <|-- Retiro
    Transaccion <|-- Transferencia
    Transaccion --> Recibo
    Transferencia --> CuentaBancaria
    Simulador --> CajeroAutomatico
```

## Diagrama De Casos De Uso

```mermaid
flowchart LR
    Usuario((Usuario))
    Autenticarse[Autenticarse]
    Consultar[Consultar saldo]
    Retirar[Retirar dinero]
    Depositar[Depositar dinero]
    Transferir[Transferir dinero]
    Recibo[Generar recibo]
    Banco[Sistema bancario]

    Usuario --> Autenticarse
    Usuario --> Consultar
    Usuario --> Retirar
    Usuario --> Depositar
    Usuario --> Transferir
    Consultar --> Banco
    Retirar --> Banco
    Depositar --> Banco
    Transferir --> Banco
    Retirar --> Recibo
    Depositar --> Recibo
    Transferir --> Recibo
```

## Diagrama De Actividades Con Carriles

Este es el diagrama de actividades con carriles de responsabilidad. Cada carril representa quien participa en una parte del proceso.

```mermaid
flowchart LR
    subgraph Usuario
        U1[Ingresa tarjeta y PIN]
        U2[Selecciona operacion]
        U3[Lee saldo o recibo]
    end

    subgraph CajeroAutomatico
        C1[Recibe datos de autenticacion]
        C2[Solicita validacion de tarjeta]
        C3[Solicita operacion]
        C4[Genera respuesta al usuario]
    end

    subgraph Banco
        B1[Busca tarjeta]
        B2[Busca cuenta destino si aplica]
    end

    subgraph Tarjeta
        T1[Valida PIN]
        T2{PIN correcto?}
        T3[Bloquea si falla 3 veces]
    end

    subgraph CuentaBancaria
        CB1[Consulta saldo]
        CB2[Valida fondos]
        CB3[Actualiza saldo]
        CB4[Registra historial]
    end

    subgraph Transaccion
        TR1[Crea deposito retiro o transferencia]
        TR2[Ejecuta operacion]
        TR3[Genera recibo]
    end

    U1 --> C1
    C1 --> B1
    B1 --> C2
    C2 --> T1
    T1 --> T2
    T2 -- No --> T3
    T3 --> C4
    T2 -- Si --> U2
    U2 --> C3
    C3 --> TR1
    TR1 --> TR2
    TR2 --> CB1
    TR2 --> CB2
    CB2 --> CB3
    CB3 --> CB4
    C3 --> B2
    CB4 --> TR3
    TR3 --> C4
    C4 --> U3
```

## Diagrama De Flujo Principal

```mermaid
flowchart TD
    A[Inicio] --> B[Crear banco]
    B --> C[Crear clientes]
    C --> D[Crear cuentas]
    D --> E[Crear tarjeta]
    E --> F[Registrar datos en banco]
    F --> G[Crear cajero automatico]
    G --> H[Autenticar tarjeta]
    H --> I{Autenticacion correcta?}
    I -- No --> J[Mostrar error]
    I -- Si --> K[Consultar saldo inicial]
    K --> L[Retirar]
    L --> M[Depositar]
    M --> N[Transferir]
    N --> O[Probar retiro rechazado]
    O --> P[Mostrar historial]
    P --> Q[Mostrar saldo final]
    Q --> R[Fin]
    J --> R
```

## Modelo Entidad-Relacion Conceptual

El proyecto no usa base de datos, pero este modelo ayuda a entender las entidades principales si luego se quisiera persistir informacion.

```mermaid
erDiagram
    CLIENTE ||--o{ CUENTA_BANCARIA : posee
    CUENTA_BANCARIA ||--|| TARJETA : asociada
    BANCO ||--o{ CUENTA_BANCARIA : administra
    BANCO ||--o{ TARJETA : emite
    CUENTA_BANCARIA ||--o{ TRANSACCION : registra
    TRANSACCION ||--o| RECIBO : genera

    CLIENTE {
        string nombre
        string documento
    }

    CUENTA_BANCARIA {
        string numero
        double saldo
    }

    TARJETA {
        string numero
        string pin
        boolean bloqueada
    }

    BANCO {
        string nombre
    }

    TRANSACCION {
        int codigo
        datetime fecha
        double monto
        string tipo
    }

    RECIBO {
        boolean exitosa
    }
```
