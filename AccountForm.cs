using AshkanBankSphere.Infrastructure;
namespace AshkanBankSphere;
public partial class AccountForm:Form
{
 public AccountForm(){InitializeComponent();ApplyLanguage();}
 private void ApplyLanguage(){RightToLeft=Localization.Persian?RightToLeft.Yes:RightToLeft.No;Text=Localization.T("افتتاح حساب","Open account");customerId.PlaceholderText=Localization.T("شناسه مشتری","Customer ID");accountNumber.PlaceholderText=Localization.T("شماره حساب","Account number");saveButton.Text=Localization.T("افتتاح حساب","Create account");typeBox.Items.Clear();typeBox.Items.AddRange(new object[]{"Savings","Current","Deposit"});typeBox.SelectedIndex=0;}
 private void Save(object? sender,EventArgs e){if(!int.TryParse(customerId.Text,out int id)){MessageBox.Show(Localization.T("شناسه مشتری معتبر نیست.","Invalid customer ID."));return;}try{new BankService().CreateAccount(id,accountNumber.Text.Trim(),typeBox.SelectedItem?.ToString()??"Savings");DialogResult=DialogResult.OK;}catch(Exception ex){MessageBox.Show(ex.Message);}}
}
