# Arquitectura universal — propuesta v0.2

## Principio de separación
```
UI español (única aplicación) / Bitácora / Exportaciones
                 |
      Contratos comunes de dominio
                 |
  Registry de adaptadores por fabricante
      |            |              |
    Trina       Powerway      Artech (?)
      |            |              |
  Transporte/codec específico y validado por modelo
      |            |              |
    NCU/TCU y controladores de campo
```

**Estado v0.2:** implementados UI, catálogo de fabricantes, datos demo, inventario COM y bitácora. **No se ha implementado ningún transporte activo.** La interfaz ITrackerAdapter hoy expone capacidades y evita representar un adaptador pendiente como operativo.

## Contratos de telemetría deseados
Cada adaptador futuro proporcionará como mínimo:
- Identidad verificable: marca, modelo, hardware, firmware, identificación dispositivo y enlace.
- Estado de conexión: conectado, desconectado, timeout, tipo de transporte, sello temporal.
- Estado NCU / TCU: modo, alarma, posición medida, posición objetivo, comunicaciones.
- Unidades SI coherentes y valor crudo para trazabilidad.
- Alarmas originales junto a traducción y severidad contextual.
- Fuente real versus demo; dato fresco versus caducado.

## Autoidentificación: no asumir compatibilidad
La autodetección NO enviará comandos exploratorios a puertos desconocidos. Primero inventario pasivo de COM/redes configuradas por el operador, después operaciones de lectura de identidad documentadas, con timeouts y sin escrituras. Si el modelo no se reconoce: selección manual; **nunca inferir fabricante por el diseño de su app**.

## Seguridad
Escrituras, movimiento, calibraciones, offsets, límites mecánicos, posiciones de viento y restablecimiento de alarmas son operaciones de riesgo. Quedan fuera del alcance operativo hasta contar con protocolos oficiales, matriz de riesgos, respaldo, autorización y validación en banco. La aplicación nueva nunca debe deshabilitar protecciones del equipo.

## Compatibilidad
Capacidades por firmware, no solo por marca. Los tres ejecutables parecidos podrían ser variantes de un OEM, pero no se afirma que lo sean sin pruebas.
