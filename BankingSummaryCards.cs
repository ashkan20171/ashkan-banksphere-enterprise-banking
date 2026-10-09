using System.Drawing.Drawing2D;
namespace AshkanBankSphere;
/// <summary>Responsive, owner-drawn KPI cards; no third-party UI dependencies.</summary>
public sealed class BankingSummaryCards : Control
{
    private int customers, accounts, transfers;
    private decimal balance;
    private bool darkMode;
    public bool DarkMode { get => darkMode; set { darkMode = value; Invalidate(); } }
    public BankingSummaryCards() { DoubleBuffered = true; ResizeRedraw = true; Font = new Font("Segoe UI", 11); }
    public void UpdateMetrics(int customerCount, int accountCount, int transferCount, decimal totalBalance)
    { customers = customerCount; accounts = accountCount; transfers = transferCount; balance = totalBalance; Invalidate(); }
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.Clear(darkMode ? Color.FromArgb(18, 28, 45) : Color.FromArgb(240, 245, 250));
        var labels = Localization.Persian ? new[] { "مشتریان", "حساب‌های فعال", "انتقال وجه", "موجودی کل (ریال)" } : new[] { "Customers", "Active accounts", "Transfers", "Total balance (IRR)" };
        var values = new[] { customers.ToString("N0"), accounts.ToString("N0"), transfers.ToString("N0"), balance.ToString("N0") };
        var accents = new[] { Color.FromArgb(27, 178, 168), Color.FromArgb(71, 140, 223), Color.FromArgb(219, 164, 70), Color.FromArgb(123, 105, 214) };
        int gap = 12, margin = 18, width = Math.Max(1, (ClientSize.Width - margin * 2 - gap * 3) / 4);
        for (int i = 0; i < 4; i++)
        {
            int x = margin + i * (width + gap);
            var rect = new Rectangle(x, 12, width, Math.Max(1, Height - 25));
            using var path = new GraphicsPath();
            int r = 17;
            path.AddArc(rect.X, rect.Y, r * 2, r * 2, 180, 90);
            path.AddArc(rect.Right - r * 2, rect.Y, r * 2, r * 2, 270, 90);
            path.AddArc(rect.Right - r * 2, rect.Bottom - r * 2, r * 2, r * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - r * 2, r * 2, r * 2, 90, 90); path.CloseFigure();
            using var background = new SolidBrush(darkMode ? Color.FromArgb(29, 44, 64) : Color.White);
            e.Graphics.FillPath(background, path);
            using var accent = new SolidBrush(accents[i]);
            e.Graphics.FillRectangle(accent, x + 16, 28, 5, 28);
            using var labelFont = new Font("Segoe UI", 10);
            using var numberFont = new Font("Segoe UI", width < 155 ? 12 : 18, FontStyle.Bold);
            using var fg = new SolidBrush(darkMode ? Color.White : Color.FromArgb(20, 42, 67));
            using var muted = new SolidBrush(darkMode ? Color.FromArgb(175, 193, 211) : Color.FromArgb(99, 115, 132));
            using var sf = new StringFormat { Alignment = Localization.Persian ? StringAlignment.Far : StringAlignment.Near, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };
            e.Graphics.DrawString(labels[i], labelFont, muted, new RectangleF(x + 25, 28, width - 43, 25), sf);
            e.Graphics.DrawString(values[i], numberFont, fg, new RectangleF(x + 15, 67, width - 30, 44), sf);
        }
    }
}
