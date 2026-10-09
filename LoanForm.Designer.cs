namespace AshkanBankSphere;
partial class LoanForm
{
    private System.ComponentModel.IContainer? components;
    private Label titleLabel = null!;
    private TextBox customerIdBox = null!, principalBox = null!, rateBox = null!, monthsBox = null!;
    private Button addButton = null!, refreshButton = null!;
    private DataGridView grid = null!;
    protected override void Dispose(bool disposing) { if (disposing) components?.Dispose(); base.Dispose(disposing); }
    private void InitializeComponent()
    {
        SuspendLayout();
        Font = new Font("Segoe UI",10F);
        BackColor = Color.FromArgb(239,246,250);
        ClientSize = new Size(1000,650);
        MinimumSize = new Size(800,520);
        StartPosition = FormStartPosition.CenterParent;
        titleLabel = new Label { Dock=DockStyle.Top, Height=75, Font=new Font("Segoe UI",20F,FontStyle.Bold), ForeColor=Color.FromArgb(21,49,78), TextAlign=ContentAlignment.MiddleCenter };
        var inputBar = new FlowLayoutPanel { Dock=DockStyle.Top, Height=110, Padding=new Padding(20), AutoScroll=true, WrapContents=true };
        customerIdBox = MakeInput(); principalBox = MakeInput(); rateBox = MakeInput(); monthsBox = MakeInput();
        addButton = MakeButton(Color.FromArgb(12,128,136)); refreshButton = MakeButton(Color.FromArgb(35,65,100));
        addButton.Click += AddLoan; refreshButton.Click += (_,_) => Reload();
        inputBar.Controls.AddRange(new Control[] { customerIdBox,principalBox,rateBox,monthsBox,addButton,refreshButton });
        grid = new DataGridView { Dock=DockStyle.Fill, BackgroundColor=Color.White, BorderStyle=BorderStyle.None, ReadOnly=true, AllowUserToAddRows=false, AllowUserToDeleteRows=false, AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill, SelectionMode=DataGridViewSelectionMode.FullRowSelect, RowHeadersVisible=false };
        Controls.Add(grid); Controls.Add(inputBar); Controls.Add(titleLabel);
        ResumeLayout(false);
    }
    private static TextBox MakeInput() => new() { Width=145, Height=36, Margin=new Padding(5,6,5,6) };
    private static Button MakeButton(Color background) => new() { AutoSize=true, Height=36, FlatStyle=FlatStyle.Flat, BackColor=background, ForeColor=Color.White, Margin=new Padding(5,6,5,6) };
}
