/*
    CollegeDB: создание базы и всех требуемых таблиц.
    Скрипт можно запускать повторно: таблицы создаются только при отсутствии.
*/
IF DB_ID(N'CollegeDB') IS NULL
BEGIN
    CREATE DATABASE CollegeDB;
END
GO

USE CollegeDB;
GO

IF OBJECT_ID(N'dbo.Groups', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Groups
    (
        GroupId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Groups PRIMARY KEY,
        Name NVARCHAR(50) NOT NULL,
        Course INT NOT NULL,
        CONSTRAINT UQ_Groups_Name UNIQUE (Name),
        CONSTRAINT CK_Groups_Course CHECK (Course BETWEEN 1 AND 6)
    );
END
GO

IF OBJECT_ID(N'dbo.Students', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Students
    (
        StudentId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Students PRIMARY KEY,
        FirstName NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        GroupId INT NULL,
        CONSTRAINT FK_Students_Groups FOREIGN KEY (GroupId)
            REFERENCES dbo.Groups(GroupId)
    );
END
GO

IF OBJECT_ID(N'dbo.Teachers', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Teachers
    (
        TeacherId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Teachers PRIMARY KEY,
        FirstName NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        Email NVARCHAR(254) NULL,
        CONSTRAINT UQ_Teachers_Email UNIQUE (Email)
    );
END
GO

IF OBJECT_ID(N'dbo.Subjects', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Subjects
    (
        SubjectId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Subjects PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        TeacherId INT NULL,
        CONSTRAINT UQ_Subjects_Name UNIQUE (Name),
        CONSTRAINT FK_Subjects_Teachers FOREIGN KEY (TeacherId)
            REFERENCES dbo.Teachers(TeacherId)
    );
END
GO

IF OBJECT_ID(N'dbo.Grades', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Grades
    (
        GradeId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Grades PRIMARY KEY,
        StudentId INT NOT NULL,
        SubjectId INT NOT NULL,
        GradeValue TINYINT NOT NULL,
        GradeDate DATE NOT NULL CONSTRAINT DF_Grades_GradeDate DEFAULT (CONVERT(date, GETDATE())),
        CONSTRAINT CK_Grades_GradeValue CHECK (GradeValue BETWEEN 1 AND 5),
        CONSTRAINT FK_Grades_Students FOREIGN KEY (StudentId)
            REFERENCES dbo.Students(StudentId),
        CONSTRAINT FK_Grades_Subjects FOREIGN KEY (SubjectId)
            REFERENCES dbo.Subjects(SubjectId)
    );
END
GO

-- Необязательные примеры данных. Раскомментируйте при необходимости.
-- INSERT INTO dbo.Groups (Name, Course) VALUES (N'ИС-21', 2);
-- INSERT INTO dbo.Teachers (FirstName, LastName, Email)
-- VALUES (N'Иван', N'Иванов', N'ivanov@example.com');
-- INSERT INTO dbo.Subjects (Name, TeacherId) VALUES (N'Базы данных', 1);
-- INSERT INTO dbo.Students (FirstName, LastName, GroupId) VALUES (N'Пётр', N'Петров', 1);
-- INSERT INTO dbo.Grades (StudentId, SubjectId, GradeValue) VALUES (1, 1, 5);
