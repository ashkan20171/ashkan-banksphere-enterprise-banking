using AshkanBankSphere.Infrastructure;
namespace AshkanBankSphere;
public partial class TransferForm : Form
{
    public TransferForm() { InitializeComponent(); ApplyLanguage(); }
    private void ApplyLanguage()
    {
        RightToLeft = Localization.Persian ? RightToLeft.Yes : RightToLeft.No;
        Text = Localization.T("انتقال وجه", "Secure transfer");
        source.PlaceholderText = Localization.T("حساب مبدأ", "Source account");
        target.PlaceholderText = Localization.T("حساب مقصد", "Destination account");
        amount.PlaceholderText = Localization.T("مبلغ", "Amount");
        submitButton.Text = Localization.T("تأیید انتقال", "Confirm transfer");
    }
    private void Send(object? sender, EventArgs e)
    {
        if (!decimal.TryParse(amount.Text, out var value) || value <= 0) { MessageBox.Show(Localization.T("مبلغ معتبر وارد کنید.", "Enter a valid positive amount.")); return; }
        if (MessageBox.Show(Localization.T("انتقال وجه تأیید شود؟", "Confirm funds transfer?"), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        try { new BankService().Transfer(source.Text.Trim(), target.Text.Trim(), value); MessageBox.Show(Localization.T("انتقال وجه انجام شد.", "Transfer completed.")); DialogResult = DialogResult.OK; }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
}
