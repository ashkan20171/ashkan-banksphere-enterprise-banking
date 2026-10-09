namespace AshkanBankSphere;

partial class UserManagementForm
{
    private System.ComponentModel.IContainer? components;
    private DataGridView usersGrid = null!;
    private TextBox usernameBox = null!;
    private TextBox passwordBox = null!;
    private ComboBox roleBox = null!;
    private CheckBox activeCheck = null!;
    private Button createButton = null!;
    private Button saveButton = null!;
    private Button refreshButton = null!;
    private Label usernameLabel = null!;
    private Label passwordLabel = null!;
    private Label roleLabel = null!;
    protected override void Dispose(bool disposing) { if (disposing) components?.Dispose(); base.Dispose(disposing); }
    private void InitializeComponent()
    {
        SuspendLayout();
        ClientSize = new Size(1040, 670);
        MinimumSize = new Size(900, 560);
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.FromArgb(239, 245, 251);
        Font = new Font("Segoe UI", 10F);
        usersGrid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White, BorderStyle = BorderStyle.None, RowHeadersVisible = false };
        usersGrid.SelectionChanged += SelectUser;
        var editor = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 155, Padding = new Padding(18), WrapContents = true, FlowDirection = FlowDirection.LeftToRight, BackColor = Color.FromArgb(19, 43, 72) };
        usernameLabel = MakeLabel(); passwordLabel = MakeLabel(); roleLabel = MakeLabel();
        usernameBox = new TextBox { Width = 165, MaxLength = 100 };
        passwordBox = new TextBox { Width = 170, UseSystemPasswordChar = true, MaxLength = 256 };
        roleBox = new ComboBox { Width = 130, DropDownStyle = ComboBoxStyle.DropDownList };
        roleBox.Items.AddRange(new object[] { "Admin", "Manager", "Teller", "Auditor" }); roleBox.SelectedIndex = 2;
        activeCheck = new CheckBox { Checked = true, AutoSize = true, ForeColor = Color.White };
        createButton = MakeButton(); saveButton = MakeButton(); refreshButton = MakeButton();
        createButton.Click += CreateUser; saveButton.Click += SaveUser; refreshButton.Click += (_, _) => RefreshUsers();
        editor.Controls.AddRange(new Control[] { usernameLabel, usernameBox, passwordLabel, passwordBox, roleLabel, roleBox, activeCheck, createButton, saveButton, refreshButton });
        Controls.Add(usersGrid); Controls.Add(editor);
        ResumeLayout(false);
    }
    private static Label MakeLabel() => new() { AutoSize = true, ForeColor = Color.White, Margin = new Padding(12, 8, 4, 0) };
    private static Button MakeButton() => new() { AutoSize = true, Height = 34, BackColor = Color.FromArgb(24, 166, 157), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(8) };
}
