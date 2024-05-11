


use drsale

select COUNT(invoiceID) from SInvoice

select * from Customer


create table Customer
(
    id int not null primary key identity(1000,1),
	cust_name varchar(120),
	cust_phone varchar(110),
	cust_addres varchar(140),
	cust_service varchar(120),
	cust_date datetime2

);
 

select * from Customer
 alter table Customer add pcity  varchar(120)
alter table Customer add pstate  varchar(120)


 
CREATE PROCEDURE sp_Customer
    @cust_name varchar(120),
    @cust_phone varchar(110),
    @cust_address varchar(140),
    @cust_service varchar(120),
    @cust_date datetime2,
	@pcity varchar(120),
	@pstate  varchar(120)
AS
BEGIN
    INSERT INTO Customer (cust_name, cust_phone, cust_addres, cust_service, cust_date,pcity,pstate)
    VALUES (@cust_name, @cust_phone, @cust_address, @cust_service, @cust_date,@pcity,@pstate);
END



 
create procedure sp_CustomerGet
@custID int
AS
BEGIN
IF @custID = 1
BEGIN
select * from Customer
END
ELSE 
BEGIN
select * from Customer WHERE ID = @custID

END
END


use drsale

create procedure sp_getCustomer
@cust_name varchar(120)
AS
BEGIN

IF @cust_name = ''
begin
RETURN	
end
else
begin
select cust_name from Customer where cust_name LIKE '%'+@cust_name+'%'
end

END


select  * from





select * from Customer where ID = {whichvaue} 

select * from Customer 
	


EXEC sp_Customer
    @cust_name = 'John Doe',
    @cust_phone = 1234567890,
    @cust_address = '123 Main Street',
    @cust_service = 'Service Type',
    @cust_date = ''; -- Use the appropriate date and time



	create view GetCustTrHis A

	SELECT * FROM Transactions WHERE REFE

	alter table Customer add Place varchar(120)

	select * from Customer




	update Customer set Balance = 0 where ID = 1000


	select * from SInvoice