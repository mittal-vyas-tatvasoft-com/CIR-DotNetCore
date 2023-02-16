---------------------------------------------------------------------- Global Configuration SP Start -----------------------------------------------------------------------------

-- #Global Configuration Currencies SP Start

-- spGetAllCurrencies Start

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


-- spGetGlobalConfigurationCurrenciesByCountryId End

-- spCreateOrUpdateGlobalConfigurationCurrenciesc Start

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCreateOrUpdateGlobalConfigurationCurrencies](@Id bigint,  
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

-- spCreateOrUpdateGlobalConfigurationCurrenciesc End


-- #Global Configuration Currencies SP End


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
	,@password NVARCHAR(25)
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
	,@password NVARCHAR(25)
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
@UserName nvarchar(20),
@Password nvarchar(20),
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