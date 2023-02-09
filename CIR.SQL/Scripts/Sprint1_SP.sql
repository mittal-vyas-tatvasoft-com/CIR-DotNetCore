---------------------------------------------------------------------- Global Configuration SP Start -----------------------------------------------------------------------------

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
		   select globalCurrency.Id,
		   globalCurrency.CountryId,
		   globalCurrency.CurrencyId,
		   globalCurrency.Enabled,
		   country.CountryName,
		   currency.CodeName

			from GlobalConfigurationCurrencies globalCurrency
			inner join CountryCodes country
			on globalCurrency.CountryId = country.Id

			inner join Currencies currency
			on globalCurrency.CurrencyId = currency.Id

			where country.Id = @countryId

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
 IF (@Id = 0  and not exists(SELECT 1 FROM GlobalConfigurationCurrencies WHERE CountryId = @CountryId and CurrencyId = @CurrencyId))
  BEGIN  
   INSERT INTO GlobalConfigurationCurrencies(CountryId,CurrencyId,Enabled)  
   VALUES (@CountryId,@CurrencyId,@Enabled);  
  END;  

ELSE IF (exists(select 1 FROM GlobalConfigurationCurrencies WHERE CountryId = @CountryId and CurrencyId = @CurrencyId))
	BEGIN
		UPDATE GlobalConfigurationCurrencies   
	   SET CountryId = @CountryId,  
		CurrencyId = @CurrencyId,  
		Enabled = @Enabled  
	   WHERE CountryId = @CountryId and CurrencyId = @CurrencyId;  
	END;
  
 ELSE IF @Id > 0  
  BEGIN  
   UPDATE GlobalConfigurationCurrencies   
   SET CountryId = @CountryId,  
    CurrencyId = @CurrencyId,  
    Enabled = @Enabled  
   WHERE Id = @Id;  
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


	select * from CTE_Holidays CH where  CH.RowNum > @FirstRec and CH.RowNum <= @LastRec order by @SortCol+' '+@SortDirection;
END    internal class Sprint1_SP
    {
    }
}
