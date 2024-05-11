use drsale


create table Accounts
(

  AccountID int not null primary key identity(1120,1),
  AccountName varchar(120),
  Balance DECIMAL(15,2),
  Type nvarchar(20)
);

Create table Transactions
(
  TransactionID INT PRIMARY KEY IDENTITY(62566,1),
  AccountID INT,
  Amount DECIMAL(15,2),
  Type VARCHAR(10),
  Date_ DATE,
  Description VARCHAR(120),
  REFRANCE_ID int
  );

drop table Transactions

 
 

 