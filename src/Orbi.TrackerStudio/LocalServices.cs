using Microsoft.Win32;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace Orbi.TrackerStudio;

public static class PortInventory
{
    // Read Windows' COM-name inventory. DOES NOT open a serial port or send any bytes.
    public static IReadOnlyList<string> ReadComNames()
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DEVICEMAP\SERIALCOMM");
            if (key is null) return Array.Empty<string>();
            return key.GetValueNames()
                .Select(name => key.GetValue(name)?.ToString() ?? "")
                .Where(value => value.StartsWith("COM", StringComparison.OrdinalIgnoreCase))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
                .ToArray();
        }
        catch
        {
            // No port access should ever prevent the offline dashboard from opening.
            return Array.Empty<string>();
        }
    }
}

public static class LocalJournal
{
    private static bool _unreadableJournal;
    private static readonly string DirectoryPath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ORBI", "TrackerStudio");
    public static string FilePath => Path.Combine(DirectoryPath, "notes.json");

    public static List<JournalEntry> Load()
    {
        try
        {
            if (!File.Exists(FilePath)) return new List<JournalEntry>();
            var json = File.ReadAllText(FilePath, Encoding.UTF8);
            return JsonSerializer.Deserialize<List<JournalEntry>>(json) ?? new List<JournalEntry>();
        }
        catch
        {
            // Preserve an unreadable journal rather than silently overwriting it later.
            _unreadableJournal = true;
            return new List<JournalEntry>();
        }
    }

    public static void Save(IEnumerable<JournalEntry> entries)
    {
        if (_unreadableJournal)
            throw new InvalidOperationException("Existe una bitácora que no se pudo leer. Haz un respaldo de notes.json antes de continuar.");
        Directory.CreateDirectory(DirectoryPath);
        var content = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
        var temporaryPath = FilePath + ".tmp";
        File.WriteAllText(temporaryPath, content, new UTF8Encoding(false));
        // Preserve the most recent readable version in case of accidental overwrite.
        if (File.Exists(FilePath)) File.Copy(FilePath, FilePath + ".bak", true);
        File.Move(temporaryPath, FilePath, true);
    }
}

public static class DemoCsvExporter
{
    public static void Write(string destination, ManufacturerDefinition manufacturer, IEnumerable<TrackerReading> readings)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ADVERTENCIA;ESTOS DATOS SON SIMULADOS;NO USAR PARA OPERACIONES EN TERRENO");
        sb.AppendLine("Fabricante;Tracker;Estado;Ángulo;Motor;Modo;Fuente");
        foreach (var row in readings)
        {
            sb.AppendLine(string.Join(";", new[]
            {
                Cell(manufacturer.DisplayName), Cell(row.Tracker), Cell(row.Estado), Cell(row.Angulo),
                Cell(row.Motor), Cell(row.Modo), Cell(row.Fuente)
            }));
        }
        File.WriteAllText(destination, sb.ToString(), new UTF8Encoding(true));
    }

    private static string Cell(string? raw)
    {
        var value = raw ?? "";
        // Prevent accidental spreadsheet formula evaluation on exported text.
        if (value.Length > 0 && "=+-@\t\r".Contains(value[0])) value = "'" + value;
        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }
}


public static class JournalCsvExporter
{
    public static void Write(string destination, IEnumerable<JournalEntry> entries)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Fecha;Fabricante;Nota");
        foreach (var entry in entries)
        {
            sb.AppendLine(string.Join(";", new[]
            {
                Cell(entry.Fecha), Cell(entry.Fabricante), Cell(entry.Nota)
            }));
        }
        File.WriteAllText(destination, sb.ToString(), new UTF8Encoding(true));
    }

    private static string Cell(string? raw)
    {
        var value = (raw ?? "").Replace("\r\n", "\n").Replace('\r', '\n');
        if (value.Length > 0 && "=+-@\t\r".Contains(value[0])) value = "'" + value;
        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }
}
