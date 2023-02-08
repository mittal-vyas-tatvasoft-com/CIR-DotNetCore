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

