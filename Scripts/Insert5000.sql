USE STGenetics
GO
DECLARE @i AS INTEGER
SET @i = 0
WHILE @i < 5000
BEGIN
	INSERT INTO Animal (Name,Breed,BirthDate,Sex,Price,Status) VALUES (CONVERT(varchar(255), NEWID()),CONVERT(varchar(255), NEWID()),cast(cast(RAND()*100000 as int) as datetime),0,RAND(),1)
	SET @i=@i+1
END
GO