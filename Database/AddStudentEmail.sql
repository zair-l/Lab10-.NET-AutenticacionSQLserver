/* Paso final del laboratorio: agrega Email a Student después de crear la base inicial. */
USE UniversityDB;
GO
IF COL_LENGTH(N'dbo.Student', N'Email') IS NULL
    ALTER TABLE dbo.Student ADD Email nvarchar(50) NULL;
GO

