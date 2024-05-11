use drsale


--for all sales showing in dowbelow
SELECT invdate  FROM SInvoice 
WHERE invdate >= '2023-12-14 00:00:00' AND invdate <= '2023-12-16 23:59:59';

create procedure GetSalesReport
@startdate varchar(120),
@enddate varchar(120)
as
begin
declare @ids varchar(max);
 set @ids = ( SELECT invoiceID  FROM SInvoice 
WHERE invdate >= '2023-12-14 00:00:00' AND invdate <= '2023-12-16 23:59:59');

select * from Sale_Items where Invoice = @ids


select * from Sale_Items


select sum(qty) as QTY,Sum(amount) as Total from Sale_Items where 

invdates =  SInvoice where @start and @enddate
item name = 


end
