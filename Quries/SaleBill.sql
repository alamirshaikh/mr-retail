
---Bill Invice in Sales -- add value 

create table SInvoice
( 
id int not null primary key identity(7000,1),
invoiceID varchar(100),
cust_Name varchar(120), 
items nvarchar(120), -- itemstable invoice id
sub_total decimal,
perdis decimal,
discount decimal,
other decimal,
TotalBill decimal,
invdate datetime
  
); 
 
ALTER table SInvoice add totalQty int

 
 

 drop table SInvoice
 
 
FROM sys.res
WHERE [object_id] = OBJECT_ID('YourTableName');

create table Sale_Items
(
id int not null primary key identity(8000,1),
sr_no int,
description nvarchar(120),
qty int,
rate decimal,
discount decimal,
amount decimal,
Invoice nvarchar(120)
);





--for insert the data 
CREATE PROCEDURE spSaleInsert
(
    @invoiceID VARCHAR(100),
    @cust_Name VARCHAR(120),
    @items nvarchar(120),
    @sub_total DECIMAL,
    @perdis DECIMAL,
    @discount DECIMAL,
    @other DECIMAL,
    @TotalBill DECIMAL,
	@invdate datetime,
	@place varchar

	)
AS
BEGIN
    INSERT INTO SInvoice (invoiceID, cust_Name, items, sub_total, perdis, discount, other, TotalBill,invdate,place)
    VALUES (@invoiceID, @cust_Name, @items, @sub_total, @perdis, @discount, @other, @TotalBill,@invdate,@place);
END;


use drsale
select * from SInvoice


 

SELECT *
FROM SInvoice
INNER JOIN Sale_Items ON SInvoice.InvoiceID = Sale_Items.Invoice
WHERE SInvoice.InvoiceID = 'INV2311255353';

SELECT * FROM SInvoice INNER JOIN Sale_Items ON SInvoice.InvoiceID = Sale_Items.Invoice WHERE SInvoice.InvoiceID = 'INV2311267171'

select * from Sale_Items where  Invoice= 'INV2311267070'

select * from Sale_Items where Invoice = 'INV2311255353'

SELECT *
FROM SInvoice
JOIN Sale_Items ON Sale_Items.id = SInvoice.ID;



drop procedure UpdateStock

 


create procedure UpdateStock
@BARCODE  varchar(120),
@UNIT  varchar(120),
@STOCK  int,
@ID int
as
begin
 
 update Product_Item set BARCODE = @BARCODE , UNIT = @UNIT,STOCK = @STOCK where ID = @ID

end


select unit from Unit


 
 