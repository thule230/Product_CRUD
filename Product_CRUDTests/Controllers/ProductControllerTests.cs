using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Product_CRUD.DataLayer;

namespace Product_CRUD.Controllers.Tests
{
    [TestClass()]
    public class ProductControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            var context = new ApplicationDbContext();
            context.ProductTypes.Add(new Models.ProductType { Description = "Drink", TypeTax = 0.05M });
            context.CapacityUnities.Add(new Models.CapacityUnity { Description = "cL" });
            context.Drinks.Add(new Models.Drink { Price = 24, Quantity = 5, ProductTypeId = 1, CapacityUnitId = 1 });
            context.SaveChanges();

            var mockLogger = new Mock<ILogger<ProductController>>();
            var controller = new ProductController(mockLogger.Object);

            var resultWithStockFilter = (NoContentResult)controller.Get(null, null, null, 6, null, null, null);
            Assert.AreEqual(204, resultWithStockFilter.StatusCode);

            var resultWithFinalPriceFilter = (NoContentResult)controller.Get(null, null, null, null, null, 100, 120);
            Assert.AreEqual(204, resultWithFinalPriceFilter.StatusCode);

            var resultWithoutFilter = (OkObjectResult)controller.Get(null, null, null, null, null, null, null);
            Assert.AreEqual(200, resultWithoutFilter.StatusCode);

            var resultWithFilter = (OkObjectResult)controller.Get(null, null, null, 1, null, 100, 500);
            Assert.AreEqual(200, resultWithFilter.StatusCode);
        }
    }
}