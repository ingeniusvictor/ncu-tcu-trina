namespace Orbi.TrackerStudio;

// Guides are explanatory text only. They do not inspect hardware or alter controllers.
public sealed record OfflineCheck(string Orden, string Area, string Verificacion, string Estado);
public sealed record SpanishTerm(string Ingles, string Espanol, string Nota);
public sealed record SoftwareCompatibility(string Fabricante, string Ambito, string Herramienta, string Estado);

public static class FieldGuides
{
    public static IReadOnlyList<OfflineCheck> Checks(string manufacturerId)
    {
        var link = manufacturerId switch
        {
            "trina" => "Revisar en documentación si el modelo utiliza LoRa u otro enlace",
            "powerway" => "Confirmar si el modelo utiliza ZigBee, serial u otro enlace",
            "arctech" => "Confirmar interfaz LoRa, RS485 u otra según versión de equipo",
            _ => "Identificar el transporte de comunicaciones en el manual del equipo"
        };
        return new[]
        {
            new OfflineCheck("01", "Identificación", "Leer etiqueta de modelo y firmware del equipo, sin modificarlo", "PENDIENTE"),
            new OfflineCheck("02", "Conexión", "Verificar visualmente cable, conversor y alimentación autorizada", "PENDIENTE"),
            new OfflineCheck("03", "Puerto", "Identificar el COM de Windows sin abrir ni transmitir tráfico", "PENDIENTE"),
            new OfflineCheck("04", "Protocolo", link, "PENDIENTE"),
            new OfflineCheck("05", "Documentación", "Confirmar si existe comando de identidad de solo lectura documentado", "PENDIENTE"),
            new OfflineCheck("06", "Seguridad", "Confirmar permiso de banco de pruebas y procedimiento O&M", "PENDIENTE"),
            new OfflineCheck("07", "Validación", "Comparar valores y unidades con la herramienta original", "NO PROBADO"),
            new OfflineCheck("08", "Fallas", "Ensayar desconexión y timeout solamente en banco autorizado", "NO PROBADO")
        };
    }

    public static IReadOnlyList<SpanishTerm> Glossary { get; } = new[]
    {
        new SpanishTerm("NCU / Communication Box", "Unidad de control central / caja de comunicaciones", "La equivalencia funcional depende del fabricante"),
        new SpanishTerm("TCU / Tracker controller", "Controlador del seguidor solar", "Verificar modelo y firmware"),
        new SpanishTerm("Actual angle", "Ángulo actual", "Grados; confirmar cero y sentido de giro"),
        new SpanishTerm("Target angle", "Ángulo objetivo", "No implica necesariamente una orden de movimiento"),
        new SpanishTerm("Motor current", "Corriente del motor", "Expresar en amperes (A)"),
        new SpanishTerm("Wind stow", "Posición de resguardo por viento", "Protección crítica; no modificar umbrales"),
        new SpanishTerm("Auto tracking", "Seguimiento automático", "Conservar modo y código original"),
        new SpanishTerm("Manual mode", "Modo manual", "Acción de riesgo, no disponible"),
        new SpanishTerm("Communication timeout", "Tiempo de espera de comunicación agotado", "No confundir con estado normal"),
        new SpanishTerm("Offline", "Sin comunicación", "Diferenciar de apagado seguro"),
        new SpanishTerm("Limit switch", "Interruptor de límite de recorrido", "Protección mecánica; no anular"),
        new SpanishTerm("Alarm reset", "Restablecimiento de alarma", "No borra falla física; acción no implementada"),
        new SpanishTerm("Baud rate", "Velocidad del enlace serial", "En bits por segundo (bps)"),
        new SpanishTerm("Device address", "Dirección del controlador", "Puede no ser una IP"),
        new SpanishTerm("RSSI", "Intensidad de señal recibida", "Escala y unidades dependen del hardware"),
        new SpanishTerm("CRC", "Código de verificación de trama", "Requiere protocolo y orden de bytes exactos"),
        new SpanishTerm("Firmware", "Firmware del controlador", "La compatibilidad depende de la revisión"),
        new SpanishTerm("Read parameters", "Leer parámetros", "Solo lectura, futura fase validada"),
        new SpanishTerm("Write parameters", "Escribir parámetros", "No implementado; requiere autorización")
    };

    public static IReadOnlyList<SoftwareCompatibility> Matrix { get; } =
        SoftwareCatalog.All.Select(s => new SoftwareCompatibility(
            AdapterCatalog.All.FirstOrDefault(v => v.Id == s.ManufacturerId)?.DisplayName ?? s.ManufacturerId,
            s.Family == "NCU" ? "Central / NCU" : "Seguidor / TCU",
            s.OriginalProduct + " " + s.Version,
            "PROTOCOLO NO VALIDADO")).ToArray();
}
