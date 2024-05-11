CREATE FUNCTION Getbar()
RETURNS INT
AS
BEGIN
    DECLARE @output INT;

    SELECT @output = MAX(ID) FROM Product_Item;

    IF @output IS NULL OR @output < 0
    BEGIN
        SET @output = 1112320;
    END
    ELSE
    BEGIN
        SET @output = @output + 2600;
    END

    RETURN @output;
END;

ALTER PROCEDURE AddProduct_List
    @path VARCHAR(120)
AS
BEGIN
    -- Enable 'Ad Hoc Distributed Queries' option
    EXEC sp_configure 'show advanced options', 1;
    RECONFIGURE;
	 
    EXEC sp_configure 'Ad Hoc Distributed Queries', 1;
    RECONFIGURE; 

	declare @bar varchar(120);DECLARE @sqlQuery NVARCHAR(MAX);
 
 
SET @sqlQuery = N'
INSERT INTO Product_Item (ITEM_NAME, STOCK, UNIT, BARCODE, SALE_PRICE, COST_PRICE, IDATE)
SELECT 
    ITEM_NAME, 
    STOCK, 
    UNIT, 
    BARCODE,
    SALE_PRICE, 
    COST_PRICE, 
    GETDATE() AS IDATE
FROM OPENROWSET(''Microsoft.ACE.OLEDB.12.0'', 
                ''Excel 12.0;Database=' + @path + ';HDR=YES;'', 
                ''SELECT * FROM [Sheet1$]'')';

EXEC sp_executesql 'C:\Users\PAWAR-PC\Desktop\data.xlsx)
END;

   drop procedure AddProduct_List
 

INSERT INTO Test1(name, marks, country)
SELECT 
    name,
    marks,
    CASE 
        WHEN ISNULL(country, '') = '' THEN 'India'
        ELSE country
    END AS country
FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0', 
                'Excel 12.0;Database=C:\student.xlsx;HDR=YES', 
                'SELECT * FROM [Sheet1$]');

 