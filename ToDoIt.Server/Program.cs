using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using ToDoIt.Server.Database;
using ToDoIt.Server.Database.Api;
using ToDoIt.Server.Stores;
using ToDoIt.Server.Stores.Api;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSingleton<IDatabaseCommandExecutor, PostgresDatabaseCommandExecutor>();
    builder.Services.AddSingleton<IToDoStore, PostgresToDoStore>();

    builder.Host.UseSerilog();

    // Add services to the container.

    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins("http://localhost:5173"); // TODO (BC) Replace with actual config value
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowCredentials();
        });
    });

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddControllers().AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.UseCors();

    app.UseAuthorization();

    app.MapControllers();

    app.UseHttpLogging();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}