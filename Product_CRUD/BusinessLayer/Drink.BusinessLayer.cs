using Product_CRUD.Enumerators;

namespace Product_CRUD.BusinessLayer
{
    public static class Drink
    {
        public static IEnumerable<object> Get(string? descriptionSearch, decimal? minPrice, decimal? maxPrice, int? minQuantity, int? maxQuantity, decimal? minFinalPrice, decimal? maxFinalPrice, decimal? minCapacity = null, decimal? maxCapacity = null, int? capacityUnit = null, IEnumerable<int>? ids = null)
        {
            try
            {
                var results = DataLayer.Drink.Get(ids, descriptionSearch, minPrice, maxPrice, minQuantity, maxQuantity, minCapacity, maxCapacity, capacityUnit);

                var productTypesIds = results.Select(result => result.ProductTypeId).Distinct().ToList();

                var productTypesResults = DataLayer.ProductType.Get(productTypesIds);

                var capacityUnitiesIds = results.Select(result => result.CapacityUnitId).Distinct().ToList();

                var capacityUnitiesResult = DataLayer.CapacityUnity.Get(capacityUnitiesIds);

                var resultsJoin = from drinks in results
                                  join productTypes in productTypesResults
                                  on drinks.ProductTypeId equals productTypes.Id
                                  join capacityUnities in capacityUnitiesResult
                                  on drinks.CapacityUnitId equals capacityUnities.Id
                                  select new
                                  {
                                      drinks.Id,
                                      drinks.Description,
                                      drinks.Price,
                                      drinks.Quantity,
                                      drinks.Capacity,
                                      FinalPrice = Product.GetFinalPrice(drinks.Price, drinks.Quantity, productTypes.TypeTax),
                                      CapacityUnit = new { capacityUnities.Id, capacityUnities.Description },
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

        public static void Add(IEnumerable<Models.Drink> newDrinks)
        {
            try
            {
                if (AllDrinksAreValid(newDrinks))
                {
                    var distinctsProductTypes = newDrinks.Select(drink => drink.ProductTypeId).Distinct().ToList();

                    if (distinctsProductTypes.Where(productType => productType != ProductType.Drink.GetId()).Any())
                    {
                        throw new ArgumentException("One or more drinks submitted are not Drink");
                    }

                    DataLayer.Drink.Add(newDrinks);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Update(IEnumerable<Models.Drink> drinksNewValues)
        {
            try
            {
                if (AllDrinksAreValid(drinksNewValues))
                {
                    var ids = drinksNewValues.Select(drink => drink.Id).ToList();
                    var drinksToUpdate = DataLayer.Drink.Get(ids);

                    foreach (var drink in drinksToUpdate)
                    {
                        var drinkToUpdateFound = drinksToUpdate.Where(drinkToUpdate => drinkToUpdate.Id == drink.Id).Single();
                        if (drink.ProductTypeId != drinkToUpdateFound.ProductTypeId)
                        {
                            throw new ArgumentException("One or more drinks submitted are trying to change the ProductType");
                        }
                    }

                    DataLayer.Drink.Update(drinksNewValues);
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
                    DataLayer.Drink.Delete(ids);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool AllDrinksAreValid(IEnumerable<Models.Drink> drinksToValidate)
        {
            foreach (var drink in drinksToValidate)
            {
                if (drink.Capacity < 0)
                {
                    throw new ArgumentException("One or more drinks submitted has negative capacity");
                }
            }

            return true;
        }
    }
}
