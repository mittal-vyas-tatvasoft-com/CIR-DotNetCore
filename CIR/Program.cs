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
builder.Services.AddScoped<IGlobalConfigurationCurrenciesService, GlobalConfigurationCurrenciesService>();
builder.Services.AddScoped<IGlobalConfigurationCurrenciesRepository, GlobalConfigurationCurrenciesRepository>();
builder.Services.AddScoped<IGlobalConfigurationHolidaysService, GlobalConfigurationHolidaysService>();
builder.Services.AddScoped<IGlobalConfigurationHolidaysRepository, GlobalConfigurationHolidaysRepository>();
builder.Services.AddScoped<IGlobalConfigurationEmailsService, GlobalConfigurationEmailsService>();
builder.Services.AddScoped<IGlobalConfigurationEmailsRepository, GlobalConfigurationEmailsRepository>();
builder.Services.AddScoped<ICommonRepository, CommonRepository>();
builder.Services.AddScoped<ICommonService, CommonService>();


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
