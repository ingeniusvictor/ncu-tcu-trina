# Insumos para avanzar hacia compatibilidad real

## Prioridad 1: comparar los tres programas
- [ ] EXE original de **Powerway**, versión exacta. Idealmente el archivo que usa el técnico y el nombre que muestra en Ayuda / Acerca de.
- [ ] EXE original identificado como **Artech / Arctech** y una confirmación del nombre comercial correcto.
- [ ] Si existen varios ejecutables por fabricante (NCU / TCU), enviar todos, indicando la función.
- [ ] Capturas de las pantallas de monitoreo, alarmas y parámetros **sin IP de planta, usuarios, contraseñas ni números de serie sensibles**.

## Prioridad 2: identificar hardware
- [ ] Una lista de modelos de NCU y TCU en servicio (placa o manual sin datos de planta).
- [ ] Versión de firmware para cada modelo que se quiera incorporar.
- [ ] Medio de conexión empleado (USB-RS485, Ethernet, radio, otro).
- [ ] Parámetros de COM/baud y topología **solo si no comprometen información sensible**.
- [ ] Si existe un manual, mapa Modbus, descripción de tramas o software development kit del fabricante, adjuntarlo.

## Prioridad 3: verificación y autorizaciones
- [ ] Confirmar que se permiten pruebas **de solo lectura** con equipos de laboratorio o banco no productivo.
- [ ] Identificar una ventana de prueba supervisada y persona responsable de autorizarla.
- [ ] Comparar la misma lectura contra software original y ORBI Tracker Studio.
- [ ] Registrar fallas y condiciones de desconexión; jamás ensayar movimientos/escrituras sin procedimiento aprobado.

## Lo que NO necesitamos
- No enviar contraseñas, credenciales de acceso, datos de empleados, IP públicas, VPN, archivos confidenciales de parque ni claves/firmware propietarios.
- No abrir ni capturar tráfico de una red de planta sin autorización.

**Mientras llegan esos insumos** se puede continuar diseño, traducción, accesibilidad, bitácora y pruebas offline. La primera prioridad de integración real será identificación del equipo + lecturas de estado, nunca escritura.
