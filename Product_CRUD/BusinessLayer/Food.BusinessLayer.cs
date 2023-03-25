using Product_CRUD.Enumerators;

namespace Product_CRUD.BusinessLayer
{
    public static class Food
    {
        public static IEnumerable<object> Get(string? descriptionSearch, decimal? minPrice, decimal? maxPrice, int? minQuantity, int? maxQuantity, decimal? minFinalPrice, decimal? maxFinalPrice, decimal? minWeight = null, decimal? maxWeight = null, int? weightUnit = null, IEnumerable<int>? ids = null)
        {
            try
            {
                var results = DataLayer.Food.Get(ids, descriptionSearch, minPrice, maxPrice, minQuantity, maxQuantity, minWeight, maxWeight, weightUnit);

                var productTypesIds = results.Select(result => result.ProductTypeId).Distinct().ToList();

                var productTypesResults = DataLayer.ProductType.Get(productTypesIds);

                var weightUnitiesIds = results.Select(result => result.WeightUnitId).Distinct().ToList();

                var weightUnitiesResult = DataLayer.WeightUnity.Get(weightUnitiesIds);

                var resultsJoin = from foods in results
                                  join productTypes in productTypesResults
                                  on foods.ProductTypeId equals productTypes.Id
                                  join weightUnities in weightUnitiesResult
                                  on foods.WeightUnitId equals weightUnities.Id
                                  select new
                                  {
                                      foods.Id,
                                      foods.Description,
                                      foods.Price,
                                      foods.Quantity,
                                      foods.Weight,
                                      FinalPrice = Product.GetFinalPrice(foods.Price, foods.Quantity, productTypes.TypeTax),
                                      WeightUnit = new { weightUnities.Id, weightUnities.Description },
                                      ProductType = new { productTypes.Id, productTypes.Description, productTypes.TypeTax }
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

        public static void Add(IEnumerable<Models.Food> newFoods)
        {
            try
            {
                if (AllFoodsAreValid(newFoods))
                {
                    var distinctsProductTypes = newFoods.Select(food => food.ProductTypeId).Distinct().ToList();

                    if (distinctsProductTypes.Where(productType => productType != ProductType.Food.GetId()).Any())
                    {
                        throw new ArgumentException("One or more foods submitted are not Food");
                    }

                    DataLayer.Food.Add(newFoods);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Update(IEnumerable<Models.Food> foodsNewValues)
        {
            try
            {
                if (AllFoodsAreValid(foodsNewValues))
                {
                    var ids = foodsNewValues.Select(food => food.Id).ToList();
                    var foodsToUpdate = DataLayer.Food.Get(ids);

                    foreach (var food in foodsToUpdate)
                    {
                        var foodToUpdateFound = foodsToUpdate.Where(foodToUpdate => foodToUpdate.Id == food.Id).Single();
                        if (food.ProductTypeId != foodToUpdateFound.ProductTypeId)
                        {
                            throw new ArgumentException("One or more foods submitted are trying to change the ProductType");
                        }
                    }

                    DataLayer.Food.Update(foodsNewValues);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Delete(IEnumerable<int> ids)
        {
            try
            {
                if (ids.Any())
                {
                    DataLayer.Food.Delete(ids);
                }
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
