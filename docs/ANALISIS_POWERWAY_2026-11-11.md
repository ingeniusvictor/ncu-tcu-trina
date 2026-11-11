# Análisis estático Powerway — 11 de noviembre de 2026

## Alcance y método
Se analizaron **exclusivamente archivos PE/.NET sin ejecutarlos**, extrayendo encabezados, cadenas Unicode y nombres de metadatos. No se estableció comunicación con equipos, no se inspeccionó tráfico en terreno y no se alteró ningún ejecutable.

## Inventario
| Equipo de trabajo (por indicación del técnico) | Archivo original | Bytes | SHA-256 | Entorno identificado |
|---|---|---:|---|---|
| NCU equivalente / caja de comunicaciones | `Communication Box_V2.161.exe` | 326144 | `56a796de8c0a7daf62702c204f1a5d93445395742998bb55e8f8d2f5a1a64458` | PE32, .NET CLR v2.0.50727, WinForms |
| TCU equivalente / seguidor | `Tracker_Test_For_Customer_V5.2.2.exe` | 1004544 | `9f40ff0ce187cb811c9ed7c92a73c0e966db00b0a1f381ec78bc791d9655de48` | PE32, .NET CLR v4.0.30319, WinForms |

**La etiqueta NCU/TCU es una equivalencia funcional aportada por el usuario, no un modelo de hardware confirmado en los metadatos.**

## Hallazgos de Communication Box V2.161
- Configuración de puerto serial y velocidades 4800/9600/19200/57600/115200.
- Interfaz multilingüe chino/inglés (no se observó interfaz española).
- Lecturas/variables: radiación, viento (instantáneo, mínimo/máximo/promedio), dirección, temperaturas, tensión de batería, profundidad de nieve, tiempo/GPS, latitud/longitud/zona horaria.
- Configuración ZigBee: Net ID, canal y velocidad; rangos de direcciones de trackers y cantidad de equipos.
- Menús de estrategias de protección e indicadores de viento.
- Referencias a acciones **All AUTO / All FLAT / All STOP** y a métodos de escritura de parámetros. **No se implementarán desde ORBI hasta validación de seguridad.**
- Nombres de código relevantes: `SendReadCommandAll`, `SendWriteCommand`, `GetCRC16CheckCode`, `COMBOX_WINDDIRECT_ADDR_BASE`, `PROTECTPRAM_ADDR_BASE`, `TRACKERCNT_ADDR_BASE`.
- Referencias a CRC16, registros y serial indican protocolo binario o enmarcado con comprobación, **sin determinar todavía su formato real, direcciones, endianess o seguridad de consultas**.

## Hallazgos de Tracker Test for Customer V5.2.2
- Configuración COM y las mismas cinco velocidades observadas en Communication Box.
- Ángulos de panel/objetivo, elevación, azimut, estado de trabajo, motor y límite.
- Corriente de motor y umbrales de protección, nivel/voltaje de batería.
- Modo AUTO/manual, nieve, lluvia, viento, posicionado nocturno, calibración y configuración ZigBee.
- Alarmas: corriente elevada, límites, fusibles de motores, pérdida de comunicación, viento remoto/local y otras.
- Contiene referencias a funciones de lectura y escritura: `SendReadCmd`, `SendWriteCmd`, `GetCRC16CheckCode`, `WIND_PROTECT_REG_ADDR_BASE`, `SOFT_LIMIT_REG_ADDR_BASE`, `MOTOR_CURRENT_REG_ADDR_BASE`.
- Aparecen referencias a una base `CollectData.mdb` y Microsoft Jet OLE DB 4.0; **no se confirmó que el ejecutable adjunto tenga toda su infraestructura de almacenamiento ni que esa dependencia se use en la ruta principal**.

## Comparación con los dos ejecutables de Trina
| Concepto | Trina NCU Monitor 2.71 | Trina TCU 2.73 Multi | Powerway Communication Box 2.161 | Powerway Tracker Test 5.2.2 |
|---|---|---|---|---|
| Tecnología | .NET/WinForms | .NET/WinForms | .NET/WinForms | .NET/WinForms |
| Puerto COM | Referencias | Referencias | Referencias | Referencias |
| Viento y protección | Referencias | Referencias | Referencias | Referencias |
| Modo / estado de seguimiento | Referencias | Referencias | Referencias | Referencias |
| Ángulos | Referencias | Referencias | Referencias astronómicas | Referencias de seguimiento |
| Corriente de motores | Asociada a TCU vía monitoreo | Referencias | No confirmada en UI | Referencias |
| Comunicación radio | LoRa en cadenas | LoRa en cadenas | ZigBee en cadenas | ZigBee en cadenas |
| Escritura de parámetros | Referencias | Referencias | Referencias | Referencias |
| Lectura con ORBI | NO | NO | NO | NO |

### ¿Se copiaron los programas?
**No hay evidencia suficiente para atribuir copia o un mismo fabricante OEM.** Se observan funciones y ciertos nombres estructurales comunes, pero eso puede explicarse por el tipo de equipos, convenciones de desarrollo o tecnologías semejantes. En particular, Trina muestra referencias LoRa y Powerway ZigBee. La similitud de la interfaz no debe usarse para asumir equivalencia de comandos.

## Decisión de arquitectura
- Pantallas universales: Conexión, NCU/Comunicación central, TCU/Seguidor, Alarmas, Parámetros (solo lectura futura), Registro.
- Adaptadores `Trina` y `Powerway` por **modelo + firmware + tipo de enlace**.
- Detección del fabricante únicamente mediante identificación de **solo lectura documentada**, con alternativa manual.
- En la fase inicial, solo catálogos informativos, ejemplos ficticios y bitácora local; **sin acceso a hardware**.

## Próximas evidencias necesarias
1. Manual o mapa de protocolo Powerway de cada equipo, preferentemente lectura de identidad/estado.
2. Modelo físico y firmware compatibles; cable de conexión y medio ZigBee/serial.
3. Ensayo supervisado y autorizado en banco de pruebas no productivo.
4. Software de Artech/Arctech para completar la comparación de tres fabricantes.

No subir estos binarios ni claves, configuraciones o capturas sensibles al repositorio público sin confirmar permisos de redistribución.
