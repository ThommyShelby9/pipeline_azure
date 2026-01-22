using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using ShoppingProject.Application.Common.Models;
using ShoppingProject.Tests.Infrastructure;

namespace ShoppingProject.UnitTests.IntegrationTests
{
    public class CacheIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public CacheIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        private async Task AuthenticateAsync()
        {
            // Login
            var loginResponse = await _client.PostAsJsonAsync("/api/v1/identity/login", new
            {
                Email = "admin@test.com",
                Password = "Admin123!"
            });

            loginResponse.EnsureSuccessStatusCode();

            var loginResult = await loginResponse.Content.ReadFromJsonAsync<ServiceResult<AuthResponse>>();

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", loginResult!.Data!.AccessToken);
        }

        [Fact]
        public async Task SetValue_Should_Return_OK()
        {
            await AuthenticateAsync();

            var response = await _client.PostAsJsonAsync(
                "/api/v1/cache/set",
                new { Key = "test-key", Value = "test-value" }
            );

            response.EnsureSuccessStatusCode();

            var getResponse = await _client.GetAsync("/api/v1/cache/test-key");
            getResponse.EnsureSuccessStatusCode();

            var result = await getResponse.Content.ReadFromJsonAsync<ServiceResult<string>>();
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("test-value", result.Data);
        }

        [Fact]
        public async Task GetValue_Should_Return_Value_When_Exists()
        {
            await AuthenticateAsync();

            await _client.PostAsJsonAsync(
                "/api/v1/cache/set",
                new { Key = "test-key", Value = "test-value" }
            );

            var response = await _client.GetAsync("/api/v1/cache/test-key");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<string>>();
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("test-value", result.Data);
        }

        [Fact]
        public async Task GetValue_Should_Return_NotFound_When_NotExists()
        {
            await AuthenticateAsync();

            var response = await _client.GetAsync("/api/v1/cache/non-existent-key");
            response.EnsureSuccessStatusCode(); // HTTP 200 OK

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<string>>();
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task DeleteValue_Should_Return_OK()
        {
            await AuthenticateAsync();

            await _client.PostAsJsonAsync(
                "/api/v1/cache/set",
                new { Key = "delete-key", Value = "delete-value" }
            );

            var response = await _client.DeleteAsync("/api/v1/cache/delete-key");
            response.EnsureSuccessStatusCode();

            var getResponse = await _client.GetAsync("/api/v1/cache/delete-key");
            getResponse.EnsureSuccessStatusCode(); // HTTP 200 OK

            var result = await getResponse.Content.ReadFromJsonAsync<ServiceResult<string>>();
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
