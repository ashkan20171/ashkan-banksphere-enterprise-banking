# Ashkan BankSphere 🏦

### Bilingual Banking Operations Desktop Platform · C# / .NET 8 / SQL Server

**A portfolio-focused banking operations prototype** demonstrating transactional database operations, role-aware workflows, WinForms UI engineering, and Persian/English localization. Built for **Visual Studio 2022**.

> **Important:** This is an educational demonstration, **not a production banking system**. No payment rails, regulatory certification, full double-entry ledger, loan disbursement, KYC/AML controls, or penetration-testing assurance. Use synthetic data only.

## Highlights

| Capability | Details |
|---|---|
| Desktop experience | Windows Forms, dashboard cards, dark/light dashboard theme, search and CSV export |
| Localization | Persian default (RTL), English (LTR), calendar presentation based on language |
| Customer and account workflows | Customer registration, account opening, account browsing |
| Fund transfers | SQL Server transaction with balance checks and audit records |
| Access control | Admin / Manager / Teller / Auditor role checks in service layer |
| User administration | User activation and role administration, salted PBKDF2-SHA256 password hashes |
| Branch directory | Branch registration and browsing |
| **Loan applications — Stage 11** | Create pending loan requests, list applications, validate terms and amounts, audit creation |
| Reporting | Transfer trends, event history, CSV export with formula-injection mitigation |

## Tech stack

- C# 12, **.NET 8 (`net8.0-windows`)**, Windows Forms
- Visual Studio 2022 (with .NET Desktop Development workload)
- Microsoft SQL Server and `Microsoft.Data.SqlClient`
- Parameterized SQL and ADO.NET services

## Quick start

1. Install **Visual Studio 2022**, the **.NET 8 SDK**, SQL Server and optionally SSMS.
2. Open `AshkanBankSphere.sln`.
3. In SSMS, run SQL scripts under `Database/` in numeric order (`001` through **`008_loans.sql`**). `001_schema.sql` is intended for a fresh database; do not rerun it against an existing schema.
4. For a new database, provision the initial admin following `scripts/CreateAdmin.ps1` and the existing SQL setup instructions. Never commit actual credentials.
5. Configure the `ASHKAN_BANK_DB` environment variable with a SQL Server connection string, for example `Server=localhost;Database=AshkanBankSphere;Trusted_Connection=True;TrustServerCertificate=True;` (trusting a certificate is a local-development convenience, not a production default).
6. Restore NuGet packages, build and run on Windows.
7. Sign in with your provisioned account. The loan applications screen is available to users with read/write permissions; only authorized writers can submit applications.

### Upgrade from Stage 10

Keep your existing database and execute **only** `Database/008_loans.sql`. The script creates `Loans` and `LoanInstallments` if missing. The loan screen currently handles **applications**, not approval, amortization scheduling, repayments or disbursement. Installments are schema groundwork for future stages.

## Project layout

```text
AshkanBankSphere/
├── AshkanBankSphere.sln
├── AshkanBankSphere.csproj
├── DashboardForm.cs / DashboardForm.Designer.cs
├── LoanForm.cs / LoanForm.Designer.cs
├── Infrastructure/
│   ├── BankDb.cs
│   ├── BankService.cs
│   ├── BankSession.cs
│   └── LoanService.cs
├── Database/
│   ├── 001_schema.sql
│   └── ... 008_loans.sql
└── scripts/CreateAdmin.ps1
```

## Engineering decisions

- Financial values use SQL `DECIMAL(19,4)` and C# `decimal`, never floating-point.
- New loan applications and their audit entries share a SQL transaction.
- Authorization is enforced inside the service, not only through button visibility.
- Database access uses parameterized SQL; user-entered values are validated before submission.
- Forms separate event handling from SQL access. UI is bilingual with Persian as default.

## Quality & security roadmap

- Automated build and SQL Server integration tests in CI
- Double-entry ledger and reconciliation; idempotency keys and replay protection
- Immutable audit log with retention policy and sensitive-data minimization
- Full loan approval, amortization and repayment flows with verified accounting rules
- MFA, session lifecycle, fine-grained permissions and secrets management
- Accessibility, DPI scaling, keyboard navigation and complete localization audit
- Threat modeling, fraud checks, encryption policy, operational monitoring and compliance review

## Recruiter notes

This repository is designed to showcase **enterprise application fundamentals** rather than claim production readiness: WinForms modernization, internationalization, SQL transaction boundaries, separation of concerns, and financial-domain modeling. Useful discussion topics include correctness under concurrency, secure authentication, and incremental modernization of desktop systems.

**Suggested GitHub topics:** `csharp`, `dotnet8`, `winforms`, `sql-server`, `banking-system`, `desktop-application`, `rtl`, `localization`, `clean-code`, `portfolio-project`.

## License

No license has been granted by default. Add a license of your choice before inviting reuse.
"# ashkan-banksphere-enterprise-banking" 
