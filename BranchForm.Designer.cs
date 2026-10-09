namespace AshkanBankSphere;
partial class BranchForm
{
    private System.ComponentModel.IContainer? components;
    private Label titleLabel = null!;
    private DataGridView grid = null!;
    private TextBox[] fields = null!;
    private Button addButton = null!, refreshButton = null!;
    protected override void Dispose(bool disposing) { if (disposing) components?.Dispose(); base.Dispose(disposing); }
    private void InitializeComponent()
    {
        SuspendLayout();
        Text = "Branches"; Size = new Size(1050, 700); MinimumSize = new Size(850, 580);
        StartPosition = FormStartPosition.CenterParent; BackColor = Color.FromArgb(240, 245, 250);
        Font = new Font("Segoe UI", 10);
        titleLabel = new Label { Dock = DockStyle.Top, Height = 80, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 21, FontStyle.Bold), ForeColor = Color.FromArgb(18, 44, 75) };
        var inputs = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 116, Padding = new Padding(18), AutoScroll = true, WrapContents = true };
        fields = Enumerable.Range(0, 5).Select(_ => new TextBox { Width = 175, Height = 38, Margin = new Padding(5) }).ToArray();
        inputs.Controls.AddRange(fields);
        var toolbar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 65, Padding = new Padding(18, 6, 18, 6) };
        addButton = MakeButton(); refreshButton = MakeButton();
        addButton.Click += AddBranch; refreshButton.Click += (_, _) => Reload();
        toolbar.Controls.AddRange(new Control[] { addButton, refreshButton });
        grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false, RowHeadersVisible = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White, BorderStyle = BorderStyle.None, ColumnHeadersHeight = 45, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
        Controls.Add(grid); Controls.Add(toolbar); Controls.Add(inputs); Controls.Add(titleLabel);
        ResumeLayout(false);
    }
    private static Button MakeButton() => new() { Width = 150, Height = 43, BackColor = Color.FromArgb(15, 133, 133), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
}
