using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Orbi.TrackerStudio;

public sealed class MainWindow : Form
{
    private static readonly Color BackgroundColor = ColorTranslator.FromHtml("#07111F");
    private static readonly Color SidebarColor = ColorTranslator.FromHtml("#0B1828");
    private static readonly Color PanelColor = ColorTranslator.FromHtml("#10253A");
    private static readonly Color RaisedColor = ColorTranslator.FromHtml("#142C43");
    private static readonly Color TextColor = ColorTranslator.FromHtml("#E9F3FF");
    private static readonly Color MutedColor = ColorTranslator.FromHtml("#93AFC5");
    private static readonly Color AccentColor = ColorTranslator.FromHtml("#19C5C0");

    private readonly ComboBox _manufacturerPicker = new();
    private readonly ComboBox _portPicker = new();
    private readonly Panel _pageHost = new();
    private readonly Label _connectedMetric = new();
    private readonly Label _trackerMetric = new();
    private readonly Label _alarmMetric = new();
    private readonly Label _manufacturerMetric = new();
    private readonly Label _portInfo = new();
    private readonly List<JournalEntry> _journal = LocalJournal.Load();
    private List<TrackerReading> _readings = new();
    private readonly List<TrackerAlert> _alerts = DemoFactory.Alerts();
    private ManufacturerDefinition _selected = AdapterCatalog.All[0];
    private string _activePage = "Resumen";

