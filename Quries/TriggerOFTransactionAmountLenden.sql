use drsale
select * from SInvoice
select * from Transactions
select * from Bill
 
EXEC sp_rename 'Transactions.[Date]', 'Date_', 'COLUMN';



 select * from Accounts
 
 

 ---to createing a trigger for transaction update calleges .
 select * from SInvoice


 drop trigger trgAfterInsertSInvoice


 alter table Transactions add Particular varchar(120)


 alter trigger trgAfterInsertSales_Items
 
alter TRIGGER trgcustomer
ON Customer
AFTER INSERT
AS
begin
update Customer set Balance = 0  where ID = (select i.ID from inserted i)

end

 select * from Customer
 
alter TRIGGER trgAfterInsertSInvoice
ON SInvoice
AFTER INSERT
AS
BEGIN
SET NOCOUNT ON;


select * from CustomerTransactions

DECLARE @billDesc VARCHAR(120);
declare @totalqt int;
declare @place varchar(120);



	  
SELECT @place = (select place from inserted)


select @totalqt = Count(qty) from Sale_Items where Invoice = (select invoiceID from inserted);



select * from SInvoice
	
UPDATE Customer SET place = @place where cust_name =  (select cust_name from inserted);

UPDATE SInvoice
SET  totalQty = @totalqt  where invoiceID = (select invoiceID from inserted);

DECLARE item_cursor CURSOR FOR
SELECT description, qty
FROM Purches_Items where Bill = @billid;

OPEN item_cursor;

FETCH NEXT FROM item_cursor INTO @item_name, @stock;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Perform operations here for each row
    -- For example, updating another table:
    UPDATE Product_Item
    SET STOCK -= @stock
    WHERE ITEM_NAME = @item_name;

    FETCH NEXT FROM item_cursor INTO @item_name, @stock;
END

CLOSE item_cursor;
DEALLOCATE item_cursor;




UPDATE Accounts
SET Balance = Balance + (SELECT SUM(TotalBill) FROM inserted)
WHERE AccountID = 1120; -- Replace with the appropriate AccountID



	 
END;

ROLLBACK;

	
	alter table Ledger_Supplier add Trans varchar(120)


select * from Customer

select * from SInvoice

create procedure GetPlace
@cust_name varchar(120)
as
begin
if (select )
end




create trigger trAfterInsertStockmin

ON SInvoice
AFTER INSERT
AS
BEGIN
SET NONCOUNT ON;

FOR ---

	UPDATE Product_Item
	SET STOCK = STOCK - 
	END



select * from Accounts
select * from SInvoice

select * from Transactions


create view amir as
begin
SELECT *
FROM SInvoice
LEFT JOIN Sale_Items ON SInvoice.id = Sale_Items.id;

end


select * from Accounts

SELECT definition
FROM sys.sql_modules



WHERE object_id = OBJECT_ID('trgAfterInsertSInvoice');
----DROP TRIGGER trgAfterInsertBill

	--trigger for debit amount in system
	 
alter TRIGGER trgAfterInsertPayment
ON Payment
AFTER INSERT
AS
BEGIN

 		DECLARE @custsr INT;
    DECLARE @srn INT;
    DECLARE @inv NVARCHAR(50);

    SELECT @custsr = COUNT(*) FROM Ledger_Supplier;
    SELECT @srn = COUNT(*) FROM Payment;

    SET @srn = @srn + 1;
    SET @custsr = @custsr + 1;

    SET @inv = 'SP-' + 
              SUBSTRING(CONVERT(NVARCHAR(8), GETDATE(), 112), 3, 4) + 
              RIGHT('00' + CONVERT(NVARCHAR(2), DAY(GETDATE())), 2) +
              RIGHT('00' + CONVERT(NVARCHAR(2), @custsr), 2) +
              RIGHT('00' + CONVERT(NVARCHAR(2), @srn), 2);


			 
