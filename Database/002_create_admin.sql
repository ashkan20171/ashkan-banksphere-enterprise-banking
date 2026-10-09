-- Run after schema creation. Supply a unique password; never commit production credentials.
USE AshkanBankSphere;
GO
DECLARE @password NVARCHAR(200)=N'ChangeMe-Immediately-2026!';
DECLARE @salt VARBINARY(16)=CRYPT_GEN_RANDOM(16);
-- App uses PBKDF2-SHA256; create initial admin with the PowerShell script instead of SQL hashing.
-- See scripts/CreateAdmin.ps1
