using System.Drawing;
using System.Windows.Forms;
namespace AshkanBankSphere;
partial class LoginForm
{
    private TextBox username = null!, password = null!;
    private Label subtitle = null!;
    private Button signInButton = null!, languageButton = null!;
    private void InitializeComponent()
    {
        Text = "Ashkan BankSphere | ورود امن";
        Size = new Size(660, 500); MinimumSize = new Size(660, 500);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(13, 27, 48);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        ForeColor = Color.White;
        Font = new Font("Segoe UI", 11);
        var title = new Label { Text = "◈  ASHKAN BANKSPHERE", Font = new Font("Segoe UI", 21, FontStyle.Bold), ForeColor = Color.FromArgb(85, 218, 205), AutoSize = true, Location = new Point(110, 68) };
        subtitle = new Label { AutoSize = true, Location = new Point(124, 135) };
        username = new TextBox { Location = new Point(124, 190), Width = 410, Height = 34 };
        password = new TextBox { UseSystemPasswordChar = true, Location = new Point(124, 251), Width = 410, Height = 34 };
        signInButton = new Button { Location = new Point(124, 324), Size = new Size(410, 48), BackColor = Color.FromArgb(18, 152, 141), FlatStyle = FlatStyle.Flat, ForeColor = Color.White };
        signInButton.FlatAppearance.BorderSize = 0;
        signInButton.Click += SignIn;
        languageButton = new Button { Location = new Point(410, 393), Size = new Size(124, 35), FlatStyle = FlatStyle.Flat, ForeColor = Color.FromArgb(88, 223, 207) };
        languageButton.Click += (_, _) => Localization.SetLanguage(!Localization.Persian);
        Controls.AddRange(new Control[] { title, subtitle, username, password, signInButton, languageButton });
        username.BorderStyle = BorderStyle.FixedSingle;
        password.BorderStyle = BorderStyle.FixedSingle;
        signInButton.Cursor = Cursors.Hand;
        languageButton.Cursor = Cursors.Hand;
        AcceptButton = signInButton;
    }
}
