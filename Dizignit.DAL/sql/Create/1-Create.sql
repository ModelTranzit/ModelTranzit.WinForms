CREATE DATABASE "ModelTranzit"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'en-US'
    LC_CTYPE = 'en-US'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

-----------------------------------------

CREATE TABLE RequestLog (
    RequestID VarChar(36) PRIMARY KEY,
    RequestDateUTC DATETIME DEFAULT GETUTCDATE(), -- Automatically set to current UTC time
    ErrorCode INT
);

-----------------------------------------
-- Drop Table ImageLog

Create TABLE ImageLog (
    ID INT IDENTITY(1,1) PRIMARY KEY, -- Auto-incrementing ID
	RequestID VarChar(36),
	Foreign key (RequestID) REFERENCES RequestLog(RequestID), 
    UTCInsert DATETIME DEFAULT GETUTCDATE(), -- Automatically set to current UTC time
	FileType INT, 
	RawImage VARBINARY(MAX),
    UTCTransStartUTC DATETIME -- Timestamp for transaction start
);

-----------------------------------------

CREATE TABLE ErrorLog (
    ID INT IDENTITY(1,1) PRIMARY KEY, -- Auto-incrementing ID
	RequestID VarChar(36),
	Foreign key (RequestID) REFERENCES RequestLog(RequestID), 
    ErrorMessage NVARCHAR(MAX),
	StackTrace NVARCHAR(MAX),
    ErrorTimeUTC DATETIME DEFAULT GETUTCDATE()
);

-----------------------------------------

CREATE PROCEDURE ImageLogInsert
    @UTCTransStartUTC DATETIME,
    @RawImage VARBINARY(MAX),
	@FileType INT,
	@RequestID VarChar(36)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO ImageLog (UTCInsert, UTCTransStartUTC, RawImage, RequestID)
    VALUES (GETUTCDATE(), @UTCTransStartUTC, @RawImage, @RequestID);
END;

-----------------------------------------

Create PROCEDURE ErrorLogInsert
    @ErrorMessage NVARCHAR(MAX),
    @StackTrace NVARCHAR(MAX),
    @RequestID VarChar(36)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO ErrorLog (RequestID, ErrorMessage, StackTrace)
    VALUES (@RequestID, @ErrorMessage, @StackTrace);
END;


-----------------------------------------

CREATE PROCEDURE RequestLogInsert
	@RequestID VarChar(36),
	@ErrorCode INT 
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO RequestLog (RequestID, ErrorCode)
    VALUES (@RequestID, @ErrorCode);
END;

-----------------------------------------