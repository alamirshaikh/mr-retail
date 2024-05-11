alter procedure MrRetailBackup
as
begin
DECLARE @BackupPath NVARCHAR(500)
		DECLARE @BackupName NVARCHAR(500)
		DECLARE @DatabaseName NVARCHAR(500)
		DECLARE @TodayDate NVARCHAR(20)

		SET @BackupPath = 'C:\MrRetail\backup\' -- Replace with your desired backup path
		SET @DatabaseName = 'drsale' -- Replace with your database name

		SET @TodayDate = REPLACE(CONVERT(VARCHAR(10), GETDATE(), 23), '-', '')

		SET @BackupName = @BackupPath + 'MrRetail' + '_' + @TodayDate + '.bak'

		BACKUP DATABASE @DatabaseName TO DISK = @BackupName
		   WITH FORMAT, 
		   MEDIANAME = 'SQLServerBackups',
		   NAME = @BackupName;
end

 


create trigger BackupCall
on Transactions
AFTER INSERT,UPDATE,DELETE
AS 

BEGIN
EXEC  MrRetailBackup
END