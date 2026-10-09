using AshkanBankSphere.Infrastructure;
namespace AshkanBankSphere;
public partial class LoginForm : Form
{
    public LoginForm()
    {
        InitializeComponent();
        Localization.Changed += OnLanguageChanged;
        FormClosed += (_, _) => Localization.Changed -= OnLanguageChanged;
        ApplyLanguage();
    }
    private void OnLanguageChanged(object? sender, EventArgs e) => ApplyLanguage();
    private void ApplyLanguage()
    {
        RightToLeft = Localization.Persian ? RightToLeft.Yes : RightToLeft.No;
        subtitle.Text = Localization.T("سامانه مدیریت بانک | ورود امن", "Enterprise banking | Secure access");
        username.PlaceholderText = Localization.T("نام کاربری", "Username");
        password.PlaceholderText = Localization.T("رمز عبور", "Password");
        signInButton.Text = Localization.T("ورود به سامانه ←", "SIGN IN →");
        languageButton.Text = Localization.Persian ? "English" : "فارسی";
    }
    private void SignIn(object? sender, EventArgs e)
    {
        try
        {
            if (new BankService().Login(username.Text.Trim(), password.Text))
            {
                Hide();
                using var dashboard = new DashboardForm();
                dashboard.ShowDialog();
                BankSession.SignOut(); Show(); password.Clear();
            }
            else MessageBox.Show(Localization.T("نام کاربری یا رمز عبور نادرست است.", "Invalid username or password."));
        }
        catch (Exception ex) { MessageBox.Show(Localization.T("خطا در اتصال به پایگاه داده: ", "Database connection failed: ") + ex.Message); }
    }
}
