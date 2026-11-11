# Protocolo de validación antes de conectar hardware

**No se ejecuta ninguna de estas pruebas automáticamente.**

1. Catalogar las versiones reales de NCU, TCU, firmware, cable USB/RS485, baud rate y medio de comunicación.
2. Obtener documentación del fabricante para las consultas de identidad, estado y alarma **de solo lectura**. Verificar permisos de uso y redistribución.
3. Usar banco de pruebas no productivo y personal autorizado; archivar evidencia anonimizada.
4. Confirmar checksum, trama, direcciones, timeouts y límites de frecuencia de interrogación.
5. Comparar cada lectura con el software original del fabricante; registrar discrepancias y calidad del dato.
6. Definir estados desconectado/caducado/error y dejar claro que *desconocido* no significa *normal*.
7. Solo después considerar controles de escritura por separado, con evaluación de riesgos, confirmaciones, trazabilidad y validación en banco.

## Restricciones de seguridad de esta versión
- No se abren puertos COM.
- No se transmite tráfico serial ni de red.
- No se descubren dispositivos activamente.
- No se escriben parámetros, calibra, hace reset de alarmas ni se ordena movimiento.
- Los indicadores demo están etiquetados y los CSV llevan advertencia explícita.

## Datos privados
No registrar credenciales, direcciones IP públicas, nombres de planta, números de serie reales, archivos de configuración ni capturas de tráfico con datos internos en un repositorio público. Las notas de terreno del operador quedan en su propio computador.
