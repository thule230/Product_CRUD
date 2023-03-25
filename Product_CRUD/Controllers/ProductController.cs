using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("[action]")]
    public ActionResult Get(string? descriptionSearch, decimal? minPrice, decimal? maxPrice, int? minQuantity, int? maxQuantity, decimal? minFinalPrice, decimal? maxFinalPrice)
    {
        try
        {
            var products = BusinessLayer.Clothing.Get(descriptionSearch, minPrice, maxPrice, minQuantity, maxQuantity, minFinalPrice, maxFinalPrice);
            products = products.Concat(BusinessLayer.Drink.Get(descriptionSearch, minPrice, maxPrice, minQuantity, maxQuantity, minFinalPrice, maxFinalPrice));
            products = products.Concat(BusinessLayer.Food.Get(descriptionSearch, minPrice, maxPrice, minQuantity, maxQuantity, minFinalPrice, maxFinalPrice));

            if (!products.Any()) return NoContent();

            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("[action]")]
    public ActionResult Delete(IEnumerable<Models.Product> products)
    {
        try
        {
            BusinessLayer.Product.Delete(products);

            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}