UPDATE Accounts 
SET Balance = Balance - (SELECT SUM(i.Amount) FROM inserted i)
WHERE AccountID = 1120;


     INSERT INTO Ledger_Supplier (SupplierName, Address, City, State, Phone, Description, Debit, Date,Trans)
    SELECT 
        p.company AS SupplierName,
        p.address AS Address,
        p.city AS City,
        p.state AS State,
        p.partimobile AS Phone,
        'Payment' AS Description,
        Sum(i.Amount) AS Debit,

        GETDATE() AS Date,
				@inv as Trans
    FROM inserted i
    JOIN Parties p ON i.name_off = p.company
    GROUP BY 
        p.company,
        p.address,
        p.city,
        p.state,
        p.partimobile;
END;




select * from Bill

alter TRIGGER trgAfterInsertBill
ON Bill
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @billDesc VARCHAR(120);

    -- Variables to hold information
    DECLARE @city VARCHAR(120),
            @state VARCHAR(120),
            @address VARCHAR(120),
            @total DECIMAL(15, 2),
            @name VARCHAR(120),
			@billid varchar(120);

	DECLARE @custsr INT;
    DECLARE @srn INT;
    DECLARE @inv NVARCHAR(50);

    SELECT @custsr = COUNT(*) FROM Ledger_Supplier;
    SELECT @srn = COUNT(*) FROM Payment;

    SET @srn = @srn + 1;
    SET @custsr = @custsr + 1;

    SET @inv = 'SP-' + 
              SUBSTRING(CONVERT(NVARCHAR(8), GETDATE(), 112), 3, 4) + 
              RIGHT('00' + CONVERT(NVARCHAR(2), DAY(GETDATE())), 2) +
              RIGHT('00' + CONVERT(NVARCHAR(2), @custsr), 2) +
              RIGHT('00' + CONVERT(NVARCHAR(2), @srn), 2);





 --cLedetr insert
		
  INSERT INTO Ledger_Supplier (SupplierName, Address, City, State, Phone, Description, Credit, Date,Trans)
    SELECT 
        p.company AS SupplierName,
        p.address AS Address,
        p.city AS City,
        p.state AS State,
        p.partimobile AS Phone,
        'Purchase' AS Description,
        SUM(i.TotalBill) AS Credit,
        GETDATE() AS Date,
		@inv as Trans
    FROM inserted i
    JOIN Parties p ON i.partiname = p.company
    GROUP BY 
        p.company,
        p.address,
        p.city,
        p.state,
        p.partimobile;


		select * from purches_Items


		select * from Transactions

    -- Inserting into Transactions table
    INSERT INTO Transactions (AccountID, Amount, Type, Date_, Description, REFRANCE_ID,Particular)
    SELECT 
        1 AS AccountID, 
        i.TotalBill AS Amount, 
        'Saving' AS Type, 
        i.billdate AS Date_, 
        'Purchase' AS Description,
        i.id AS REFRANCE_ID,
		i.partiname as Particular,
    FROM inserted i;




	--updating the stock
	--UPDATING STOCK MAIN PURPOSE OF THIS
 
 DECLARE @item_name VARCHAR(100);
DECLARE @stock INT;


select @billid = i.items from INSERTED i;
 

DECLARE item_cursor CURSOR FOR
SELECT description, qty
FROM Purches_Items where Bill = @billid;

OPEN item_cursor;

FETCH NEXT FROM item_cursor INTO @item_name, @stock;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Perform operations here for each row
    -- For example, updating another table:
    UPDATE Product_Item
    SET STOCK += @stock
    WHERE ITEM_NAME = @item_name;

    FETCH NEXT FROM item_cursor INTO @item_name, @stock;
END

CLOSE item_cursor;
DEALLOCATE item_cursor;



    -- Updating Accounts table
    UPDATE Accounts
    SET Balance = Balance - (SELECT SUM(TotalBill) FROM inserted)
    WHERE AccountID = 1120; -- Replace with the appropriate AccountID


 
END;




