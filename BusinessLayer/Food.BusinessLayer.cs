using Product_CRUD.Enumerators;

namespace Product_CRUD.BusinessLayer
{
    public static class Food
    {
        public static IEnumerable<object> GetFoods(IEnumerable<int>? ids, string? descriptionSearch, decimal? minPrice, decimal? maxPrice, int? minQuantity, int? maxQuantity, decimal? minFinalPrice, decimal? maxFinalPrice, decimal? minWeight, decimal? maxWeight, int? weightUnit)
        {
            try
            {
                var results = DataLayer.Food.GetFoods(ids, descriptionSearch, minPrice, maxPrice, minQuantity, maxQuantity, minWeight, maxWeight, weightUnit);

                var productTypesIds = results.Select(result => result.ProductTypeId).Distinct().ToList();

                var productTypesResults = DataLayer.ProductType.GetProductTypes(productTypesIds);

                var weightUnitsIds = results.Select(result => result.WeightUnitId).Distinct().ToList();

                var weightUnitsResult = DataLayer.WeightUnity.GetWeightUnities(weightUnitsIds);

                var resultsJoin = from foods in results
                                  join productTypes in productTypesResults
                                  on foods.ProductTypeId equals productTypes.Id
                                  join weightUnits in weightUnitsResult
                                  on foods.WeightUnitId equals weightUnits.Id
                                  select new
                                  {
                                      foods.Id,
                                      foods.Description,
                                      foods.Price,
                                      foods.Quantity,
                                      foods.Weight,
                                      FinalPrice = (foods.Price * foods.Quantity + (foods.Price * productTypes.TypeTax)),
                                      weightUnit = new { weightUnits.Id, weightUnits.Description },
                                      productType = new { productTypes.Id, productTypes.Description, productTypes.TypeTax }
                                  };

                var finalResults = resultsJoin.ToList();

                if (minFinalPrice.HasValue)
                {
                    finalResults = finalResults.Where(finalResult => finalResult.FinalPrice >= minFinalPrice.Value).ToList();
                }

                if (maxFinalPrice.HasValue)
                {
                    finalResults = finalResults.Where(finalResult => finalResult.FinalPrice <= maxFinalPrice.Value).ToList();
                }

                return resultsJoin;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void AddFoods(IEnumerable<Models.Food> newFoods)
        {
            try
            {
                var distinctsProductTypes = newFoods.Select(food => food.ProductTypeId).Distinct().ToList();

                if (distinctsProductTypes.Where(productType => productType != Enumerators.ProductType.Food.GetId()).Any())
                {
                    throw new Exception("One or more products submitted are not Food");
                }

                foreach (var food in newFoods)
                {
                    if (food.Weight < 0)
                    {
                        throw new Exception("One or more products submitted has negative weight");
                    }
                }

                DataLayer.Food.AddFoods(newFoods);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
