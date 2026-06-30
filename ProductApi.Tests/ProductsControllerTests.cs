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

    [Fact]
    public async Task GetById_Should_Return_Product_When_Exists()
    {
        var mockService = new Mock<IProductService>();

        mockService.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(new Product { Id = 1, Name = "PC", Price = 1000 });

        var controller = new ProductsController(mockService.Object);

        var result = await controller.GetById(1);

        var ok = result as OkObjectResult;

        ok.Should().NotBeNull();
    }

    [Fact]
    public async Task GetById_Should_Return_NotFound_When_Missing()
    {
        var mockService = new Mock<IProductService>();

        mockService.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync((Product?)null);

        var controller = new ProductsController(mockService.Object);

        var result = await controller.GetById(1);

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Create_Should_Return_CreatedProduct()
    {
        var mockService = new Mock<IProductService>();

        var product = new Product { Id = 1, Name = "PC", Price = 1000 };

        mockService.Setup(s => s.CreateAsync(product))
            .ReturnsAsync(product);

        var controller = new ProductsController(mockService.Object);

        var result = await controller.Create(product);

        var created = result as CreatedAtActionResult;

        created.Should().NotBeNull();
        created!.Value.Should().Be(product);
    }

    [Fact]
    public async Task Update_Should_Return_NoContent_When_Success()
    {
        var mockService = new Mock<IProductService>();

        mockService.Setup(s => s.UpdateAsync(1, It.IsAny<Product>()))
            .ReturnsAsync(true);

        var controller = new ProductsController(mockService.Object);

        var result = await controller.Update(1, new Product());

        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Update_Should_Return_NotFound_When_Fails()
    {
        var mockService = new Mock<IProductService>();

        mockService.Setup(s => s.UpdateAsync(1, It.IsAny<Product>()))
            .ReturnsAsync(false);

        var controller = new ProductsController(mockService.Object);

        var result = await controller.Update(1, new Product());

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Delete_Should_Return_NoContent_When_Success()
    {
        var mockService = new Mock<IProductService>();

        mockService.Setup(s => s.DeleteAsync(1))
            .ReturnsAsync(true);

        var controller = new ProductsController(mockService.Object);

        var result = await controller.Delete(1);

        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Delete_Should_Return_NotFound_When_Fails()
    {
        var mockService = new Mock<IProductService>();

        mockService.Setup(s => s.DeleteAsync(1))
            .ReturnsAsync(false);

        var controller = new ProductsController(mockService.Object);

        var result = await controller.Delete(1);

        result.Should().BeOfType<NotFoundResult>();
    }
}