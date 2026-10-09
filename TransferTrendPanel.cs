using System.Data;
using System.Drawing.Drawing2D;
namespace AshkanBankSphere;
public sealed class TransferTrendPanel : Control
{
    private readonly List<decimal> amounts = new();
    public string Title { get; set; } = "Daily transfers";
    public TransferTrendPanel() { DoubleBuffered = true; BackColor = Color.White; ForeColor = Color.FromArgb(20,45,75); Font = new Font("Segoe UI", 10); }
    public void LoadData(DataTable data)
    {
        amounts.Clear();
        foreach (DataRow row in data.Rows) amounts.Add(Convert.ToDecimal(row["TotalAmount"]));
        Invalidate();
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g=e.Graphics; g.SmoothingMode=SmoothingMode.AntiAlias;
        using var titleBrush=new SolidBrush(ForeColor);
        g.DrawString(Title,Font,titleBrush,16,12);
        if(amounts.Count==0) { g.DrawString(Localization.T("داده‌ای برای نمایش وجود ندارد.","No transfer data yet."),Font,Brushes.Gray,16,65);return; }
        decimal max=Math.Max(1m,amounts.Max());
        float width=(Width-40f)/amounts.Count;
        using var bar=new SolidBrush(Color.FromArgb(15,151,151));
        for(int i=0;i<amounts.Count;i++)
        {
            float h=(float)(amounts[i]/max)*105f;
            g.FillRectangle(bar,20+i*width,Height-18-h,Math.Max(3,width-6),h);
        }
    }
}
