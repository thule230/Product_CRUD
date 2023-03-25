using Microsoft.AspNetCore.Mvc;

namespace Product_CRUD.Controllers;

[ApiController]
[Route("[controller]")]
public class DrinkController : ControllerBase
{
    private readonly ILogger<DrinkController> _logger;

    public DrinkController(ILogger<DrinkController> logger)
    {
        _logger = logger;
    }

    [HttpGet("[action]")]
    public ActionResult Get([FromQuery] IEnumerable<int>? ids, string? descriptionSearch, decimal? minPrice, decimal? maxPrice, int? minQuantity, int? maxQuantity, decimal? minFinalPrice, decimal? maxFinalPrice, decimal? minCapacity, decimal? maxCapacity, int? capacityUnit)
    {
        try
        {
            var drinks = BusinessLayer.Drink.Get(descriptionSearch, minPrice, maxPrice, minQuantity, maxQuantity, minFinalPrice, maxFinalPrice, minCapacity, maxCapacity, capacityUnit, ids);

            if (drinks == null || !drinks.Any()) return NoContent();

            return Ok(drinks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("[action]")]
    public ActionResult Add([FromBody] IEnumerable<Models.Drink> newDrinks)
    {
        try
        {
            BusinessLayer.Drink.Add(newDrinks);

            return Created(string.Empty, newDrinks);
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
    public ActionResult Update(IEnumerable<Models.Drink> drinksNewValues)
    {
        try
        {
            BusinessLayer.Drink.Update(drinksNewValues);

            return Ok(drinksNewValues);
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
    public ActionResult Delete(IEnumerable<int> ids)
    {
        try
        {
            BusinessLayer.Drink.Delete(ids);

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

