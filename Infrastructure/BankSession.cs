namespace AshkanBankSphere.Infrastructure;
/// <summary>Demo application session. Permissions are also checked in BankService.</summary>
public static class BankSession
{
    public static string Username { get; private set; } = "";
    public static string Role { get; private set; } = "";
    public static bool CanManageUsers => Role == "Admin";
    public static void RequireManageUsers() { if (!CanManageUsers) throw new UnauthorizedAccessException("Administrator access required."); }
    public static bool CanWrite => Role is "Admin" or "Manager" or "Teller";
    public static bool CanAudit => Role is "Admin" or "Manager" or "Auditor";
    public static void SignIn(string username, string role) { Username = username; Role = role; }
    public static void SignOut() { Username = ""; Role = ""; }
    public static void RequireWrite() { if (!CanWrite) throw new UnauthorizedAccessException("Your role cannot modify banking records."); }
    public static void RequireAudit() { if (!CanAudit) throw new UnauthorizedAccessException("Your role cannot view audit events."); }
}
