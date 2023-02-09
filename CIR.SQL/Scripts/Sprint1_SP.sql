---------------------------------------------------------------------- Global Configuration SP Start -----------------------------------------------------------------------------
-- #Global Configuration CutOffTimes SP Start


-- spGetGlobalConfigurationCutOffTimesByCountryId Start
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
-- spCreateOrUpdateGlobalConfigurationCutOffTimes End


-- #Global Configuration CutOffTimes SP End
---------------------------------------------------------------------- Global Configuration SP Start -----------------------------------------------------------------------------