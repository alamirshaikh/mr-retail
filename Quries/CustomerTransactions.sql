select * from CustomerTransactions
CREATE TABLE CustomerTransactions
(
    ID INT NOT NULL PRIMARY KEY IDENTITY(13,23),
    Cust_ID INT,
    Cust_Name VARCHAR(120),
    Last_Date DATETIME,
    PayMode VARCHAR(120),
	InvoiceId varchar(120),
    Amount decimal,
	Paid decimal,
    Quantity INT, 
);
 
 


 
ALTER  table Customer add Balance decimal
 
 
select * from Customer
 
select * from OwnerInformation

 
alter PROCEDURE InsertCustomerTransaction
    @Cust_ID INT,
    @Cust_Name VARCHAR(120),
	@InvoiceId varchar(120),
    @PayMode VARCHAR(120),
    @Amount DECIMAL,
    @Paid DECIMAL,
    @Quantity INT
AS
BEGIN
    -- Insert into CustomerTransactions table
    INSERT INTO CustomerTransactions (Cust_ID, Cust_Name, Last_Date, PayMode, Amount, Paid, Quantity)
    VALUES (@Cust_ID, @Cust_Name,GETDATE(), @Paymode, @Amount, @Paid, @Quantity);

    -- Update customer balances in the Customers table based on payment method
    
        -- Apply logic for cash transactions
	 
		   declare @diff decimal;
		   set @diff = @Paid - @Amount
		   UPDATE Customer
        SET
         Balance = Balance + @diff
         WHERE
            id = @Cust_ID;
		 
	 
	  
     
END;

select * from CustomerTransactions




 






--this for customer transactions entry


