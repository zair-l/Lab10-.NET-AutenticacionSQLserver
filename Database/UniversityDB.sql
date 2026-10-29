IF DB_ID(N'UniversityDB') IS NULL CREATE DATABASE UniversityDB;
GO
USE [UniversityDB];
GO
IF OBJECT_ID(N'dbo.Course', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Course] (
        [CourseID] INT IDENTITY (1, 1) NOT NULL,
        [Title] NVARCHAR (50) NULL,
        [Credits] INT NULL,
        PRIMARY KEY CLUSTERED ([CourseID] ASC)
    );
END;
GO
IF OBJECT_ID(N'dbo.Student', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Student] (
        [StudentID] INT IDENTITY (1, 1) NOT NULL,
        [LastName] NVARCHAR (50) NULL,
        [FirstName] NVARCHAR (50) NULL,
        [Email] NVARCHAR (50) NULL,
        [EnrollmentDate] DATETIME NULL,
        PRIMARY KEY CLUSTERED ([StudentID] ASC)
    );
END;
GO
IF OBJECT_ID(N'dbo.Enrollment', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Enrollment] (
        [EnrollmentID] INT IDENTITY (1, 1) NOT NULL,
        [Grade] DECIMAL (4, 2) NULL,
        [CourseID] INT NOT NULL,
        [StudentID] INT NOT NULL,
        PRIMARY KEY CLUSTERED ([EnrollmentID] ASC),
        CONSTRAINT [FK_dbo.Enrollment_dbo.Course_CourseID] FOREIGN KEY ([CourseID])
            REFERENCES [dbo].[Course] ([CourseID]) ON DELETE CASCADE,
        CONSTRAINT [FK_dbo.Enrollment_dbo.Student_StudentID] FOREIGN KEY ([StudentID])
            REFERENCES [dbo].[Student] ([StudentID]) ON DELETE CASCADE
    );
END;
GO
MERGE INTO dbo.Course AS Target
USING (VALUES (1, N'Economics', 3), (2, N'Literature', 3), (3, N'Chemistry', 4))
    AS Source (CourseID, Title, Credits)
ON Target.CourseID = Source.CourseID
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Title, Credits) VALUES (Source.Title, Source.Credits);
GO
MERGE INTO dbo.Student AS Target
USING (VALUES
    (1, N'Tibbetts', N'Donnie', CAST('2013-09-01' AS DATETIME)),
    (2, N'Guzman', N'Liza', CAST('2012-01-13' AS DATETIME)),
    (3, N'Catlett', N'Phil', CAST('2011-09-03' AS DATETIME))
) AS Source (StudentID, LastName, FirstName, EnrollmentDate)
ON Target.StudentID = Source.StudentID
WHEN NOT MATCHED BY TARGET THEN
    INSERT (LastName, FirstName, EnrollmentDate)
    VALUES (Source.LastName, Source.FirstName, Source.EnrollmentDate);
GO
MERGE INTO dbo.Enrollment AS Target
USING (VALUES
    (1, CAST(2.00 AS DECIMAL(4,2)), 1, 1),
    (2, CAST(3.50 AS DECIMAL(4,2)), 1, 2),
    (3, CAST(4.00 AS DECIMAL(4,2)), 2, 3),
    (4, CAST(1.80 AS DECIMAL(4,2)), 2, 1),
    (5, CAST(3.20 AS DECIMAL(4,2)), 3, 1),
    (6, CAST(4.00 AS DECIMAL(4,2)), 3, 2)
) AS Source (EnrollmentID, Grade, CourseID, StudentID)
ON Target.EnrollmentID = Source.EnrollmentID
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Grade, CourseID, StudentID)
    VALUES (Source.Grade, Source.CourseID, Source.StudentID);
GO
