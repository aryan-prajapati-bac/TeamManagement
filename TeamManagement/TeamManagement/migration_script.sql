BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ADD [secondMobileNumber] nvarchar(max) NOT NULL DEFAULT N'ert';
GO

UPDATE [Users] SET [secondMobileNumber] = N'ert'
WHERE [Email] = N'aryanprajapati2112001@gmail.com';
SELECT @@ROWCOUNT;

GO

UPDATE Users SET secondMobileNumber = 'ert' WHERE secondMobileNumber IS NULL
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240307054009_init10', N'8.0.2');
GO

COMMIT;
GO

