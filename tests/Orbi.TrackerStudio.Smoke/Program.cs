using System.Text;
using Orbi.TrackerStudio;

static void Check(bool condition, string message)
{
    if (!condition) throw new InvalidOperationException("SMOKE FAILED: " + message);
    Console.WriteLine("PASS: " + message);
}

Check(AdapterCatalog.All.Count == 3, "tres fabricantes catalogados");
Check(AdapterCatalog.All.Select(x => x.Id).Distinct().Count() == 3, "identificadores únicos");
Check(AdapterCatalog.All[0].Id == "trina" && AdapterCatalog.All[0].DevelopmentPriority.StartsWith("P1"), "Trina prioridad 1");
Check(AdapterCatalog.All[1].Id == "powerway" && AdapterCatalog.All[1].DevelopmentPriority.StartsWith("P2"), "Powerway prioridad 2");
Check(AdapterCatalog.All[2].Id == "arctech" && AdapterCatalog.All[2].DevelopmentPriority.Contains("diferida"), "Arctech prioridad diferida");
Check(AdapterCatalog.All.All(x => !x.LiveTelemetryValidated && !x.SafeAutoDetectionValidated && !x.CommandsValidated),
    "ningún fabricante anuncia protocolo validado ni comandos operativos");

Check(SoftwareCatalog.All.Count == 6, "seis roles NCU/TCU catalogados");
foreach (var vendor in AdapterCatalog.All)
{
    Check(SoftwareCatalog.Find(vendor.Id, "NCU") is not null, vendor.DisplayName + " tiene rol NCU");
    Check(SoftwareCatalog.Find(vendor.Id, "TCU") is not null, vendor.DisplayName + " tiene rol TCU");
    var readings = DemoFactory.Readings(vendor);
    Check(readings.Count == 12, vendor.DisplayName + " entrega 12 filas DEMO");
    Check(readings.All(r => r.Tracker.StartsWith("DEMO-") && r.Fuente.Contains("DEMO")),
        vendor.DisplayName + " etiqueta todas las filas como simuladas");
    Check(FieldGuides.Checks(vendor.Id).Count >= 7, vendor.DisplayName + " guía diagnóstica disponible");
}
Check(SoftwareCatalog.All.Where(x => x.ManufacturerId == "arctech")
    .All(x => x.OriginalProduct.StartsWith("Tracker AI Controller")),
    "Arctech comparte un ejecutable de referencia para ambos roles");
Check(FieldGuides.Matrix.Count == SoftwareCatalog.All.Count, "matriz coincide con catálogo");
Check(FieldGuides.Matrix.All(c => c.Estado.Contains("NO VALIDADO")), "todas las interfaces siguen sin validar");
Check(FieldGuides.Glossary.Count >= 15, "glosario técnico español disponible");

var alerts = DemoFactory.Alerts();
Check(alerts.All(x => x.Nivel.Contains("SIMULADO") && x.Fuente == "DEMO"),
    "alarmas ficticias siempre identificadas");

var destination = Path.Combine(Path.GetTempPath(), "orbi_tracker_test_" + Guid.NewGuid().ToString("N") + ".csv");
try
{
    DemoCsvExporter.Write(destination, AdapterCatalog.All[0],
        new[] { new TrackerReading { Tracker = "=2+3", Estado = "DEMO", Fuente = "DEMO" } });
    var csv = File.ReadAllText(destination, Encoding.UTF8);
    Check(csv.Contains("SON SIMULADOS"), "CSV advierte contenido ficticio");
    Check(csv.Contains("\"'=2+3\""), "CSV neutraliza fórmula de hoja de cálculo");
}
finally
{
    if (File.Exists(destination)) File.Delete(destination);
}
var notesDestination = Path.Combine(Path.GetTempPath(), "orbi_notes_test_" + Guid.NewGuid().ToString("N") + ".csv");
try
{
    JournalCsvExporter.Write(notesDestination,
        new[] { new JournalEntry { Fecha = "2026-11-11", Fabricante = "Trina", Nota = "=SUM(1,2)" } });
    var notesCsv = File.ReadAllText(notesDestination, Encoding.UTF8);
    Check(notesCsv.Contains("Fecha;Fabricante;Nota"), "encabezados de bitácora exportada");
    Check(notesCsv.Contains("\"'=SUM(1,2)\""), "exportación de notas neutraliza fórmulas CSV");
}
finally
{
    if (File.Exists(notesDestination)) File.Delete(notesDestination);
}
Console.WriteLine("Todos los ensayos de lógica offline finalizaron correctamente.");
