using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Orbi.TrackerStudio;

// View-only modules. They never instantiate a SerialPort, network client or firmware transport.
public sealed partial class MainWindow
{
    private void ShowNcuWorkspace()
    {
        var reference = SoftwareCatalog.Find(_selected.Id, "NCU");
        var page = PageFrame("NCU / Comunicación central — " + _selected.DisplayName,
            reference is null ? "Software original aún no catalogado" : reference.OriginalProduct + " · " + reference.Version + " · referencia estática");
        var content = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4,
            Padding = new Padding(0, 0, 0, 4)
        };
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 133));
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
        content.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        content.Controls.Add(InfoStrip("CENTRAL DESCONECTADA",
            "No hay lecturas de viento, radio, GPS, firmware ni seguimiento en tiempo real. Modo seguro de demostración.",
            ColorTranslator.FromHtml("#F4A261")), 0, 0);

        var cards = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1 };
        for (var i = 0; i < 3; i++) cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333f));
        cards.Controls.Add(StatusCard("COMUNICACIÓN", "SIN ENLACE",
            "COM: no abierto\nRadio: sin validar", MutedColor), 0, 0);
        cards.Controls.Add(StatusCard("SENSORES", "SIN LECTURA",
            "Viento y temperatura\nSin fuente real", MutedColor), 1, 0);
        cards.Controls.Add(StatusCard("CONTROLADORES", "NO IDENTIFICADOS",
            "NCU / TCU: 0 conectados\nNo se ha consultado ningún equipo", MutedColor), 2, 0);
        content.Controls.Add(cards, 0, 1);
        content.Controls.Add(InfoStrip("PROGRAMA DE REFERENCIA",
            reference is null ? "No se dispone de inventario para esta familia." : reference.ObservedFunctions,
            AccentColor), 0, 2);

        var footer = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
        footer.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));
        footer.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        footer.Controls.Add(MakeLabel("Qué se incorporará después de validar el protocolo", 12, TextColor, 5, 4, true, 800, 35), 0, 0);
        var text = new RichTextBox
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            BorderStyle = BorderStyle.None,
            Font = new Font("Segoe UI", 10.5f),
            BackColor = PanelColor,
            ForeColor = TextColor,
            Text = "• Identidad de la NCU/ComBox y versión de firmware.\n"
                + "• Estado de enlaces y cantidad real de trackers visibles.\n"
                + "• Variables meteorológicas y estado de protección de viento.\n"
                + "• Alarmas de comunicación con fuente, fecha y validez de lectura.\n\n"
                + "No disponible: órdenes grupales, escritura de parámetros, reseteos o movimiento.\n"
                + "Las pantallas originales de distintos fabricantes pueden parecerse sin usar el mismo protocolo."
        };
        footer.Controls.Add(text, 0, 1);
        content.Controls.Add(footer, 0, 3);
        page.Controls.Add(content, 0, 1);
        _pageHost.Controls.Add(page);
    }

    private void ShowTcuWorkspace()
    {
        var reference = SoftwareCatalog.Find(_selected.Id, "TCU");
        var page = PageFrame("TCU / Seguidores — " + _selected.DisplayName,
            (reference?.OriginalProduct ?? "Sin programa catalogado") + " · EJEMPLOS SIMULADOS · Sin conexión");
        var body = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 1 };
        body.RowStyles.Add(new RowStyle(SizeType.Absolute, 63));
        body.RowStyles.Add(new RowStyle(SizeType.Absolute, 57));
        body.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        body.Controls.Add(InfoStrip("SEGUIDORES DE DEMOSTRACIÓN",
            "IDs DEMO-TCU y mediciones ficticias para evaluar el diseño. NO representan equipos físicos.", 
            ColorTranslator.FromHtml("#F4A261")), 0, 0);
        var filters = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, WrapContents = false,
            Padding = new Padding(4, 9, 0, 2)
        };
        var search = new TextBox { Width = 230, Font = new Font("Segoe UI", 10f) };
        search.PlaceholderText = "Buscar identificador DEMO...";
        var mode = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList, Width = 155,
            Font = new Font("Segoe UI", 10f), BackColor = Color.White
        };
        mode.Items.AddRange(new object[] { "Todos", "Avisos simulados", "Normales simulados" });
        mode.SelectedIndex = 0;
        var count = new Label
        {
            Text = "", Width = 210, Height = 30, TextAlign = ContentAlignment.MiddleLeft,
            ForeColor = AccentColor, Font = new Font("Segoe UI", 9f)
        };
        filters.Controls.Add(search);
        filters.Controls.Add(mode);
        filters.Controls.Add(count);
        body.Controls.Add(filters, 0, 1);

        var grid = CreateGrid();
        body.Controls.Add(grid, 0, 2);
        void Filter()
        {
            var query = search.Text.Trim();
            var rows = _readings
                .Where(r => (query.Length == 0 || r.Tracker.Contains(query, StringComparison.OrdinalIgnoreCase))
                    && (mode.SelectedIndex == 0
                        || (mode.SelectedIndex == 1 && r.Estado.StartsWith("ALERTA", StringComparison.OrdinalIgnoreCase))
                        || (mode.SelectedIndex == 2 && r.Estado.StartsWith("NORMAL", StringComparison.OrdinalIgnoreCase))))
                .ToList();
            grid.DataSource = new BindingList<TrackerReading>(rows);
            count.Text = rows.Count + " resultado(s) — DEMO";
        }
        search.TextChanged += (_, _) => Filter();
        mode.SelectedIndexChanged += (_, _) => Filter();
        Filter();
        page.Controls.Add(body, 0, 1);
        _pageHost.Controls.Add(page);
    }

    private void ShowDiagnosticsWorkspace()
    {
        var page = PageFrame("Diagnóstico guiado — SIN CONEXIÓN",
            "Lista de comprobación para preparar una prueba autorizada. No ejecuta acciones ni consultas.");
        var body = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 1 };
        body.RowStyles.Add(new RowStyle(SizeType.Absolute, 68));
        body.RowStyles.Add(new RowStyle(SizeType.Absolute, 64));
        body.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        body.Controls.Add(InfoStrip("VERIFICACIONES NO EJECUTADAS",
            "Las siguientes comprobaciones son instrucciones, no resultados de diagnóstico.", 
            ColorTranslator.FromHtml("#F4A261")), 0, 0);
        var top = new Panel { Dock = DockStyle.Fill, BackColor = RaisedColor, Padding = new Padding(9) };
        var label = new Label
        {
            Left = 12, Top = 14, AutoSize = false, Height = 42, Width = 580,
            ForeColor = TextColor, Font = new Font("Segoe UI", 10f),
            Text = "Inventario COM del sistema: " + DescribePorts()
        };
        var refresh = MakeButton("Actualizar listado", AccentColor, 620, 10, 170, 38, darkText: true);
        refresh.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        refresh.Click += (_, _) =>
        {
            RefreshComNames();
            label.Text = "Inventario COM del sistema: " + DescribePorts();
        };
        top.Resize += (_, _) =>
        {
            refresh.Left = Math.Max(350, top.ClientSize.Width - 190);
            label.Width = Math.Max(250, refresh.Left - 25);
        };
        top.Controls.Add(label);
        top.Controls.Add(refresh);
        body.Controls.Add(top, 0, 1);

        var grid = CreateGrid();
        grid.DataSource = new BindingList<OfflineCheck>(FieldGuides.Checks(_selected.Id).ToList());
        body.Controls.Add(grid, 0, 2);
        page.Controls.Add(body, 0, 1);
        _pageHost.Controls.Add(page);
    }

    private string DescribePorts()
    {
        var ports = PortInventory.ReadComNames();
        return ports.Count == 0 ? "ningún nombre COM detectado." : string.Join(", ", ports) + " (solo inventario).";
    }

    private void ShowGlossaryWorkspace()
    {
        var page = PageFrame("Diccionario técnico inglés → español",
            "Traducciones orientativas O&M; confirmar nombre, unidad y significado en cada firmware.");
        var grid = CreateGrid();
        grid.DataSource = new BindingList<SpanishTerm>(FieldGuides.Glossary.ToList());
        page.Controls.Add(grid, 0, 1);
        _pageHost.Controls.Add(page);
    }

    private void ShowCompatibilityWorkspace()
    {
        var page = PageFrame("Compatibilidad de software y hardware",
            "Se identificaron interfaces de tres fabricantes; NINGÚN protocolo de hardware está validado.");
        var body = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
        body.RowStyles.Add(new RowStyle(SizeType.Absolute, 64));
        body.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        body.Controls.Add(InfoStrip("6 ROLES CATALOGADOS · 0 CONEXIONES REALES",
            "Arctech tiene un solo EXE con secciones Tracker y ComBox; esa presencia no prueba paridad funcional.",
            ColorTranslator.FromHtml("#F4A261")), 0, 0);
        var grid = CreateGrid();
        grid.DataSource = new BindingList<SoftwareCompatibility>(FieldGuides.Matrix.ToList());
        body.Controls.Add(grid, 0, 1);
        page.Controls.Add(body, 0, 1);
        _pageHost.Controls.Add(page);
    }

    private static Panel InfoStrip(string title, string detail, Color accent)
    {
        var p = new Panel { Dock = DockStyle.Fill, Margin = new Padding(2, 3, 3, 7), BackColor = RaisedColor };
        var stripe = new Panel { BackColor = accent, Dock = DockStyle.Left, Width = 5 };
        var heading = MakeLabel(title, 10, accent, 16, 6, true, 850, 26);
        heading.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
        var body = MakeLabel(detail, 9, TextColor, 16, 30, false, 890, 30);
        body.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
        p.Resize += (_, _) =>
        {
            heading.Width = Math.Max(200, p.ClientSize.Width - 26);
            body.Width = Math.Max(200, p.ClientSize.Width - 26);
        };
        p.Controls.Add(stripe);
        p.Controls.Add(heading);
        p.Controls.Add(body);
        return p;
    }

    private static Panel StatusCard(string label, string state, string note, Color color)
    {
        var p = new Panel { Dock = DockStyle.Fill, Margin = new Padding(3, 10, 10, 10), BackColor = RaisedColor };
        p.Controls.Add(MakeLabel(label, 9, MutedColor, 14, 10, true, 300, 24));
        p.Controls.Add(MakeLabel(state, 17, color, 14, 37, true, 320, 37));
        p.Controls.Add(MakeLabel(note, 8, TextColor, 14, 80, false, 320, 45));
        return p;
    }
}
