using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using ShoppingProject.Infrastructure.Data;

namespace ShoppingProject.Tests.Infrastructure;

/// <summary>
/// Custom WebApplicationFactory for integration tests that configures
/// test-specific services and replaces external dependencies with test doubles.
/// </summary>
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            // Add in-memory configuration for tests
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnection"] = null, // Use InMemory database
                ["ConnectionStrings:AuditConnection"] = null,
                ["ConnectionStrings:RedisConnection"] = "localhost:6379", // Will be replaced with InMemory cache
                ["RabbitMq:Host"] = "localhost",
                ["RabbitMq:Username"] = "guest",
                ["RabbitMq:Password"] = "guest",
                ["KeyVault:Name"] = "", // Disable KeyVault for tests
                ["Jwt:Key"] = "test-jwt-key-for-integration-tests-must-be-long-enough",
                ["Jwt:Issuer"] = "test-issuer",
                ["Jwt:Audience"] = "test-audience"
            });
        });

        builder.ConfigureTestServices(services =>
        {
            // Replace RabbitMQ with InMemory test harness
            services.RemoveAll(typeof(IBus));
            services.RemoveAll(typeof(IHostedService));

            services.AddMassTransitTestHarness();

            // Replace Redis distributed cache with Memory cache
            services.RemoveAll(typeof(IDistributedCache));
            services.AddDistributedMemoryCache();

            // Ensure databases use InMemory provider
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
            services.RemoveAll(typeof(DbContextOptions<AuditDbContext>));

            // Remove background services that depend on external services
            var hostedServices = services
                .Where(descriptor => descriptor.ServiceType == typeof(IHostedService))
                .ToList();

            foreach (var service in hostedServices)
            {
                services.Remove(service);
            }

            // Re-add MassTransit test harness hosted service
            services.AddMassTransitTestHarness();
        });

        builder.UseEnvironment("Testing");
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        // Create the host for testing
        var host = base.CreateHost(builder);

        // Seed test data if needed
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                // Seed test data here if needed
                SeedTestData(context);
            }
            catch (Exception ex)
            {
                // Log or handle initialization errors
                Console.WriteLine($"An error occurred seeding the test database: {ex.Message}");
            }
        }

        return host;
    }

    private static void SeedTestData(ApplicationDbContext context)
    {
        // Add any test data seeding logic here
        // For now, we'll let individual tests manage their own data
    }
}
