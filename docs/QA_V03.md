# QA v0.3 — pruebas offline y criterios de aceptación

Esta versión es un **demostrador de escritorio sin comunicación con hardware**. La prueba de uso puede realizarse en Windows sin cables, conversores ni equipos conectados.

## Verificación automática en GitHub Actions
El flujo `Windows build & offline smoke` debe:
1. Restaurar .NET 8, compilar la aplicación WinForms y el ejecutable de pruebas.
2. Comprobar que el catálogo contiene exactamente tres fabricantes y seis roles funcionales.
3. Confirmar que ningún fabricante declara capacidad de telemetría o comandos validados.
4. Comprobar que las 12 lecturas ficticias por fabricante llevan `DEMO`.
5. Comprobar el catálogo del controlador combinado de Arctech.
6. Validar el glosario y las guías pasivas.
7. Verificar que ambos exportadores CSV neutralizan texto con apariencia de fórmula.
8. Publicar una versión portable para Windows x64 si todo lo anterior pasa.

**No basta que el código esté publicado para considerarlo compilado.** Registrar el resultado de Actions por SHA.

## Pruebas manuales de interfaz (pendientes de realizar en Windows)
- [ ] Abrir `Orbi.TrackerStudio.exe`, confirmar título `v0.3 alpha` y banner `MODO DEMO`.
- [ ] Cambiar entre Trina, Powerway y Arctech.
- [ ] Vista NCU: todas las métricas de comunicación, viento y equipos deben aparecer como no conectadas/sin lectura.
- [ ] Vista TCU: buscar `DEMO-TCU-04` y filtrar por `Avisos simulados`; los resultados deben cambiar sin conexión.
- [ ] Vista Alarmas: comprobar que no se presentan como eventos reales.
- [ ] Vista Diagnóstico: ver lista completa marcada `PENDIENTE` o `NO PROBADO`.
- [ ] Vista Glosario: revisar traducciones y unidades.
- [ ] Vista Compatibilidad: verificar seis roles y `PROTOCOLO NO VALIDADO`.
- [ ] Vista Bitácora: guardar una nota local, cerrar y volver a abrir para probar persistencia.
- [ ] Guardar segunda nota y comprobar `%LOCALAPPDATA%\ORBI\TrackerStudio\notes.json.bak`.
- [ ] Exportar bitácora CSV, verificar codificación y caracteres acentuados.
- [ ] Exportar DEMO CSV, comprobar advertencia de datos simulados.
- [ ] Probar en resolución 1366×768 (escalado 100% y 125%) y 1920×1080.
- [ ] Probar sin ningún COM y con un conversor conectado *sin ningún controlador unido*.

## Bloqueos expresos
- Conexión COM real: NO IMPLEMENTADA.
- Autodetección de NCU/TCU: NO IMPLEMENTADA.
- Consultas de datos reales: NO IMPLEMENTADAS.
- Movimiento, calibración, escritura de parámetros, restablecer alarmas y actualización de firmware: NO IMPLEMENTADOS.
- No probar directamente en una planta en operación.

## Criterio para habilitar la primera lectura real
1. Confirmar fabricante, modelo, hardware, firmware y protocolo documentado.
2. Contar con permiso y entorno de banco no productivo.
3. Implementar identificador, timeout y lectura de estado como operaciones acotadas.
4. Asegurar que nunca se presenta lectura caducada como vigente.
5. Contrastar con herramienta original del fabricante y documentar el resultado.

No publicar imágenes con IP, llaves, números de serie, nombres de planta ni credenciales.
