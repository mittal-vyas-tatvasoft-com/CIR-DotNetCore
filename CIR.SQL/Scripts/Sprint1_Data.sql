--Add Data in Currencies Table---
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'AED', N'د.إ')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'AUD', N'$')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'BGN', N'лв')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'BHD', N'')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'BRL', N'R$')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'CAD', N' $')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'CHF', N'CHf')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'CNY', N'¥')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'CZK', N'')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'DKK', N'Kr.')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'EGP', N'e£')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'EUR', N'€')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'GBP', N'£')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'HKD', N' $')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'HRK', N'kn')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'HUF', N'Ft')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'IDR', N'Rp')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'ILS', N'₪')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'INR', N'₹')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'JOD', N'د.ا')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'JPY', N'¥')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'KRW', N'₩')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'KWD', N'د.ك')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'MAD', N'€')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'MXN', N' $')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'MYR', N'RM')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'NOK', N'kr')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'NZD', N'$')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'OMR', N'')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'PHP', N'₱')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'PLN', N'zł')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'QAR', N'ر.ق')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'RON', N'lei')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'RUB', N'₽')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'SAR', N'SR')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'SEK', N'kr')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'SGD', N'$')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'THB', N'฿')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'TRY', N'₺')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'USD', N'$')
INSERT [dbo].[Currencies] ([CodeName], [Symbol]) VALUES (N'ZAR', N'R')

--Add Data in CountryCode Table---
INSERT [dbo].[CountryCodes] ([Code], [CountryName]) VALUES (N'AF', N'Afghanistan')
INSERT [dbo].[CountryCodes] ([Code], [CountryName]) VALUES (N'AL', N'Albania')
INSERT [dbo].[CountryCodes] ([Code], [CountryName]) VALUES (N'DZ', N'Algeria')
INSERT [dbo].[CountryCodes] ([Code], [CountryName]) VALUES (N'AS', N'American Samoa')
INSERT [dbo].[CountryCodes] ([Code], [CountryName]) VALUES (N'AD', N'Andorra')
INSERT [dbo].[CountryCodes] ([Code], [CountryName]) VALUES (N'AO', N'Angola')
INSERT [dbo].[CountryCodes] ([Code], [CountryName]) VALUES (N'In', N'India')

--Add Data in Cultures Table---
INSERT [dbo].[Cultures] ([ParentId], [Name], [DisplayName], [Enabled], [NativeName]) VALUES (NULL, N'D', N'D', 1, N'd')
INSERT [dbo].[Cultures] ([ParentId], [Name], [DisplayName], [Enabled], [NativeName]) VALUES (NULL, N'A', N'A', 0, N'a')
INSERT [dbo].[Cultures] ([ParentId], [Name], [DisplayName], [Enabled], [NativeName]) VALUES (NULL, N'Catalan', N'Catalan', 0, N'ca')
INSERT [dbo].[Cultures] ([ParentId], [Name], [DisplayName], [Enabled], [NativeName]) VALUES (NULL, N'German', N'German', 0, N'ge')
INSERT [dbo].[Cultures] ([ParentId], [Name], [DisplayName], [Enabled], [NativeName]) VALUES (NULL, N'English', N'English', 0, N'en')
INSERT [dbo].[Cultures] ([ParentId], [Name], [DisplayName], [Enabled], [NativeName]) VALUES (NULL, N'Spanish', N'Spanish', 0, N'sp')
INSERT [dbo].[Cultures] ([ParentId], [Name], [DisplayName], [Enabled], [NativeName]) VALUES (NULL, N'Dutch', N'Dutch', 0, N'du')



--Add Data in Roles Table---
INSERT [dbo].[Roles] ([Name], [Description],[WrongLoginAttempts], [CreatedOn], [LastEditedOn], [AllPermissions]) VALUES (N'Super Admin', N'',10, GetDate(), NULL, 1)

--Add Data in Users Table---
INSERT [dbo].[Users] ([UserName], [Password], [Email], [SalutationLookupItemId], [FirstName], 
[LastName], [RoleId], [Enabled], [LastLogOn], [CreatedOn], [LastEditedOn], [ResetRequired], 
[DefaultAdminUser], [TimeZone], [CultureLCId], [EmployeeId], [PhoneNumber], [ScheduledActiveChange], 
[LoginAttempts], [CompanyName], [PortalThemeId]) 
VALUES 
(N'tatva@gmail.com', N'Tatva@1234', N'tatva@gmail.com', NULL, N'testFirst', N'testLast', 3, 0, 
GETDATE(), GETDATE(), NULL, 1, 1, N'IST', 1, N'EMP001', N'7979797979', GETDATE(), 10, N'TestCompany',
NULL)

--Add Data in GlobalConfigurationEmail Table --
INSERT INTO [dbo].[GlobalConfigurationEmails]([FieldTypeId],[CultureId],[Content],[Subject])VALUES(1,1,'Default1','Default1')
GO
INSERT INTO [dbo].[GlobalConfigurationEmails]([FieldTypeId],[CultureId],[Content],[Subject])VALUES(2,1,'Default2','Defaut2')
Go
INSERT INTO [dbo].[GlobalConfigurationEmails]([FieldTypeId],[CultureId],[Content],[Subject])VALUES(3,1,'Default3','Default3')
GO
INSERT INTO [dbo].[GlobalConfigurationEmails]([FieldTypeId],[CultureId],[Content],[Subject])VALUES(4,1,'Default4','Default4')
GO


---Add data in GlobalConfigurationStyle Table--
insert into GlobalConfigurationStyles values ('Logo (Background)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Header (Text)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Header - Hover (Text)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Logo Height','Logo height in pixels (numerical only)',0,'Textbox','integer','30',1);
insert into GlobalConfigurationStyles values ('Page Title (Background)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Page Title (Text)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Progress Bar - Complete (Foreground)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Progress Bar - Incomplete (Foreground)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Progress Step - Previous (Text)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Progress Step - Current (Text)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Progress Step - Next (Text)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Page (Background)','Applies to the whole site',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Body (Background)','Applies to the text areas under tab titles',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Borders/Separators (Background)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Descriptive Box (Background)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Descriptive Box (Text)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Descriptive Box Link (Text)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Tab Title - Current (Background)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Tab Title - Current (Text)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Tab Title - Previous/Next (Background)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Tab Title - Previous/Next (Text)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Button (Background)','No Hover/Not ready',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Button (Text)','No Hover/Not ready',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Button - Hover (Background)','Hover/Ready',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Button - Hover (Text)','Hover/Ready',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Secondary Button (Background)','No Hover/Not ready',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Secondary Button (Text)','No Hover/Not ready',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Secondary Button - Hover (Background)','Hover/Ready',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Secondary Button - Hover (Text)','Hover/Ready',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Tool Button (Background)','No Hover/Not ready',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Tool Button (Text)','No Hover/Not ready',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Tool Button - Hover (Background)','Hover/Ready',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Tool Button - Hover (Text)','Hover/Ready',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Alternate Item (Background)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Radial Button - Selected (Background)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Checkbox - Checked (Background)','',1,'Colorpicker','string','#32a852',1);
insert into GlobalConfigurationStyles values ('Checkbox - Checked (Foreground)','',1,'Colorpicker','string','#32a852',1);