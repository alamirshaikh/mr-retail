


create view GetInVoice AS 
select * from SInvoice 



select unit from Product_Item where ITEM_NAME = 'Bering 6201' 
select * from SInvoice
select * from Sale_Items

CREATE VIEW GetinBill AS
SELECT SI.id, SI.cust_Name,SI.invdate,SI.invoiceID,SI.other,SI.perdis,SI.sub_total,SI.TotalBill, 
       SItems.id AS SItems_id, SItems.sr_no,SItems.description,SItems.rate,SItems.qty,SItems.discount,SItems.amount FROM Sale_Items SItems 
JOIN SInvoice SI ON SItems.id = SI.id;


select * from  Customer

CREATE VIEW GetInvoices AS
SELECT 
   SI.*,
   SItems.*
FROM 
    SInvoice SI
JOIN 
    Sale_Items SItems ON SItems.id = SI.id
JOIN 
    Unit UI ON SItems.id = UI.id;

	 

SELECT * FROM GetInvoices WHERE invoiceID = 'INV23110677'


 
 