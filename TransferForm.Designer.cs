using System.Drawing;
using System.Windows.Forms;
namespace AshkanBankSphere;
partial class TransferForm
{
    private TextBox source, target, amount;
    private Button submitButton;
    private void InitializeComponent()
    {
        Text = "انتقال وجه"; Size = new Size(490, 390); StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.FromArgb(239, 246, 250); Font = new Font("Segoe UI", 11);
        source = new TextBox { Location = new Point(54, 55), Width = 370 };
        target = new TextBox { Location = new Point(54, 113), Width = 370 };
        amount = new TextBox { Location = new Point(54, 171), Width = 370 };
        submitButton = new Button { Location = new Point(54, 239), Size = new Size(370, 48), BackColor = Color.FromArgb(18, 145, 136), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        submitButton.Click += Send;
        Controls.AddRange(new Control[] { source, target, amount, submitButton });
    }
}
