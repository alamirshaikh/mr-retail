use drsale

CREATE TABLE [dbo].Unit
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1), 
    [Unit] VARCHAR(50) NULL, 
    [DateU] DATETIME NOT NULL
)
 
create procedure spUnit
@Command int,
@Unit varchar(50)
as
begin

IF @Command = 0
begin 

insert into Unit (Unit,DateU) values(@Unit,GETDATE())
end
IF @Command = 1
begin 
delete from Unit where Unit = @Unit
end
end


create procedure spUnitSelect
as
begin
select Unit from Unit;
end

