USE AshkanBankSphere;
GO
IF OBJECT_ID(N'dbo.AuditEvents', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.AuditEvents
    (
        Id BIGINT IDENTITY(1,1) PRIMARY KEY,
        EventType NVARCHAR(60) NOT NULL,
        Details NVARCHAR(1000) NOT NULL,
        CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_AuditEvents_CreatedAt DEFAULT SYSUTCDATETIME()
    );
    CREATE INDEX IX_AuditEvents_CreatedAt ON dbo.AuditEvents(CreatedAt DESC);
END;
GO
