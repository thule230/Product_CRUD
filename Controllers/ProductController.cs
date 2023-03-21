using Microsoft.AspNetCore.Mvc;
using Product_CRUD.Models;

namespace Product_CRUD.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetAllProducts")]
    public IEnumerable<Product> GetAllProducts()
    {
        var result = new List<Drink>();

        result.Add(new Drink());

        return result;
    }
}
