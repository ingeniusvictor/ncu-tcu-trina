# ORBI Tracker Studio
### Estación universal de trabajo para seguidores solares · Windows · Español

Proyecto comunitario en fase temprana para técnicos e ingenieros de operación y mantenimiento (O&M) fotovoltaico. La meta es unificar, en una sola interfaz comprensible, la supervisión de controladores NCU/TCU y sistemas equivalentes de distintos fabricantes.

**Estado actual: v0.2.0-alpha — demostrador de escritorio.** La interfaz, la selección de fabricante, el inventario local de puertos COM, la bitácora y el exportador CSV son funcionales. **Los valores de trackers y alarmas son simulados. No existe todavía comunicación con hardware ni capacidad de escribir parámetros o mover motores.**

## Fabricantes contemplados
- Trina Solar: ejecutables NCU Monitor 2.71 y TCU V2.73 Multi disponibles para análisis estático privado.
- Powerway: analizados Communication Box V2.161 (NCU equivalente) y Tracker Test for Customer V5.2.2 (TCU equivalente); protocolos y modelos pendientes de validar.
- Arctech Solar (alias de terreno «Artech»): analizado Tracker AI Controller (archivo 20230101-1, versión interna 1.0.0.0) con secciones Tracker y ComBox; **cobertura real del CommBox anterior pendiente de verificar**.

**La semejanza de las pantallas no demuestra compatibilidad de protocolos.** Cada modelo/revisión/firmware deberá validarse individualmente.

## Análisis confirmado de Powerway (11-11-2026)

Se incorporó el inventario de `Communication Box V2.161` (NCU equivalente) y `Tracker Test for Customer V5.2.2` (TCU equivalente), con una [comparación estática por función](docs/ANALISIS_POWERWAY_2026-11-11.md) y una [matriz multifabricante](docs/MATRIZ_FUNCIONAL.md). Ambos son Windows .NET; contienen referencias a comunicaciones seriales, ZigBee, variables de seguidores y protección. **Esto no acredita compatibilidad binaria entre Powerway y Trina.**

## Análisis confirmado de Arctech (11-11-2026)

El archivo `Tracker_AI_Controller_20230101-1.exe` contiene componentes `ComBoxTabItem`, `ComBoxButton` y controles de tracker bajo una aplicación .NET WPF. Los [hallazgos estáticos](docs/ANALISIS_ARCTECH_2026-11-11.md) son consistentes con una interfaz de ambos ámbitos dentro de un EXE, **sin demostrar que la nueva aplicación reemplaza íntegramente los dos programas antiguos**. Un anexo técnico antiguo de Arctech documenta dos herramientas separadas. Se añadieron referencias a la misma aplicación en el catálogo para ambas vistas NCU y TCU, sin implementar conexión real.

## Cómo ejecutar en Windows

Opción A — descargar el compilado cuando termine GitHub Actions: **Actions → Windows build → última ejecución correcta → Artifacts → ORBI-Tracker-Studio-win-x64**. Descomprime y ejecuta `Orbi.TrackerStudio.exe`.

Opción B — compilar localmente con .NET 8 SDK:

```powershell
dotnet run --project src/Orbi.TrackerStudio/Orbi.TrackerStudio.csproj
```

Esta aplicación no requiere internet para sus funciones locales. El artefacto `win-x64` publicado por CI será autocontenido si la compilación termina correctamente.

## Funciones en esta etapa
- Interfaz única en español, con selector Trina / Powerway / Artech.
- Panel de resumen, alarmas **simuladas**, bitácora local y vista de compatibilidad.
- Enumeración de nombres de puertos COM en Windows (no abre puertos).
- Exportación CSV de datos **DEMO**, etiquetados como tales.
- Notas de terreno persistidas localmente en `%LOCALAPPDATA%\ORBI\TrackerStudio\notes.json`.
- No guarda credenciales, IP de planta ni identificadores reales.

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