alter TRIGGER trgAfterInsertSInvoice  ON SInvoice  AFTER INSERT
AS 
BEGIN     
SET NOCOUNT ON;      
DECLARE @billDesc VARCHAR(120);   
declare @totalqt int;      
declare @place varchar(120);   
INSERT INTO Transactions (AccountID, Amount, Type, Date_, Description,REFRANCE_ID)    
SELECT       
1 AS AccountID,          
i.TotalBill AS Amount,          
'Saving' AS Type,          
i.invdate AS Date_,          
CONCAT('Bill For', i.cust_Name) AS Description,    
(select id from Customer where cust_name = i.cust_name) AS REFRANCE_ID               
FROM inserted i;                
SELECT @place = (select place from inserted)       
select @totalqt = Count(qty) from Sale_Items where Invoice = (select invoiceID from inserted);      
select * from SInvoice      UPDATE Customer SET place = @place where cust_name =  (select cust_name from inserted);    
UPDATE SInvoice   SET  totalQty = @totalqt  where invoiceID = (select invoiceID from inserted);              

  


  DECLARE @item_name VARCHAR(100);
DECLARE @stock INT;
Declare @billid varchar(120)

 
select @billid = i.items from INSERTED i;
 
 select * from SInvoice

DECLARE item_cursor CURSOR FOR
SELECT  totalQty
FROM SInvoice where items = @billid;

OPEN item_cursor;

FETCH NEXT FROM item_cursor INTO @item_name, @stock;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Perform operations here for each row
    -- For example, updating another table:
    UPDATE Product_Item
    SET STOCK -= @stock
    WHERE ITEM_NAME = @item_name;

    FETCH NEXT FROM item_cursor INTO @item_name, @stock;
END

CLOSE item_cursor;
DEALLOCATE item_cursor;


UPDATE Accounts      SET Balance = Balance + (SELECT SUM(TotalBill) FROM inserted)   
WHERE AccountID = 1120;  
 
END




















ALTER TRIGGER trgAfterInsertSInvoice
ON SInvoice
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @billDesc VARCHAR(120);
    DECLARE @totalqt INT;
    DECLARE @place VARCHAR(120);

    -- Assuming you have only one row in the "inserted" table, you can use variables directly
    SELECT TOP 1 @place = place, @totalqt = COUNT(qty)
    FROM inserted
    GROUP BY place;

    -- Insert into Transactions
    INSERT INTO Transactions (AccountID, Amount, Type, Date_, Description, REFRANCE_ID)
    SELECT
        1 AS AccountID,
        i.TotalBill AS Amount,
        'Saving' AS Type,
        i.invdate AS Date_,
        CONCAT('Bill For', i.cust_Name) AS Description,
        (SELECT id FROM Customer WHERE cust_name = i.cust_name) AS REFRANCE_ID
    FROM inserted i;

    -- Update Customer and SInvoice
    UPDATE Customer
    SET place = @place
    WHERE cust_name IN (SELECT cust_name FROM inserted);

    UPDATE SInvoice
    SET totalQty = @totalqt
    WHERE invoiceID IN (SELECT invoiceID FROM inserted);

    -- Update Accounts
    UPDATE Accounts
    SET Balance = Balance + (SELECT SUM(TotalBill) FROM inserted)
    WHERE AccountID = 1120;
  


  DECLARE @item_name VARCHAR(100);
DECLARE @stock INT;
Declare @billid varchar(120)

 
select @billid = i.items from INSERTED i;
 
 select * from SInvoice

DECLARE item_cursor CURSOR FOR
SELECT  totalQty
FROM SInvoice where items = @billid;

OPEN item_cursor;

FETCH NEXT FROM item_cursor INTO @item_name, @stock;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Perform operations here for each row
    -- For example, updating another table:
    UPDATE Product_Item
    SET STOCK -= @stock
    WHERE ITEM_NAME = @item_name;

    FETCH NEXT FROM item_cursor INTO @item_name, @stock;
