use drsale

ALTER PROCEDURE [dbo].[InsOwner]
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
    @panInfo VARCHAR(50),
	@email varchar(120),
    @bankName VARCHAR(100),
    @accountName VARCHAR(100),
    @accountNumber VARCHAR(20),
    @ifsc VARCHAR(15),
	@licence varchar(255),
	@logo varchar(max)
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
            BusinessAddress = @businessAddress,
            email = @email,
			logo = @logo

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
        INSERT INTO OwnerInformation (OwnerName, ShopName, ShopPhone, ShopMobile, City, State, BusinessAddress,Licence,email,logo)
        VALUES (@ownerName, @shopName, @shopPhone, @shopMobile, @city, @state, @businessAddress,@licence,@email,@logo);

   INSERT INTO Tax (GSTIN, TypeB, GST, SGST, CGST, HSNCode)
    VALUES (@gstin, @typeB, @gst, @sgst, @cgst, @hsncode);
    
	--for bank
	 INSERT INTO Bank (PANInfo, BankName, AccountName, AccountNumber, IFSC)
    VALUES (@panInfo, @bankName, @accountName, @accountNumber, @ifsc);
   
   END

  
     

END;
select * from OwnerInformation
 
alter table OwnerInformation add logo varchar(max)