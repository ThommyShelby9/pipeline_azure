using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using ShoppingProject.Domain.Constants;
using ShoppingProject.Infrastructure.Data;
using ShoppingProject.Infrastructure.Identity;

namespace ShoppingProject.Tests.Infrastructure;

/// <summary>
/// Custom WebApplicationFactory for integration tests that configures
/// test-specific services and replaces external dependencies with test doubles.
/// </summary>
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = Guid.NewGuid().ToString();

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
                ["JwtSettings:Secret"] = "test-jwt-secret-key-for-integration-tests-minimum-32-characters-required",
                ["JwtSettings:Issuer"] = "test-issuer",
                ["JwtSettings:Audience"] = "test-audience"
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

            // Replace DbContext with InMemory database explicitly
            services.RemoveAll<DbContextOptions<ApplicationDbContext>>();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase(_dbName);
            });

            services.RemoveAll<DbContextOptions<AuditDbContext>>();
            services.AddDbContext<AuditDbContext>(options =>
            {
                options.UseInMemoryDatabase($"{_dbName}_Audit");
            });

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

                // Seed Identity data for tests
                SeedIdentityDataAsync(services).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                // Log or handle initialization errors
                Console.WriteLine($"An error occurred seeding the test database: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw; // Re-throw to see the error in test output
            }
        }

        return host;
    }

    private static async Task SeedIdentityDataAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        // Seed Roles
        var roles = new[] { Roles.Administrator, Roles.Client };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Seed Admin User
        var adminEmail = "admin@test.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            var user = new ApplicationUser
            {
                UserName = "admin",
                Email = adminEmail,
                EmailConfirmed = true,
                FirstName = "Test",
                LastName = "Admin",
                Gender = "Male",
                PhoneNumber = "+905551234567",
                PhoneNumberConfirmed = true,
            };

            var result = await userManager.CreateAsync(user, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.Administrator);
            }
        }

        // Seed Client User
        var clientEmail = "user@test.com";
        var clientUser = await userManager.FindByEmailAsync(clientEmail);
        if (clientUser == null)
        {
            var user = new ApplicationUser
            {
                UserName = "user",
                Email = clientEmail,
                EmailConfirmed = true,
                FirstName = "Test",
                LastName = "User",
                Gender = "Male",
                PhoneNumber = "+905559876543",
                PhoneNumberConfirmed = true,
            };

            var result = await userManager.CreateAsync(user, "User123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.Client);
            }
        }
    }
}
