using AshkanBankSphere.Infrastructure;
namespace AshkanBankSphere;
public partial class DashboardForm : Form
{
    private readonly BankService service = new();
    private string currentTable = "Accounts";
    private bool darkTheme;
    private readonly Button loansButton = new() { AutoSize = true, Height = 38, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(20, 137, 148), ForeColor = Color.White };
    private readonly Button branchesButton = new() { AutoSize = true, Height = 38, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(20, 137, 148), ForeColor = Color.White };
    private readonly Button manageUsersButton = new() { AutoSize = true, Height = 38, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(20, 137, 148), ForeColor = Color.White };
    public DashboardForm()
    {
        InitializeComponent();
        actions.Controls.Add(manageUsersButton);
        actions.Controls.Add(branchesButton);
        actions.Controls.Add(loansButton);
        loansButton.Click += (_, _) => { using var dialog = new LoanForm(); dialog.ShowDialog(this); };
        branchesButton.Click += (_, _) => { using var dialog = new BranchForm(); dialog.ShowDialog(this); };
        manageUsersButton.Click += (_, _) => { using var dialog = new UserManagementForm(); dialog.ShowDialog(this); };
        Localization.Changed += OnLanguageChanged;
        FormClosed += (_, _) => Localization.Changed -= OnLanguageChanged;
        ApplyLanguage();
        ApplyTheme();
        ApplyPermissions();
        LoadTable(currentTable);
    }
    private void OnLanguageChanged(object? sender, EventArgs e) => ApplyLanguage();
    private void ApplyLanguage()
    {
        RightToLeft = Localization.Persian ? RightToLeft.Yes : RightToLeft.No;
        sidebar.Dock = Localization.Persian ? DockStyle.Right : DockStyle.Left;
        actions.FlowDirection = Localization.Persian ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        loansButton.Text = Localization.T("درخواست‌های تسهیلات", "Loan applications");
        branchesButton.Text = Localization.T("مدیریت شعب", "Branches");
        manageUsersButton.Text = Localization.T("مدیریت کاربران", "User management");
        accountsButton.Text = Localization.T("حساب‌های بانکی", "Bank accounts");
        customersButton.Text = Localization.T("مدیریت مشتریان", "Customers");
        transfersButton.Text = Localization.T("تاریخچه انتقال‌ها", "Transfers history");
        auditButton.Text = Localization.T("رویدادهای امنیتی", "Audit events");
        themeButton.Text = darkTheme ? Localization.T("☀ تم روشن", "☀ Light theme") : Localization.T("☾ تم تاریک", "☾ Dark theme");
        Text = $"Ashkan BankSphere  •  {BankSession.Username} ({BankSession.Role})";
        chartButton.Text = Localization.T("نمودار انتقال‌ها", "Transfer chart");
        trends.Title = Localization.T("مبلغ انتقال روزانه (۱۴ روز اخیر)", "Daily transfer amount (last 14 days)");
        openAccountButton.Text = Localization.T("+ افتتاح حساب", "+ Open account");
        searchBox.PlaceholderText = Localization.T("جست‌وجو در اطلاعات...", "Search records...");
        languageButton.Text = Localization.T("English  /  تغییر زبان", "فارسی  /  Language");
        addButton.Text = Localization.T("+ مشتری جدید", "+ New customer");
        transferButton.Text = Localization.T("⇄ انتقال وجه", "⇄ Transfer funds");
        refreshButton.Text = Localization.T("↻ تازه‌سازی", "↻ Refresh");
        exportButton.Text = Localization.T("↓ خروجی CSV", "↓ Export CSV");
        heading.Text = currentTable switch { "Customers" => Localization.T("مدیریت مشتریان", "Customer management"), "Transfers" => Localization.T("تاریخچه انتقال وجه", "Transfer history"), "Audit" => Localization.T("رویدادهای ثبت‌شده", "Audit events"), _ => Localization.T("حساب‌های بانکی", "Bank accounts") };
        dateLabel.Text = Localization.T("تاریخ امروز: ", "Today: ") + Localization.Date(DateTime.Today);
        if (grid.DataSource != null) try { RefreshSummary(); } catch { }
        summaryCards.Invalidate();
        grid.RightToLeft = Localization.Persian ? RightToLeft.Yes : RightToLeft.No;
        foreach (DataGridViewColumn column in grid.Columns)
            column.HeaderText = TranslateColumn(column.Name);
    }
    private static string TranslateColumn(string name) => name switch
    {
        "Id" => Localization.T("شناسه", "ID"),
        "FullName" => Localization.T("نام و نام خانوادگی", "Full name"),
        "NationalId" => Localization.T("کد ملی", "National ID"),
        "Phone" => Localization.T("تلفن", "Phone"),
        "CreatedAt" => Localization.T("تاریخ ثبت", "Created at"),
        "AccountNumber" => Localization.T("شماره حساب", "Account number"),
        "AccountType" => Localization.T("نوع حساب", "Account type"),
        "Balance" => Localization.T("موجودی", "Balance"),
        "Currency" => Localization.T("ارز", "Currency"),
        "Status" => Localization.T("وضعیت", "Status"),
        "SourceAccount" => Localization.T("حساب مبدأ", "Source account"),
        "TargetAccount" => Localization.T("حساب مقصد", "Target account"),
        "Amount" => Localization.T("مبلغ", "Amount"),
        "EventType" => Localization.T("نوع رویداد", "Event type"),
        "Details" => Localization.T("جزئیات", "Details"),
        _ => name
    };
    private void ApplyPermissions()
    {
        manageUsersButton.Visible = BankSession.CanManageUsers;
        branchesButton.Visible = BankSession.CanAudit || BankSession.CanWrite;
        loansButton.Visible = BankSession.CanAudit || BankSession.CanWrite;
        addButton.Visible = BankSession.CanWrite;
        openAccountButton.Visible = BankSession.CanWrite;
        transferButton.Visible = BankSession.CanWrite;
        auditButton.Visible = BankSession.CanAudit;
    }
    private void ToggleTheme() { darkTheme = !darkTheme; ApplyTheme(); ApplyLanguage(); }
    private void ApplyTheme()
    {
        var bg = darkTheme ? Color.FromArgb(18, 28, 45) : Color.FromArgb(240, 245, 250);
        var surface = darkTheme ? Color.FromArgb(29, 44, 64) : Color.White;
        var fg = darkTheme ? Color.FromArgb(229, 238, 247) : Color.FromArgb(18, 44, 75);
        BackColor = bg; contentPanel.BackColor = bg; actions.BackColor = bg; summaryCards.DarkMode = darkTheme;
        topPanel.BackColor = surface; heading.ForeColor = fg; dateLabel.ForeColor = fg;
        summaryLabel.BackColor = darkTheme ? Color.FromArgb(34, 67, 78) : Color.FromArgb(224, 240, 243);
        summaryLabel.ForeColor = fg; grid.BackgroundColor = surface;
        grid.DefaultCellStyle.BackColor = surface; grid.DefaultCellStyle.ForeColor = fg;
        grid.AlternatingRowsDefaultCellStyle.BackColor = darkTheme ? Color.FromArgb(35, 53, 73) : Color.FromArgb(247, 250, 253);
        searchBox.BackColor = surface; searchBox.ForeColor = fg;
    }
    private void LoadTable(string type)
    {
        currentTable = type;
        trends.Visible = false;
        try { grid.DataSource = type switch { "Customers" => service.Customers(), "Transfers" => service.Transfers(), "Audit" => service.AuditEvents(), _ => service.Accounts() }; RefreshSummary(); ApplyLanguage(); FilterRows(); }
        catch (Exception ex) { MessageBox.Show(Localization.T("خطا در دریافت اطلاعات: ", "Unable to load data: ") + ex.Message); }
    }
    private void RefreshSummary()
    {
        var m=service.Summary();
        summaryLabel.Text=Localization.T($"مشتریان: {m.customers:N0}    |    حساب‌های فعال: {m.accounts:N0}    |    انتقال‌ها: {m.transfers:N0}    |    موجودی ریالی: {m.balance:N0}",$"Customers: {m.customers:N0}    |    Active accounts: {m.accounts:N0}    |    Transfers: {m.transfers:N0}    |    IRR balance: {m.balance:N0}");
        summaryCards.UpdateMetrics(m.customers, m.accounts, m.transfers, m.balance);
    }
    private void FilterRows()
    {
        if(grid.DataSource is not System.Data.DataTable table)return;
        var term=searchBox.Text.Trim().Replace("'","''").Replace("[","[[]").Replace("%","[%]").Replace("*","[*]");
        if(term.Length==0){table.DefaultView.RowFilter="";return;}
        var fields=table.Columns.Cast<System.Data.DataColumn>().Select(c=>$"CONVERT([{c.ColumnName.Replace("]","]]" )}], 'System.String') LIKE '%{term}%'");
        table.DefaultView.RowFilter=string.Join(" OR ",fields);
    }
    private void ExportCsv(object? sender, EventArgs e)
    {
        if (grid.DataSource is not System.Data.DataTable table || table.Rows.Count == 0)
        {
            MessageBox.Show(Localization.T("داده‌ای برای خروجی وجود ندارد.", "No records to export."));
            return;
        }
        using var dialog = new SaveFileDialog
        {
            Filter = "CSV (*.csv)|*.csv",
            FileName = $"BankSphere-{currentTable}-{DateTime.Now:yyyyMMdd-HHmm}.csv"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            using var writer = new System.IO.StreamWriter(dialog.FileName, false, new System.Text.UTF8Encoding(true));
            static string Escape(object? value)
            {
                var text = Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture) ?? "";
                // Prevent spreadsheet formula injection when opening exported files.
                if (text.Length > 0 && "=+-@\t\r".Contains(text[0])) text = "'" + text;
                return "\"" + text.Replace("\"", "\"\"") + "\"";
            }
            writer.WriteLine(string.Join(",", table.Columns.Cast<System.Data.DataColumn>().Select(c => Escape(TranslateColumn(c.ColumnName)))));
            foreach (System.Data.DataRow row in table.DefaultView.ToTable().Rows)
                writer.WriteLine(string.Join(",", row.ItemArray.Select(Escape)));
            MessageBox.Show(Localization.T("فایل CSV ذخیره شد.", "CSV file saved."));
        }
        catch (Exception ex)
        {
            MessageBox.Show(Localization.T("خطا در ذخیره فایل: ", "Export failed: ") + ex.Message);
        }
    }
    private void OpenAccount(object? sender,EventArgs e)
    {
        using var form=new AccountForm();
        if(form.ShowDialog(this)==DialogResult.OK)LoadTable("Accounts");
    }
    private void AddCustomer(object? sender, EventArgs e)
    {
        using var form = new CustomerForm();
        if (form.ShowDialog(this) == DialogResult.OK) LoadTable("Customers");
    }
    private void Transfer(object? sender, EventArgs e)
    {
        using var form = new TransferForm();
        if (form.ShowDialog(this) == DialogResult.OK) LoadTable("Accounts");
    }
    private void ShowTrends()
    {
        try
        {
            LoadTable("Transfers");
            trends.LoadData(service.TransferTrends());
            trends.Visible = true;
            trends.BringToFront();
        }
        catch (Exception ex) { MessageBox.Show(Localization.T("خطا در نمایش نمودار: ", "Chart failed: ") + ex.Message); }
    }

}
