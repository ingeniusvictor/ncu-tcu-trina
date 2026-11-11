# Referencia visual — Trina Solar TCU V2.73 Multitype/Update2

**Fuente:** dos capturas suministradas por el operador, 11 de noviembre de 2026. Una muestra **Para Display**, otra **Simulations**. Este documento es un inventario visual para diseñar una interfaz en español. Posteriormente, el operador confirmó que las capturas fueron tomadas durante una intervención real con movimiento del tracker. No constituye validación de protocolo ni de cada lectura o confirmación de comando.

## Aclaración posterior del operador (contexto de terreno)

El operador confirmó expresamente, después de compartir las capturas, que **estaba físicamente junto al tracker realizando movimientos reales** con el programa de Trina. Por tanto, **no son únicamente pantallas de laboratorio o una demostración visual**: corresponden a una sesión real de trabajo en campo.

Esta declaración confirma el contexto operativo, pero **no demuestra por sí sola la semántica de cada indicador**. En particular, las etiquetas `Device connecting: Comm connecting` y `Command Status: OK` no aportan por sí mismas confirmación inequívoca de entrega, ejecución, posición alcanzada ni validez/frescura de todas las mediciones. No se registraron tramas, marca temporal de respuesta ni comparación entre posición física y pantalla.

**Consecuencia para ORBI:** diseñar indicadores separados de puerto abierto, controlador identificado, lectura válida con timestamp, comando enviado, acuse de recibo y movimiento verificado, sin asumir equivalencias hasta documentar el protocolo. Las pruebas no deben requerir movimientos adicionales ni interferir con la operación activa.

## Identidad y comunicación visibles
- Título de ventana: `TCU V2.73-Multitype_Update2`.
- Menú superior: `UserRole`, `Slave box`.
- Selector de idioma: `CN / EN` (no se ve opción en español).
- Selector de `TCU type`: `Multi` / `Crossbeam`; la captura tiene Crossbeam marcado.
- `Serial Port Setting`: `PortName`, `BaudRate`, estado con recuadro verde y botón `Close`, `Dev Addr`, `Get Addr`, `Refresh` (ms), `Stop Monitoring`.
- Barra de estado: `Role: Operator`; `Device connecting: Comm connecting`; `Command Status: OK`.

**Punto crítico:** la cadena `Comm connecting` y la cadena `Command Status: OK` pueden tener ámbitos distintos y no confirman por sí solas que los campos estén actualizados desde hardware. Ningún número ni parámetro mostrado se guarda como valor real o recomendado.

## Pestañas observadas

| Pestaña | Traducción propuesta | Evidencia |
|---|---|---|
| Para Display | Lecturas y parámetros | Captura observada |
| Para Setting | Configuración de parámetros | Solo nombre de pestaña, sin contenido |
| SaveData | Registro y exportación | Solo nombre de pestaña, sin contenido |
| Simulations | Simulación / maniobras | Captura observada; contiene órdenes potencialmente reales |

## Para Display: bloques de campos observados

| Grupo | Campos originales | Presentación española propuesta |
|---|---|---|
| Basic Information | Time, Edition, Workmode | Hora, edición del controlador, modo de trabajo |
| Angle Information | Elevation, Azimuth, Target, Actual, Stow, Snow, Wind, Cleaning, Night Reset, Hail | Ángulos solares, objetivo, actual y posiciones de protección/mantenimiento |
| Battery Para | Bat vol/percent, Temperature, Power Supply | Tensión/SoC de batería, temperatura, fuente de alimentación |
| Astronomic Para | Longitude, Latitude, TimeZone | Coordenadas astronómicas, zona horaria |
| Array Para | Width, Length, East Slope, West Slope | Geometría y pendientes de estructura |
| Smart Tracking | Ast Cal, Record, Optimize, Cal Switch | Cálculos/ajustes de seguimiento inteligente, interpretación pendiente de manual |
| Wireless Para | Lora Channel, Lora Airspeed, Lora Addr | Canal, velocidad de aire, dirección radio LoRa |
| Motor Para | Current, OverCurrent, Speed Set | Corriente de motor, límite de sobrecorriente, consigna de velocidad |
| Limit Para | East Limit, West Limit | Límites mecánicos/lógicos por orientación |
| Other Para | Offline Switch, Accuracy, Motor Polarity, Tilt Polarity | Opciones del controlador, tolerancia y polaridades |
| Stent Para | Stent Type, Wind Dir, Wind East, Wind West, Axis | Tipo estructura, dirección de viento, ángulos/variables de resguardo, eje |
| ALARM | Manual Stow, Hail, High Wind, Snow, Medium Wind, Low Wind | Estados de protección/meteorología |
| Err State | Motor err, OverCurrent, RTC Err, Limited Bat, inclinometer Err, Difference in angles, East Limit Err, West Limit Err, Comm Err, Low Bat, Lora Err | Alarmas de motor, reloj, batería, inclinómetro, diferencia angular, límites y enlace |

**No se copiaron coordenadas, valores de límites, umbrales, dirección radio ni cifras de la captura como configuración de ORBI.** En particular, una lectura de temperatura visual anómala no acredita temperatura física real: puede ser un valor inválido, de muestra, sin conexión o de sensor y requiere contrastar origen y tiempo de la lectura.

## Simulations: comandos visibles, clasificados como NO OPERATIVOS

- `AUTO`, `Manual`: selección de modos.
- `Manual Stow`, `Hail`, `Snow`, `Activated`: estados/opciones de protección o activación.
- `EAST`, `WEST`, `Stop`, `CLEANING`, `Rain`, `Target` y campo objetivo: maniobras o consignas.
- La segunda captura tiene `Manual` resaltado visualmente. **No confirma que un equipo físico esté en modo manual**, ni que exista conexión/confirmación de orden.

**No trasladar estos comandos a botones activos en ORBI antes de tener protocolo, modelos/firmware, procedimiento de seguridad y pruebas supervisadas.** Los valores originales se dejan como metadatos de catálogo solamente.

## Diseño funcional derivado
1. Panel TCU en español con identificación y comunicaciones diferenciadas de la validez del dato.
2. Inventario de variables por grupos y unidades; valores `SIN LECTURA` hasta conectar y validar el equipo.
3. Panel de alarmas con nombres originales, interpretación orientativa ES y severidad `NO VALIDADA`.
4. Lista de funciones de los originales en modo documental, sin botones para transmitir órdenes.
5. Compatible desde diseño con Trina (P1) y Powerway (P2) pero sin asumir compatibilidad de campos/tramas.

**Pendiente:** capturas de `Para Setting` y `SaveData` sin datos sensibles, pantalla Trina NCU Monitor y capturas Powerway. También se requieren modelos/firmware y documentación de solo lectura para integrar la primera telemetría real.
