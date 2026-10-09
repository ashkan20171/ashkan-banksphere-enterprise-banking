using System.Drawing;
using System.Windows.Forms;
namespace AshkanBankSphere;
partial class DashboardForm
{
    private DataGridView grid = null!;
    private Label heading = null!, dateLabel = null!;
    private Label summaryLabel = null!;
    private BankingSummaryCards summaryCards = null!;
    private TransferTrendPanel trends = null!;
    private TextBox searchBox = null!;
    private Panel sidebar = null!;
    private Button accountsButton = null!, customersButton = null!, transfersButton = null!, auditButton = null!, chartButton = null!, addButton = null!, openAccountButton = null!, transferButton = null!, languageButton = null!, refreshButton = null!, exportButton = null!;
    private FlowLayoutPanel actions = null!;
    private Button themeButton = null!;
    private Panel topPanel = null!, contentPanel = null!;
    private void InitializeComponent()
    {
        Text = "Ashkan BankSphere";
        WindowState = FormWindowState.Maximized;
        MinimumSize = new Size(900, 580);
        BackColor = Color.FromArgb(240, 245, 250);
        Font = new Font("Segoe UI", 11);
        sidebar = new Panel { Dock = DockStyle.Right, Width = 246, BackColor = Color.FromArgb(14, 29, 51) };
        var brand = new Label { Text = "◈ BANKSPHERE", ForeColor = Color.FromArgb(88, 223, 207), Font = new Font("Segoe UI", 18, FontStyle.Bold), AutoSize = true, Location = new Point(18, 30) };
        sidebar.Controls.Add(brand);
        accountsButton = Nav(110, () => LoadTable("Accounts"));
        customersButton = Nav(171, () => LoadTable("Customers"));
        transfersButton = Nav(232, () => LoadTable("Transfers"));
        auditButton = Nav(293, () => LoadTable("Audit"));
        chartButton = Nav(354, () => ShowTrends());
        languageButton = Nav(430, () => Localization.SetLanguage(!Localization.Persian));
        themeButton = Nav(491, () => ToggleTheme());
        sidebar.Controls.AddRange(new Control[] { accountsButton, customersButton, transfersButton, auditButton, chartButton, languageButton, themeButton });
        topPanel = new Panel { Dock = DockStyle.Top, Height = 106, BackColor = Color.White };
        heading = new Label { Font = new Font("Segoe UI", 23, FontStyle.Bold), Location = new Point(30, 17), AutoSize = true, ForeColor = Color.FromArgb(18, 44, 75) };
        dateLabel = new Label { Font = new Font("Segoe UI", 10), Location = new Point(34, 70), AutoSize = true, ForeColor = Color.FromArgb(92, 108, 127) };
        topPanel.Controls.AddRange(new Control[] { heading, dateLabel });
        summaryLabel = new Label { Visible = false };
        summaryCards = new BankingSummaryCards { Dock = DockStyle.Top, Height = 150 };
        actions = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 125, Padding = new Padding(20, 15, 12, 8), BackColor = Color.FromArgb(240, 245, 250) };
        addButton = ActionButton(); addButton.Click += AddCustomer;
        openAccountButton = ActionButton(); openAccountButton.Click += OpenAccount;
        transferButton = ActionButton(); transferButton.Click += Transfer;
        refreshButton = ActionButton(); refreshButton.Click += (_, _) => LoadTable(currentTable);
        exportButton = ActionButton(); exportButton.Click += ExportCsv;
        actions.Controls.AddRange(new Control[] { addButton, openAccountButton, transferButton, refreshButton, exportButton });
        grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White, BorderStyle = BorderStyle.None, AllowUserToAddRows = false, AllowUserToDeleteRows = false, RowHeadersVisible = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, EnableHeadersVisualStyles = false, ColumnHeadersHeight = 45, RowTemplate = { Height = 38 } };
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(24, 48, 77);
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(202, 239, 234);
        grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 38, 61);
        searchBox = new TextBox { Dock = DockStyle.Top, Height = 38, PlaceholderText = "Search", Font = new Font("Segoe UI", 12), Margin = new Padding(8) };
        searchBox.TextChanged += (_, _) => FilterRows();
        trends = new TransferTrendPanel { Dock = DockStyle.Bottom, Height = 185, Visible = false };
        contentPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(22) };
        contentPanel.Controls.Add(grid); contentPanel.Controls.Add(trends); contentPanel.Controls.Add(searchBox);
        Controls.Add(contentPanel); Controls.Add(actions); Controls.Add(summaryCards); Controls.Add(topPanel); Controls.Add(sidebar);
    }
    private static Button Nav(int y, Action action)
    {
        var button = new Button { Location = new Point(15, y), Size = new Size(216, 49), FlatStyle = FlatStyle.Flat, ForeColor = Color.White, BackColor = Color.FromArgb(31, 57, 84), Cursor = Cursors.Hand };
        button.FlatAppearance.BorderSize = 0;
        button.Click += (_, _) => action();
        return button;
    }
    private static Button ActionButton()
    {
        var button = new Button { Width = 190, Height = 45, Margin = new Padding(5), FlatStyle = FlatStyle.Flat, ForeColor = Color.White, BackColor = Color.FromArgb(15, 133, 133), Cursor = Cursors.Hand };
        button.FlatAppearance.BorderSize = 0;
        return button;
    }
}
