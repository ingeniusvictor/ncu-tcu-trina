# Plan de prioridad O&M — Trina primero, Powerway segundo

Actualizado: 2026-11-11. La selección responde a las plantas atendidas actualmente por el usuario.

## Orden vinculante para desarrollo
1. **P1 Trina Solar:** `NCU Monitor 2.71` y `TCU Multi 2.73 (80 canales)`.
2. **P2 Powerway:** `Communication Box V2.161` y `Tracker Test for Customer V5.2.2`.
3. **Backlog Arctech:** `Tracker AI Controller 20230101-1` documentado, pero sin implementación de comunicación real mientras no sea relevante para nuevas plantas.

El núcleo seguirá siendo multifabricante: un modelo de datos común, perfiles por marca/modelo/firmware y adaptadores separados. **Priorizar no significa equiparar protocolos.** No cambiar el nombre del producto a uno específico de Trina.

## Hitos y criterios

### Fase A — Interfaz y terminología (sin equipo)
- [x] Catálogo de tres marcas y seis roles lógicos.
- [x] UI unificada española con pantallas NCU y TCU, diagnóstico offline, bitácora y alarmas demo.
- [ ] Mapear los controles **reales** de Trina NCU/TCU contra sus elementos de diseño en español (requerir capturas de pestañas sin datos sensibles).
- [ ] Hacer lo mismo con Powerway Communication Box / Tracker Test.
- [ ] Tratar toda lectura de muestra como `DEMO` y todo campo de dispositivo no conectado como `SIN LECTURA`, no `NORMAL`.
- [ ] Validar diseño en notebook Windows y accesibilidad bajo luz solar.

### Fase B — Primer enlace real, Trina (solo lectura)
- [ ] Confirmar modelos físicos, firmware y medio de conexión de **un NCU** y **un TCU** de laboratorio.
- [ ] Revisar documentación/licencias de consultas de identidad y estado.
- [ ] Implementar un adaptador Trina separado con solo lecturas documentadas y timeout conservador.
- [ ] Validar en banco autorizado sin acceso a parque productivo; comparar contra los ejecutables originales.
- [ ] Registrar frescura del dato, incertidumbre, códigos originales, traducción y unidades.

### Fase C — Powerway (solo lectura)
- [ ] Confirmar modelos y firmware de Communication Box / Tracker y medio físico (serial/ZigBee según modelo).
- [ ] Con documentación técnica autorizada, implementar adaptador Powerway independiente.
- [ ] Validar identidad, estado y alarmas contra software original en banco.
- [ ] Unificar presentación sin reutilizar tramas de Trina por parecido visual.

### Fase D — Arctech (diferida)
- [x] Inspección estática y referencias visuales Tracker, LoRa y Scan.
- [ ] Confirmar rol CommBox y paridad con software anterior cuando sea una necesidad operativa.
- [ ] Integrar únicamente tras cerrar prioridades de Trina y Powerway y disponer de hardware/documentación.

## Seguridad del producto
No implementar movimientos de trackers, resguardos, cambios de límites, calibración, firmware, asignación de ID, parámetros de radio, escritura ni exploración activa hasta tener aprobación del operador responsable, protocolos documentados y validación específica. Las vistas demostrativas nunca deben sugerir disponibilidad operativa.

## Insumos más útiles a continuación
1. Capturas **Trina**: NCU Monitor y TCU Multi — monitoreo, conexiones, alarmas, parámetros, con datos sensibles cubiertos.
2. Capturas **Powerway**: Communication Box y Tracker Test — mismos módulos.
3. Modelos/firmware e interfaces de laboratorio compatibles Trina primero, Powerway después.
4. Manuales oficiales de lectura de estado/identidad para las versiones correspondientes.

Se considera Arctech documentación de referencia, **no bloqueante**.
