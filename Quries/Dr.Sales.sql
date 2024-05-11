create database drsale;

use drsale

 
create table Product_Item
(
   ID INT NOT NULL PRIMARY KEY IDENTITY(5000,1),
   PR_CODE NVARCHAR(60),
   ITEM_NAME NVARCHAR(120),
   TYPE_TAX  INT,
   STOCK INT,
   UNIT NVARCHAR(50),
   BARCODE NVARCHAR(120),
   SALE_PRICE DECIMAL,
   ACCOUNT NVARCHAR(60),
   DESCRIPTIONS NVARCHAR(120),
   COST_PRICE DECIMAL,
   pr_ACCOUNT NVARCHAR(50),
   pr_COSTPRICE DECIMAL,
   pr_DESCRIPTION DECIMAL,
   IDATE DATE,
   USER_N NVARCHAR(50)
);
 
 alter table Product_Item add  pic image 

 drop procedure spInsertProductItem

  CREATE PROCEDURE spInsertProductItem
(
    @PR_CODE NVARCHAR(10),
    @ITEM_NAME NVARCHAR(120),
    @TYPE_TAX INT,
    @STOCK INT,
    @UNIT NVARCHAR(50),
    @BARCODE NVARCHAR(120),
    @SALE_PRICE DECIMAL,
    @ACCOUNT NVARCHAR(60),
    @DESCRIPTIONS NVARCHAR(120),
    @COST_PRICE DECIMAL,
    @pr_ACCOUNT NVARCHAR(50),
    @pr_COSTPRICE DECIMAL,
    @pr_DESCRIPTION NVARCHAR(120),
    @IDATE DATE,
    @USER_N NVARCHAR(50)
)
AS
BEGIN
    INSERT INTO Product_Item (PR_CODE, ITEM_NAME, TYPE_TAX, STOCK, UNIT, BARCODE, SALE_PRICE, ACCOUNT, DESCRIPTIONS, COST_PRICE, pr_ACCOUNT, pr_COSTPRICE, pr_DESCRIPTION, IDATE, USER_N)
    VALUES (
        @PR_CODE,
        @ITEM_NAME,
        @TYPE_TAX,
        @STOCK,
        @UNIT,
        @BARCODE,
        @SALE_PRICE,
        @ACCOUNT,
        @DESCRIPTIONS,
        @COST_PRICE,
        @pr_ACCOUNT,
        @pr_COSTPRICE,
        @pr_DESCRIPTION,
        @IDATE,
        @USER_N
    );
END;



EXEC spInsertProductItem
    @PR_CODE = N'P',
    @ITEM_NAME = N'Product Name',
    @TYPE_TAX = 1, -- Use an integer, not an NVARCHAR
    @STOCK = 100,
    @UNIT = N'Each',
    @BARCODE = N'1234567890',
    @SALE_PRICE = 19.99,
    @ACCOUNT = N'Account Name',
    @DESCRIPTIONS = N'Product Description',
    @COST_PRICE = 14.99,
    @pr_ACCOUNT = N'PR Account',
    @pr_COSTPRICE = 12.99,
    @pr_DESCRIPTION = N'PR Description',
    @IDATE = '2023-10-30',
    @USER_N = N'User Name';




create procedure spGetProducts

as
begin
select * from Product_Item;
end


exec spGetProducts


create procedure TotalItems
as
begin
select Count(ID) from Product_Item
end
 
 

create procedure SetAllBarcode
as
begin



DECLARE item_cursor CURSOR FOR
SELECT BARCODE
FROM Product_Item where BARCODE IS NULL ;

OPEN item_cursor;
DECLARE @NewBarcode VARCHAR(120);
FETCH NEXT FROM item_cursor INTO  @NewBarcode

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Perform operations here for each row
    -- For example, updating another table:
      EXEC GetbarGetBarcode @NewBarcode OUTPUT;
	UPDATE Product_Item
    SET BARCODE = @NewBarcode
    WHERE BARCODE IS NULL;
	
    FETCH NEXT FROM item_cursor INTO @NewBarcode;
END

CLOSE item_cursor;
DEALLOCATE item_cursor;

end


exec SetAllBarcode


---barcode update 


 
CREATE procedure [dbo].[GetBarcode]
AS
BEGIN
declare @output int
 SELECT @output =  Count(ID) FROM Product_Item
IF @output < 0
begin
select 500000
END
 
 select  @output+1000000
 
END
