





use drsale
select * from Customer


create table Mechanic
(

  ID INT NOT NULL PRIMARY KEY IDENTITY(80,90),
  Mechanic_Name varchar(120),
  Item_Name varchar(120),
  Item_Cost decimal,
  Item_price decimal,
  Date datetime

);


alter PROCEDURE AddMechanic
    @Mechanic_Name varchar(120),
    @Item_Name varchar(120),
    @Item_Cost decimal,
    @Item_price decimal,
	@Date datetime
AS
BEGIN
    INSERT INTO Mechanic (Mechanic_Name, Item_Name, Item_Cost, Item_price,Date)
    VALUES (@Mechanic_Name, @Item_Name, @Item_Cost, @Item_price,@Date);
END;



CREATE PROCEDURE GetMechanic

AS
BEGIN
    SELECT DISTINCT *
    FROM Mechanic;
END;



create procedure GetMacItem
@name varchar(120)
as
begin

select Mechanic_Name,Item_Name,Item_price from Mechanic where Mechanic_Name = @name
end




create procedure GetItemMac
@name varchar(120)
as
begin

select Item_Name,Item_price,Item_Cost from Mechanic where Item_Name = @name
end




select * from Product_Item