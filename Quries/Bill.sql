use drsale

select * from SInvoice


create table Bill
( 
id int not null primary key identity(7000,1),
BillID varchar(100),
partiname varchar(120), 
items nvarchar(120), -- itemstable invoice id
sub_total decimal,
perdis decimal,
discount decimal,
other decimal,
TotalBill decimal,
billdate datetime
  
); 


CREATE PROCEDURE InsertBill
    @BillID varchar(100),
    @partiname varchar(120),
    @items nvarchar(120),
    @sub_total decimal,
    @perdis decimal,
    @discount decimal,
    @other decimal,
    @TotalBill decimal,
    @billdate datetime
AS
BEGIN
    INSERT INTO Bill (BillID, partiname, items, sub_total, perdis, discount, other, TotalBill, billdate)
    VALUES (@BillID, @partiname, @items, @sub_total, @perdis, @discount, @other, @TotalBill, @billdate)
END



CREATE PROCEDURE DeleteBill
    @id int
AS
BEGIN
    DELETE FROM Bill WHERE id = @id
END


CREATE PROCEDURE UpdateBill
    @id int,
    @BillID varchar(100),
    @partiname varchar(120),
    @items nvarchar(120),
    @sub_total decimal,
    @perdis decimal,
    @discount decimal,
    @other decimal,
    @TotalBill decimal,
    @billdate datetime
AS
BEGIN
    UPDATE Bill
    SET BillID = @BillID,
        partiname = @partiname,
        items = @items,
        sub_total = @sub_total,
        perdis = @perdis,
        discount = @discount,
        other = @other,
        TotalBill = @TotalBill,
        billdate = @billdate
    WHERE id = @id
END


CREATE PROCEDURE SelectBill
    @id int
AS
BEGIN

  IF @ID = 0
  BEGIN
  SELECT * FROM Bill
  END
  ELSE
  BEGIN
  SELECT * FROM Bill WHERE id = @id
  END
END



create table purches_Items
(
id int not null primary key identity(8000,1),
sr_no int,
description nvarchar(120),
qty int,
rate decimal,
discount decimal,
amount decimal,
Bill nvarchar(120)
);


--for insert the data 
CREATE PROCEDURE insertitems
(
    @sr_no int,
    @description VARCHAR(120),
    @qty int,
    @rate DECIMAL,
    @discount  DECIMAL,
    @amount DECIMAL,
    @Bill varchar(120)

	)
AS
BEGIN
    INSERT INTO purches_Items (sr_no,  description, qty, rate, discount, amount, Bill)
    VALUES (@sr_no, @description, @qty, @rate, @discount, @amount, @Bill);
END;


