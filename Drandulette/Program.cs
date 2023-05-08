using Drandulette.Controllers.Data;
using Microsoft.EntityFrameworkCore;

void ConfigureServices(IServiceCollection services)
{
    var connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>("ConnectionString");
    var serverVersion = ServerVersion.AutoDetect(connectionString);
    services.AddDbContext<DranduletteContext>(
        dbContextOptions => dbContextOptions
            .UseMySql(connectionString, serverVersion, options => options.EnableRetryOnFailure())
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
    );
}

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000").AllowAnyMethod()
                                                                      .AllowAnyHeader();
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
ConfigureServices(builder.Services);
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
