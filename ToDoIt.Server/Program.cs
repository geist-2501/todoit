using System.Text.Json.Serialization;
using Serilog;
using Serilog.Events;
using ToDoIt.Server.Database;
using ToDoIt.Server.Database.Api;
using ToDoIt.Server.Logging;
using ToDoIt.Server.Services;
using ToDoIt.Server.Services.Api;
using ToDoIt.Server.Stores;
using ToDoIt.Server.Stores.Api;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{CorrelationId}] [{Source}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

try
{
    Log.Information("Startin' this hoe");
    
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSingleton<IDatabaseCommandExecutor, PostgresDatabaseCommandExecutor>();
    builder.Services.AddSingleton<IToDoStore, PostgresToDoStore>();
    builder.Services.AddSingleton<IToDoService, ToDoService>();

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

    app.UseSerilogRequestLogging();
    app.UseRequestLogContext();
    
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