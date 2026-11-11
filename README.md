# ORBI Tracker Studio
### Estación universal de trabajo para seguidores solares · Windows · Español

Proyecto comunitario en fase temprana para técnicos e ingenieros de operación y mantenimiento (O&M) fotovoltaico. La meta es unificar, en una sola interfaz comprensible, la supervisión de controladores NCU/TCU y sistemas equivalentes de distintos fabricantes.

**Estado actual: v0.3.0-alpha — demostrador de escritorio.** La interfaz, la selección de fabricante, el inventario local de puertos COM, la bitácora y el exportador CSV son funcionales. **Los valores de trackers y alarmas son simulados. No existe todavía comunicación con hardware ni capacidad de escribir parámetros o mover motores.**

## Fabricantes contemplados
- Trina Solar: ejecutables NCU Monitor 2.71 y TCU V2.73 Multi disponibles para análisis estático privado.
- Powerway: analizados Communication Box V2.161 (NCU equivalente) y Tracker Test for Customer V5.2.2 (TCU equivalente); protocolos y modelos pendientes de validar.
- Arctech Solar (alias de terreno «Artech»): analizado Tracker AI Controller (archivo 20230101-1, versión interna 1.0.0.0) con pantalla Tracker confirmada y referencias internas a ComBox; **no se ha observado una pantalla CommBox autónoma ni comprobado paridad con la herramienta antigua**.

**La semejanza de las pantallas no demuestra compatibilidad de protocolos.** Cada modelo/revisión/firmware deberá validarse individualmente.

## Análisis confirmado de Powerway (11-11-2026)

Se incorporó el inventario de `Communication Box V2.161` (NCU equivalente) y `Tracker Test for Customer V5.2.2` (TCU equivalente), con una [comparación estática por función](docs/ANALISIS_POWERWAY_2026-11-11.md) y una [matriz multifabricante](docs/MATRIZ_FUNCIONAL.md). Ambos son Windows .NET; contienen referencias a comunicaciones seriales, ZigBee, variables de seguidores y protección. **Esto no acredita compatibilidad binaria entre Powerway y Trina.**

## Análisis confirmado de Arctech (11-11-2026)

El archivo `Tracker_AI_Controller_20230101-1.exe` contiene componentes `ComBoxTabItem`, `ComBoxButton` y controles de tracker bajo una aplicación .NET WPF. Los [hallazgos estáticos](docs/ANALISIS_ARCTECH_2026-11-11.md) muestran componentes relacionados con ambos ámbitos, pero la [captura de la interfaz](docs/REFERENCIA_UI_ARCTECH_TRACKER_2026-11-11.md) solo confirma la pantalla Tracker; **la sustitución completa del software CommBox antiguo sigue sin demostrarse**. Un anexo técnico antiguo de Arctech documenta dos herramientas separadas. Se añadieron referencias a la misma aplicación en el catálogo para ambas vistas NCU y TCU, sin implementar conexión real.

## Cómo ejecutar en Windows

Opción A — descargar el compilado cuando termine GitHub Actions: **Actions → Windows build → última ejecución correcta → Artifacts → ORBI-Tracker-Studio-win-x64**. Descomprime y ejecuta `Orbi.TrackerStudio.exe`.

Opción B — compilar localmente con .NET 8 SDK:

```powershell
dotnet run --project src/Orbi.TrackerStudio/Orbi.TrackerStudio.csproj
```

Esta aplicación no requiere internet para sus funciones locales. El artefacto `win-x64` publicado por CI será autocontenido si la compilación termina correctamente.

## Funciones en esta etapa
- Interfaz única en español, con selector Trina / Powerway / Arctech.
- Panel de resumen y módulos separados NCU/TCU para los tres fabricantes, **sin comunicación real**.
- Filtro de seguidores por identificador y estado ficticio; alarmas **simuladas** siempre identificadas.
- Diagnóstico guiado **sin ejecutar pruebas**, glosario técnico español y matriz de seis roles catalogados.
- Bitácora local con copia de seguridad `notes.json.bak` y exportación CSV.
- Enumeración de nombres de puertos COM en Windows (no abre puertos).
- Exportación CSV de datos **DEMO**, etiquetados como tales, y exportación independiente de notas locales. Ambos sanitizan textos con apariencia de fórmula.
- Notas de terreno persistidas localmente en `%LOCALAPPDATA%\ORBI\TrackerStudio\notes.json`.
- No guarda credenciales, IP de planta ni identificadores reales.

## Calidad y pruebas offline

El flujo [Windows build & offline smoke](.github/workflows/windows-build.yml) compila en Windows e incluye pruebas de consistencia: catálogo de 3 fabricantes, 6 roles NCU/TCU, ausencia de telemetría validada, identificación de todos los datos DEMO y seguridad de exportación CSV.

Pruebas reproducibles en una máquina Windows con .NET 8 SDK:

```powershell
dotnet run --project tests/Orbi.TrackerStudio.Smoke/Orbi.TrackerStudio.Smoke.csproj --configuration Release
```

La lista de [pruebas manuales v0.3](docs/QA_V03.md) incluye revisión de interfaz en portátil, persistencia local, exportaciones y la prohibición de pruebas con hardware productivo. La [compilación y las pruebas offline del PR #4](docs/CI_V03_2026-11-11.md) finalizaron correctamente en Windows; **la comprobación gráfica real y la validación con hardware siguen pendientes**.

## Seguridad
**Sin conexión real y sin escrituras.** No contiene comandos de mover trackers, cambiar límites o deshabilitar protecciones. La detección automática se diseñará tras disponer de tramas oficiales o capturas autorizadas de solo lectura. Nunca usar datos DEMO para tomar decisiones operativas.

No subir a este repositorio binarios ajenos, contraseñas, archivos de configuración de plantas, direcciones públicas, llaves, firmware ni capturas con datos sensibles. La disponibilidad gratuita de una aplicación no equivale automáticamente a permiso de redistribución.

## Organización
- `src/Orbi.TrackerStudio/`: aplicación Windows independiente.
- `docs/ARQUITECTURA.md`: diseño universal por adaptadores.
- `docs/MATRIZ_FUNCIONAL.md`: funcionalidades confirmadas y pendientes.
- `docs/PROTOCOLO_Y_SEGURIDAD.md`: validación antes de acceder a controladores.
- `.github/workflows/windows-build.yml`: compilación Windows automática.

**Repositorio histórico:** el nombre de URL `ncu-tcu-trina` se mantiene para no romper enlaces. La aplicación se llama **ORBI Tracker Studio** y no está afiliada oficialmente con los fabricantes.

La licencia de distribución del código propio queda pendiente de decisión del titular. Hasta que se incorpore un archivo LICENSE, no se presume una licencia de código abierto.
