using System.Net.Http.Json;
using Bogus;
using MassTransit;
using MassTransit.Testing;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingProject.Application.Common.Interfaces;
using ShoppingProject.Application.Common.Models;
using ShoppingProject.Application.Contracts.Audit;
using ShoppingProject.Application.DTOs;
using ShoppingProject.Domain.Entities;
using ShoppingProject.Infrastructure.Bus.Events;
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

        // Arrange - Use default factory which already has InMemory database configured
        _output.WriteLine("[Arrange] Creating HTTP client");
        var client = _factory.CreateClient();

        _output.WriteLine("[Arrange] Getting MassTransit test harness");
        var harness = _factory.Services.GetRequiredService<ITestHarness>();

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

        // Act
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

        // Assert
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

        // Wait for the outbox processor and MassTransit to process the message
        _output.WriteLine("[Act] Waiting 2 seconds for message processing");
        await Task.Delay(2000);

        // Wait for the audit event to be published
        _output.WriteLine("[Assert] Checking if IAuditEvent was published");
        var published = await harness.Published.Any<IAuditEvent>(x => x.Context.Message.EntityName == "Product");
        _output.WriteLine($"[Assert] IAuditEvent published: {published}");

        if (!published)
        {
            _output.WriteLine("[Debug] Listing all published messages:");
            var allPublished = harness.Published.Select<object>().ToList();
            _output.WriteLine($"[Debug] Total published messages: {allPublished.Count}");
            foreach (var msg in allPublished)
            {
                _output.WriteLine($"[Debug] - Message type: {msg.MessageType}");
            }
        }

        Assert.True(published);

        // Verify database persistence
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
                _output.WriteLine($"[Assert] Audit log - EntityName: {auditLog.EntityName}, Action: {auditLog.Action}");
            }

            Assert.NotNull(auditLog);
            Assert.Equal("Product", auditLog.EntityName);
            Assert.Equal("Added", auditLog.Action);
            Assert.NotNull(auditLog.Hash);
            Assert.NotNull(auditLog.PreviousHash);
        }

        var publishedMessage = harness.Published.Select<IAuditEvent>().First();
        _output.WriteLine($"[Assert] Published message - Action: {publishedMessage.Context.Message.Action}, EntityName: {publishedMessage.Context.Message.EntityName}");
        Assert.Equal("Added", publishedMessage.Context.Message.Action);
        Assert.Equal("Product", publishedMessage.Context.Message.EntityName);
        Assert.NotNull(publishedMessage.Context.Message.CorrelationId);
        _output.WriteLine("[Test] SaveEntity_Should_Publish_IAuditEvent passed");
    }
}
