

SELECT SUM(CAST(Credit AS DECIMAL(18, 2))) - SUM(CAST(Debit AS DECIMAL(18, 2))) AS CreditMinusDebit
FROM Ledger_Supplier where SupplierName =  'Sam ltd'  AND Date >= '2023-12-27 00:00:00' AND  date <= '2023-12-27 23:59:59'



SELECT SUM(CAST(Credit AS DECIMAL(18, 2))) AS CreditMinusDebit
FROM Ledger_Supplier where SupplierName =  'Sam ltd'  AND Date >= '2023-12-27 00:00:00' AND  date <= '2023-12-27 23:59:59'

SELECT SUM(CAST(Debit AS DECIMAL(18, 2))) AS Debit
FROM Ledger_Supplier where SupplierName =  'Sam ltd'  AND Date >= '2023-12-27 00:00:00' AND  date <= '2023-12-27 23:59:59'




SELECT * FROM Ledger_Supplier where SupplierName =  'Sam ltd'  AND Date >= '2023-12-27 00:00:00' AND  date <= '2023-12-27 23:59:59'
select * from Ledger_Supplier