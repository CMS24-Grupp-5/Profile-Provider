using Microsoft.EntityFrameworkCore;
using Profile.Data;
using Profile.Data.Repositories;
using Profile.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});





if (builder.Environment.IsDevelopment())
{
    var inMemoryDb = Guid.NewGuid().ToString();
    builder.Services.AddDbContext<DataContext>(x => x.UseInMemoryDatabase(inMemoryDb));
}
else
{
    builder.Services.AddDbContext<DataContext>(x =>
          x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

}
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileService, ProfileService>();




var app = builder.Build();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ventixe AuthServiceProvider API");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();


app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }