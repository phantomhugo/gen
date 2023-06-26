CREATE DATABASE STGenetics
GO
USE STGenetics
GO
CREATE TABLE Animal(AnimalId UNIQUEIDENTIFIER NOT NULL default NEWID(),Name VARCHAR (100) NOT NULL ,Breed VARCHAR (100) NOT NULL ,BirthDate DATE  NOT NULL ,Sex BIT  NOT NULL ,Price DECIMAL(18,2)  NOT NULL ,Status BIT  NOT NULL , PRIMARY KEY(AnimalId))
