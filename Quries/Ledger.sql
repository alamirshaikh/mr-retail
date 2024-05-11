use drsale

CREATE TABLE Ledger_Supplier (
    SupplierID INT PRIMARY KEY identity(120,1),
    SupplierName VARCHAR(100) NOT NULL,
    ContactName VARCHAR(100),
    Address VARCHAR(255),
	State varchar(120),
    City VARCHAR(100),
    Phone VARCHAR(20),
	Date datetime,
	Description varchar(120),
	Debit varchar(120),
	Credit varchar(120),
    Balance varchar(120)
);

CREATE PROCEDURE GetBalance
    @SupplierName VARCHAR(120)
AS
BEGIN
    DECLARE @total DECIMAL(18, 2);
    DECLARE @debit DECIMAL(18, 2);

    SELECT @total = ISNULL(SUM(CONVERT(DECIMAL(18, 2), Credit)), 0)
    FROM Ledger_Supplier
    WHERE SupplierName = @SupplierName;

    SELECT @debit = ISNULL(SUM(CONVERT(DECIMAL(18, 2), Debit)), 0)
    FROM Ledger_Supplier
    WHERE SupplierName = @SupplierName;

    SET @total = @total - @debit;

    SELECT @total AS Balance;
END;
 
  select * from Ledger_Supplier
  
 exec GetBalance

select * from Parties


  
SELECT SUM(CONVERT(DECIMAL(18, 2), Credit)) AS TotalCredit
FROM Ledger_Supplier;

--payment table and operation perfonrm
create table Payment
(
	 ID int not null primary key identity(10000,15),
	 name_off VARCHAR(120),
	 TransactionNo varchar(120),
	 TranDate datetime,
	 PaymentMode varchar(100),
	 Amount decimal

);



CREATE PROCEDURE InsertPayment
    @NameOf VARCHAR(120),
    @TransactionNo VARCHAR(120),
    @TranDate DATETIME,
    @PaymentMode VARCHAR(100),
    @Amount DECIMAL
AS
BEGIN
    INSERT INTO Payment (name_off, TransactionNo, TranDate, PaymentMode, Amount)
    VALUES (@NameOf, @TransactionNo, @TranDate, @PaymentMode, @Amount);
END;








select * from Ledger_Supplier

 



 CREATE TRIGGER trgAfterInsertCustomerTransactions
ON CustomerTransactions
AFTER INSERT
AS
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
        Balance
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
    NULL AS Credit,
    NULL AS Balance
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


 TRUNCATE TABLE bill

 select * from Accounts
  