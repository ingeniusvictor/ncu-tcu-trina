# Insumos para avanzar hacia comunicación real

## Archivos recibidos e inspeccionados estáticamente (2026-11-11)
- [x] Trina **NCU Monitor 2.71**.
- [x] Trina **TCU Multi 2.73 (80 canales)**.
- [x] Powerway **Communication Box V2.161** (equivalente NCU según operador).
- [x] Powerway **Tracker Test for Customer V5.2.2** (equivalente TCU según operador).
- [x] Arctech **Tracker AI Controller, archivo 20230101-1**: un EXE WPF que contiene módulos `Tracker` y `ComBoxTabItem`.

## Lo que quedó por confirmar de Arctech
- [ ] ¿El mismo programa permite ejecutar **todas** las funciones de la antigua herramienta CommBox, o solo muestra una pestaña relacionada? Captura o manual de usuario sin datos sensibles.
- [ ] Modelos y versiones firmware de AI Controller/Control Box y CommBox donde se utiliza este programa.
- [ ] Si conservas los EXE *separados* antiguos, compartirlos es **opcional** para completar la comparación; **ya no es necesario subir de nuevo el programa combinado**.
- [ ] Aclarar si la instalación de la herramienta requiere archivos auxiliares, controladores USB, bases de datos u otros componentes no presentes en el EXE.
- [ ] Confirmar qué conexión física utiliza para cada rol (RS485, LoRa, USB, Ethernet, etc.).

## Insumos comunes a los tres fabricantes
1. [ ] Modelo exacto y firmware de al menos un NCU/caja central y un TCU/controlador de seguidor **de banco de pruebas**.
2. [ ] Manual, mapa de registros o especificación de protocolo de **consultas de identidad y estado de solo lectura** aplicable a ese firmware.
3. [ ] Medio de conexión y conversor que usan los técnicos en terreno (sin IP públicas, claves, cuentas ni datos de plantas).
4. [ ] Confirmación de pruebas supervisadas, autorizadas y sin alteraciones de equipos productivos.
5. [ ] Pantallas anonimizadas de los programas originales, para contrastar traducciones, nombres, estados y unidades.

## Qué no se debe compartir
Credenciales, archivos de configuración confidenciales de plantas, IP públicas, datos personales, llaves, firmware, ni capturas de tráfico de producción sin autorización.

## Desarrollo que puede seguir sin más archivos
UX premium, traducciones en español, arquitectura de adaptadores, modo offline, bitácora, exportación, tests y empaquetado Windows. **La comunicación real sigue pendiente de protocolos y pruebas de solo lectura.**

Detalles: [Matriz funcional](MATRIZ_FUNCIONAL.md), [Powerway](ANALISIS_POWERWAY_2026-11-11.md), [Arctech](ANALISIS_ARCTECH_2026-11-11.md).
