USE drsale


SELECT * FROM SInvoice
SELECT * FROM Sale_Items
--adding new table column
ALTER TABLE Sale_Items
ALTER COLUMN invdate datetime;


--please excute in client pc
alter TRIGGER UpdateSalesItem
ON Sale_Items
AFTER INSERT
AS
BEGIN
    UPDATE Sale_Items
    SET invdate = GETDATE()
    FROM Sale_Items si
    INNER JOIN inserted i ON si.id = i.id;


	UPDATE Product_Item
    SET STOCK = STOCK - (SELECT qty FROM inserted) WHERE  ITEM_NAME = (select description from inserted)

END;

	
 



select * from Sale_Items

use drsale
 
 
--list of sales in datees item count and total
 
CREATE FUNCTION GetListReport (@start VARCHAR(120), @end VARCHAR(120))
RETURNS TABLE
AS
RETURN
(

SELECT invdate,description, COUNT(description) AS NameCount, SUM(amount) AS Total
FROM Sale_Items
WHERE invdate >= @start AND invdate <= @end
GROUP BY description,invdate

);

alter procedure GetSaleReport
@start varchar(120),
@end varchar(120)
as
begin
exec GetSalesInfo @start,@end

select * from GetListReport(@start, @end)
end
 
exec GetSaleReport '2023-12-25 00:00:00', '2023-12-25 23:59:59'

--higy sale item by date--
SELECT description, COUNT(description) AS TotalQty, SUM(amount) AS Total
FROM Sale_Items
WHERE invdate >= '2023-12-25 00:00:00' AND invdate <= '2023-12-25 23:59:59'
GROUP BY description
HAVING COUNT(description) = (SELECT MAX(TotalQty) FROM (SELECT COUNT(description) AS TotalQty FROM Sale_Items WHERE invdate >= '2023-12-25 00:00:00' AND invdate <= '2023-12-25 23:59:59' GROUP BY description) AS SubQueryAlias);

--no of sales--
SELECT COUNT(qty),Sum(Amount) as Amt
FROM Sale_Items
WHERE invdate >= '2023-12-25 00:00:00' AND invdate <= '2023-12-25 23:59:59';

--lowest sale product--
SELECT TOP 1 description, COUNT(description) AS TotalQty, SUM(amount) AS Total
FROM Sale_Items
WHERE invdate >= '2023-12-25 00:00:00' AND invdate <= '2023-12-25 23:59:59'
GROUP BY description
HAVING COUNT(description) = (SELECT MIN(TotalQty) FROM (SELECT COUNT(description) AS TotalQty FROM Sale_Items WHERE invdate >= '2023-12-25 00:00:00' AND invdate <= '2023-12-25 23:59:59' GROUP BY description) AS SubQueryAlias);

    CREATE TABLE CombinedResults (
        Type VARCHAR(50),
        Description VARCHAR(100),
		TotalSales decimal(18,2), 
        TotalQty INT,
        TotalAmount DECIMAL(18, 2)
    );

 
alter PROCEDURE GetSalesInfo
    @StartDate VARCHAR(120),
    @EndDate VARCHAR(120)
AS
BEGIN
    -- Temporary table to store combined results
 truncate table CombinedResults
    -- Highest sale item by date
    INSERT INTO CombinedResults (Type, Description, TotalQty, TotalAmount)
    SELECT 'Highest Sale Item', description, COUNT(description) AS TotalQty, SUM(amount) AS Total
    FROM Sale_Items
    WHERE invdate >= @StartDate AND invdate <= @EndDate
    GROUP BY description
    HAVING COUNT(description) = (
        SELECT MAX(TotalQty)
        FROM (
            SELECT COUNT(description) AS TotalQty
            FROM Sale_Items
            WHERE invdate >= @StartDate AND invdate <= @EndDate
            GROUP BY description
        ) AS SubQueryAlias
    );

    -- No of sales and total amount
    INSERT INTO CombinedResults (Type, TotalSales, TotalAmount)
    SELECT 'Total Sales and Amount', COUNT(qty) AS TotalSales, SUM(amount) AS TotalAmount
    FROM Sale_Items
    WHERE invdate >= @StartDate AND invdate <= @EndDate;

    -- Lowest sale product by date
    INSERT INTO CombinedResults (Type, Description, TotalQty, TotalAmount)
    SELECT TOP 1 'Lowest Sale Product', description, COUNT(description) AS TotalQty, SUM(amount) AS Total
    FROM Sale_Items
    WHERE invdate >= @StartDate AND invdate <= @EndDate
    GROUP BY description
    HAVING COUNT(description) = (
        SELECT MIN(TotalQty)
        FROM (
            SELECT COUNT(description) AS TotalQty
            FROM Sale_Items
            WHERE invdate >= @StartDate AND invdate <= @EndDate
            GROUP BY description
        ) AS SubQueryAlias
    );

    -- Select combined results
 
	 
END;
	

	de, 

	create view HighSale as
	SELECT Description
    FROM CombinedResults where Type='Highest Sale Item';
 
 	create view LowSale as
	SELECT Description
    FROM CombinedResults where Type='Lowest Sale Product';

  
	create view TotalSale as
	select TotalSales,TotalAmount from CombinedResults where Type = 'Total Sales and Amount'



exec GetSalesInfo '2023-12-25 00:00:00','2023-12-25 23:59:59'









