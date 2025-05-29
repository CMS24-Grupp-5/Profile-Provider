using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Profile.Data;
using Profile.Data.Repositories;
using Profile.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrera Controllers och OpenAPI/Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Profile API", Version = "v1" });

    // API-nyckel autentisering f�r Swagger
    var apiScheme = new OpenApiSecurityScheme
    {
        Name = "x-Api-Key",
        Description = "Ange giltig API-nyckel",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme",
        Reference = new OpenApiReference
        {
            Id = "ApiKey",
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition("ApiKey", apiScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { apiScheme, Array.Empty<string>() }
    });
});

// Konfigurera CORS-policy f�r utveckling
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Databaskonfiguration baserat p� milj�
if (builder.Environment.IsDevelopment())
{
    // Anv�nder In-Memory databas vid utveckling
    var inMemoryDb = Guid.NewGuid().ToString();
    builder.Services.AddDbContext<DataContext>(x => x.UseInMemoryDatabase(inMemoryDb));
}
else
{
    // Anv�nder SQL Server databas i produktion
    builder.Services.AddDbContext<DataContext>(x =>
        x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
}

// Dependency Injection f�r tj�nster och repositories
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileService, ProfileService>();

var app = builder.Build();

// Anv�nd Swagger i alla milj�er
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Profile API v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

// F�r integrationstester
public partial class Program { }
