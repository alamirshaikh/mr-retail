


select * from Transactions


select * from Bill
select * from Payment

select * from Ledger_Supplier
create table SupplierTransactions
(
    ID int not null primary key identity(152,1),
	SupplierID int,
	Date datetime,
	SupplierC varchar(120),
	TransactionN varchar(120),
	Description varchar(120),
	Debit varchar(120),
	Credit varchar(120)

);