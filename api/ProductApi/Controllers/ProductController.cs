using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private static readonly List<Product> _products = new();
    private static int _nextId = 1;

    // GET: api/products
    [HttpGet]
    public ActionResult<List<Product>> GetAll()
    {
        return Ok(_products);
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound(new { message = "Product not found" });

        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Create(Product product)
    {
        // validação simples
        if (string.IsNullOrWhiteSpace(product.Name))
            return BadRequest(new { message = "Name is required" });

        if (product.Price <= 0)
            return BadRequest(new { message = "Price must be greater than 0" });

        product.Id = _nextId++;
        _products.Add(product);

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Product updatedProduct)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound(new { message = "Product not found" });

        if (string.IsNullOrWhiteSpace(updatedProduct.Name))
            return BadRequest(new { message = "Name is required" });

        if (updatedProduct.Price <= 0)
            return BadRequest(new { message = "Price must be greater than 0" });

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound(new { message = "Product not found" });

        _products.Remove(product);

        return NoContent();
    }
}