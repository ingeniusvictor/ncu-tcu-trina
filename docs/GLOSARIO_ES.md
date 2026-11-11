# Glosario técnico ES para interfaz universal

Traducciones de trabajo; **deben cotejarse con los manuales originales** de cada fabricante y modelo. Nunca confundir el texto visible con una equivalencia demostrada de comando.

| Texto frecuente en inglés/chino | Propuesta en español de terreno | Nota |
|---|---|---|
| Network Control Unit (NCU) | Unidad de control de red (NCU) | Conservar sigla |
| Tracker Control Unit (TCU) | Unidad de control del seguidor (TCU) | Conservar sigla |
| Tracking mode | Modo de seguimiento | Confirmar modos disponibles |
| Manual mode | Modo manual | Operación protegida, no disponible v0.2 |
| Auto tracking | Seguimiento automático | No implica conexión real |
| Stow | Posición de resguardo | Puede depender de viento y estrategia |
| Wind stow | Resguardo por viento | Nunca modificar umbrales sin autorización |
| Actual angle | Ángulo actual | Grados; validar sentido de giro |
| Target angle | Ángulo objetivo | Grados; no equivale necesariamente a movimiento |
| Motor current | Corriente del motor | A, confirmar precisión y referencia |
| Communication timeout | Tiempo de espera agotado | Mostrar cuándo se obtuvo la última lectura |
| Overcurrent | Sobrecorriente | Verificar código de alarma |
| East / West limit | Límite este / oeste | Riesgo mecánico, no editable v0.2 |
| Clear alarms | Restablecer alarmas | Nunca borrar eventos sin permiso |
| Firmware version | Versión de firmware | Registrar al comprobar compatibilidad |
| Device address | Dirección de dispositivo | No confundir con IP |
| COM port | Puerto COM | Solo inventario en v0.2 |
| Baud rate | Velocidad de comunicación | bps; sin configuración serial real en v0.2 |
| Read parameters | Leer parámetros | Futuro: solo con protocolo verificado |
| Write parameters | Escribir parámetros | Fuera de alcance operativo de v0.2 |
| Calibration | Calibración | Fuera de alcance operativo de v0.2 |
| Alarm status | Estado de alarmas | Diferenciar activa, histórica, desconocida |
| Offline | Sin comunicación | No interpretar como equipo seguro |

## Reglas de UX
1. Etiquetar todas las lecturas como **REAL / DEMO**, con última actualización, unidad y fuente.
2. Evitar traducir siglas técnicas que el operador necesita identificar en manuales.
3. Diferenciar **sin comunicación**, **sin alarma** y **estado desconocido**.
4. Mostrar nombre común solo si la semántica y unidades coinciden; conservar la nomenclatura original en detalles avanzados.
5. No exponer botones de movimiento, escritura, calibración o borrado en una instalación sin validar.
