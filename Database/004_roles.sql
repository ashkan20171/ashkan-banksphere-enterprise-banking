USE AshkanBankSphere;
GO
IF COL_LENGTH('dbo.Users','Role') IS NULL
    ALTER TABLE dbo.Users ADD Role NVARCHAR(30) NOT NULL CONSTRAINT DF_Users_Role DEFAULT 'Teller';
GO
-- Role metadata only. Authorization checks must be implemented before production use.
