﻿using Microsoft.AspNetCore.Mvc;

namespace Product_CRUD.Controllers;

[ApiController]
[Route("[controller]")]
public class FoodController : ControllerBase
{
    private readonly ILogger<FoodController> _logger;

    public FoodController(ILogger<FoodController> logger)
    {
        _logger = logger;
    }

    [HttpGet("[action]")]
    public ActionResult Get([FromQuery] IEnumerable<int>? ids, string? descriptionSearch, decimal? minPrice, decimal? maxPrice, int? minQuantity, int? maxQuantity, decimal? minFinalPrice, decimal? maxFinalPrice, decimal? minWeight, decimal? maxWeight, int? weightUnit)
    {
        try
        {
            var foods = BusinessLayer.Food.Get(descriptionSearch, minPrice, maxPrice, minQuantity, maxQuantity, minFinalPrice, maxFinalPrice, minWeight, maxWeight, weightUnit, ids);

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
    public ActionResult Add([FromBody] IEnumerable<Models.Food> newFoods)
    {
        try
        {
            BusinessLayer.Food.Add(newFoods);

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
    public ActionResult Update(IEnumerable<Models.Food> foodsNewValues)
    {
        try
        {
            BusinessLayer.Food.Update(foodsNewValues);

            return Ok(foodsNewValues);
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
            BusinessLayer.Food.Delete(ids);

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

