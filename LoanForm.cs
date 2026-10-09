using AshkanBankSphere.Infrastructure;
namespace AshkanBankSphere;
public partial class LoanForm : Form
{
    private readonly LoanService service = new();
    public LoanForm()
    {
        InitializeComponent();
        Localization.Changed += LanguageChanged;
        FormClosed += (_, _) => Localization.Changed -= LanguageChanged;
        ApplyLanguage(); Reload();
    }
    private void LanguageChanged(object? sender, EventArgs e) => ApplyLanguage();
    private void ApplyLanguage()
    {
        RightToLeft = Localization.Persian ? RightToLeft.Yes : RightToLeft.No;
        RightToLeftLayout = Localization.Persian;
        Text = Localization.T("تسهیلات و درخواست وام", "Loan applications");
        titleLabel.Text = Text;
        customerIdBox.PlaceholderText = Localization.T("شناسه مشتری", "Customer ID");
        principalBox.PlaceholderText = Localization.T("اصل مبلغ (ریال)", "Principal (IRR)");
        rateBox.PlaceholderText = Localization.T("نرخ سالانه درصد", "Annual rate %");
        monthsBox.PlaceholderText = Localization.T("مدت (ماه)", "Term (months)");
        addButton.Text = Localization.T("ثبت درخواست", "Submit application");
        refreshButton.Text = Localization.T("تازه‌سازی", "Refresh");
        addButton.Enabled = BankSession.CanWrite;
        grid.RightToLeft = RightToLeft;
    }
    private void Reload()
    {
        try { grid.DataSource = service.List(); }
        catch (Exception ex) { MessageBox.Show(this,ex.Message,Localization.T("خطا", "Error")); }
    }
    private void AddLoan(object? sender, EventArgs e)
    {
        try
        {
            if (!int.TryParse(customerIdBox.Text,out var customerId) ||
                !decimal.TryParse(principalBox.Text,System.Globalization.NumberStyles.Number,System.Globalization.CultureInfo.InvariantCulture,out var principal) ||
                !decimal.TryParse(rateBox.Text,System.Globalization.NumberStyles.Number,System.Globalization.CultureInfo.InvariantCulture,out var rate) ||
                !int.TryParse(monthsBox.Text,out var months))
                throw new ArgumentException(Localization.T("لطفاً مقادیر عددی معتبر وارد کنید.","Enter valid numeric values (use dot for decimals)."));
            service.Apply(customerId,principal,rate,months);
            Reload();
            MessageBox.Show(this,Localization.T("درخواست وام ثبت شد.","Loan application submitted."));
        }
        catch (Exception ex) { MessageBox.Show(this,ex.Message,Localization.T("خطا", "Error")); }
    }
}
