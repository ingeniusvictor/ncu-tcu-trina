# Insumos necesarios para avanzar hacia comunicación real

**Recibido y analizado de manera estática (11-11-2026):**
- [x] Trina NCU Monitor 2.71 (NCU).
- [x] Trina TCU 2.73 Multi (80 canales) (TCU).
- [x] Powerway Communication Box V2.161 (equivalente funcional NCU, por indicación del técnico).
- [x] Powerway Tracker Test for Customer V5.2.2 (equivalente funcional TCU, por indicación del técnico).

Ver [análisis Powerway](ANALISIS_POWERWAY_2026-11-11.md) y [matriz funcional](MATRIZ_FUNCIONAL.md).

## Lo prioritario ahora
1. [ ] Adjuntar el ejecutable **Artech / Arctech** para NCU/comunicaciones, si existe, y aclarar el nombre comercial exacto del fabricante.
2. [ ] Adjuntar el ejecutable **Artech / Arctech** para TCU/seguidores, si existe.
3. [ ] Modelo de placa y versión de firmware de **un NCU/Communication Box y un TCU** de Powerway y de Trina, sin datos de planta.
4. [ ] Manual técnico, mapa de registros, especificación de comunicación o SDK correspondiente a **consultas de identificación y estado de solo lectura**.
5. [ ] Medio de conexión que se utiliza en terreno: USB-RS485, RS232, ZigBee, LoRa, Ethernet, conversor, etc.
6. [ ] Capturas no sensibles de las pantallas de monitoreo, alarmas y conexión de los originales para comparar nomenclatura.

## Antes de cualquier integración con equipos reales
- [ ] Acordar ensayos supervisados de solo lectura en banco o ambiente no productivo autorizado.
- [ ] Comparar identificador de equipo, estados, alarmas y unidades con el software original.
- [ ] Probar desconexión, timeout, errores CRC y frescura de telemetría.
- [ ] Evaluar por separado escrituras, calibraciones, movimiento de trackers y cambios de seguridad: NO incluidos en las primeras versiones.

**No compartir** contraseñas, credenciales de planta, archivos de configuración confidenciales, IP públicas, capturas con números de serie o datos personales, firmware ni tramas de producción sin autorización.

## Lo que puedo continuar sin más materiales
Diseño premium, internacionalización, reorganización NCU/TCU, validaciones offline, manejo de bitácora, empaquetado Windows, glosario técnico y pruebas automatizadas del núcleo desconectado.
