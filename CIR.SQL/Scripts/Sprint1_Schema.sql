--Roles Table to Add New Column
Alter table Roles Add WrongLoginAttempts bigint Not NULL;

--GlobalConfigurationFonts Table to Add New Column
ALTER TABLE [dbo].[GlobalConfigurationFonts]
ADD [FontFileName] nvarchar(MAX) NOT NULL;

--GlobalConfigurationWeekends Table to Add New Column
Alter Table [dbo].[GlobalConfigurationWeekends]
ADD [Enabled] bit NOT NULL;
