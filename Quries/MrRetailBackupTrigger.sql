



Declare @tab_name varchar(120);


select  @tab_name = TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Bill'
IF @tab_name = 'Bill'
begin

select 'exist'

end
else
begin
select 'Not exist'
end