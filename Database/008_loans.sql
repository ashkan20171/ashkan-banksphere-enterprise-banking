USE AshkanBankSphere;
GO
IF OBJECT_ID(N'dbo.Loans', N'U') IS NULL
BEGIN
 CREATE TABLE dbo.Loans (
  Id BIGINT IDENTITY(1,1) PRIMARY KEY,
  CustomerId INT NOT NULL REFERENCES dbo.Customers(Id),
  Principal DECIMAL(19,4) NOT NULL CHECK (Principal > 0),
  AnnualRatePercent DECIMAL(9,4) NOT NULL CHECK (AnnualRatePercent >= 0 AND AnnualRatePercent <= 100),
  TermMonths INT NOT NULL CHECK (TermMonths BETWEEN 1 AND 360),
  Status NVARCHAR(20) NOT NULL DEFAULT N'Pending' CHECK (Status IN (N'Pending',N'Approved',N'Rejected',N'Closed')),
  CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
  ApprovedAt DATETIME2 NULL,
  CONSTRAINT CK_Loans_ApprovedDate CHECK (Status <> N'Approved' OR ApprovedAt IS NOT NULL)
 );
 CREATE INDEX IX_Loans_CustomerId ON dbo.Loans(CustomerId);
END;
GO
IF OBJECT_ID(N'dbo.LoanInstallments', N'U') IS NULL
BEGIN
 CREATE TABLE dbo.LoanInstallments (
  Id BIGINT IDENTITY(1,1) PRIMARY KEY,
  LoanId BIGINT NOT NULL REFERENCES dbo.Loans(Id),
  InstallmentNumber INT NOT NULL CHECK (InstallmentNumber > 0),
  DueDate DATE NOT NULL,
  Amount DECIMAL(19,4) NOT NULL CHECK (Amount > 0),
  PaidAmount DECIMAL(19,4) NOT NULL DEFAULT 0 CHECK (PaidAmount >= 0),
  CONSTRAINT UQ_LoanInstallments UNIQUE (LoanId, InstallmentNumber),
  CONSTRAINT CK_LoanInstallments_Paid CHECK (PaidAmount <= Amount)
 );
END;
GO
