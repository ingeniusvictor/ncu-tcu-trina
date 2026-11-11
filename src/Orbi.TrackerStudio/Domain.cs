namespace Orbi.TrackerStudio;

// A manufacturer plug-in describes its capabilities without assuming shared protocols.
public interface ITrackerAdapter
{
    string Id { get; }
    string DisplayName { get; }
    string DeviceFamilies { get; }
    bool LiveTelemetryValidated { get; }
    bool SafeAutoDetectionValidated { get; }
    bool CommandsValidated { get; }
}

public sealed record ManufacturerDefinition(
    string Id,
    string DisplayName,
    string DeviceFamilies,
    bool LiveTelemetryValidated = false,
    bool SafeAutoDetectionValidated = false,
    bool CommandsValidated = false) : ITrackerAdapter;

public static class AdapterCatalog
{
    public static IReadOnlyList<ManufacturerDefinition> All { get; } = new[]
    {
        new ManufacturerDefinition("trina", "Trina Solar", "NCU / TCU"),
        new ManufacturerDefinition("powerway", "Powerway", "Modelos pendientes"),
        new ManufacturerDefinition("artech", "Artech / Arctech (por confirmar)", "Modelos pendientes")
    };
}

// Software references are static inventory only: no binaries are embedded or launched.
public sealed record VendorSoftwareReference(
    string ManufacturerId,
    string Family,
    string OriginalProduct,
    string Version,
    string ObservedFunctions,
    string Evidence);

public static class SoftwareCatalog
{
    public static IReadOnlyList<VendorSoftwareReference> All { get; } = new[]
    {
        new VendorSoftwareReference("trina", "NCU", "NCU Monitor", "2.71",
            "Monitoreo de control central, viento, parámetros astronómicos y comunicación",
            "Inspección estática de ejecutable facilitado"),
        new VendorSoftwareReference("trina", "TCU", "TCU Multi (80 canales)", "2.73",
            "Seguimiento, ángulos, alarmas, motor, límites y configuración",
            "Inspección estática de ejecutable facilitado"),
        new VendorSoftwareReference("powerway", "NCU", "Communication Box", "2.161",
            "Comunicación central, ZigBee, viento, datos meteorológicos y control grupal",
            "Inspección estática de ejecutable facilitado"),
        new VendorSoftwareReference("powerway", "TCU", "Tracker Test for Customer", "5.2.2",
            "Ángulos, corriente de motores, protección, nieve, lluvia, límites y alarmas",
            "Inspección estática de ejecutable facilitado")
    };

    public static VendorSoftwareReference? Find(string manufacturerId, string family) =>
        All.FirstOrDefault(x => x.ManufacturerId == manufacturerId && x.Family == family);
}

public sealed class TrackerReading
{
    public string Tracker { get; set; } = "";
    public string Estado { get; set; } = "";
    public string Angulo { get; set; } = "";
    public string Motor { get; set; } = "";
    public string Modo { get; set; } = "";
    public string Fuente { get; set; } = "DEMO";
}

public sealed class TrackerAlert
{
    public string Nivel { get; set; } = "";
    public string Equipo { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public string Fuente { get; set; } = "DEMO";
}

public sealed class JournalEntry
{
    public string Fecha { get; set; } = "";
    public string Fabricante { get; set; } = "";
    public string Nota { get; set; } = "";
}

public static class DemoFactory
{
    // Deliberately fictional equipment IDs and measurements. Never infer plant health.
    public static List<TrackerReading> Readings(ManufacturerDefinition manufacturer)
    {
        var list = new List<TrackerReading>();
        for (var i = 1; i <= 12; i++)
        {
            var alert = i == 4 || i == 9;
            list.Add(new TrackerReading
            {
                Tracker = "DEMO-TCU-" + i.ToString("00"),
                Estado = alert ? "ALERTA SIMULADA" : "NORMAL SIMULADO",
                Angulo = (i * 4 - 28).ToString("+0;-0;0") + "°",
                Motor = (alert ? 1.8 : 0.4 + i * 0.03).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + " A",
                Modo = manufacturer.Id == "trina" ? "Seguimiento (DEMO)" : "Automático (DEMO)",
                Fuente = "DEMO — no es telemetría"
            });
        }
        return list;
    }

    public static List<TrackerAlert> Alerts()
    {
        return new List<TrackerAlert>
        {
            new() { Nivel = "AVISO SIMULADO", Equipo = "DEMO-TCU-04", Descripcion = "Ejemplo de pérdida de comunicación. No corresponde a un equipo real." },
            new() { Nivel = "AVISO SIMULADO", Equipo = "DEMO-TCU-09", Descripcion = "Ejemplo de consumo de motor elevado. No corresponde a un equipo real." }
        };
    }
}
