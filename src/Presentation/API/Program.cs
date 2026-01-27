using AspNetCoreRateLimit;
using Azure.Identity;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using RabbitMQ.Client;
using Serilog;
using ShoppingProject.Application;
using ShoppingProject.Infrastructure;
using ShoppingProject.Infrastructure.Constants;
using ShoppingProject.Infrastructure.Data;
using ShoppingProject.Infrastructure.Identity;
using ShoppingProject.WebApi;
using ShoppingProject.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

var keyVaultName = builder.Configuration["KeyVault:Name"];
// Only configure KeyVault if name is provided and valid (not empty, not a placeholder, not just whitespace)
if (!string.IsNullOrWhiteSpace(keyVaultName) &&
    !keyVaultName.StartsWith("$(") &&
    !keyVaultName.Contains("KeyVault") &&
    keyVaultName.Length > 0)
{
    try
    {
        var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net/");
        builder.Configuration.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());
    }
    catch (Exception ex)
    {
        // Log warning but don't fail startup - KeyVault is optional for local development
        Console.WriteLine($"Warning: Could not connect to KeyVault '{keyVaultName}': {ex.Message}");
    }
}

// Rate limiting services
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Host.UseSerilog(
    (context, configuration) => configuration.ReadFrom.Configuration(context.Configuration)
);

// DI extension
builder.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddWebApiServices(builder.Configuration);

// OutputCache
builder.Services.AddOutputCache();

// Health Checks
builder
    .Services.AddHealthChecks()
    .AddNpgSql(
        builder.Configuration.GetConnectionString(
            ConfigurationConstants.ConnectionStrings.DefaultConnection
        )!,
        name: "postgres",
        tags: new[] { "ready" }
    )
    .AddRedis(
        builder.Configuration.GetConnectionString(
            ConfigurationConstants.ConnectionStrings.RedisConnection
        )!,
        name: "redis",
        tags: new[] { "ready" }
    )
    .AddRabbitMQ(
        sp =>
        {
            var connStr = builder.Configuration.GetConnectionString(
                ShoppingProject
                    .Infrastructure
                    .Constants
                    .ConfigurationConstants
                    .ConnectionStrings
                    .RabbitMqConnection
            )!;

            var factory = new ConnectionFactory
            {
                Uri = new Uri(connStr),
                AutomaticRecoveryEnabled = true,
                TopologyRecoveryEnabled = true,
            };

            // v7+ için: sadece async bağlantı mevcut
            return factory.CreateConnectionAsync().GetAwaiter().GetResult();
        },
        name: "rabbitmq",
        tags: new[] { "ready" }
    );

builder.Services.AddHealthChecksUI().AddInMemoryStorage();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        app.DescribeApiVersions()
            .Select(d => d.GroupName)
            .ToList()
            .ForEach(groupName =>
                options.SwaggerEndpoint(
                    $"/swagger/{groupName}/swagger.json",
                    groupName.ToUpperInvariant()
                )
            );
    });
}

// Initialize database with migrations and seed data
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

// Rate Limiting
app.UseIpRateLimiting();

// Middleware pipeline
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseExceptionHandler();
app.UseHttpsRedirection();

// Serve static files from wwwroot (React frontend)
app.UseStaticFiles();

app.UseSerilogRequestLogging();

// Map health checks BEFORE authentication so they bypass auth middleware
// These are public endpoints used by load balancers and monitoring
app.MapHealthChecks(
    "/health",
    new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    }
);
app.MapHealthChecksUI(options => options.UIPath = "/health-ui");

app.UseCors(AppConstants.CorsPolicies.AllowReactApp);
app.UseAuthentication();
app.UseAuthorization();

// OutputCache middleware
app.UseOutputCache();
app.UseResponseCaching();

app.MapControllers();

app.UseWebSockets();
app.UseMiddleware<WebSocketEchoMiddleware>();

app.MapHub<ShoppingProject.Infrastructure.Hubs.NotificationHub>("/hubs/notifications");

// Fallback to index.html for SPA routing (React Router)
app.MapFallbackToFile("index.html");

await app.RunAsync();

// Make Program class accessible to integration tests
public partial class Program
{
    protected Program() { }
}
