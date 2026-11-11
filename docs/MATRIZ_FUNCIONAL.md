# Matriz funcional universal — v0.2 alpha (11-11-2026)

**Base de evidencia:** inspección estática de cuatro ejecutables .NET originales aportados por un técnico O&M. Funciones listadas son **referencias presentes en textos o metadatos del software**: no implican hardware compatible, validación de protocolo ni comportamiento comprobado.

| Concepto | Trina NCU 2.71 | Trina TCU 2.73 Multi | Powerway Communication Box 2.161 | Powerway Tracker Test 5.2.2 | Artech/Arctech |
|---|---|---|---|---|---|
| Equivalencia operativa | NCU | TCU | NCU/caja central | TCU/seguidor | Por confirmar |
| Plataforma | .NET WinForms | .NET WinForms | .NET WinForms CLR 2 | .NET WinForms CLR 4 | Pendiente |
| COM / baudrate | Sí, referencias | Sí, referencias | Sí, referencias | Sí, referencias | Pendiente |
| Radio | LoRa mencionado | LoRa mencionado | ZigBee mencionado | ZigBee mencionado | Pendiente |
| Viento / protección | Referencias | Referencias | Referencias | Referencias | Pendiente |
| Ángulo / posición | Referencias | Referencias | Datos astronómicos | Referencias tracker | Pendiente |
| Motor / corriente | No concluyente | Referencias | No concluyente | Referencias | Pendiente |
| Nieve / lluvia | Referencias | Referencias | Nieve y viento | Nieve / lluvia | Pendiente |
| Alarmas | Referencias | Referencias | Comunicación y protección | Alarmas detalladas | Pendiente |
| Parámetros editables en software original | Sí, referencias | Sí, referencias | Sí, referencias | Sí, referencias | Pendiente |
| Integración real en ORBI | NO | NO | NO | NO | NO |

**Lo que sí comparten:** propósito de supervisión/configuración, campos técnicos y una interfaz Windows tradicional. **Lo que no se ha demostrado:** frames binarios, significados de registros, CRC, secuencia de autenticación, equivalencia de modelos, seguridad de escrituras, o que exista un OEM común.

## Ejecutables analizados (no se incluyen en el repositorio)
- Trina NCU Monitor 2.71 client: SHA-256 `d6811a599d95a9f68fb979f302bf4ddc5e81ad9c71d6796020280ac4b4b298ea`.
- Trina TCU V2.73 Multi (80 canales): SHA-256 `31c1b330a0686795e56fdc803fa69cd05072aea44bea7ce6f2a34ad276ea35a7`.
- Powerway Communication Box V2.161: SHA-256 `56a796de8c0a7daf62702c204f1a5d93445395742998bb55e8f8d2f5a1a64458`.
- Powerway Tracker Test for Customer V5.2.2: SHA-256 `9f40ff0ce187cb811c9ed7c92a73c0e966db00b0a1f381ec78bc791d9655de48`.

Más detalle: [Análisis Powerway](ANALISIS_POWERWAY_2026-11-11.md). Continuación: [Insumos necesarios](NECESITO_DE_TI.md).
