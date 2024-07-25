
using ARCN.API.EnvHandler;
using ARCN.API.Extensions;
using ARCN.API.Extensions;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

Log.Logger = new LoggerConfiguration()
                          .WriteTo.Console()
                          .CreateLogger();

try
{
    Log.Information("Starting web host");
    DotEnv.Load();
    var builder = WebApplication.CreateBuilder(args);
    // NLog: Setup NLog for Dependency injection
    builder.Host.UseSerilog((ctx, lc) => lc
                              .WriteTo.Console(new JsonFormatter())
                              .WriteTo.File(new JsonFormatter(), "Logs/log.txt",
                              rollingInterval: RollingInterval.Infinite, // No time-based rolling
                              fileSizeLimitBytes: 1_048_576, // 1 MB
                              rollOnFileSizeLimit: true, // Enable rolling based on file size
                              retainedFileCountLimit: null // No limit on the number of retained files
                                                )
                              .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information));

    builder.Services.AddHttpLogging(opts =>
    {
        opts.LoggingFields =
            HttpLoggingFields.RequestPath |
            HttpLoggingFields.RequestMethod |
            HttpLoggingFields.ResponseStatusCode |
            HttpLoggingFields.RequestBody |
            HttpLoggingFields.ResponseBody;
    });

    
    var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

    app.SeedDatabase();


    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}