    public MainWindow()
    {
        Text = "ORBI Tracker Studio  •  v0.2 alpha  •  DEMOSTRACIÓN";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1060, 720);
        Size = new Size(1380, 840);
        BackColor = BackgroundColor;
        ForeColor = TextColor;
        Font = new Font("Segoe UI", 10f);
        DoubleBuffered = true;
        BuildInterface();
        _manufacturerPicker.Items.AddRange(AdapterCatalog.All.Cast<object>().ToArray());
        _manufacturerPicker.SelectedIndex = 0;
        RefreshComNames();
        SwitchPage("Resumen");
    }

    private void BuildInterface()
    {
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, Margin = Padding.Empty, Padding = Padding.Empty };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 238));
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        Controls.Add(root);
        root.Controls.Add(BuildSidebar(), 0, 0);

        var work = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 5, Padding = new Padding(24, 12, 24, 10), BackColor = BackgroundColor };
        work.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
        work.RowStyles.Add(new RowStyle(SizeType.Absolute, 92));
        work.RowStyles.Add(new RowStyle(SizeType.Absolute, 127));
        work.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        work.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        root.Controls.Add(work, 1, 0);

        work.Controls.Add(BuildHeader(), 0, 0);
        work.Controls.Add(BuildToolbar(), 0, 1);
        work.Controls.Add(BuildMetrics(), 0, 2);
        _pageHost.Dock = DockStyle.Fill;
        _pageHost.Padding = new Padding(14, 12, 14, 12);
        _pageHost.BackColor = PanelColor;
        work.Controls.Add(_pageHost, 0, 3);
        work.Controls.Add(MakeLabel("MODO DEMO · Sin conexión real · No se envían comandos · Datos ilustrativos sin validez operativa",
            9, MutedColor, 4, 8), 0, 4);
    }

    private Panel BuildSidebar()
    {
        var bar = new Panel { Dock = DockStyle.Fill, BackColor = SidebarColor };
        bar.Controls.Add(MakeLabel("ORBI", 29, AccentColor, 25, 28, true, 180, 45));
        bar.Controls.Add(MakeLabel("TRACKER STUDIO", 11, TextColor, 28, 76, true, 190, 27));
        bar.Controls.Add(MakeLabel("UNIVERSAL  /  O&M", 9, MutedColor, 28, 109, false, 180, 25));
        bar.Controls.Add(MakeLabel("ESTACIÓN DE TRABAJO", 8, MutedColor, 28, 167, true, 180, 24));
        var pages = new[] { "Resumen", "Alarmas", "Bitácora", "Compatibilidad" };
        for (var i = 0; i < pages.Length; i++)
        {
            var page = pages[i];
            var button = MakeButton("   " + page, RaisedColor, 24, 210 + i * 57, 186, 43);
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Click += (_, _) => SwitchPage(page);
            bar.Controls.Add(button);
        }
        var status = MakeLabel("●  SOLO LECTURA / DEMO\n\nTrina · Powerway · Artech\nProtocolos por validar",
            10, AccentColor, 24, 575, false, 190, 104);
        status.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        bar.Controls.Add(status);
        bar.Resize += (_, _) => status.Top = Math.Max(490, bar.ClientSize.Height - 145);
        return bar;
    }

    private Panel BuildHeader()
    {
        var p = new Panel { Dock = DockStyle.Fill };
        p.Controls.Add(MakeLabel("Estación universal de trackers", 23, TextColor, 8, 10, true, 775, 43));
        p.Controls.Add(MakeLabel("NCU / TCU y controladores compatibles · interfaz unificada en español", 10, MutedColor, 10, 58, false, 760, 28));
        return p;
    }

    private Panel BuildToolbar()
    {
        var p = new Panel { Dock = DockStyle.Fill, BackColor = PanelColor, Padding = new Padding(10) };
        p.Controls.Add(MakeLabel("FABRICANTE", 8, MutedColor, 15, 8, true, 210, 18));
        _manufacturerPicker.SetBounds(14, 32, 252, 31);
        _manufacturerPicker.DropDownStyle = ComboBoxStyle.DropDownList;
        _manufacturerPicker.DisplayMember = nameof(ManufacturerDefinition.DisplayName);
        _manufacturerPicker.FlatStyle = FlatStyle.Flat;
        _manufacturerPicker.BackColor = Color.White;
        _manufacturerPicker.ForeColor = Color.Black;
        _manufacturerPicker.SelectedIndexChanged += (_, _) => SelectManufacturer();
        p.Controls.Add(_manufacturerPicker);
        p.Controls.Add(MakeLabel("PUERTO COM (SOLO INVENTARIO)", 8, MutedColor, 283, 8, true, 255, 18));
        _portPicker.SetBounds(282, 32, 140, 31);
        _portPicker.DropDownStyle = ComboBoxStyle.DropDownList;
        p.Controls.Add(_portPicker);
        var refresh = MakeButton("Actualizar COM", RaisedColor, 438, 30, 143, 36);
        refresh.Click += (_, _) => RefreshComNames();
        p.Controls.Add(refresh);
        var export = MakeButton("Exportar DEMO CSV", AccentColor, 595, 30, 172, 36, darkText: true);
        export.Click += (_, _) => ExportCsv();
        p.Controls.Add(export);
        _portInfo.SetBounds(782, 32, 340, 40);
        _portInfo.ForeColor = MutedColor;
        _portInfo.Font = new Font("Segoe UI", 9f);
        p.Controls.Add(_portInfo);
        return p;
    }

    private TableLayoutPanel BuildMetrics()
    {
        var metrics = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1, Padding = Padding.Empty };
        for (var i = 0; i < 4; i++) metrics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
        metrics.Controls.Add(MetricCard("TRACKERS DE EJEMPLO", _trackerMetric), 0, 0);
        metrics.Controls.Add(MetricCard("ALARMAS SIMULADAS", _alarmMetric), 1, 0);
        metrics.Controls.Add(MetricCard("EQUIPOS CONECTADOS", _connectedMetric), 2, 0);
        metrics.Controls.Add(MetricCard("FABRICANTES PREVISTOS", _manufacturerMetric), 3, 0);
        return metrics;
    }

    private Panel MetricCard(string title, Label metric)
    {
        var card = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0, 10, 12, 10), BackColor = RaisedColor };
        card.Controls.Add(MakeLabel(title, 8, MutedColor, 14, 12, true, 220, 25));
        metric.SetBounds(14, 37, 180, 53);
        metric.Font = new Font("Segoe UI Semibold", 26f, FontStyle.Bold);
        metric.ForeColor = TextColor;
        card.Controls.Add(metric);
        return card;
    }

    private void SelectManufacturer()
    {
        if (_manufacturerPicker.SelectedItem is not ManufacturerDefinition manufacturer) return;
        _selected = manufacturer;
        _readings = DemoFactory.Readings(manufacturer);
        _trackerMetric.Text = _readings.Count.ToString();
        _alarmMetric.Text = _alerts.Count.ToString();
        _connectedMetric.Text = "0";
        _manufacturerMetric.Text = AdapterCatalog.All.Count.ToString();
        SwitchPage(_activePage);
    }

    private void SwitchPage(string page)
    {
        _activePage = page;
        _pageHost.Controls.Clear();
        if (page == "Resumen") ShowOverview();
        else if (page == "Alarmas") ShowAlerts();
        else if (page == "Bitácora") ShowJournal();
        else ShowCompatibility();
    }

    private void ShowOverview()
    {
        var panel = PageFrame("Resumen de trackers — DATOS SIMULADOS",
            "Esta tabla no representa ningún parque real, controlador conectado ni lectura de campo.");
        var grid = CreateGrid();
        grid.DataSource = new BindingList<TrackerReading>(_readings);
        panel.Controls.Add(grid, 0, 1);
        _pageHost.Controls.Add(panel);
    }

    private void ShowAlerts()
    {
        var panel = PageFrame("Alarmas de demostración", "No existen alarmas leídas de NCU o TCU reales.");
        var grid = CreateGrid();
        grid.DataSource = new BindingList<TrackerAlert>(_alerts);
        panel.Controls.Add(grid, 0, 1);
        _pageHost.Controls.Add(panel);
    }

    private void ShowJournal()
    {
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 1 };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 68));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 116));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.Controls.Add(SectionHeader("Bitácora local del operador", "Las notas son tuyas; no son registros automáticos de equipos."), 0, 0);

        var input = new Panel { Dock = DockStyle.Fill, BackColor = RaisedColor };
        var editor = new TextBox { Multiline = true, ScrollBars = ScrollBars.Vertical, BackColor = Color.White,
            ForeColor = Color.Black, SetBoundsPlaceholder = 0 };
        // SetBoundsPlaceholder is intentionally replaced in the build before release.
        layout.Controls.Add(input, 0, 1);
        var grid = CreateGrid();
        grid.DataSource = new BindingList<JournalEntry>(_journal.OrderByDescending(x => x.Fecha).ToList());
        layout.Controls.Add(grid, 0, 2);
        _pageHost.Controls.Add(layout);
    }

    private void ShowCompatibility()
    {
        var layout = PageFrame("Matriz de compatibilidad", "Se habilitará comunicación al validar cada modelo y firmware en banco.");
        var info = new RichTextBox
        {
            Dock = DockStyle.Fill,
            BorderStyle = BorderStyle.None,
            ReadOnly = true,
            BackColor = PanelColor,
            ForeColor = TextColor,
            Font = new Font("Consolas", 11f),
            Text =
                "FABRICANTE               FAMILIAS                 TELEMETRÍA REAL\n" +
                "Trina Solar              NCU / TCU                PENDIENTE\n" +
                "Powerway                 Por identificar          PENDIENTE\n" +
                "Artech / Arctech         Nombre y modelos         PENDIENTE\n\n" +
                "Detección automática: NO DISPONIBLE.\n" +
                "Escritura de parámetros: NO IMPLEMENTADA.\n" +
                "Control de movimiento: NO IMPLEMENTADO.\n" +
                "Inventario de puertos COM: SÍ, sin abrir puertos.\n\n" +
                "Se requiere comparar programas, manuales y tramas autorizadas\n" +
                "de lectura antes de añadir conexiones reales."
        };
        layout.Controls.Add(info, 0, 1);
        _pageHost.Controls.Add(layout);
    }

    private static TableLayoutPanel PageFrame(string title, string subtitle)
    {
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 1 };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.Controls.Add(SectionHeader(title, subtitle), 0, 0);
        return layout;
    }

    private static Panel SectionHeader(string title, string subtitle)
    {
        var p = new Panel { Dock = DockStyle.Fill };
        p.Controls.Add(MakeLabel(title, 15, TextColor, 3, 4, true, 800, 30));
        p.Controls.Add(MakeLabel(subtitle, 9, MutedColor, 4, 40, false, 900, 24));
        return p;
    }

    private static DataGridView CreateGrid()
    {
        return new DataGridView
        {
            Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false,
            AllowUserToDeleteRows = false, AllowUserToResizeRows = false, MultiSelect = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoGenerateColumns = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor = PanelColor, GridColor = ColorTranslator.FromHtml("#28425A"),
            BorderStyle = BorderStyle.None, RowHeadersVisible = false,
            EnableHeadersVisualStyles = false, ColumnHeadersHeight = 42,
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
            ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = RaisedColor, ForeColor = AccentColor,
                SelectionBackColor = RaisedColor, Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Padding = new Padding(8)
            },
            DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = PanelColor, ForeColor = TextColor, SelectionBackColor = ColorTranslator.FromHtml("#19465A"),
                SelectionForeColor = Color.White, Font = new Font("Segoe UI", 9f),
                Padding = new Padding(8)
            },
            AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = RaisedColor, ForeColor = TextColor, SelectionBackColor = ColorTranslator.FromHtml("#19465A"),
                SelectionForeColor = Color.White, Font = new Font("Segoe UI", 9f),
                Padding = new Padding(8)
            },
            RowTemplate = { Height = 42 }
        };
    }

    private void RefreshComNames()
    {
        var ports = PortInventory.ReadComNames();
        _portPicker.Items.Clear();
        _portPicker.Items.Add("(sin seleccionar)");
        foreach (var port in ports) _portPicker.Items.Add(port);
        _portPicker.SelectedIndex = 0;
        _portInfo.Text = ports.Count == 0 ? "No se detectaron nombres COM." : ports.Count + " puerto(s) encontrado(s).\nNo se estableció conexión.";
    }

    private void ExportCsv()
    {
        using var dialog = new SaveFileDialog
        {
            Title = "Exportar telemetría SIMULADA",
            Filter = "CSV delimitado por punto y coma (*.csv)|*.csv",
            FileName = "ORBI_Tracker_DEMO_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".csv"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            DemoCsvExporter.Write(dialog.FileName, _selected, _readings);
            MessageBox.Show(this, "CSV exportado. Contiene SOLAMENTE datos ficticios.", "ORBI Tracker Studio",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "No se pudo exportar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private static Button MakeButton(string text, Color back, int x, int y, int width, int height, bool darkText = false)
    {
        return new Button
        {
            Text = text, Left = x, Top = y, Width = width, Height = height,
            FlatStyle = FlatStyle.Flat, BackColor = back, ForeColor = darkText ? BackgroundColor : TextColor,
            Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
            Cursor = Cursors.Hand, UseVisualStyleBackColor = false,
            FlatAppearance = { BorderSize = 0 }
        };
    }

    private static Label MakeLabel(string text, float size, Color color, int x, int y, bool bold = false, int width = 850, int height = 34)
    {
        return new Label
        {
            Text = text, Left = x, Top = y, Width = width, Height = height,
            ForeColor = color, BackColor = Color.Transparent,
            Font = new Font("Segoe UI", size, bold ? FontStyle.Bold : FontStyle.Regular),
            TextAlign = ContentAlignment.MiddleLeft
        };
    }
}
