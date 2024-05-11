

create table Parties
(
  ID int primary key identity(6580,1),
  pname varchar(120),
  company varchar(120),
  partiphone varchar(120),
  partimobile varchar(120),
  paninformation nvarchar(120),
  city  varchar(120),
  state varchar(120),
  address varchar(120)
);


 
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
  INSERT INTO Parties (pname, company, partiphone, partimobile, paninformation, city, state, address,BankName,ACName,ACNumber,IFCCODE)
  VALUES (@pname, @company, @partiphone, @partimobile, @paninformation, @city, @state, @address,@BankName,@ACName,@ACNumber,@IFCCODE);
END;
select * from Parties

 