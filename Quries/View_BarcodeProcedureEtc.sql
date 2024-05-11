use drsale




select * from dbo.Customer


alter table Customer add pcity  varchar(120)
alter table Customer add pstate  varchar(120)




 
create procedure GetBarcode
AS
BEGIN
declare @output int
 SELECT @output =  MAX(ID) FROM Product_Item
IF @output < 0
begin
select 1112320
END
 
 select  @output+2600
 
END


select * from Parties
--adding new column in Parties
alter table Parties add BankName varchar(120)
alter table Parties add ACName varchar(120)
alter table Parties add ACNumber varchar(120)
alter table Parties add IFCCODE varchar(120)





 
 
 declare @output int
exec GetBarcode
print(@output)