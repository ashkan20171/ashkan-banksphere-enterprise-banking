using System.Data;
using Microsoft.Data.SqlClient;
namespace AshkanBankSphere.Infrastructure;
/// <summary>Portfolio-only loan applications; does not disburse funds or integrate payment rails.</summary>
public sealed class LoanService
{
    public DataTable List()
    {
        if (!BankSession.CanAudit && !BankSession.CanWrite) throw new UnauthorizedAccessException("Access denied.");
        using var connection = BankDb.Open();
        using var command = new SqlCommand("SELECT l.Id,c.FullName,l.Principal,l.AnnualRatePercent,l.TermMonths,l.Status,l.CreatedAt FROM dbo.Loans l JOIN dbo.Customers c ON c.Id=l.CustomerId ORDER BY l.Id DESC",connection);
        using var adapter = new SqlDataAdapter(command);
        var table = new DataTable(); adapter.Fill(table); return table;
    }
    public void Apply(int customerId, decimal principal, decimal annualRate, int months)
    {
        BankSession.RequireWrite();
        if (customerId <= 0 || principal <= 0 || annualRate < 0 || annualRate > 100 || months < 1 || months > 360)
            throw new ArgumentException("Invalid loan application values.");
        using var connection = BankDb.Open();
        using var tx = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            using var cmd = new SqlCommand("INSERT INTO dbo.Loans(CustomerId,Principal,AnnualRatePercent,TermMonths) VALUES (@customer,@principal,@rate,@months)",connection,tx);
            cmd.Parameters.Add("@customer",SqlDbType.Int).Value=customerId;
            var amount = cmd.Parameters.Add("@principal",SqlDbType.Decimal); amount.Precision=19; amount.Scale=4; amount.Value=principal;
            var rate = cmd.Parameters.Add("@rate",SqlDbType.Decimal); rate.Precision=9; rate.Scale=4; rate.Value=annualRate;
            cmd.Parameters.Add("@months",SqlDbType.Int).Value=months;
            cmd.ExecuteNonQuery();
            using var audit = new SqlCommand("INSERT INTO dbo.AuditEvents(EventType,Details) VALUES (@type,@details)",connection,tx);
            audit.Parameters.AddWithValue("@type","LoanApplicationCreated");
            audit.Parameters.AddWithValue("@details",$"User={BankSession.Username}; CustomerId={customerId}; TermMonths={months}");
            audit.ExecuteNonQuery(); tx.Commit();
        }
        catch { tx.Rollback(); throw; }
    }
}
