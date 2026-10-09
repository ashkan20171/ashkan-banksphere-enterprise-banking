using System.Globalization;
using System.Threading;
namespace AshkanBankSphere;
public static class Localization
{
    public static bool Persian { get; private set; } = true;
    public static event EventHandler? Changed;
    public static void SetLanguage(bool persian)
    {
        Persian = persian;
        var culture = CultureInfo.GetCultureInfo(persian ? "fa-IR" : "en-US");
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
        Changed?.Invoke(null, EventArgs.Empty);
    }
    public static string T(string fa, string en) => Persian ? fa : en;
    public static string Date(DateTime value)
    {
        if (!Persian) return value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        var calendar = new PersianCalendar();
        return $"{calendar.GetYear(value):0000}/{calendar.GetMonth(value):00}/{calendar.GetDayOfMonth(value):00}";
    }
}
