using System.Drawing;
namespace AshkanBankSphere;
partial class AccountForm
{
 private TextBox customerId=null!,accountNumber=null!;private ComboBox typeBox=null!;private Button saveButton=null!;
 private void InitializeComponent(){Text="Open account";Size=new Size(490,370);StartPosition=FormStartPosition.CenterParent;FormBorderStyle=FormBorderStyle.FixedDialog;MaximizeBox=false;BackColor=Color.FromArgb(242,247,250);Font=new Font("Segoe UI",11);customerId=new TextBox{Location=new Point(40,44),Width=390};accountNumber=new TextBox{Location=new Point(40,105),Width=390};typeBox=new ComboBox{Location=new Point(40,165),Width=390,DropDownStyle=ComboBoxStyle.DropDownList};saveButton=new Button{Location=new Point(40,230),Size=new Size(390,50),BackColor=Color.FromArgb(10,132,133),ForeColor=Color.White,FlatStyle=FlatStyle.Flat};saveButton.FlatAppearance.BorderSize=0;saveButton.Click+=Save;Controls.AddRange(new Control[]{customerId,accountNumber,typeBox,saveButton});}
}
