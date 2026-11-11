# Arctech: análisis de Tracker AI Controller y evolución de herramientas

**Fecha de análisis:** 2026-11-11.  
**Método:** análisis **estático** del ejecutable facilitado por un técnico de O&M y consulta de documentación pública. No se ejecutó el archivo, no se conectó hardware y no se enviaron comandos.

## Ejecutable recibido

| Campo | Resultado |
|---|---|
| Nombre proporcionado | `Tracker_AI_Controller_20230101-1.exe` |
| Tamaño | 8.085.504 bytes |
| SHA-256 | `87c8c5518920c08ff056d15a7bf71bc6204f7b02cce11dcb23133e4e55f6d34c` |
| Formato | PE32 Windows, .NET Framework CLR 4.0.30319 |
| Interfaz detectada | WPF: `MainWindow.xaml`, `mainwindow.baml`, `System.Windows` y `WindowsFormsIntegration` |
| Nombre de producto incrustado | `Tracker_AI_Controller` |
| Versión incrustada | `1.0.0.0` |
| Fecha | `20230101` aparece en nombre de archivo, **no fue verificada como fecha oficial de lanzamiento** |
| Estado de comunicación con hardware | **No probado, no operativo en ORBI** |

## Evidencia concreta de funciones internas

Los nombres de componentes y cadenas de la aplicación incluyen:

- **Panel de tracker:** `Tracker`, `ControllerIDTextbox`, `AngleTextBlock`, `MotorCurTextBlock`, `ReadAllButton`, `WriteParaButton`, `AIAngleButton`, `ScanControllerButton`.
- **Panel de caja central:** `ComBoxTabItem`, `ComBoxButton`, `TrackerSumTextBox`; confirma una sección o integración visual relacionada con ComBox. **No confirma por sí solo paridad completa con el software CommBox antiguo.**
- **Comunicaciones:** `RS485`, `LORA`, `Arctech_Lora_Read`, `Arctech_Lora_Write`, `EByteLoRa`, `OpenSerialPort`, `PortComBox`, `400T/900T`, `433`, `915`.
- **Modos de trabajo y protección:** `AutoButton`, `WindButton`, `SnowButton`, `RainButton`, `Flood`, `MaintenanceButton`, `CalibButton`, `ResetButton`.
- **Otras funciones potencialmente sensibles:** `UpgradeControllerFirmware`, `WriteAll`, `DebugWrite`, `SimulationWindButton`. Son **nombres detectados**, no funcionalidades reproducidas ni validadas.

Estas referencias son evidencia de una **interfaz con funciones diversas**, no prueba de acceso a un NCU o TCU específico, y menos aún de compatibilidad con otro fabricante.

## Qué confirma la documentación externa

1. Un anexo de configuración de repetidores de Arctech enumera **dos aplicaciones separadas**, literalmente *AI Track Controller Arctech Solar Software* y *CommBox Arctech Solar Software*. Fuente: https://www.scribd.com/document/1016331915/11-ANNEX-XI-Repeaters-Rev01
2. La página oficial de Arctech sobre Wind Stow distingue ajustes locales con *Arctech Smart Tool* y ajustes distribuidos desde NCU con *Arctech Solar Communication Control*. Fuente: https://en.arctechsolar.com/wind-stow-strategy/
3. El manual público **Protocol Explanation V2.26' for Arctech Communication Box** (fuente secundaria con aviso explícito de derechos) describe mapas de registros, alarmas y formato de lectura. **No copiar ni implementar directamente sin confirmar licencia y compatibilidad de versión.** Fuente: https://www.scribd.com/document/1016331907/Protocol-Fo-Arctech-Communicaiton-Box-V2-26
4. Arctech indica oficialmente que SkySmart II puede usar LoRa o RS485: https://www.arctechsolar.us/uploads/files/cpsc3.pdf

## Conclusión acotada

**Sí existían aplicaciones diferentes para controladores y CommBox.** El ejecutable aportado incorpora un módulo `ComBox` junto al de tracker, lo que resulta **consistente con una interfaz unificada**. No encontramos una nota oficial de lanzamiento o changelog que demuestre que **reemplazó íntegramente** ambas herramientas; eso queda **pendiente de validación**, preferiblemente con pantalla real del programa y manual de la versión.

## Implicaciones para ORBI Tracker Studio

- Identificar comercialmente el fabricante como **Arctech Solar**, conservando «Artech» como alias de uso en terreno.
- En el catálogo, registrar **un mismo ejecutable** para vistas «NCU/ComBox» y «TCU/Tracker», con **cobertura no validada**.
- No clasificar `WPF`, `LoRa` y `RS485` como protocolos universales entre marcas.
- No activar escaneos, escritura de parámetros, movimiento ni firmware sin autorizaciones, documentación y pruebas en banco no productivo.
- Solicitar el nombre y revisión del hardware Arctech en el que se usa ese ejecutable.

**No se incorporó al repositorio público el ejecutable de terceros.**

## Nueva evidencia visual: pantalla Tracker

El 2026-11-11 se recibió una **captura de la ventana AI Track Controller** con título TRACKER y `Ver:20230101`. La barra lateral muestra `Tracker`, `LoRa`, `Scan`, `Upgrade`, `Broadcast`, `Debug` y `About`. Se ven bloques **Information**, **Parameter** y **Alarm**, además de controles de modos y parámetros.

**Corrección importante de alcance:** en esta captura **no aparece una pestaña CommBox autónoma**. `ControlBox` es el nombre de un grupo de parámetros del controlador; no basta para afirmar que representa la NCU/CommBox. Los símbolos `ComBoxTabItem` y `ComBoxButton` del análisis estático siguen siendo relevantes, pero no prueban que el programa incluya todas las funciones de la aplicación CommBox antigua.

La captura también presenta simultáneamente `Click To Open` y una luz verde `Normal`. Por eso no deben interpretarse ni la luz ni los valores del formulario como prueba de conexión o de lecturas válidas.

El detalle de campos, alarmas originales, traducciones y distribución está registrado en [Referencia UI Arctech Tracker](REFERENCIA_UI_ARCTECH_TRACKER_2026-11-11.md). La captura **no se subió al repositorio público**.
