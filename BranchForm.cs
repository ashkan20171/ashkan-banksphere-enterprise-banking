using AshkanBankSphere.Infrastructure;
namespace AshkanBankSphere;
public partial class BranchForm : Form
{
    private readonly BranchService service = new();
    public BranchForm()
    {
        InitializeComponent();
        Localization.Changed += OnLanguageChanged;
        FormClosed += (_, _) => Localization.Changed -= OnLanguageChanged;
        ApplyLanguage(); Reload();
    }
    private void OnLanguageChanged(object? sender, EventArgs e) => ApplyLanguage();
    private void ApplyLanguage()
    {
        RightToLeft = Localization.Persian ? RightToLeft.Yes : RightToLeft.No;
        RightToLeftLayout = Localization.Persian;
        Text = Localization.T("مدیریت شعب", "Branch directory");
        titleLabel.Text = Text;
        addButton.Text = Localization.T("ثبت شعبه", "Add branch");
        refreshButton.Text = Localization.T("تازه‌سازی", "Refresh");
        var names = new[] { Localization.T("کد شعبه", "Code"), Localization.T("نام فارسی", "Persian name"), Localization.T("نام انگلیسی", "English name"), Localization.T("شهر", "City"), Localization.T("نشانی", "Address") };
        for (int i = 0; i < fields.Length; i++) fields[i].PlaceholderText = names[i];
        grid.RightToLeft = RightToLeft;
        string[] headers = { Localization.T("شناسه", "ID"), names[0], names[1], names[2], names[3], names[4], Localization.T("فعال", "Active") };
        for (int i = 0; i < grid.Columns.Count && i < headers.Length; i++) grid.Columns[i].HeaderText = headers[i];
    }
    private void Reload()
    {
        try { grid.DataSource = service.ListBranches(); ApplyLanguage(); }
        catch (Exception ex) { MessageBox.Show(this, ex.Message, Localization.T("خطای دیتابیس", "Database error")); }
    }
    private void AddBranch(object? sender, EventArgs e)
    {
        try
        {
            service.AddBranch(fields[0].Text, fields[1].Text, fields[2].Text, fields[3].Text, fields[4].Text);
            foreach (var field in fields) field.Clear();
            Reload();
        }
        catch (Exception ex) { MessageBox.Show(this, ex.Message, Localization.T("خطا", "Error")); }
    }
}
