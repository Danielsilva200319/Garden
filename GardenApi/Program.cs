using System.Reflection;
using AspNetCoreRateLimit;
using GardenApi.Extensions;
/* using GardenApi.Helpers.Error; */
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
/* var logger = new LoggerConfiguration()
					.ReadFrom.Configuration(builder.Configuration)
					.Enrich.FromLogContext()
					.CreateLogger(); */

//builder.Logging.ClearProviders();
/* builder.Logging.AddSerilog(logger); */
// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigureRateLimiting();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
builder.Services.ConfigureCors();
builder.Services.AddApplicationServices();
/* builder.Services.AddJwt(builder.Configuration); */
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GardenContext>(options =>
{
    string connectionstring = builder.Configuration.GetConnectionString("ConexMysql");
    options.UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring));
});

var app = builder.Build();
/* app.UseMiddleware<ExceptionMiddleware>(); */

app.UseStatusCodePagesWithReExecute("/errors/{0}");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<GardenContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var _logger = loggerFactory.CreateLogger<Program>();
        _logger.LogError(ex, "Ocurrio un error durante la migracion");
    }
}
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseIpRateLimiting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();