using System.Data;
using Microsoft.Data.SqlClient;
namespace AshkanBankSphere.Infrastructure;
public sealed class BranchService
{
    public DataTable ListBranches()
    {
        using var connection = BankDb.Open();
        using var command = new SqlCommand("SELECT Id, BranchCode, NameFa, NameEn, City, Address, IsActive FROM dbo.Branches ORDER BY BranchCode", connection);
        using var adapter = new SqlDataAdapter(command);
        var result = new DataTable(); adapter.Fill(result); return result;
    }
    public void AddBranch(string code, string persianName, string englishName, string city, string address)
    {
        BankSession.RequireManageUsers();
        if (string.IsNullOrWhiteSpace(code) || code.Length > 20 || string.IsNullOrWhiteSpace(persianName) || persianName.Length > 150)
            throw new ArgumentException("Branch code and Persian name are required and must fit their maximum lengths.");
        if (englishName.Length > 150 || city.Length > 100 || address.Length > 300) throw new ArgumentException("Field length exceeded.");
        using var connection = BankDb.Open();
        using var command = new SqlCommand("INSERT INTO dbo.Branches(BranchCode,NameFa,NameEn,City,Address) VALUES(@code,@fa,@en,@city,@address)", connection);
        command.Parameters.Add("@code", SqlDbType.NVarChar, 20).Value = code.Trim();
        command.Parameters.Add("@fa", SqlDbType.NVarChar, 150).Value = persianName.Trim();
        command.Parameters.Add("@en", SqlDbType.NVarChar, 150).Value = englishName.Trim();
        command.Parameters.Add("@city", SqlDbType.NVarChar, 100).Value = city.Trim();
        command.Parameters.Add("@address", SqlDbType.NVarChar, 300).Value = address.Trim();
        command.ExecuteNonQuery();
    }
}
