USE AshkanBankSphere;
GO
-- Run schema migrations 001, 003 and 004 before enabling user administration.
IF COL_LENGTH('dbo.Users','Role') IS NULL
    ALTER TABLE dbo.Users ADD Role NVARCHAR(30) NOT NULL CONSTRAINT DF_Users_Role_Stage9 DEFAULT 'Teller';
GO
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name='CK_Users_Role')
    ALTER TABLE dbo.Users ADD CONSTRAINT CK_Users_Role CHECK (Role IN ('Admin','Manager','Teller','Auditor'));
GO
