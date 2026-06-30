using Xunit;
using Moq;
using FluentAssertions;
using ProductApi.Controllers;
using ProductApi.Services;
using ProductApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Tests;

public class ProductsControllerTests
{
    [Fact]
    public async Task GetAll_Should_Return_Products()
    {
        // Arrange
        var mockService = new Mock<IProductService>();

        mockService.Setup(s => s.GetAllAsync())
            .ReturnsAsync(new List<Product>
            {
                new Product { Id = 1, Name = "PC", Price = 1000 },
                new Product { Id = 2, Name = "Mouse", Price = 50 }
            });

        var controller = new ProductsController(mockService.Object);

        // Act
        var result = await controller.GetAll();

        // Assert
        var okResult = result as OkObjectResult;

        okResult.Should().NotBeNull();
        var products = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult!.Value);

        products.Should().HaveCount(2);
    }
}