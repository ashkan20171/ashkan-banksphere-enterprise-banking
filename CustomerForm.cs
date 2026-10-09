using AshkanBankSphere.Infrastructure;
namespace AshkanBankSphere;
public partial class CustomerForm : Form
{
    public CustomerForm() { InitializeComponent(); ApplyLanguage(); }
    private void ApplyLanguage()
    {
        RightToLeft = Localization.Persian ? RightToLeft.Yes : RightToLeft.No;
        Text = Localization.T("مشتری جدید", "New customer");
        fullName.PlaceholderText = Localization.T("نام و نام خانوادگی", "Full name");
        nationalId.PlaceholderText = Localization.T("کد ملی", "National ID");
        phone.PlaceholderText = Localization.T("شماره تماس", "Phone");
        saveButton.Text = Localization.T("ذخیره مشتری", "Save customer");
    }
    private void Save(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(fullName.Text) || string.IsNullOrWhiteSpace(nationalId.Text)) { MessageBox.Show(Localization.T("نام و کد ملی الزامی هستند.", "Name and ID are required.")); return; }
        try { new BankService().AddCustomer(fullName.Text.Trim(), nationalId.Text.Trim(), phone.Text.Trim()); DialogResult = DialogResult.OK; }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
}
