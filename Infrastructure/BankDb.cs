using Microsoft.Data.SqlClient;
namespace AshkanBankSphere.Infrastructure;
public static class BankDb {
 public static string ConnectionString => Environment.GetEnvironmentVariable("ASHKAN_BANK_DB") ?? "Server=localhost;Database=AshkanBankSphere;Trusted_Connection=True;TrustServerCertificate=True;";
 public static SqlConnection Open(){var c=new SqlConnection(ConnectionString);c.Open();return c;}
}
