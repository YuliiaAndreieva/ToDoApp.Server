using Serilog;
using ToDoApp.BLL.Configuration;
using ToDoApp.WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStorage(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddBllServices();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var seeder = new DatabaseSeeder(app.Services);
await seeder.SeedAsync();
app.UseCors(options =>
{
    options.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});
app.UseSerilogRequestLogging();
app.MapControllers();
app.Run();
