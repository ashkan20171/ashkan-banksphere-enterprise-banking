USE AshkanBankSphere;
GO
-- Run Stage 5 migration (004_roles.sql) first.
-- Assign roles explicitly to existing users; never infer roles from user names.
-- Example, after reviewing your user record:
-- UPDATE dbo.Users SET Role = N'Admin' WHERE Username = N'your-admin-username';
-- Allowed application roles: Admin, Manager, Teller, Auditor.
-- Auditor can read audit history but cannot create customers/accounts or transfer funds.
-- Teller can modify records but cannot view audit history.
