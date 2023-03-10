using CIR.Application.Services;
using CIR.Application.Services.Common;
using CIR.Application.Services.GlobalConfiguration;
using CIR.Application.Services.Users;
using CIR.Application.Services.Utilities;
using CIR.Common.CommonModels;
using CIR.Common.Data;
using CIR.Common.EmailGeneration;
using CIR.Common.Helper;
using CIR.Core.Interfaces;
using CIR.Core.Interfaces.Common;
using CIR.Core.Interfaces.GlobalConfiguration;
using CIR.Core.Interfaces.Users;
using CIR.Core.Interfaces.Utilities;
using CIR.Data.Data;
using CIR.Data.Data.Common;
using CIR.Data.Data.GlobalConfiguration;
using CIR.Data.Data.Users;
using CIR.Data.Data.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
	var jwtSecurityScheme = new OpenApiSecurityScheme
	{
		BearerFormat = "JWT",
		Name = "JWT Authentication",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http,
		Scheme = JwtBearerDefaults.AuthenticationScheme,
		Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

		Reference = new OpenApiReference
		{
			Id = JwtBearerDefaults.AuthenticationScheme,
			Type = ReferenceType.SecurityScheme
		}
	};

	s.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

	s.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{ jwtSecurityScheme, Array.Empty<string>() }
	});
});

//add dbcontext
var connectionString = builder.Configuration.GetConnectionString("CIR");
builder.Services.AddDbContext<CIRDbContext>(item => item.UseSqlServer(connectionString));

//add appsettings
var appSettings = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<JwtAppSettings>(appSettings);

//add emailgeneration appsettings
var emailGeneration = builder.Configuration.GetSection("EmailGeneration");
builder.Services.Configure<EmailModel>(emailGeneration);

//add thumbnailcreation appsettings
var thumbnailCreation = builder.Configuration.GetSection("ThumbnailCreation");
builder.Services.Configure<ThumbnailModel>(thumbnailCreation);
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<JwtGenerateToken>();
builder.Services.AddScoped<EmailGeneration>();
builder.Services.AddScoped<ThumbnailCreation>();
builder.Services.AddScoped<CSVExport>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGlobalConfigurationFontsServices, GlobalConfigurationFontsServices>();
builder.Services.AddScoped<IGlobalConfigurationFontsRepository, GlobalConfigurationFontsRepository>();
builder.Services.AddScoped<IGlobalConfigurationCurrenciesService, GlobalConfigurationCurrenciesService>();
builder.Services.AddScoped<IGlobalConfigurationCurrenciesRepository, GlobalConfigurationCurrenciesRepository>();
builder.Services.AddScoped<IGlobalConfigurationHolidaysService, GlobalConfigurationHolidaysService>();
builder.Services.AddScoped<IGlobalConfigurationHolidaysRepository, GlobalConfigurationHolidaysRepository>();
builder.Services.AddScoped<IGlobalConfigurationEmailsService, GlobalConfigurationEmailsService>();
builder.Services.AddScoped<IGlobalConfigurationEmailsRepository, GlobalConfigurationEmailsRepository>();
builder.Services.AddScoped<ICommonRepository, CommonRepository>();
builder.Services.AddScoped<ICommonService, CommonService>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<IRolesRepository, RolesRepository>();
builder.Services.AddScoped<IGlobalConfigurationCutOffTimesRepository, GlobalConfigurationCutOffTimesRepository>();
builder.Services.AddScoped<IGlobalConfigurationCutOffTimesService, GlobalConfigurationCutOffTimesService>();
builder.Services.AddScoped<IGlobalConfigurationMessagesService, GlobalConfigurationMessagesService>();
builder.Services.AddScoped<IGlobalConfigurationMessagesRepository, GlobalConfigurationMessagesRepository>();
builder.Services.AddScoped<IGlobalConfigurationReasonsService, GlobalConfigurationReasonsService>();
builder.Services.AddScoped<IGlobalConfigurationReasonsRepository, GlobalConfigurationReasonsRepository>();
builder.Services.AddScoped<IGlobalConfigurationStylesService, GlobalConfigurationStylesService>();
builder.Services.AddScoped<IGlobalConfigurationStylesRepository, GlobalConfigurationStylesRepository>();

builder.Services.AddScoped<IGlobalConfigurationFieldsRepository, GlobalConfigurationFieldsRepository>();
builder.Services.AddScoped<IGlobalConfigurationFieldsService, GlobalConfigurationFieldsService>();
builder.Services.AddScoped<ISystemSettingsLookupsService, SystemSettingsLookupsService>();
builder.Services.AddScoped<ISystemSettingsLookupsRepository, SystemSettingsLookupsRepository>();

builder.Services.AddScoped<IGlobalConfigurationWeekendRepository,GlobalConfigurationWeekendRepository>();
builder.Services.AddScoped<IGlobalConfigurationWeekendService, GlobalConfigurationWeekendService>();

//allow origin
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader()
			));


//add authentication 
builder.Services.AddAuthentication().AddJwtBearer("JWTScheme", x =>
{
	x.RequireHttpsMetadata = false;
	x.SaveToken = true;
	x.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		ValidateIssuer = false,
		ValidateAudience = false,
		RequireExpirationTime = true,
		ClockSkew = TimeSpan.Zero,
		ValidateLifetime = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:AuthKey"]))
	};
});
builder.Services.AddAuthorization();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader()
			);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
