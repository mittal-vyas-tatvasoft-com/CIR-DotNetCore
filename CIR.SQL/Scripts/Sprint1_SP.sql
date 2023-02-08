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