END

CLOSE item_cursor;
DEALLOCATE item_cursor;


END;  










--Actutal SInvoice

select * from Ledger_Customer

 
 

 alter TRIGGER trgAfterInsertCustomerTransactions
ON CustomerTransactions
AFTER INSERT
AS				
BEGIN
     INSERT INTO Ledger_Customer (





alter TRIGGER trgAfterInsertSInvoice
ON SInvoice
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @billDesc VARCHAR(120);
    declare @totalqt int;
    declare @place varchar(120);
    INSERT INTO Transactions
    (
        AccountID,
        Amount,
        Type,
        Date_,
        Description,
        REFRANCE_ID
    )
    SELECT 1 AS AccountID,
           i.TotalBill AS Amount,
           'Saving' AS Type,
           i.invdate AS Date_,
           CONCAT('Bill For', i.cust_Name) AS Description,
           (
               select id from Customer where cust_name = i.cust_name
           ) AS REFRANCE_ID
    FROM inserted i;
    SELECT @place =
    (
        select place from inserted
    )
    select @totalqt = Count(qty)
    from Sale_Items
    where Invoice =
    (
        select invoiceID from inserted
    );
    select *
    from SInvoice
    UPDATE Customer
    SET place = @place
    where cust_name =
    (
        select cust_name from inserted
    );
    UPDATE SInvoice
    SET totalQty = @totalqt
    where invoiceID =
    (
        select invoiceID from inserted
    );
    UPDATE Accounts
    SET Balance = Balance +
                  (
                      SELECT SUM(TotalBill) FROM inserted
                  )
    WHERE AccountID = 1120;

	 
   DECLARE @PAID DECIMAL;


SELECT @PAID = Paid
FROM CustomerTransactions ct
WHERE ct.Cust_Name IN (SELECT Cust_Name FROM inserted);
  
  INSERT INTO Ledger_Customer (
    CustomerName,
    ContactName,
    Address,
    State,
    City,
    Phone,
    Date,
    Description,
    Debit,
    Credit,
    Balance,
	Trans,
	Cust_name
)
SELECT 
    p.cust_name AS CustomerName,
    NULL AS ContactName,
    p.cust_addres AS Address,
    p.pstate AS State,
    p.pcity AS City,
    p.cust_phone AS Phone,
    GETDATE() AS Date,
    'SALE' AS Description,
    NULL AS Debit,
    (select TotalBill from inserted) AS Credit,
    NULL AS Balance,
	(SELECT items from inserted) AS   Trans,
	(SELECT cust_name from inserted) AS   Cust_name

FROM 
    inserted i
JOIN 
    Customer p ON i.cust_Name = p.cust_name
GROUP BY 
    p.cust_name,
    p.cust_addres,
    p.pstate,
    p.pcity,
    p.cust_phone;
  


IF @PAID >0
BEGIN
    

 
    INSERT INTO Ledger_Customer (
        CustomerName,
        ContactName,
        Address,
        State,
        City,
        Phone,
        Date,
        Description,
        Debit,
        Credit,
        Balance,
		Trans,
		Cust_name
    )
    SELECT 
        'Cash Account' AS CustomerName,
        NULL AS ContactName,
        p.cust_addres AS Address,
        p.pstate AS State,
        p.pcity AS City,
        p.cust_phone AS Phone,
        GETDATE() AS Date,
        'Payment' AS Description,
        @PAID AS Debit,
        NULL AS Credit,
        NULL AS Balance,
	(SELECT items from inserted) AS   Trans,
	(SELECT cust_name from inserted) AS   Cust_name
    FROM 
        inserted i
    JOIN 
        Customer p ON i.cust_Name = p.cust_name
    GROUP BY 
        p.cust_name,
        p.cust_addres,
        p.pstate,
        p.pcity,
        p.cust_phone;
END;




-- Assuming you have an appropriate number of columns in SInvoice

 END;
 
 select * from Ledger_Customer

 
 ALTER TRIGGER trgAfterInsertCustomerTransactions  
	ON CustomerTransactions 
	AFTER INSERT 
	AS 
	BEGIN      
	
	select * from CustomerTransactions

INSERT INTO
   Transactions ( AccountID, Amount, Type, Date_, Description, REFRANCE_ID,Particular,voucher_t,Pay_T ) 
   SELECT
      1 AS AccountID,
	  i.Paid AS Amount,
      '	Saving' AS Type,
      i.Last_Date AS Date_,
      'Sale/Recovery' AS Description,
      (
         select
            id 
         from
            Customer 
         where
            cust_name = i.cust_name 
      )
      AS REFRANCE_ID ,
	  i.Cust_Name As Paritcular,
	  i.PayMode  AS voucher_t
   FROM
      inserted i;

 	INSERT INTO Ledger_Customer (  
	CustomerName,     
	ContactName,       
	Address,      
	State,     
	City,       
	Phone,       
	Date,        
	Description, 
	Debit,       
	Credit,     
	Balance,     
	Trans,   
	Cust_name  
	) 
	SELECT  
	'Cash Account' AS CustomerName,  
	NULL AS ContactName,  
	p.cust_addres AS Address,   
	p.pstate AS State,    
	p.pcity AS City,    
	p.cust_phone AS Phone, 
	GETDATE() AS Date,   
	(SELECT PayMode FROM inserted) AS Description,   
	(SELECT Paid FROM inserted) AS Debit,   
	NULL AS Credit,      NULL AS Balance, 
	(SELECT InvoiceId from inserted) AS   Trans,   
	(SELECT cust_name from inserted) AS Cust_name   
	from inserted i 
	JOIN  Customer p ON i.cust_Name = p.cust_name
	GROUP BY  p.cust_name,
	p.cust_addres,
	p.pstate,
	p.pcity,
	p.cust_phone;

	END;



 --trigger for  Delete Invoice then Delete Record Remove Balance


 create trigger trgonInvoiceRemve
 on SInvoice
 AFTER Delete
 as
 begin



 end











 --its a new trigger afterbill

 CREATE TRIGGER trgAfterInsertBill 
ON Bill AFTER INSERT AS 
BEGIN
   SET
      NOCOUNT 
      ON;
DECLARE @billDesc VARCHAR(120);
   DECLARE @city VARCHAR(120),
   @state VARCHAR(120),
   @address VARCHAR(120),
   @total DECIMAL(15, 2),
   @name VARCHAR(120),
   @billid varchar(120);
   DECLARE @custsr INT;
   DECLARE @srn INT;
   DECLARE @inv NVARCHAR(50);
   SELECT @custsr = COUNT(*) FROM Ledger_Supplier;
   SELECT @srn = COUNT(*) FROM Payment;
   SET @srn = @srn + 1;
   SET @custsr = @custsr + 1;
   SET @inv = 'SP-' +SUBSTRING(CONVERT(NVARCHAR(8), GETDATE(), 112), 3, 4) +  RIGHT('00' + CONVERT(NVARCHAR(2),
   DAY(GETDATE())), 2) + RIGHT('00' + CONVERT(NVARCHAR(2), @custsr), 2) + RIGHT('00' + CONVERT(NVARCHAR(2), @srn), 2);
   --cLedetr insert
   INSERT INTO Ledger_Supplier (SupplierName, Address, City, State, Phone, Description, Credit, Date,Trans)
   SELECT p.company AS SupplierName,
   p.address AS Address,
   p.city AS City,
   p.state AS State,
   p.partimobile AS Phone,
   'Purchase' AS Description,
   SUM(i.TotalBill) AS Credit,
   GETDATE() AS Date,
   @inv as Trans 
   FROM inserted i 
   JOIN Parties p ON i.partiname = p.company
   GROUP BY 
   p.company,
   p.address,
   p.city,
   p.state,
   p.partimobile;
   select * from purches_Items
   select * from Transactions
   -- Inserting into Transactions table
   INSERT INTO Transactions (AccountID, Amount, Type, Date_, Description, REFRANCE_ID,Particular)
   SELECT 1 AS AccountID,
   i.TotalBill AS Amount,
   'Saving' AS Type,
   i.billdate AS Date_,
   'Purchase' AS Description,
   i.id AS REFRANCE_ID,
   i.partiname as Particular
   FROM inserted i;
   --updating the stock   --UPDATING STOCK MAIN PURPOSE OF THIS
   DECLARE @item_name VARCHAR(100);
   DECLARE @stock INT;
   select @billid = i.items from INSERTED i;
   DECLARE item_cursor CURSOR FOR  SELECT description, qty  FROM Purches_Items where Bill = @billid;
   OPEN item_cursor;
   FETCH NEXT FROM item_cursor INTO @item_name, @stock; 
   WHILE @@FETCH_STATUS = 0  BEGIN 
   -- Perform operations here for each row      -- For example, updating another table: 
   UPDATE Product_Item     
   SET STOCK += @stock   
   WHERE ITEM_NAME = @item_name; 
   FETCH NEXT FROM item_cursor INTO @item_name, @stock;
   END  
   CLOSE item_cursor;
   DEALLOCATE item_cursor;
   -- Updating Accounts table      
   UPDATE Accounts    
   SET Balance = Balance - (SELECT SUM(TotalBill) FROM inserted)
   WHERE AccountID = 1120; -- Replace with the appropriate AccountID         
   END;

--end this line


alter TRIGGER trgAfterInsertSInvoice 
ON SInvoice AFTER INSERT AS 
BEGIN
   SET
      NOCOUNT 
      ON;
DECLARE @billDesc VARCHAR(120);
declare @totalqt int;
declare @place varchar(120);

select * from Transactions
select * from CustomerTransactions



SELECT
   @place = 
   (
      select
         place 
      from
         inserted 
   )
   select
      @totalqt = Count(qty) 
   from
      Sale_Items 
   where
      Invoice = 
      (
         select
            invoiceID 
         from
            inserted 
      )
;
select
   * 
from
   SInvoice 
   UPDATE
      Customer 
   SET
      place = @place 
   where
      cust_name = 
      (
         select
            cust_name 
         from
            inserted 
      )
;
UPDATE
   SInvoice 
SET
   totalQty = @totalqt 
where
   invoiceID = 
   (
      select
         invoiceID 
      from
         inserted 
   )
;
UPDATE
   Accounts 
SET
   Balance = Balance + ( 
   SELECT
      SUM(TotalBill) 
   FROM
      inserted ) 
   WHERE
      AccountID = 1120;
DECLARE @PAID DECIMAL;
SELECT
   @PAID = Paid 
FROM
   CustomerTransactions ct 
WHERE
   ct.Cust_Name IN 
   (
      SELECT
         Cust_Name 
      FROM
         inserted
   )
;
INSERT INTO
   Ledger_Customer ( CustomerName, ContactName, Address, State, City, Phone, Date, Description, Debit, Credit, Balance, Trans, Cust_name ) 
   SELECT
      p.cust_name AS CustomerName,
      NULL AS ContactName,
      p.cust_addres AS Address,
      p.pstate AS State,
      p.pcity AS City,
      p.cust_phone AS Phone,
      GETDATE() AS Date,
      'SALE' AS Description,
      NULL AS Debit,
      (
         select
            TotalBill 
         from
            inserted
      )
      AS Credit,
      NULL AS Balance,
      (
         SELECT
            items 
         from
            inserted
      )
      AS Trans,
      (
         SELECT
            cust_name 
         from
            inserted
      )
      AS Cust_name 
   FROM
      inserted i 
      JOIN
         Customer p 
         ON i.cust_Name = p.cust_name 
   GROUP BY
      p.cust_name,
      p.cust_addres,
      p.pstate,
      p.pcity,
      p.cust_phone;
IF @PAID > 0 
BEGIN
   INSERT INTO
      Ledger_Customer ( CustomerName, ContactName, Address, State, City, Phone, Date, Description, Debit, Credit, Balance, Trans, Cust_name ) 
      SELECT
         'Cash Account' AS CustomerName,
         NULL AS ContactName,
         p.cust_addres AS Address,
         p.pstate AS State,
         p.pcity AS City,
         p.cust_phone AS Phone,
         GETDATE() AS Date,
         'Payment' AS Description,
         @PAID AS Debit,
         NULL AS Credit,
         NULL AS Balance,
         (
            SELECT
               items 
            from
               inserted
         )
         AS Trans,
         (
            SELECT
               cust_name 
            from
               inserted
         )
         AS Cust_name 
      FROM
         inserted i 
         JOIN
            Customer p 
            ON i.cust_Name = p.cust_name 
      GROUP BY
         p.cust_name,
         p.cust_addres,
         p.pstate,
         p.pcity,
         p.cust_phone;
END
;
-- Assuming you have an appropriate number of columns in SInvoice    
END;




--this trigger for Kharcha after add in transactions

alter trigger trgExpanses
on  Kharcha
AFTER INSERT
AS
begin
 
  
    INSERT INTO Transactions (AccountID, Amount, Type, Date_, Description, REFRANCE_ID,Particular,voucher_t)
    SELECT 
        1 AS AccountID, 
        i.Amount AS Amount, 
        'Saving' AS Type, 
        i.kharcha_date AS Date_, 
        'Kharcha' AS Description,
        i.ID AS REFRANCE_ID,
		i.Kharcha_Name as Particular,
		'Cash' AS voucher_t
    FROM inserted i;

end



 alter TRIGGER trgAfterInsertPayment 
ON Payment AFTER INSERT AS 
BEGIN

 INSERT INTO Transactions (AccountID, Amount, Type, Date_, Description, REFRANCE_ID,Particular,voucher_t)
    SELECT 
        1 AS AccountID, 
        i.Amount AS Amount, 
        'Saving' AS Type, 
        i.TranDate AS Date_, 
        'Payment' AS Description,
        i.ID AS REFRANCE_ID,
		i.name_off as Particular,
		i.PaymentMode as voucher_t
    FROM inserted i;





   DECLARE @custsr INT;
DECLARE @srn INT;
DECLARE @inv NVARCHAR(50);
SELECT
   @custsr = COUNT(*) 
FROM
   Ledger_Supplier;
SELECT
   @srn = COUNT(*) 
FROM
   Payment;
SET
   @srn = @srn + 1;
SET
   @custsr = @custsr + 1;
SET
   @inv = 'SP-' + SUBSTRING(CONVERT(NVARCHAR(8), GETDATE(), 112), 3, 4) + RIGHT('00' + CONVERT(NVARCHAR(2), DAY(GETDATE())), 2) + RIGHT('00' + CONVERT(NVARCHAR(2), @custsr), 2) + RIGHT('00' + CONVERT(NVARCHAR(2), @srn), 2);
UPDATE
   Accounts 
SET
   Balance = Balance - (
   SELECT
      SUM(i.Amount) 
   FROM
      inserted i) 
   WHERE
      AccountID = 1120;
INSERT INTO
   Ledger_Supplier (SupplierName, Address, City, State, Phone, Description, Debit, Date, Trans) 
   SELECT
      p.company AS SupplierName,
      p.address AS Address,
      p.city AS City,
      p.state AS State,
      p.partimobile AS Phone,
      'Payment' AS Description,
      Sum(i.Amount) AS Debit,
      GETDATE() AS Date,
      @inv as Trans 
   FROM
      inserted i 
      JOIN
         Parties p 
         ON i.name_off = p.company 
   GROUP BY
      p.company,
      p.address,
      p.city,
      p.state,
      p.partimobile;
END
;

select * from CustomerTransactions


select * from Transactions