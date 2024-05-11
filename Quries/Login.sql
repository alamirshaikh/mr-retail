


CREATE TABLE Login (
    UserID INT PRIMARY KEY identity(100,8),
    Username VARCHAR(50) NOT NULL,
    Password VARCHAR(50) NOT NULL,
    Email VARCHAR(100),
    LastLogin TIMESTAMP
);


CREATE PROCEDURE Access 
    @p_username VARCHAR(50),
    @p_password VARCHAR(50)
AS
BEGIN
    DECLARE @userCount INT;

    -- Check if username and password match
    SELECT @userCount = COUNT(*)
    FROM Login
    WHERE Username = @p_username AND Password = @p_password;

    -- If username and password match, return user information
    IF @userCount > 0
    BEGIN
        SELECT *
        FROM Login
        WHERE Username = @p_username AND Password = @p_password;
    END
    ELSE
    BEGIN
        SELECT 'Username and password do not match' AS Message;
    END
END;


SELECT * FROM Bank

 