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
                                      FinalPrice = Product.GetFinalPrice(foods.Price, foods.Quantity, productTypes.TypeTax),
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

                return finalResults;
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
                if (AllFoodsAreValid(newFoods))
                {
                    var distinctsProductTypes = newFoods.Select(food => food.ProductTypeId).Distinct().ToList();

                    if (distinctsProductTypes.Where(productType => productType != Enumerators.ProductType.Food.GetId()).Any())
                    {
                        throw new ArgumentException("One or more foods submitted are not Food");
                    }

                    DataLayer.Food.AddFoods(newFoods);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void UpdateFoods(IEnumerable<Models.Food> foodsUpdated)
        {
            try
            {
                if (AllFoodsAreValid(foodsUpdated))
                {
                    var ids = foodsUpdated.Select(food => food.Id).ToList();
                    var foodsToUpdate = DataLayer.Food.GetFoods(ids);

                    foreach (var food in foodsToUpdate)
                    {
                        var foodToUpdate = foodsToUpdate.Where(food => food.Id == food.Id).Single();
                        if (food.ProductTypeId != foodToUpdate.ProductTypeId)
                        {
                            throw new ArgumentException("One or more foods submitted are trying to change the ProductType");
                        }
                    }

                    DataLayer.Food.UpdateFoods(foodsUpdated);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void DeleteFoods(IEnumerable<int> ids)
        {
            try
            {
                DataLayer.Food.DeleteFoods(ids);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool AllFoodsAreValid(IEnumerable<Models.Food> foodsToValidate)
        {
            foreach (var food in foodsToValidate)
            {
                if (food.Weight < 0)
                {
                    throw new ArgumentException("One or more foods submitted has negative weight");
                }
            }

            return true;
        }
    }
}
