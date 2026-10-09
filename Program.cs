using System.Windows.Forms;
namespace AshkanBankSphere;
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Localization.SetLanguage(true);
        Application.Run(new LoginForm());
    }
}
