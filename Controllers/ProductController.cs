using Microsoft.AspNetCore.Mvc;

namespace Product_CRUD.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    [HttpGet("[action]")]
    public ActionResult GetProducts(int? id, string? descriptionSearch, decimal? minPrice, decimal? maxPrice, int? minQuantity, int? maxQuantity, decimal? minFinalPrice, decimal? maxFinalPrice)
    {
        return Ok();
    }

    [HttpGet("[action]")]
    public ActionResult GetDrinks(int? id, string? descriptionSearch, decimal? minPrice, decimal? maxPrice, int? minQuantity, int? maxQuantity, decimal? minFinalPrice, decimal? maxFinalPrice, decimal? minCapacity, decimal? maxCapacity, int? capacityUnit)
    {
        return Ok();
    }

    [HttpGet("[action]")]
    public ActionResult GetFoods(int? id, string? descriptionSearch, decimal? minPrice, decimal? maxPrice, int? minQuantity, int? maxQuantity, decimal? minFinalPrice, decimal? maxFinalPrice, decimal? minWeight, decimal? maxWeight, int? weightUnit)
    {
        var foods = BusinessLayer.Food.GetFoods(id, descriptionSearch, minPrice, maxPrice, minQuantity, maxQuantity, minFinalPrice, maxFinalPrice, minWeight, maxWeight, weightUnit);

        if (foods == null || !foods.Any()) return NoContent();

        return Ok(foods);
    }

    [HttpGet("[action]")]
    public ActionResult GetClothings(int? id, string? descriptionSearch, decimal? minPrice, decimal? maxPrice, int? minQuantity, int? maxQuantity, decimal? minFinalPrice, decimal? maxFinalPrice, int? size)
    {
        return Ok();
    }
}