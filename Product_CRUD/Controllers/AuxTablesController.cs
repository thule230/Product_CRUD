using Microsoft.AspNetCore.Mvc;

namespace Product_CRUD.Controllers;

[ApiController]
[Route("[controller]")]
public class AuxTablesController : ControllerBase
{
    [HttpPost("[action]")]
    public ActionResult PopulateAuxTables()
    {
        if (!DataLayer.ProductType.GetProductTypes().Any())
        {
            var productTypes = new List<Models.ProductType>
            {
                new Models.ProductType { Description = "Drink", TypeTax = 0.05M },
                new Models.ProductType { Description = "Food", TypeTax = 0.06M },
                new Models.ProductType { Description = "Clothing", TypeTax = 0.07M }
            };
            DataLayer.ProductType.AddProductType(productTypes);
        }

        if (!DataLayer.CapacityUnity.GetCapacityUnities().Any())
        {
            var capacityUnities = new List<Models.CapacityUnity>
            {
                new Models.CapacityUnity { Description = "cL" },
                new Models.CapacityUnity { Description = "mL" },
                new Models.CapacityUnity { Description = "L" }
            };
            DataLayer.CapacityUnity.AddCapacityUnities(capacityUnities);
        }

        if (!DataLayer.WeightUnity.GetWeightUnities().Any())
        {
            var weightUnities = new List<Models.WeightUnity>
            {
                new Models.WeightUnity { Description = "mg" },
                new Models.WeightUnity { Description = "g" },
                new Models.WeightUnity { Description = "kg" }
            };
            DataLayer.WeightUnity.AddWeightUnities(weightUnities);
        }

        if (!DataLayer.ClothingSize.GetClothingSizes().Any())
        {
            var clothingSizes = new List<Models.ClothingSize>
            {
                new Models.ClothingSize { Description = "XS" },
                new Models.ClothingSize { Description = "S" },
                new Models.ClothingSize { Description = "M" },
                new Models.ClothingSize { Description = "L" },
                new Models.ClothingSize { Description = "XL" }
            };

            DataLayer.ClothingSize.AddClothingSizes(clothingSizes);
        }

        return Ok();
    }

    [HttpGet("[action]")]
    public ActionResult GetProductTypes()
    {
        return Ok(DataLayer.ProductType.GetProductTypes());
    }

    [HttpGet("[action]")]
    public ActionResult GetCapacityUnities()
    {
        return Ok(DataLayer.CapacityUnity.GetCapacityUnities());
    }

    [HttpGet("[action]")]
    public ActionResult GetClothingSizes()
    {
        return Ok(DataLayer.ClothingSize.GetClothingSizes());
    }

    [HttpGet("[action]")]
    public ActionResult GetWeightUnities()
    {
        return Ok(DataLayer.WeightUnity.GetWeightUnities());
    }

}

