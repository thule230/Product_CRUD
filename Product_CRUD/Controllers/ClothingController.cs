using Microsoft.AspNetCore.Mvc;

namespace Product_CRUD.Controllers;

[ApiController]
[Route("[controller]")]
public class ClothingController : ControllerBase
{
    private readonly ILogger<ClothingController> _logger;

    public ClothingController(ILogger<ClothingController> logger)
    {
        _logger = logger;
    }

    [HttpGet("[action]")]
    public ActionResult Get([FromQuery] IEnumerable<int>? ids, string? descriptionSearch, decimal? minPrice, decimal? maxPrice, int? minQuantity, int? maxQuantity, decimal? minFinalPrice, decimal? maxFinalPrice, int? size)
    {
        try
        {
            var clothings = BusinessLayer.Clothing.Get(descriptionSearch, minPrice, maxPrice, minQuantity, maxQuantity, minFinalPrice, maxFinalPrice, size, ids);

            if (clothings == null || !clothings.Any()) return NoContent();

            return Ok(clothings);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("[action]")]
    public ActionResult Add([FromBody] IEnumerable<Models.Clothing> newClothings)
    {
        try
        {
            BusinessLayer.Clothing.Add(newClothings);

            return Created(string.Empty, newClothings);
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
    public ActionResult Update(IEnumerable<Models.Clothing> clothingsNewValues)
    {
        try
        {
            BusinessLayer.Clothing.Update(clothingsNewValues);

            return Ok(clothingsNewValues);
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
            BusinessLayer.Clothing.Delete(ids);

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

