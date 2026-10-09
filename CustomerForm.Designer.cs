using System.Drawing;
using System.Windows.Forms;
namespace AshkanBankSphere;
partial class CustomerForm
{
    private TextBox fullName, nationalId, phone;
    private Button saveButton;
    private void InitializeComponent()
    {
        Text = "مشتری جدید"; Size = new Size(490, 390); StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.FromArgb(239, 246, 250); Font = new Font("Segoe UI", 11);
        fullName = new TextBox { Location = new Point(54, 55), Width = 370 };
        nationalId = new TextBox { Location = new Point(54, 113), Width = 370 };
        phone = new TextBox { Location = new Point(54, 171), Width = 370 };
        saveButton = new Button { Location = new Point(54, 239), Size = new Size(370, 48), BackColor = Color.FromArgb(18, 145, 136), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        saveButton.Click += Save;
        Controls.AddRange(new Control[] { fullName, nationalId, phone, saveButton });
    }
}
