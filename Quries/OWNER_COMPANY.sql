use drsale

CREATE TABLE OwnerInformation (
    OwnerID int not null primary key identity(1980,1),
    OwnerName VARCHAR(255),
    ShopName VARCHAR(255),
    ShopPhone VARCHAR(20),
    ShopMobile VARCHAR(20),
    City VARCHAR(100),
    State VARCHAR(100),
    BusinessAddress VARCHAR(255)
);

 

alter table OwnerInformation add Licence varchar(255)		

CREATE TABLE Tax (
    TAXID int not null primary key identity(564,9),
    GSTIN VARCHAR(15),
    TypeB VARCHAR(50),
    GST DECIMAL(10,2),
    SGST DECIMAL(5,2),
    CGST DECIMAL(5,2),
    HSNCode VARCHAR(15)
);
  

CREATE TABLE Bank (
    BANKID int not null primary key identity(1800,1),
    PANInfo VARCHAR(20),
    BankName VARCHAR(100),
    AccountName VARCHAR(100),
    AccountNumber VARCHAR(20),
    IFSC varchar(15)
);
 

---create a stre procedure for 
 

 SELECT * FROM OwnerInformation

 

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

    -- Check if the record exists in OwnerInformation table
    IF   (SELECT COUNT(OwnerID) FROM OwnerInformation) > 0
    BEGIN
        -- Update the existing record in OwnerInformation
        UPDATE OwnerInformation
        SET ShopName = @shopName,
            ShopPhone = @shopPhone,
            ShopMobile = @shopMobile,
            City = @city,
            State = @state,
			OwnerName = @ownerName,
			Licence = @licence,
            BusinessAddress = @businessAddress
        WHERE OwnerID = (select top 1 OwnerID from OwnerInformation);

		  UPDATE Tax
        SET TypeB = @typeB,
            GST = @gst,
            SGST = @sgst,
            CGST = @cgst,
            HSNCode = @hsncode,
			GSTIN = @gstin
        WHERE TAXID = (select top 1 TAXID from Tax);


		      UPDATE Bank
        SET BankName = @bankName,
            AccountName = @accountName,
            AccountNumber = @accountNumber,
            IFSC = @ifsc,
			PANInfo = @panInfo
        WHERE   BANKID = (select top 1 BANKID from Bank)

    END
    ELSE
    BEGIN
        -- Insert into OwnerInformation if the record does not exist
        INSERT INTO OwnerInformation (OwnerName, ShopName, ShopPhone, ShopMobile, City, State, BusinessAddress,Licence)
        VALUES (@ownerName, @shopName, @shopPhone, @shopMobile, @city, @state, @businessAddress,@licence);

   INSERT INTO Tax (GSTIN, TypeB, GST, SGST, CGST, HSNCode)
    VALUES (@gstin, @typeB, @gst, @sgst, @cgst, @hsncode);
    
	--for bank
	 INSERT INTO Bank (PANInfo, BankName, AccountName, AccountNumber, IFSC)
    VALUES (@panInfo, @bankName, @accountName, @accountNumber, @ifsc);
   
   END

  
     

END;


SELECT * FROM Tax
 

EXEC sp_rename 'TAX.TAXID', 'OwnerID', 'COLUMN';

select *from Bank

 

-- Check the join between OwnerInformation and Bank
 

create procedure upowner
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
as
begin
UPDATE OwnerInformation 
    SET OwnerName = @ownerName, 
        ShopName = @shopName, 
        ShopPhone = @shopPhone, 
        ShopMobile = @shopMobile, 
        City = @city, 
        State = @state, 
        BusinessAddress = @businessAddress
    WHERE ownerID > 0;


	UPDATE Tax 
    SET GSTIN = gstin, 
        TypeB = typeB, 
        GST = gst, 
        SGST = sgst, 
        CGST = cgst, 
        HSNCode = hsncode
    WHERE TAXID  > 0;

	 UPDATE Bank 
    SET PANInfo = panInfo, 
        BankName = bankName, 
        AccountName = accountName, 
        AccountNumber = accountNumber, 
        IFSC = ifsc
    WHERE BANKID > 0;


end
 
