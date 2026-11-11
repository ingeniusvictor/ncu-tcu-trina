# Evidencia visual Arctech: LoRa y Scan — 11 de noviembre de 2026

**Alcance:** análisis visual de dos capturas aportadas por usuario. No se ejecutó software ni se conectó un controlador. Se registran funciones y textos de UI; no se copian imágenes con posibles configuraciones a repositorio público.

## Pantalla LoRa (UI "LORA", Ver:20230101)

**Controles observables:**
- Fabricante del módulo radio mostrado: **EBYTE / Chengdu Ebyte Electronic Technology Co., Ltd.** Es la marca del módulo/elemento de radio mostrado, no evidencia de que fabrique el controlador de seguidores.
- Selector `900T` y botones de valores predeterminados `400T Default` y `2.4G Default`.
- Campos `Channel`, `NetID`, `Power`, `Air Rate`, `Key`.
- Botones `Read`, `Write`, opción `Remote`, `OneStep(E)`.
- Ayudas de UI: `400Tband: 0–83` con ecuación de referencia `410.125 + Channel` MHz; `900Tband: 0–80` con ecuación `850.125 + Channel` MHz. Para 2.4G se muestran fórmulas de canal diferentes según modo de velocidad aérea.
- El selector muestra, en esa captura, potencia `30dbm` y `0.3kbps`. **Esos valores son visibles en la interfaz, no una recomendación ni prueba de configuración aplicada.**

**Interpretación:** esta es una pantalla de **parámetros de radio** y podría facilitar configuración local o remota de módulos LoRa; la vista **no acredita** una NCU/CommBox completa. Tampoco se puede afirmar que el programa haya realizado una lectura exitosa, ni que un valor visible provenga del dispositivo conectado.

**Seguridad:** no trasladar números de canales, claves, potencias, valores predeterminados o instrucciones de `Write` directamente a ORBI para equipos reales. Las unidades, límites de banda y requisitos regulatorios dependen del hardware, versión y país. No almacenar ni registrar claves de radio.

## Pantalla Scan (UI "SCAN", Ver:20230101)

- Tres zonas principales tituladas `CONTROLLER COMMUNICATION STATUS`, `FIRMWARE VERSION OF CONTROLLER` y `STRING BUCK MODULE VOLTAGE`.
- Controles inferiores: rango numérico **1–150** mostrado en la captura, botón `Scan Controller` y `Stop`.
- Los tres paneles están **vacíos**. No se observa evidencia de respuestas, controlador detectado ni resultado de escaneo.
- Sigue visible el selector inferior `LoraTest`, campo ID, `Click To Open` e indicador `Normal`. Ninguno de estos elementos confirma una conexión activa en la imagen.

**Interpretación:** la herramienta está diseñada para recorrer/controlar un rango de identificadores y visualizar estado de comunicación, versión firmware y tensión de módulo FV/DC-DC. **No se ha validado** si sus consultas se originan desde una NCU/CommBox o desde un adaptador radio conectado a la TCU. Tampoco podemos identificar el protocolo ni garantizar que escanear sea inocuo; por tanto, ORBI no ofrecerá un escaneo activo hasta que exista especificación técnica, autorización y validación en banco no productivo.

## Implicación del hallazgo previo sobre CommBox

Estas pantallas muestran funciones de radio y exploración, pero **no muestran una pestaña `CommBox` explícita ni confirman que reemplazaron el software central antiguo**. Los nombres de código `ComBoxTabItem` y `ComBoxButton` encontrados en el ejecutable son indicios de componentes, no evidencia de paridad operacional.

## UX sugerida para ORBI, sin comunicación hardware

- `Conectividad`: inventario local de COM y tarjeta informativa del enlace, sin controles de escritura ni claves.
- `Equipos`: campo de identificación de modelo y firmware **no verificado**, con estado de lectura `DESCONOCIDO` o `SIN LECTURA`.
- `Estado de escaneo`: `NO DISPONIBLE` hasta disponer del protocolo específico autorizado.
- `Radio`: mostrar categorías de parámetros de forma documental, sin replicar ni prellenar valores de la captura.
- `Diagnóstico`: preservar origen, unidad y fecha de cada futura lectura, distinguir respuesta válida de estimación, y limitar las consultas por rate-limit/timeout documentados.

## Decisión de prioridad del proyecto (instrucción del usuario)

**Primero Trina, segundo Powerway; Arctech queda como referencia y backlog diferido** porque actualmente el usuario opera más parques de Trina y Powerway y ninguno de Arctech. La nueva evidencia permite completar documentación de Arctech sin desviar tiempo de integración.

Ver también [Referencia Tracker](REFERENCIA_UI_ARCTECH_TRACKER_2026-11-11.md), [Análisis estático Arctech](ANALISIS_ARCTECH_2026-11-11.md) y [Plan de trabajo prioritario](PRIORIDADES_TRINA_POWERWAY.md).
