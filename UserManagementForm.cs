using AshkanBankSphere.Infrastructure;

namespace AshkanBankSphere;

public partial class UserManagementForm : Form
{
    private readonly UserAdministrationService service = new();
    public UserManagementForm()
    {
        BankSession.RequireManageUsers();
        InitializeComponent();
        Localization.Changed += LanguageChanged;
        FormClosed += (_, _) => Localization.Changed -= LanguageChanged;
        ApplyLanguage(); RefreshUsers();
    }
    private void LanguageChanged(object? sender, EventArgs e) => ApplyLanguage();
    private void ApplyLanguage()
    {
        RightToLeft = Localization.Persian ? RightToLeft.Yes : RightToLeft.No;
        RightToLeftLayout = Localization.Persian;
        Text = Localization.T("مدیریت کاربران | اشکان بانک‌اسفیر", "User management | Ashkan BankSphere");
        createButton.Text = Localization.T("ایجاد کاربر", "Create user");
        saveButton.Text = Localization.T("ذخیره تغییرات", "Save changes");
        refreshButton.Text = Localization.T("تازه‌سازی", "Refresh");
        usernameLabel.Text = Localization.T("نام کاربری", "Username");
        passwordLabel.Text = Localization.T("رمز عبور جدید", "New password");
        roleLabel.Text = Localization.T("نقش", "Role");
        activeCheck.Text = Localization.T("کاربر فعال", "Active user");
    }
    private void RefreshUsers()
    {
        try { usersGrid.DataSource = service.GetUsers(); usersGrid.ClearSelection(); }
        catch (Exception ex) { MessageBox.Show(this, ex.Message); }
    }
    private void CreateUser(object? sender, EventArgs e)
    {
        try { service.CreateUser(usernameBox.Text, passwordBox.Text, roleBox.Text); passwordBox.Clear(); RefreshUsers(); }
        catch (Exception ex) { MessageBox.Show(this, ex.Message); }
    }
    private void SaveUser(object? sender, EventArgs e)
    {
        if (usersGrid.CurrentRow?.DataBoundItem is not System.Data.DataRowView row) return;
        try
        {
            service.UpdateUser(Convert.ToInt32(row["Id"]), Convert.ToString(row["Username"]) ?? "", roleBox.Text, activeCheck.Checked);
            RefreshUsers();
        }
        catch (Exception ex) { MessageBox.Show(this, ex.Message); }
    }
    private void SelectUser(object? sender, EventArgs e)
    {
        if (usersGrid.CurrentRow?.DataBoundItem is not System.Data.DataRowView row) return;
        usernameBox.Text = Convert.ToString(row["Username"]);
        roleBox.SelectedItem = Convert.ToString(row["Role"]);
        activeCheck.Checked = Convert.ToBoolean(row["IsActive"]);
    }
}
