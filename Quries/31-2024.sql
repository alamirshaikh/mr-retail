USE [drsale]
GO
/****** Object:  StoredProcedure [dbo].[InsertCustomerTransaction]    Script Date: 2024-01-31 10:57:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[InsertCustomerTransaction]
    @Cust_ID INT,
    @Cust_Name VARCHAR(120),
	@InvoiceId varchar(120),
    @PayMode VARCHAR(120),
    @Amount DECIMAL,
    @Paid DECIMAL,
    @Quantity INT
AS
BEGIN
    DISABLE TRIGGER trgAfterInsertCustomerTransactions ON CustomerTransactions;
    -- Insert into CustomerTransactions table
    INSERT INTO CustomerTransactions (Cust_ID, Cust_Name, Last_Date, PayMode, Amount, Paid, Quantity,InvoiceId)
    VALUES (@Cust_ID, @Cust_Name,GETDATE(), @Paymode, @Amount, @Paid, @Quantity,@InvoiceId);
	ENABLE TRIGGER trgAfterInsertCustomerTransactions ON CustomerTransactions;

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