using System.Net.Http.Json;
using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingProject.Application.Common.Interfaces;
using ShoppingProject.Application.Common.Models;
using ShoppingProject.Application.DTOs;
using ShoppingProject.Infrastructure.Data;
using ShoppingProject.Tests.Infrastructure;
using Xunit.Abstractions;

namespace ShoppingProject.UnitTests.IntegrationTests;

public class AuditIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly Faker _faker = new();
    private readonly ITestOutputHelper _output;

    public AuditIntegrationTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _factory = factory;
        _output = output;
    }

    [Fact]
    public async Task SaveEntity_Should_Publish_IAuditEvent()
    {
        _output.WriteLine("[Test] SaveEntity_Should_Publish_IAuditEvent started");

        // Arrange
        _output.WriteLine("[Arrange] Creating HTTP client");
        var client = _factory.CreateClient();

        // Login as admin
        _output.WriteLine("[Act] Logging in as admin@test.com");
        var loginResponse = await client.PostAsJsonAsync(
            "/api/v1/identity/login",
            new { Email = "admin@test.com", Password = "Admin123!" }
        );
        _output.WriteLine($"[Act] Login response status: {loginResponse.StatusCode}");
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<
            ServiceResult<AuthResponse>
        >();
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                loginResult!.Data!.AccessToken
            );
        _output.WriteLine("[Act] Admin authenticated successfully");

        // Act - Create a product which should trigger ProductCreatedEvent -> AuditEvent
        var productName = _faker.Commerce.ProductName();
        _output.WriteLine($"[Act] Creating product: {productName}");
        var createResponse = await client.PostAsJsonAsync(
            "/api/v1/products",
            new
            {
                Title = productName,
                Price = 100.0,
                Description = "Test audit",
                Category = "electronics",
                Image = "https://example.com/img.jpg",
            }
        );
        _output.WriteLine($"[Act] Create product response status: {createResponse.StatusCode}");

        if (!createResponse.IsSuccessStatusCode)
        {
            var error = await createResponse.Content.ReadAsStringAsync();
            _output.WriteLine($"[Error] Product creation failed: {error}");
            throw new Exception(
                $"Product creation failed with status {createResponse.StatusCode}. Error: {error}"
            );
        }
        createResponse.EnsureSuccessStatusCode();
        _output.WriteLine("[Act] Product created successfully");

        // Assert - Verify the audit event was stored in the Outbox (using Outbox pattern)
        _output.WriteLine("[Assert] Verifying audit event in Outbox table");
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Wait a bit for the event to be persisted
            await Task.Delay(500);

            var outboxMessages = await dbContext.OutboxMessages
                .Where(m => m.Type.Contains("AuditEvent") || m.Type.Contains("ProductCreatedEvent"))
                .ToListAsync();

            _output.WriteLine($"[Assert] Found {outboxMessages.Count} outbox message(s)");

            foreach (var msg in outboxMessages)
            {
                _output.WriteLine($"[Assert] Outbox message - Type: {msg.Type}, OccurredOn: {msg.OccurredOnUtc}, Processed: {msg.IsProcessed}");
            }

            // Should have at least one event (ProductCreatedEvent which triggers AuditEvent)
            Assert.NotEmpty(outboxMessages);
            _output.WriteLine("[Assert] Outbox contains domain events - Outbox pattern working correctly");
        }

        // Verify audit log was created in database
        _output.WriteLine("[Assert] Verifying audit log in database");
        using (var scope = _factory.Services.CreateScope())
        {
            var auditContext = scope.ServiceProvider.GetRequiredService<IAuditDbContext>();

            var auditLog = await ((AuditDbContext)auditContext).AuditLogs
                .OrderByDescending(x => x.Timestamp)
                .FirstOrDefaultAsync();

            _output.WriteLine($"[Assert] Audit log found: {auditLog != null}");

            if (auditLog != null)
            {
                _output.WriteLine($"[Assert] Audit log - EntityName: {auditLog.EntityName}, Action: {auditLog.Action}, Timestamp: {auditLog.Timestamp}");
            }

            Assert.NotNull(auditLog);
            Assert.Equal("Product", auditLog.EntityName);
            Assert.Equal("Added", auditLog.Action);
            Assert.NotNull(auditLog.Hash);
            Assert.NotNull(auditLog.PreviousHash);

            _output.WriteLine("[Assert] Audit log verified successfully");
        }

        _output.WriteLine("[Test] SaveEntity_Should_Publish_IAuditEvent passed - Events stored in Outbox and Audit log created");
    }
}
