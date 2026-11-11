# Evidencia visual: AI Track Controller de Arctech

**Fecha de recepción:** 2026-11-11.  
**Fuente:** captura de pantalla facilitada por un técnico O&M de la interfaz `AI Track Controller`.  
**Estado:** análisis **visual** de una única captura; no se ha interactuado con la app ni conectado hardware. La captura original no se redistribuye en este repositorio, ya que podría contener parámetros de instalación.

## Identidad observada

- Título de la ventana: **AI Track Controller**.
- Encabezado principal: **TRACKER**.
- Leyenda de versión visible: `Ver:20230101`. Es un texto de UI; no implica por sí solo el número de versión interno de archivo ni fecha oficial de distribución.
- Barra lateral visible: `Tracker`, `LoRa`, `Scan`, `Upgrade`, `Broadcast`, `Debug`, `About`.
- Zona inferior izquierda: selección `LoraTest`, desplegables, campo `ID`, botón `Click To Open`, indicador verde `Normal`.

## Distribución visual en la pantalla Tracker

| Zona | Elementos visibles (nombre de UI) | Interpretación para ORBI |
|---|---|---|
| Information / Version | Firmware, Hardware | Datos de identidad del controlador |
| Information / Angle | Elevation, Azimuth, TargetAngle, ActualAngle | Geometría del seguidor; conservar unidad angular y referencia |
| Information / Sensor | MotorCur, Temperature, WindSpeed | Motor, temperatura, viento |
| Information / PVPower | PVBuckVol, PVSensorCur, PVStringVol | Tensiones/corrientes FV; significado exacto por manual |
| Information / BatteryPower | BatVol/SOC, BatCur, BatTemp, ChargeStatus, ChargerError | Alimentación y salud de batería |
| Time | Hora mostrada y `Update Time` | La hora de pantalla no confirma sincronización del equipo |
| NewID | ID, Broadcast, Change ID | Cambio de identificador: operación de escritura crítica |
| Parameter / Astronomy | Longitude, Latitude, TimeZone | Parámetros astronómicos |
| Parameter / Array | ModuleWidth, PostSpace, N-S Deflection, TerrSlope-E, TerrSlope-M | Geometría mecánica |
| Parameter / ControlBox | Accuracy, MaxMotor Current, TypeCom, Heat, Facing West, NoComProtection, Wind Mode | Ajustes de control/protección |
| Parameter / WorkingAngle | ESoftLimit, WSoftLimit, CleanAngle, NightDock, ProWindSpeed, ProDelay, FacingWindStow | Límites, posiciones y defensa meteorológica |
| Acciones de parámetros | Read Parameter, Write Parameter, Open Config File | Lectura, escritura y carga de configuración (no asumir semántica por nombre) |
| Panel Alarm | indicadores grises y códigos de alarmas | Alertas sin prueba de lectura activa |
| Barra inferior | AutoTrack, WindMode, SnowMode, FloodMode, Manual, M-West, Horizontal, LowPower, WindDirection, RainMode, CleanMode, Go!, M-East, Calib, Reset | Órdenes potencialmente activas: fuera del alcance operativo inicial |

## Alarmas visibles (códigos originales conservados)

| Código mostrado | Traducción orientativa ES | Precaución |
|---|---|---|
| AngleNoCom | Sin comunicación con sensor de ángulo | Verificar diagnóstico del fabricante |
| AngleNoChange | Ángulo sin cambio detectado | Puede ser condición o alarma |
| SoftLimit-E | Límite lógico Este | No cambiar ajustes sin procedimiento |
| SoftLimit-W | Límite lógico Oeste | No cambiar ajustes sin procedimiento |
| MotorOverCur | Sobrecorriente de motor | No inferir causa sin medición |
| MotorReversal | Movimiento inverso del motor | Revisar lógica y sensor en equipo autorizado |
| EmergencyStop | Parada de emergencia | Requiere procedimiento seguro |
| TimeLost | Hora / tiempo perdido | Comparar estado de RTC |
| RTCError | Error de reloj de tiempo real | Validar sincronización |
| BatSOCLow | Bajo estado de carga de batería | SOC sujeto a calibración |
| BatError | Error de batería | Sin diagnóstico de causa |
| BatNoCom | Sin comunicación de batería | No confundir con batería descargada |
| NTCError | Error de sensor NTC | Verificar sonda/entrada |
| ParaSaveError | Error al guardar parámetros | No repetir escritura sin evaluar condición |
| ExternFlashError | Error de memoria flash externa | Puede comprometer configuración |
| DriverError | Error de controlador del motor | No supone diagnóstico de hardware específico |

**Ningún indicador gris/verde, cifra, fecha o parámetro de la captura se considera telemetría válida.** La presencia simultánea de `Click To Open` y `Normal` es evidencia de que la UI debe mostrar por separado el estado de enlace, la validez de datos y el estado operativo; no acredita conexión activa.

## Qué NO prueba la imagen

1. No hay una pestaña explícita `ComBox` visible en la barra lateral. `ControlBox` en el formulario de parámetros **no equivale necesariamente a la NCU/CommBox central**.
2. `ComBoxTabItem` y `ComBoxButton` aparecieron en análisis estático del EXE, pero la captura no prueba que exista una sección habilitada de CommBox ni paridad funcional con el software separado antiguo.
3. La existencia de botones `Read Parameter`, `Write Parameter`, `Broadcast`, `Upgrade`, `Reset` o `Go!` **no implica que ORBI pueda ejecutar esos comandos** ni que estén seguros para equipos productivos.
4. No se conoce qué etiquetas de la pantalla cambian según modelo, firmware, permiso de acceso o si un controlador está conectado.

## Traducción a arquitectura UX universal

- Pantallas principales: **Resumen**, **NCU / Central**, **TCU / Seguidores**, **Alarmas**, **Diagnóstico**, **Parámetros (futuro, solo lectura)**, **Bitácora**.
- Niveles del dato: **sin conexión**, **sin lectura válida**, **dato caducado**, **dato real validado**, **dato simulado**.
- Alarmas: conservar código de fabricante y agregar nombre descriptivo y contexto en español; no cambiar automáticamente severidad sin manual de firmware.
- Parámetros: agrupar por **Astronomía**, **Geometría**, **Controlador**, **Protecciones y ángulos**.
- Comandos de movimiento, calibración, cambio de límites, defensa por viento, modificación de ID, actualización de firmware y escrituras: **no implementados** hasta protocolos, permisos y pruebas de banco.
- Empezar comparando **la misma pantalla Tracker** con Trina y Powerway: tabla de campos, unidades, estados y denominaciones, sin inferir que los registros seriales coinciden.

## Evidencia pendiente

Una imagen de `LoRa` y otra de `Scan` tomada **sin controlador conectado ni datos sensibles** permitiría saber si son paneles de comunicación central o solo herramientas para el controlador de seguimiento. También sería útil la pantalla `About` para comprobar nomenclatura del fabricante. No es necesario usar `Upgrade`, `Broadcast`, `Debug` ni pulsar botones de control.

**Referencia relacionada:** [Análisis estático del EXE](ANALISIS_ARCTECH_2026-11-11.md).
