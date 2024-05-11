CREATE TABLE Ledger_Customer (
    CustomerID INT PRIMARY KEY identity(120,1),
    CustomerName VARCHAR(100) NOT NULL,
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

ALTER TABLE Ledger_Customer  ADD Trans varchar(120)
ALTER TABLE Ledger_Customer  ADD Cust_name varchar(120)


SELECT * FROM Ledger_Customer

CREATE PROCEDURE GetBalanceCustomer
    @CustomerName VARCHAR(120)
AS
BEGIN
    DECLARE @total DECIMAL(18, 2);
    DECLARE @debit DECIMAL(18, 2);

    SELECT @total = ISNULL(SUM(CONVERT(DECIMAL(18, 2), Credit)), 0)
    FROM Ledger_Customer
    WHERE CustomerName = @CustomerName;

    SELECT @debit = ISNULL(SUM(CONVERT(DECIMAL(18, 2), Debit)), 0)
    FROM Ledger_Customer
    WHERE CustomerName = @CustomerName;

    SET @total = @total - @debit;

    SELECT @total AS Balance;
END;


