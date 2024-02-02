using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZajeciaREST.Domain.Request;
using ZajeciaREST.Domain.Response;

namespace ZajeciaREST.ComponentTest;
public class BasicTest
{

    private readonly HttpClient _client;

    public BasicTest()
    {
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile(configPath);
            });
        });
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateProduct_ReturnsOkResult()
    {
        // Arrange

        var product = new AddProductRequest { Name = "New Product", Price = 10.00 };

        // Act
        var response = await _client.PostAsJsonAsync("/api/product", product);
        response.EnsureSuccessStatusCode();
        var createdProduct = await response.Content.ReadFromJsonAsync<ProductResponse>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        createdProduct.Should().NotBeNull();
        createdProduct.Name.Should().Be("New Product");
        createdProduct.Price.Should().Be(10.00);
    }


    [Fact]
    public async Task GetCreatedProduct_ReturnsProduct()
    {
        // Arrange

        var product = new AddProductRequest { Name = "New Product", Price = 10.00 };

        // Act
        var response = await _client.PostAsJsonAsync("/api/product", product);
        response.EnsureSuccessStatusCode();
        var createdProduct = await response.Content.ReadFromJsonAsync<ProductResponse>();

        var responseGet = await _client.GetAsync($"api/product/{createdProduct.Id}");
        responseGet.EnsureSuccessStatusCode();
        var getProduct = await responseGet.Content.ReadFromJsonAsync<ProductResponse>();

        // Assert
        responseGet.StatusCode.Should().Be(HttpStatusCode.OK);
        getProduct.Should().NotBeNull();
        getProduct.Name.Should().Be(createdProduct.Name);
        getProduct.Price.Should().Be(createdProduct.Price);
    }

}
