USE STGenetics
GO
INSERT INTO Animal (Name,Breed,BirthDate,Sex,Price,Status) VALUES ('Tom','Random','2012-02-02',1,100.20,1)
GO
UPDATE Animal SET Name = 'Sam' where AnimalId=(SELECT TOP 1 AnimalId from Animal)
GO
DELETE Animal where AnimalId=(SELECT TOP 1 AnimalId from Animal)
GO
