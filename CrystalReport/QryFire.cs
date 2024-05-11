using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport
{
   public class QryFire
    {
        public static List<string> qry = new List<string>();
        public static List<string> qry1 = new List<string>();
        public static string Table;
        private static string st;

        public static void Call()
        {

            Table = @"
use drsale
 create table Product_Item
(
   ID INT NOT NULL PRIMARY KEY IDENTITY(5000, 1),
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


            create table Customer
            (
                id int not null primary key identity(1000, 1),
                cust_name varchar(120),
                cust_phone varchar(110),
                cust_addres varchar(140),
                cust_service varchar(120),
                cust_date datetime2

            );


            alter table Customer add pcity  varchar(120)
alter table Customer add pstate varchar(120)




create table Bill
(
id int not null primary key identity(7000, 1),
BillID varchar(100),
partiname varchar(120),
items nvarchar(120), --itemstable invoice id
sub_total decimal,
perdis decimal,
discount decimal,
other decimal,
TotalBill decimal,
billdate datetime

);


            create table Accounts
            (

              AccountID int not null primary key identity(1120, 1),
              AccountName varchar(120),
              Balance DECIMAL(15, 2),
              Type nvarchar(20)
            );

            Create table Transactions
            (
              TransactionID INT PRIMARY KEY IDENTITY(62566, 1),
              AccountID INT,
              Amount DECIMAL(15, 2),
              Type VARCHAR(10),
              Date_ DATE,
              Description VARCHAR(120),
              REFRANCE_ID int
              );


            create table Payment
          (
               ID int not null primary key identity(10000, 15),
               name_off VARCHAR(120),
               TransactionNo varchar(120),
               TranDate datetime,
               PaymentMode varchar(100),
               Amount decimal

          );

            CREATE TABLE Ledger_Supplier(
                SupplierID INT PRIMARY KEY identity(120, 1),
                SupplierName VARCHAR(100) NOT NULL,
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



            CREATE TABLE Login(
                UserID INT PRIMARY KEY identity(100, 8),
                Username VARCHAR(50) NOT NULL,
                Password VARCHAR(50) NOT NULL,
                Email VARCHAR(100),
                LastLogin TIMESTAMP
            );



            CREATE TABLE OwnerInformation(
                OwnerID int not null primary key identity(1980, 1),
                OwnerName VARCHAR(255),
                ShopName VARCHAR(255),
                ShopPhone VARCHAR(20),
                ShopMobile VARCHAR(20),
                City VARCHAR(100),
                State VARCHAR(100),
                BusinessAddress VARCHAR(255)
            );



            alter table OwnerInformation add Licence varchar(255)		

CREATE TABLE Tax(
    TAXID int not null primary key identity(564, 9),
    GSTIN VARCHAR(15),
    TypeB VARCHAR(50),
    GST DECIMAL(10, 2),
    SGST DECIMAL(5, 2),
    CGST DECIMAL(5, 2),
    HSNCode VARCHAR(15)
);


            CREATE TABLE Bank(
                BANKID int not null primary key identity(1800, 1),
                PANInfo VARCHAR(20),
                BankName VARCHAR(100),
                AccountName VARCHAR(100),
                AccountNumber VARCHAR(20),
                IFSC varchar(15)
            );


            create table Parties
            (
              ID int primary key identity(6580, 1),
              pname varchar(120),
              company varchar(120),
              partiphone varchar(120),
              partimobile varchar(120),
              paninformation nvarchar(120),
              city  varchar(120),
              state varchar(120),
              address varchar(120)
            );


            create table SInvoice
            (
            id int not null primary key identity(7000, 1),
            invoiceID varchar(100),
            cust_Name varchar(120),
            items nvarchar(120), --itemstable invoice id
            sub_total decimal,
            perdis decimal,
            discount decimal,
            other decimal,
            TotalBill decimal,
            invdate datetime

            );

            ALTER table SInvoice add totalQty int

            create table Sale_Items
            (
            id int not null primary key identity(8000, 1),
sr_no int,
description nvarchar(120),
qty int,
rate decimal,
discount decimal,
amount decimal,
Invoice nvarchar(120)
);




            create table purches_Items
            (
            id int not null primary key identity(8000, 1),
            sr_no int,
            description nvarchar(120),
            qty int,
            rate decimal,
            discount decimal,
            amount decimal,
            Bill nvarchar(120)
            );

            CREATE TABLE[dbo].Unit
           (
           
               [Id] INT NOT NULL PRIMARY KEY identity(1,1), 
    [Unit] VARCHAR(50) NULL, 
    [DateU] DATETIME NOT NULL
)
 
alter table Customer add Place varchar(120)
";
             
  st = @"
USE drsale;

CREATE PROCEDURE SelectBill
    @id int
AS
BEGIN
    IF @ID = 0
    BEGIN
        SELECT * FROM Bill
    END
    ELSE
    BEGIN
        SELECT * FROM Bill WHERE id = @id
    END
END;

CREATE PROCEDURE insertitems
(
    @sr_no int,
    @description VARCHAR(120),
    @qty int,
    @rate DECIMAL,
    @discount DECIMAL,
    @amount DECIMAL,
    @Bill varchar(120)
)
AS
BEGIN
    INSERT INTO purches_Items (sr_no, description, qty, rate, discount, amount, Bill)
    VALUES (@sr_no, @description, @qty, @rate, @discount, @amount, @Bill)
END;

CREATE PROCEDURE sp_Customer
    @cust_name varchar(120),
    @cust_phone varchar(110),
    @cust_address varchar(140),
    @cust_service varchar(120),
    @cust_date datetime2,
    @pcity varchar(120),
    @pstate varchar(120)
AS
BEGIN
    INSERT INTO Customer (cust_name, cust_phone, cust_addres, cust_service, cust_date, pcity, pstate)
    VALUES (@cust_name, @cust_phone, @cust_address, @cust_service, @cust_date, @pcity, @pstate)
END;

CREATE PROCEDURE spSaleInsert
(
    @invoiceID VARCHAR(100),
    @cust_Name VARCHAR(120),
    @items nvarchar(120),
    @sub_total DECIMAL,
    @perdis DECIMAL,
    @discount DECIMAL,
    @other DECIMAL,
    @TotalBill DECIMAL,
    @invdate datetime,
    @place varchar
)
AS
BEGIN
    INSERT INTO SInvoice (invoiceID, cust_Name, items, sub_total, perdis, discount, other, TotalBill, invdate, place)
    VALUES (@invoiceID, @cust_Name, @items, @sub_total, @perdis, @discount, @other, @TotalBill, @invdate, @place)
END;

CREATE PROCEDURE UpdateStock
    @BARCODE varchar(120),
    @UNIT varchar(120),
    @STOCK int,
    @ID int
AS
BEGIN
    UPDATE Product_Item SET BARCODE = @BARCODE, UNIT = @UNIT, STOCK = @STOCK WHERE ID = @ID
END;

CREATE PROCEDURE sp_CustomerGet
    @custID int
AS
BEGIN
    IF @custID = 1
    BEGIN
        SELECT * FROM Customer
    END
    ELSE 
    BEGIN
        SELECT * FROM Customer WHERE ID = @custID
    END
END;

CREATE PROCEDURE sp_getCustomer
    @cust_name varchar(120)
AS
BEGIN
    IF @cust_name = ''
    BEGIN
        RETURN
    END
    ELSE
    BEGIN
        SELECT cust_name FROM Customer WHERE cust_name LIKE '%' + @cust_name + '%'
    END
END;

CREATE PROCEDURE InsOwner
    @ownerName VARCHAR(255),
    @shopName VARCHAR(255),
    @shopPhone VARCHAR(20),
    @shopMobile VARCHAR(20),
    @city VARCHAR(100),
    @state VARCHAR(100),
    @businessAddress VARCHAR(255),
    @gstin VARCHAR(15),
    @typeB VARCHAR(50),
    @gst DECIMAL(10,2),
    @sgst DECIMAL(5,2),
    @cgst DECIMAL(5,2),
    @hsncode VARCHAR(15),
    @panInfo VARCHAR(20),
    @bankName VARCHAR(100),
    @accountName VARCHAR(100),
    @accountNumber VARCHAR(20),
    @ifsc VARCHAR(15),
    @licence varchar(255)
AS
BEGIN
    -- Your stored procedure logic here
END;

ALTER TABLE Parties
ADD BankName VARCHAR(120),
    ACName VARCHAR(120),
    ACNumber VARCHAR(120),
    IFCCODE VARCHAR(120);

CREATE PROCEDURE InsertIntoParties
    @pname VARCHAR(120),
    @company VARCHAR(120),
    @partiphone VARCHAR(120),
    @partimobile VARCHAR(120),
    @paninformation NVARCHAR(120),
    @city VARCHAR(120),
    @state VARCHAR(120),
    @address VARCHAR(120),
    @BankName VARCHAR(120),
    @ACName VARCHAR(120),
    @ACNumber VARCHAR(120),
    @IFCCODE varchar(120)
AS
BEGIN
    INSERT INTO Parties (pname, company, partiphone, partimobile, paninformation, city, state, address, BankName, ACName, ACNumber, IFCCODE)
    VALUES (@pname, @company, @partiphone, @partimobile, @paninformation, @city, @state, @address, @BankName, @ACName, @ACNumber, @IFCCODE)
END; 
";



            qry1 = new List<string>
{
    @" use drsale
CREATE TRIGGER trgAfterInsertPayment
    ON Payment
    AFTER INSERT
    AS
    BEGIN
        UPDATE Accounts 
        SET Balance = Balance - (SELECT SUM(i.Amount) FROM inserted i)
        WHERE AccountID = 1120;

        INSERT INTO Ledger_Supplier (SupplierName, Address, City, State, Phone, Description, Debit, Date)
        SELECT 
            p.company AS SupplierName,
            p.address AS Address,
            p.city AS City,
            p.state AS State,
            p.partimobile AS Phone,
            'Payment' AS Description,
            Sum(i.Amount) AS Debit,
            GETDATE() AS Date
        FROM inserted i
        JOIN Parties p ON i.name_off = p.company
        GROUP BY 
            p.company,
            p.address,
            p.city,
            p.state,
            p.partimobile;
    END;
    ",
    @"use drsale
CREATE TRIGGER trgAfterInsertBill
    ON Bill
    AFTER INSERT
    AS
    BEGIN
        SET NOCOUNT ON;

        DECLARE @billDesc VARCHAR(120);

        -- Variables to hold information
        DECLARE @city VARCHAR(120),
                @state VARCHAR(120),
                @address VARCHAR(120),
                @total DECIMAL(15, 2),
                @name VARCHAR(120),
                @billid varchar(120));

        INSERT INTO Ledger_Supplier (SupplierName, Address, City, State, Phone, Description, Credit, Date)
        SELECT 
            p.company AS SupplierName,
            p.address AS Address,
            p.city AS City,
            p.state AS State,
            p.partimobile AS Phone,
            'Purchase' AS Description,
            SUM(i.TotalBill) AS Credit,
            GETDATE() AS Date
        FROM inserted i
        JOIN Parties p ON i.partiname = p.company
        GROUP BY 
            p.company,
            p.address,
            p.city,
            p.state,
            p.partimobile;

        -- Inserting into Transactions table
        INSERT INTO Transactions (AccountID, Amount, Type, Date_, Description, REFRANCE_ID)
        SELECT 
            1 AS AccountID, 
            i.TotalBill AS Amount, 
            'Saving' AS Type, 
            i.billdate AS Date_, 
            CONCAT('Credit Purchase ', i.partiname) AS Description,
            i.id AS REFRANCE_ID
        FROM inserted i;

        --updating the stock
        DECLARE @item_name VARCHAR(100);
        DECLARE @stock INT;

        select @billid = i.items from INSERTED i;

        DECLARE item_cursor CURSOR FOR
        SELECT description, qty
        FROM Purches_Items where Bill = @billid;

        OPEN item_cursor;

        FETCH NEXT FROM item_cursor INTO @item_name, @stock;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            -- Perform operations here for each row
            -- For example, updating another table:
            UPDATE Product_Item
            SET STOCK += @stock
            WHERE ITEM_NAME = @item_name;

            FETCH NEXT FROM item_cursor INTO @item_name, @stock;
        END

        CLOSE item_cursor;
        DEALLOCATE item_cursor;

        -- Updating Accounts table
        UPDATE Accounts
        SET Balance = Balance - (SELECT SUM(TotalBill) FROM inserted)
        WHERE AccountID = 1120; -- Replace with the appropriate AccountID
    END;
    ",
    @"use drsale
   EXEC sp_rename 'Transactions.[Date]', 'Date_', 'COLUMN';
",
    @"
 
 CREATE TRIGGER trgAfterInsertSInvoice
ON SInvoice
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @billDesc VARCHAR(120);
	declare @totalqt int;
    declare @place varchar(120);
	INSERT INTO Transactions (AccountID, Amount, Type, Date_, Description,REFRANCE_ID)
     
	SELECT 
        1 AS AccountID, 
        i.TotalBill AS Amount, 
        'Saving' AS Type, 
        i.invdate AS Date_, 
        CONCAT('Bill For', i.cust_Name) AS Description,
		(select id from Customer where cust_name = i.cust_name) AS REFRANCE_ID

		
		 
    FROM inserted i;

 SELECT @place = (select place from inserted)


	select @totalqt = Count(qty) from Sale_Items where Invoice = (select invoiceID from inserted);

	select * from SInvoice
	
	UPDATE Customer SET place = @place where cust_name =  (select cust_name from inserted);

	UPDATE SInvoice
	SET totalQty =  @totalqt where invoiceID = (select invoiceID from inserted);

	update Product_Item 
	set STOCK = STOCK - @totalqt

    UPDATE Accounts
    SET Balance = Balance + (SELECT SUM(TotalBill) FROM inserted)
    WHERE AccountID = 1120; -- Replace with the appropriate AccountID



	 
END;

	"
    // Add other SQL script blocks here
};
        }
        public static void ExecuteQueries()
        {

            Call();
            SqlConnection connection = new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=master;Integrated Security=True;");





            try
            {
                connection.Open();
                connection.Execute("create database drsale");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error executing query: "+ ex.Message);
                // Handle or log the exception as needed
            }





            SqlConnection connection1 = new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=master;Integrated Security=True;");


            try
            {
                    connection1.Open();
                    connection1.Execute(Table);
                    connection1.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error executing query: "+ ex.Message);
                    // Handle or log the exception as needed
                }

            SqlConnection connection3 = new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=master;Integrated Security=True;");



            
                try
                {
                    connection3.Open();
                    connection3.Execute(st);
                    connection3.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error executing query: " + st + "\nError: " + ex.Message);
                    // Handle or log the exception as needed
                }
            



            SqlConnection connection2 = new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=master;Integrated Security=True;");


            foreach (string query in qry)
                {
                    try
                    {
                        connection2.Open();
                        connection2.Execute(query);
                    connection2.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error executing query: " + query + "\nError: " + ex.Message);
                        // Handle or log the exception as needed
                    }
                }
          


        }
            public QryFire()
        {



            // Add all your SQL queries as strings in this list





        }

        
    }
}
