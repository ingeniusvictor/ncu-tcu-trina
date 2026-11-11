# Matriz inicial de funciones

Fuente: inspección estática de los dos ejecutables de Trina aportados por el usuario, **sin ejecutarlos contra hardware**. La presencia de cadenas o controles no demuestra el funcionamiento del comando.

| Función | Trina NCU 2.71 | Trina TCU 2.73 | Powerway | Artech/Arctech | V0.2 |
|---|---|---|---|---|---|
| Selección de COM / baudios | UI observada | UI observada | Pendiente EXE | Pendiente EXE | Inventario COM pasivo |
| Estado/monitoreo | UI observada | UI observada | Por verificar | Por verificar | Solo DEMO |
| Modo de tracker / dirección | UI observada | UI observada | Por verificar | Por verificar | Solo DEMO |
| Ángulo actual / objetivo | Referencias | UI observada | Por verificar | Por verificar | Solo DEMO |
| Corriente de motor | Por verificar | Referencias | Por verificar | Por verificar | Solo DEMO |
| Alertas / bitácora | Referencias | Referencias | Por verificar | Por verificar | Alarmas DEMO y notas reales locales |
| Viento / defensa | Referencias | Referencias | Por verificar | Por verificar | Sin comandos |
| Escritura / calibración | Referencias | Referencias | Por verificar | Por verificar | BLOQUEADO / NO IMPLEMENTADO |
| Identificación automática | Por validar | Por validar | Por validar | Por validar | No implementada |

## Ejecutables evaluados (nunca publicados en el repo)
- NCU Monitor 2.71 client: SHA-256 `d6811a599d95a9f68fb979f302bf4ddc5e81ad9c71d6796020280ac4b4b298ea`.
- TCU V2.73 Multi (80 canales): SHA-256 `31c1b330a0686795e56fdc803fa69cd05072aea44bea7ce6f2a34ad276ea35a7`.

Pendientes: Powerway (nombre/versión exactos), Artech/Arctech (fabricante, versión), fotografías no sensibles de interfaz, modelos y firmware compatibles, interfaz física y guía técnica/autorización de lectura.
