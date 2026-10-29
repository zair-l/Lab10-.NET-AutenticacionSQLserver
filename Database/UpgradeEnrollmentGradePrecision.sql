USE [UniversityDB];
GO

-- Grade is entered on a 0–20 scale, so DECIMAL(3,2) is too small for values >= 10.
ALTER TABLE [dbo].[Enrollment]
    ALTER COLUMN [Grade] DECIMAL(4, 2) NULL;
GO

SELECT
    @@SERVERNAME AS ServerName,
    DB_NAME() AS DatabaseName,
    NUMERIC_PRECISION AS GradePrecision,
    NUMERIC_SCALE AS GradeScale
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = N'dbo'
  AND TABLE_NAME = N'Enrollment'
  AND COLUMN_NAME = N'Grade';
GO
