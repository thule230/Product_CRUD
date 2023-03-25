using Microsoft.AspNetCore.Mvc;

namespace Product_CRUD.Controllers;

[ApiController]
[Route("[controller]")]
public class FoodController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;

    public FoodController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpGet("[action]")]
    public ActionResult GetFoods([FromQuery] IEnumerable<int>? ids, string? descriptionSearch, decimal? minPrice, decimal? maxPrice, int? minQuantity, int? maxQuantity, decimal? minFinalPrice, decimal? maxFinalPrice, decimal? minWeight, decimal? maxWeight, int? weightUnit)
    {
        try
        {
            var foods = BusinessLayer.Food.GetFoods(ids, descriptionSearch, minPrice, maxPrice, minQuantity, maxQuantity, minFinalPrice, maxFinalPrice, minWeight, maxWeight, weightUnit);

            if (foods == null || !foods.Any()) return NoContent();

            return Ok(foods);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("[action]")]
    public ActionResult AddFoods([FromBody] IEnumerable<Models.Food> newFoods)
    {
        try
        {
            BusinessLayer.Food.AddFoods(newFoods);

            return Created(string.Empty, newFoods);
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

    [HttpPut("[action]")]
    public ActionResult UpdateFoods(IEnumerable<Models.Food> foods)
    {
        try
        {
            BusinessLayer.Food.UpdateFoods(foods);

            return Ok(foods);
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

    [HttpDelete("[action]")]
    public ActionResult DeleteFoods(IEnumerable<int> ids)
    {
        try
        {
            BusinessLayer.Food.DeleteFoods(ids);

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

