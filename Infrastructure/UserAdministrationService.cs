using System.Data;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;

namespace AshkanBankSphere.Infrastructure;

/// <summary>Administrative operations, authorized in the service layer.</summary>
public sealed class UserAdministrationService
{
    private static readonly string[] Roles = { "Admin", "Manager", "Teller", "Auditor" };
    public DataTable GetUsers()
    {
        BankSession.RequireManageUsers();
        using var connection = BankDb.Open();
        using var command = new SqlCommand("SELECT Id, Username, Role, IsActive FROM dbo.Users ORDER BY Username", connection);
        using var adapter = new SqlDataAdapter(command);
        var result = new DataTable(); adapter.Fill(result); return result;
    }
    public void CreateUser(string username, string password, string role)
    {
        BankSession.RequireManageUsers();
        username = username.Trim();
        if (username.Length is < 3 or > 100) throw new ArgumentException("Username must have 3–100 characters.");
        if (password.Length < 12 || password.Length > 256) throw new ArgumentException("Password must have 12–256 characters.");
        if (!Roles.Contains(role)) throw new ArgumentException("Invalid role.");
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 210000, HashAlgorithmName.SHA256, 32);
        using var connection = BankDb.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            using var command = new SqlCommand("INSERT INTO dbo.Users(Username,PasswordHash,PasswordSalt,Role,IsActive) VALUES(@username,@hash,@salt,@role,1)", connection, transaction);
            command.Parameters.Add("@username", SqlDbType.NVarChar, 100).Value = username;
            command.Parameters.Add("@hash", SqlDbType.VarBinary, 32).Value = hash;
            command.Parameters.Add("@salt", SqlDbType.VarBinary, 16).Value = salt;
            command.Parameters.Add("@role", SqlDbType.NVarChar, 30).Value = role;
            command.ExecuteNonQuery();
            WriteAudit(connection, transaction, "UserCreated", username);
            transaction.Commit();
        }
        catch { transaction.Rollback(); throw; }
        finally { CryptographicOperations.ZeroMemory(hash); }
    }
    public void UpdateUser(int id, string username, string role, bool active)
    {
        BankSession.RequireManageUsers();
        if (id <= 0 || !Roles.Contains(role)) throw new ArgumentException("Invalid user or role.");
        if (string.Equals(username, BankSession.Username, StringComparison.OrdinalIgnoreCase) && (!active || role != "Admin"))
            throw new InvalidOperationException("You cannot deactivate or demote your own administrator account.");
        using var connection = BankDb.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            // Keep at least one active administrator.
            using var command = new SqlCommand(@"UPDATE dbo.Users SET Role=@role,IsActive=@active WHERE Id=@id;
IF @@ROWCOUNT = 0 THROW 50001, 'User not found', 1;
IF NOT EXISTS (SELECT 1 FROM dbo.Users WITH (UPDLOCK,HOLDLOCK) WHERE Role='Admin' AND IsActive=1)
 THROW 50002, 'At least one active administrator is required', 1;", connection, transaction);
            command.Parameters.Add("@role", SqlDbType.NVarChar, 30).Value = role;
            command.Parameters.Add("@active", SqlDbType.Bit).Value = active;
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            command.ExecuteNonQuery();
            WriteAudit(connection, transaction, "UserUpdated", $"id={id}; active={active}; role={role}");
            transaction.Commit();
        }
        catch { transaction.Rollback(); throw; }
    }
    private static void WriteAudit(SqlConnection connection, SqlTransaction transaction, string eventType, string details)
    {
        using var audit = new SqlCommand("INSERT INTO dbo.AuditEvents(EventType,Details) VALUES(@event,@details)", connection, transaction);
        audit.Parameters.Add("@event", SqlDbType.NVarChar, 60).Value = eventType;
        audit.Parameters.Add("@details", SqlDbType.NVarChar, 1000).Value = $"actor={BankSession.Username}; {details}";
        audit.ExecuteNonQuery();
    }
}
