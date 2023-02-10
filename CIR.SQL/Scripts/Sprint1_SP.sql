﻿---------------------------------------------------------------------- Global Configuration SP Start -----------------------------------------------------------------------------

-- #Global Configuration Currencies SP Start

-- spGetAllCurrencies Start

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetAllCurrencies]

AS
BEGIN
	
 SELECT Id, CodeName, Symbol FROM Currencies

END
GO

-- spGetAllCurrencies End

-- spGetGlobalConfigurationCurrenciesByCountryId Start

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



/****** Object:  StoredProcedure [dbo].[spGetHolidayById]    Script Date: 08-02-2023 17:17:05 ******/
SET ANSI_NULLS ON
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


---------------------------------------------------------------------- USER SP STARTED -----------------------------------------------------------------------------
--- SP spGetUserIdWise START

/****** Object:  StoredProcedure [dbo].[spGetUserIdWise]    Script Date: 10-02-2023 03:34:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spGetUserIdWise](@id integer)
AS
BEGIN
	SELECT * FROM Users WHERE Id = @id;
END
--- SP spGetUserIdWise END


--- SP spAddUpdateUsers START
/****** Object:  StoredProcedure [dbo].[spAddUpdateUsers]    Script Date: 10-02-2023 03:36:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spAddUpdateUsers] 
(	
  @Id int,
  @UserName nvarchar(max),
  @Password nvarchar(max),
  @Email nvarchar(max),
  @SalutationLookupItemId int,
  @FirstName nvarchar(max),
  @LastName nvarchar(max),
  @RoleId int,
  @Enabled bit,    
  @ResetRequired bit,
  @DefaultAdminUser bit,
  @TimeZone nvarchar(max),
  @CultureLcid int,
  @EmployeeId nvarchar(max),
  @PhoneNumber nvarchar(max),
  @ScheduledActiveChange datetime,
  @LoginAttempts int,
  @CompanyName nvarchar(max),
  @PortalThemeId int
)	
AS
BEGIN	
	Declare @Messsage nvarchar(max)='Error in user in sql';
	IF(@Id = '0')
	BEGIN
		Insert into [Users]
		(UserName,[Password],Email,SalutationLookupItemId,FirstName,LastName,RoleId,[Enabled],LastLogOn,CreatedOn,ResetRequired,
		DefaultAdminUser,TimeZone,CultureLCId,EmployeeId,PhoneNumber,ScheduledActiveChange,LoginAttempts,CompanyName,PortalThemeId) 
		Values
		(@UserName,@Password,@Email,@SalutationLookupItemId,@FirstName,@LastName,@RoleId,@Enabled,GETDATE(),GetDate(),@ResetRequired
		,@DefaultAdminUser,@TimeZone,@CultureLcid,@EmployeeId,@PhoneNumber,@ScheduledActiveChange,@LoginAttempts,@CompanyName,@PortalThemeId);

		SET @Messsage = 'UserDetail saved successfully.';
	END
	ELSE
	BEGIN
		Update [Users] set
		UserName = @UserName,
		[Password] = @Password,
		Email = @Email,
		SalutationLookupItemId = @SalutationLookupItemId,
		FirstName = @FirstName,
		LastName = @LastName,
		RoleId = @RoleId,
		[Enabled] = @Enabled,
		LastEditedOn = GETDATE(),
		ResetRequired = @ResetRequired,
		DefaultAdminUser = @DefaultAdminUser,
		TimeZone = @TimeZone,
		CultureLCId = @CultureLcid,
		EmployeeId = @EmployeeId,
		PhoneNumber = @PhoneNumber,
		ScheduledActiveChange = @ScheduledActiveChange,
		LoginAttempts = @LoginAttempts,
		CompanyName = @CompanyName,
		PortalThemeId = @PortalThemeId		
		where Id = @Id;

		SET @Messsage = 'UserDetail updated successfully.';
	END
	SELECT @Messsage	
END
--- SP spAddUpdateUsers END

-- SP spDeleteUser START
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spDeleteUser]
(
	@Id int
)
AS
BEGIN
	update Users set
	Enabled = 0
	where Id = @Id
END
-- SP spDeleteUser END

--- SP spGetFilteredUsersList START
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[spGetFilteredUsersList]
@DisplayLength int,
@DisplayStart int,
@SortCol nvarchar(max),
@Search nvarchar(255) = '',
@SortDir nvarchar(10),
@RoleId int,
@Enabled bit
as

begin
    Declare @FirstRec int, @LastRec int
    Set @FirstRec = @DisplayStart;
    Set @LastRec = @DisplayStart + @DisplayLength;
    Declare @SortDirection nvarchar(max);  

	IF(@SortDir IS NULL OR @SortDir = '')
	BEGIN
		set @SortDirection = 'asc'
	END
	ELSE
	BEGIN
		set @SortDirection = @SortDir
	END			
	
    ;With CTE_Users as
    (
         Select ROW_NUMBER() over (order by
        
         case when (@SortCol = 'UserName' and @SortDir='asc')
             then U.UserName
         end asc,
         case when (@SortCol = 'UserName' and @SortDir='desc')
             then U.UserName
         end desc,
        
       case when (@SortCol = 'Email' and @SortDir='asc')
             then U.Email
         end asc,
         case when (@SortCol = 'Email' and @SortDir='desc')
             then U.Email
         end desc,

		 case when (@SortCol = 'RoleName' and @SortDir='asc')
             then R.[Name]
         end asc,
         case when (@SortCol = 'RoleName' and @SortDir='desc')
             then R.[Name]
         end desc,
   
		case when (@SortCol = 'FullName' and @SortDir='asc')
            then U.FirstName
        end asc,
        case when (@SortCol = 'FullName' and @SortDir='desc')
            then U.FirstName
        end desc,

		case when (@SortCol = 'CreatedOn' and @SortDir='asc')
            then U.CreatedOn
        end asc,
        case when (@SortCol = 'CreatedOn' and @SortDir='desc')
            then U.CreatedOn
        end desc
        )
         as RowNum,
         COUNT(*) over() as TotalCount,
         U.Id,U.UserName,U.[Password],U.Email,U.SalutationLookupItemId,U.FirstName,U.LastName,(U.FirstName+' '+U.LastName) As FullName
		,U.RoleId,R.[Name] RoleName,
		U.Enabled,U.LastLogOn,U.CreatedOn,U.LastEditedOn,U.ResetRequired,U.DefaultAdminUser,U.TimeZone
		,U.CultureLCId,C.DisplayName CultureDisplayName,C.NativeName CultureNativeName 
		,U.EmployeeId,U.PhoneNumber,U.ScheduledActiveChange,U.LoginAttempts,U.CompanyName,U.PortalThemeId
		from Users U
		join Roles R ON R.Id = U.RoleId
		join Cultures C ON C.Id = U.CultureLCId
         where
		 U.RoleId = case when ISNULL(@RoleId,0) = 0 then U.RoleId else @RoleId END AND		 
		 U.[Enabled] = case when @Enabled = 'true' then 1 when @Enabled = 'false' then 0 else  U.[Enabled] END AND
		 (@Search IS NULL
                 Or U.UserName like '%' + @Search + '%'
                 Or U.FirstName like '%' + @Search + '%'
				 Or U.LastName like '%' + @Search + '%'
				 Or R.[Name] like '%' + @Search + '%'				 
				 Or U.[Enabled] = (case when @Search = 'true' then  1 when @Search = 'false' then 0 end)
                 Or U.Email like '%' + @Search + '%')		
    )	
	Select * from CTE_Users where RowNum > @FirstRec and RowNum <= @LastRec order by @SortCol+' '+@SortDirection;END

--- SP spGetFilteredUsersList END



















---------------------------------------------------------------------- USER SP END ---------------------------------------------------------------------------------