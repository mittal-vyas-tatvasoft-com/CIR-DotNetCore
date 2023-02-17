---------------------------------------------------------------------- Global Configuration SP Start -----------------------------------------------------------------------------

---- #Global Configuration Currencies SP Start

-- Check currency exists or not SP start

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCurrencyExists]
(
	@CodeName nvarchar(3)
)
AS
BEGIN
	Declare @Sql nvarchar(max);
	Declare @Count int;	
	Declare @Result nvarchar(max);

	IF(@CodeName is not null AND @CodeName <> '')
	BEGIN
		Set @Sql = 'select @Count = Count(Id) from Currencies where CodeName = '''+@CodeName+'''';
		exec sp_executeSql @Sql,N'@Count int out',@Count out		
	END
	set @Result = case when @Count > 0 then 'true' else 'false' end;
	select @Result	
END
GO

-- Check currency exists or not SP end

-- Add new currency SP start

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAddNewCurrency](
                  @CodeName nvarchar(3),  
                  @Symbol nvarchar(3)  )    
AS  
BEGIN  
	BEGIN
		 INSERT INTO Currencies(CodeName, Symbol)  
			   VALUES (@CodeName, @Symbol); 
	END;
END; 
GO

-- Add new currency Sp end

-- Create or Update global configuration currency SP start

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCreateOrUpdateGlobalConfigurationCurrencies](  
                  @CountryId bigint,  
                  @CurrencyId bigint,  
                  @Enabled bit)  
  
AS  
BEGIN  
	IF(exists(SELECT 1 FROM Currencies WHERE Id = @CurrencyId) and exists(SELECT 1 FROM CountryCodes WHERE Id = @CountryId))
	BEGIN
		 IF (not exists(SELECT 1 FROM GlobalConfigurationCurrencies WHERE CountryId = @CountryId and CurrencyId = @CurrencyId))
			BEGIN  
			   INSERT INTO GlobalConfigurationCurrencies(CountryId,CurrencyId,Enabled)  
			   VALUES (@CountryId,@CurrencyId,@Enabled);  
			END;  

		ELSE 
			BEGIN
				UPDATE GlobalConfigurationCurrencies   
				SET CountryId = @CountryId,  
				CurrencyId = @CurrencyId,  
				Enabled = @Enabled  
				WHERE CountryId = @CountryId and CurrencyId = @CurrencyId;  
			END;
	END;
END; 
GO

-- Create or Update global configuration currency SP end

-- get lobal configuration currency countryId wise SP start

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetGlobalConfigurationCurrenciesByCountryId]
(
	@countryId int
)

AS
	BEGIN

		SELECT CountryId, CurrencyId, Enabled FROM GlobalConfigurationCurrencies WHERE CountryId=@countryId;

	END;
GO

-- get lobal configuration currency countryId wise SP end

-- get all currencies SP start

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetAllCurrencies]

AS
BEGIN
	
 SELECT Id, CodeName, Symbol FROM Currencies ORDER BY CodeName

END
GO

-- get all currencies SP end

---- #Global Configuration Currencies SP End


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetGlobalConfigurationCutOffTimesByCountryId]( @CountryId bigint)

AS
BEGIN
	SELECT TOP 1 * FROM GlobalConfigurationCutOffTimes where CountryId = @CountryId
END
GO
-- spGetGlobalConfigurationCutOffTimesByCountryId End

-- spCreateOrUpdateGlobalConfigurationCutOffTimes Start


--#Global Configuration Fields SP Start

/****** Object:  StoredProcedure [dbo].[spCreateOrUpdateGlobalConfigurationFields]    Script Date: 15-02-2023 15:25:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCreateOrUpdateGlobalConfigurationFields]
@Id as bigint,
@FieldTypeId as bigint,
@Enabled as bit,
@Required as bit
AS
BEGIN

 IF (not exists(SELECT 1 FROM GlobalConfigurationFields WHERE Id = @Id))
			BEGIN  
			   INSERT INTO GlobalConfigurationFields(FieldTypeId,Enabled,Required)  
			   VALUES (@FieldTypeId,@Enabled,@Required)
			END

		ELSE 
			BEGIN
				UPDATE GlobalConfigurationFields   
				SET FieldTypeId = @FieldTypeId,  
				Enabled = @Enabled,  
				Required = @Required  
				WHERE Id = @Id
			END;

END

GO

/****** Object:  StoredProcedure [dbo].[spGetGlobalConfigurationFields]    Script Date: 15-02-2023 15:26:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetGlobalConfigurationFields]
AS
BEGIN
Select * from GlobalConfigurationFields 
END

GO

--#Global Configuration Fields SP End



-- spCreateOrUpdateGlobalConfigurationEmails Start
CREATE PROCEDURE [dbo].[spCreateOrUpdateGlobalConfigurationEmails](
                @FieldTypeId bigint,
                  @CultureId bigint,
                  @Content nvarchar(MAX),
                  @Subject nvarchar(255))

AS
BEGIN
 Declare @Result bit=0;
  IF exists (select 1 from Cultures Where Id = @CultureId)
  Begin
	if exists(select 1 from GlobalConfigurationEmails where CultureId = @CultureId AND FieldTypeId = @FieldTypeId)
	Begin
			UPDATE GlobalConfigurationEmails
			SET FieldTypeId = @FieldTypeId,
			CultureId = @CultureId,
			Content = @Content,
			Subject = @Subject
			WHERE CultureId = @CultureId AND FieldTypeId = @FieldTypeId
	End
	else
	Begin
			INSERT into GlobalConfigurationEmails (FieldTypeId,CultureId,Content,[Subject]) VALUES (@FieldTypeId,@CultureId,@Content,@Subject)
	End
	 set @Result = 1
  End
  SELECT @Result
END
-- spCreateOrUpdateGlobalConfigurationEmails End


-- spGetGlobalConfigurationEmailsByCultureId Start
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spGetGlobalConfigurationEmailsByCultureId]( @CultureId bigint)

AS
BEGIN
	IF Exists(select 1 From GlobalConfigurationEmails where CultureId = @CultureId)
		Begin
			SELECT [Id],[FieldTypeId],[CultureId],[Content],[Subject]
			FROM GlobalConfigurationEmails WHERE CultureId = @CultureId
		End
	Else
		Begin
			SELECT [Id],[FieldTypeId],[CultureId],[Content],[Subject]
			FROM GlobalConfigurationEmails WHERE CultureId = 1
		End
END
GO

-- spGetGlobalConfigurationEmailsByCultureId End

-- spGetCultures Start

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spGetCultures]
AS
BEGIN
 SELECT Id,ParentId,[Name],DisplayName,[Enabled],NativeName FROM Cultures
END

-- spGetCultures End

---------------------------------------------------------------------- Global Configuration SP End -----------------------------------------------------------------------------
/****** Object:  StoredProcedure [dbo].[spCreateOrUpdateGlobalConfigurationHolidays]    Script Date: 08-02-2023 17:11:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spCreateOrUpdateGlobalConfigurationHolidays]
(
	@Id bigint,
	@CountryId bigint,
	@Date date,
	@Description nvarchar(255)
)
AS
BEGIN
	IF @Id = 0
		BEGIN
			Insert into Holidays(CountryId,Date,Description) values(@CountryId,@Date,@Description)
		END

	IF @Id > 0
		BEGIN
			Update Holidays SET
			CountryId = @CountryId,
			Date = @Date,
			Description = @Description
			WHERE Id = @Id
		END
END


/****** Object:  StoredProcedure [dbo].[spDeleteHolidays]    Script Date: 08-02-2023 17:16:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spDeleteHolidays]
(
	@Id bigint
)
AS
BEGIN
	Delete from Holidays where Id = @Id
END

	Update Portal2GlobalConfigurationCutOffTimes
	Set CutOffTimeOverride = @CutOffTime,
    CutOffDayOverride = @CutOffDay
	Where GlobalConfigurationCutOffTimeId = @Id

	SET @Result = 1;

   END

 SELECT @Result
END
GO

SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spGetHolidayById]
(
	@Id bigint
)
AS
BEGIN
	select * from Holidays where Id = @Id;
END



/****** Object:  StoredProcedure [dbo].[spGetFilteredHolidays]    Script Date: 08-02-2023 17:17:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spGetFilteredHolidays]
(
	@DisplayLength int,
	@DisplayStart int,
	@SortCol nvarchar(max)='',
	@Search nvarchar(255) = '',
	@SortDir bit,
	@CountryCodeId int,
	@CountryNameId int
)
AS
BEGIN
	Declare @FirstRec int, @LastRec int
    Set @FirstRec = @DisplayStart;
    Set @LastRec = @DisplayStart + @DisplayLength;
    Declare @SortDirection nvarchar(max);
	print(@SortDir);
	IF(@SortDir = '1')
	BEGIN
		set @SortDirection = 'asc'
	END
	IF(@SortDir = '0')
	BEGIN
		set @SortDirection = 'desc'
	END
	print(@SortDirection);
	;With CTE_Holidays as
    (
		Select ROW_NUMBER() over (order by

         case when (@SortCol = 'CountryName' and @SortDirection='asc')
             then C.CountryName
         end asc,
         case when (@SortCol = 'CountryName' and @SortDirection='desc')
             then C.CountryName
         end desc,

       case when (@SortCol = 'CountryCode' and @SortDirection='asc')
             then C.Code
         end asc,
         case when (@SortCol = 'CountryCode' and @SortDirection='desc')
             then C.Code
         end desc,

		 case when (@SortCol = 'Date' and @SortDirection='asc')
             then H.[Date]
         end asc,
         case when (@SortCol = 'Date' and @SortDirection='desc')
             then H.[Date]
         end desc,

		  case when (@SortCol = 'Description' and @SortDirection='asc')
             then H.[Description]
         end asc,
         case when (@SortCol = 'Description' and @SortDirection='desc')
             then H.[Description]
         end desc
        )
		as RowNum,

	      H.Id,H.CountryId,H.[Date],H.[Description],C.CountryName,C.Code CountryCode
			from Holidays H
		join CountryCodes C ON C.Id = H.CountryId
		where
		H.CountryId = case when ISNULL(@CountryCodeId,0) = 0 then H.CountryId else @CountryCodeId END AND
		H.CountryId = case when ISNULL(@CountryNameId,0) = 0 then H.CountryId else @CountryNameId END and
		(@Search IS NULL
                 Or H.[Description] like '%' + @Search + '%'
				 Or C.CountryName like '%' + @Search + '%'
				 Or C.Code like '%' + @Search + '%')
)

/****** Object:  StoredProcedure [dbo].[spGetAllRoles]    Script Date: 08-02-2023 17:34:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Gohil Amit>
-- Create date:  Script Date: 08-02-2023 14:20:10
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spGetAllRoles]
-- spCreateOrUpdateGlobalConfigurationCutOffTimes End

as
begin

  select Id,[Name],[Description],WrongLoginAttempts,AllPermissions from Roles
end

/****** Object:  StoredProcedure [dbo].[spGetRoles]    Script Date: 08-02-2023 17:35:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Gohil Amit>
-- Create date:  Script Date: 08-02-2023 17:25:10
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spGetRoles]
(
@DisplayLength int,
@DisplayStart int,
@SortCol nvarchar(max)='',
@Search nvarchar(255) = '',
@SortDir bit
)
AS
BEGIN
	Declare @FirstRec int, @LastRec int
    Set @FirstRec = @DisplayStart;
    Set @LastRec = @DisplayStart + @DisplayLength;
    Declare @SortDirection nvarchar(max);
	print(@SortDir);
	IF(@SortDir = '1')
	BEGIN
		set @SortDirection = 'asc'
	END
	IF(@SortDir = '0')
	BEGIN
		set @SortDirection = 'desc'
	END
	print(@SortDirection);
	;With CTE_Roles as
    (
		Select ROW_NUMBER() over (order by

         case when (@SortCol = 'Name' and @SortDirection='asc')
             then R.[Name]
         end asc,
         case when (@SortCol = 'CountryName' and @SortDirection='desc')
             then R.[Name]
         end desc,

		  case when (@SortCol = 'Description' and @SortDirection='asc')
             then R.[Description]
         end asc,
         case when (@SortCol = 'Description' and @SortDirection='desc')
             then R.[Description]
         end desc
        )
         as RowNum,
		 COUNT(*) over() as TotalCount,
	      R.Id,R.[Name],R.[Description],R.WrongLoginAttempts,R.AllPermissions,R.CreatedOn,R.LastEditedOn
			from Roles R
		where
		(@Search IS NULL
                 Or R.[Name] like '%' + @Search + '%'
				 Or R.[Description] like '%' + @Search + '%')
)


	select * from CTE_Roles CH where  CH.RowNum > @FirstRec and CH.RowNum <= @LastRec order by @SortCol+' '+@SortDirection;
END

/****** Object:  StoredProcedure [dbo].[spGetRoleDetailByRoleId]    Script Date: 08-02-2023 17:36:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Gohil Amit>
-- Create date:  Script Date: 08-02-2023 17:30:10
-- Description:	<Description,,>
-- =============================================
ALTER proc [dbo].[spGetRoleDetailByRoleId]
(
	@RoleId int
)
as

begin

	select R.Id,R.[Name],R.[Description],R.WrongLoginAttempts,R.AllPermissions,
	RG.Id As GroupId,RGS.SubSiteId As SiteId,
	RGC.CultureLCId As CultureId,
	RGP.PermissionEnumId As PrivilegesId
	from Roles R
	left outer join RoleGrouping RG ON RG.RoleId = R.Id
	left outer join RoleGrouping2SubSite RGS ON RGS.RoleGroupingId = RG.Id
	left outer join RoleGrouping2Culture RGC ON RGC.RoleGroupingId = RG.Id
	left outer join RoleGrouping2Permission RGP ON RGP.RoleGroupingId = RG.Id
	where R.Id = @RoleId

end


/****** Object:  StoredProcedure [dbo].[spGetLanguagesListByRole]    Script Date: 08-02-2023 17:39:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Gohil Amit>
-- Create date:  Script Date: 08-02-2023 17:40:00
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spGetLanguagesListByRole]

as
begin

  select Id,ParentId,Name,DisplayName,[Enabled],NativeName from Cultures
  where [Enabled]=1
end

/****** Object:  StoredProcedure [dbo].[spGetSubSites]    Script Date: 08-02-2023 17:36:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Gohil Amit>
-- Create date:  Script Date: 08-02-2023 17:35:10
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spGetSubSites]

as
begin

	select Id,Directory,DisplayName,Domain,[Description],AssetId,[Enabled],SystemEmailFromAddress,FaviconAssetId,
	RobotTxt,[Stopped],ShowTax,PortalId,BCCEmailAddress,CloudFrontDistributionId,EmailStopped
	from SubSites
end

/****** Object:  StoredProcedure [dbo].[spDeleteRoles]    Script Date: 08-02-2023 17:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Gohil Amit>
-- Create date:  Script Date: 08-02-2023 17:38:00
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spDeleteRoles]
(
	@RoleId bigint
)
AS
BEGIN
	DELETE FROM Roles where Id = @RoleId
END

	select * from CTE_Holidays CH where  CH.RowNum > @FirstRec and CH.RowNum <= @LastRec order by @SortCol+' '+@SortDirection;
END
/****** Object:  StoredProcedure [dbo].[spRemoveSection]    Script Date: 08-02-2023 17:38:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Gohil Amit>
-- Create date:  Script Date: 08-02-2023 17:39:00
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spRemoveSection]
(
	@GroupId bigint
)
AS
BEGIN
	DELETE FROM RoleGrouping where Id = @GroupId
END
END

----------------------------------------------------------------------------- spLogin Start ---------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [spLogin] (
	@userName NVARCHAR(50)
	,@password NVARCHAR(255)
	)
AS
BEGIN
	SELECT [Id]
		,[UserName]
		,[FirstName]
		,[LastName]
		,[RoleId]
	FROM [Users]
	WHERE [UserName] = @userName
		AND [Password] = @password
END
GO

-- spLogin End

-- spResetLoginAttempts Start


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spResetLoginAttempts] (
	@userName NVARCHAR(50)
	,@password NVARCHAR(255)
	)
AS
BEGIN
	UPDATE Users
	SET [LoginAttempts] = 0
	WHERE [UserName] = @userName
		AND [Password] = @password
END
GO

-- spResetLoginAttempts End

-- spResetPassword Start


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spResetPassword]
(
@UserName nvarchar(50),
@Password nvarchar(255),
@ResetRequired bit
)
AS
BEGIN
	UPDATE Users SET
	Password  = @Password,
	ResetRequired = @ResetRequired
	WHERE UserName = @UserName
END
GO

-- spResetPassword End

-- spResetRequired Start


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spResetRequired]
(
@userId bigint
)
AS
BEGIN
	UPDATE Users SET
	ResetRequired = 1
	WHERE Id = @userId

END
GO

-- spResetRequired End

-- spIncrementLoginAttempts Start

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spIncrementLoginAttempts]
(
@userId bigint
)
AS
BEGIN
	UPDATE Users SET
	LoginAttempts = LoginAttempts + 1
	WHERE Id = @userId
	END
GO

-- spIncrementLoginAttempts End

-- spGetUserDataForLogin Start


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spGetUserDataForLogin] (@userName NVARCHAR(50))
AS
BEGIN
	SELECT [Id]
	,[UserName]
	,[Password]
	,[LoginAttempts]
	,[ResetRequired]
	FROM [Users]
	WHERE [UserName] = @userName
END
GO

-- spGetUserDataForLogin End

--------------------------------------------------------------------------------- Login End ----------------------------------------------------------------------------------------

/****** Object:  StoredProcedure [dbo].[spCreateOrUpdateGlobalConfigurationFonts]    Script Date: 07-02-2023 18:39:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCreateOrUpdateGlobalConfigurationFonts]
(
	@Id bigint,
	@Name nvarchar(255),
	@FontFamily nvarchar(255),
	@FontFileName nvarchar(1024),
	@IsDefault bit,
	@Enabled bit
)
AS
BEGIN
	IF @Id = 0
		BEGIN
		  Insert into GlobalConfigurationFonts(Name,FontFamily,FontFileName,IsDefault,Enabled)
		  values(@Name,@FontFamily,@FontFileName,@IsDefault,@Enabled)
		END

	ELSE IF @Id > 0
		BEGIN
			Update GlobalConfigurationFonts set
			Name = @Name,
			FontFamily = @FontFamily,
			FontFileName = @FontFileName,
			IsDefault = @IsDefault,
			Enabled = @Enabled
			where Id = @Id
		END

END
GO

/****** Object:  StoredProcedure [dbo].[spGetGlobalConfigurationFonts]    Script Date: 07-02-2023 18:05:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetGlobalConfigurationFonts]
AS
BEGIN
	select * from GlobalConfigurationFonts
END

GO
/****** Object:  StoredProcedure [dbo].[spGetCountries]    Script Date: 13-02-2023 16:26:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spGetCountries]
AS
BEGIN
	select * from CountryCodes
END

-- GlobalCutOffTimes SP start

-- spGetGlobalConfigurationCutOffTimesByCountryId start
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spGetGlobalConfigurationCutOffTimesByCountryId]( @CountryId bigint)

AS
BEGIN
	SELECT TOP 1 * FROM GlobalConfigurationCutOffTimes where CountryId = @CountryId
END
GO
-- spGetGlobalConfigurationCutOffTimesByCountryId end

-- spCreateOrUpdateGlobalConfigurationCutOffTimes start
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spCreateOrUpdateGlobalConfigurationCutOffTimes]
(
 @Id bigint,
 @CountryId bigint,
 @CutOffTime time(7),
 @CutOffDay smallint
)

AS
BEGIN
 Declare @Result bit = 0;

 IF(@Id = 0 and exists(select 1 from CountryCodes where Id = @CountryId) and not exists(select 1 from GlobalConfigurationCutOffTimes where CountryId = @CountryId))
 BEGIN
   INSERT INTO GlobalConfigurationCutOffTimes(CountryId, CutOffTime, CutOffDay)
   VALUES (@CountryId, @CutOffTime, @CutOffDay)

   SET @Result = 1;

 END
 ELSE
  IF(exists(select 1 from CountryCodes where Id = @CountryId) and exists(select 1 from GlobalConfigurationCutOffTimes where CountryId = @CountryId))
   BEGIN
    UPDATE GlobalConfigurationCutOffTimes SET
    CountryId = @CountryId,
    CutOffTime = @CutOffTime,
    CutOffDay = @CutOffDay
    WHERE Id = @Id

	Update Portal2GlobalConfigurationCutOffTimes
	Set CutOffTimeOverride = @CutOffTime,
    CutOffDayOverride = @CutOffDay
	Where GlobalConfigurationCutOffTimeId = @Id

	SET @Result = 1;

   END

 SELECT @Result
END
GO
-- spCreateOrUpdateGlobalConfigurationCutOffTimes end
-- GlobalCutOffTimes SP end




--spGetGlobalConfigurationMessagesListByCultureId Start

/****** Object:  StoredProcedure [dbo].[spGetGlobalConfigurationMessagesListByCultureId]    Script Date: 15-02-2023 18:20:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Last UpdatedBy: <>
-- =============================================

ALTER PROCEDURE [dbo].[spGetGlobalConfigurationMessagesListByCultureId]
(
	@CultureId int
)
as
BEGIN

	select GCM.Id,GCM.[Type],GCM.Content,GCM.CultureId,C.[Name] CultureName from GlobalConfigurationMessages GCM
	join Cultures C ON C.Id = GCM.CultureId
	where GCM.CultureId = @CultureId

END

--spGetGlobalConfigurationMessagesListByCultureId End

--spCreateOrUpdateGlobalConfigurationMessages Start

/****** Object:  StoredProcedure [dbo].[spCreateOrUpdateGlobalConfigurationMessages]    Script Date: 15-02-2023 18:03:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Last UpdatedBy: <>
-- =============================================

CREATE PROCEDURE [dbo].[spCreateOrUpdateGlobalConfigurationMessages]
(  
 @Id int,  
 @Type nvarchar(max),  
 @Content nvarchar(max),  
 @CultureId int  
)  
  
AS  
BEGIN  
 IF(@Id = 0)  
 BEGIN  
   INSERT INTO GlobalConfigurationMessages([Type],Content,CultureId)  
   VALUES (@Type,@Content,@CultureId)  
 END;  
 ELSE  
  BEGIN  
   UPDATE GlobalConfigurationMessages SET  
   Type = @Type,  
   Content = @Content,  
   CultureId = @CultureId     
   WHERE Id = @Id  

   Update Portal2GlobalConfigurationMessages 
   Set ValueOverride = @Content
   where GlobalConfigurationMessageId =@Id

  END;  
END;

--spCreateOrUpdateGlobalConfigurationMessages End
