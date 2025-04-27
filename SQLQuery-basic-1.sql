CREATE DATABASE DotNetCourseDatabase
GO

USE DotNetCourseDatabase;
GO

-- Create the schema if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'TutorialAppSchema')
BEGIN
    EXEC('CREATE SCHEMA TutorialAppSchema');
END
GO

-- Drop the table if it exists
IF OBJECT_ID('TutorialAppSchema.Computer', 'U') IS NOT NULL
BEGIN
    DROP TABLE TutorialAppSchema.Computer;
END
GO

-- Create the table
CREATE TABLE TutorialAppSchema.Computer
(
    ComputerId INT IDENTITY(1, 1) PRIMARY KEY,
    Motherboard NVARCHAR(255),
    CPUCores INT,
    HasWife BIT,
    HasLTE DECIMAL(18, 4),
    ReleaseDate DATETIME,
    Price DECIMAL(18, 4),
    VideoCard NVARCHAR(50)
);
GO;

SELECT * FROM  TutorialAppSchema.Computer;

GO;

INSERT INTO TutorialAppSchema.Computer 
    (Motherboard, CPUCores, HasWife, HasLTE, ReleaseDate, Price, VideoCard)
VALUES 
    ('ASUS ROG STRIX Z590-E', 8, 1, 1.0000, '2023-01-15', 1499.9900, 'NVIDIA RTX 3080'),
    ('Gigabyte B550M DS3H', 6, 0, 0.0000, '2022-08-10', 799.9900, 'AMD Radeon RX 6700 XT'),
    ('MSI MPG X570 Gaming Plus', 12, 1, 0.0000, '2023-06-20', 1799.5000, 'NVIDIA RTX 4070'),
    ('ASRock B450M PRO4', 4, 0, 1.0000, '2021-11-05', 499.9900, 'Intel UHD Graphics 630'),
    ('Dell OEM Board', 2, 0, 1.0000, '2020-03-12', 299.9900, 'None');
