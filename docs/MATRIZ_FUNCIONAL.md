# Matriz funcional universal — v0.2 alpha (11-11-2026)

**Base de evidencia:** inspección estática de cinco ejecutables de tres fabricantes aportados por el usuario. Los nombres de componentes y cadenas son **referencias presentes en los binarios**, no pruebas de funcionamiento de equipos físicos ni equivalencia de protocolos.

| Concepto | Trina NCU Monitor 2.71 | Trina TCU Multi 2.73 | Powerway Communication Box 2.161 | Powerway Tracker Test 5.2.2 | Arctech Tracker AI Controller (archivo 20230101-1) |
|---|---|---|---|---|---|
| Rol del software | NCU | TCU | Caja central / NCU equivalente | Tracker / TCU equivalente | **Tracker confirmado visualmente; referencias internas a ComBox sin pantalla verificada** |
| Tecnología UI | .NET WinForms | .NET WinForms | .NET WinForms | .NET WinForms | **.NET 4 + WPF** |
| COM / RS485 | Referencias | Referencias | Referencias | Referencias | Referencias explícitas |
| Radio / inalámbrica | LoRa en cadenas | LoRa en cadenas | ZigBee en cadenas | ZigBee en cadenas | LoRa, 433/915 y 2.4 GHz en cadenas |
| Viento / protección | Referencias | Referencias | Referencias | Referencias | `WindButton`, `WindProtection` |
| Ángulos y posición | Referencias | Referencias | Datos astronómicos | Ángulo de tracker | `AngleTextBlock`, `AIAngleButton` |
| Corriente de motor | No concluyente | Referencias | No concluyente | Referencias | `MotorCurTextBlock`, `MaxMotorCurTextBox` |
| Nieve / lluvia | Referencias | Referencias | Referencias | Referencias | `SnowButton`, `RainButton` |
| Alarmas | Referencias | Referencias | Referencias | Referencias | Indicadores de límite, corriente, batería, falla de ángulo |
| Escrituras (solo código original) | Referencias | Referencias | Referencias | Referencias | `WriteAllButton`, `WriteParaButton` |
| Funcionalidad real de protocolo en ORBI | No implementada | No implementada | No implementada | No implementada | No implementada |
| Paridad real de funciones NCU y TCU | Por validar | Por validar | Por validar | Por validar | **No acreditada: captura muestra Tracker, no CommBox autónomo** |

**Interpretación:** Arctech presenta una interfaz WPF diferente. El EXE contiene referencias internas a componentes CommBox, pero la única pantalla recibida muestra Tracker, LoRa y Scan y no una sección CommBox explícita. **No está demostrado que sustituya el software CommBox antiguo.** Más detalle: [referencia visual](REFERENCIA_UI_ARCTECH_TRACKER_2026-11-11.md).

## Huellas de archivos analizados
- Trina NCU Monitor 2.71 client: SHA-256 `d6811a599d95a9f68fb979f302bf4ddc5e81ad9c71d6796020280ac4b4b298ea`.
- Trina TCU Multi (80 canales) 2.73: SHA-256 `31c1b330a0686795e56fdc803fa69cd05072aea44bea7ce6f2a34ad276ea35a7`.
- Powerway Communication Box V2.161: SHA-256 `56a796de8c0a7daf62702c204f1a5d93445395742998bb55e8f8d2f5a1a64458`.
- Powerway Tracker Test for Customer V5.2.2: SHA-256 `9f40ff0ce187cb811c9ed7c92a73c0e966db00b0a1f381ec78bc791d9655de48`.
- Arctech Tracker AI Controller (archivo 20230101-1): SHA-256 `87c8c5518920c08ff056d15a7bf71bc6204f7b02cce11dcb23133e4e55f6d34c`.

Ver [Análisis Trina (estado inicial)](ARQUITECTURA.md), [Análisis Powerway](ANALISIS_POWERWAY_2026-11-11.md), [Análisis Arctech](ANALISIS_ARCTECH_2026-11-11.md) y [Pendientes para campo](NECESITO_DE_TI.md).

**No se incluyen ejecutables de terceros en este repositorio público.**
