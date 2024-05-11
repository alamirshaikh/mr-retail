

---Bill Invice in Sales -- add value 

create table SInvoice
( 
id int not null primary key identity(7000,1),
invoiceID varchar(100),
cust_Name varchar(120), 
items int, -- itemstable invoice id
sub_total decimal,
perdis decimal,
discount decimal,
other decimal,
TotalBill decimal,
invdate datetime
  
);


select * from SInvoice

--drop table SInvoice
 

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

use drsale
 

--for insert the data
CREATE PROCEDURE spSaleInsert
(
    @invoiceID VARCHAR(100),
    @cust_Name VARCHAR(120),
    @items INT,
    @sub_total DECIMAL,
    @perdis DECIMAL,
    @discount DECIMAL,
    @other DECIMAL,
    @TotalBill DECIMAL,
    @invdate datetime
)
AS
BEGIN
    INSERT INTO SInvoice (invoiceID, cust_Name, items, sub_total, perdis, discount, other, TotalBill,invdate)
    VALUES (@invoiceID, @cust_Name, @items, @sub_total, @perdis, @discount, @other, @TotalBill,@invdate);
END;
