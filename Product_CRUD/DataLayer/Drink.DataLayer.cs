namespace Product_CRUD.DataLayer
{
    public static class Drink
    {
        public static IEnumerable<Models.Drink> Get(IEnumerable<int>? ids = null, string? descriptionSearch = null, decimal? minPrice = null, decimal? maxPrice = null, int? minQuantity = null, int? maxQuantity = null, decimal? minCapacity = null, decimal? maxCapacity = null, int? capacityUnit = null)
        {
            try
            {
                using var context = new ApplicationDbContext();

                var query = from drinks in context.Drinks
                            select drinks;

                if (ids != null && ids.Any())
                {
                    query = query.Where(drink => ids.Contains(drink.Id));
                }

                if (descriptionSearch != null)
                {
                    query = query.Where(drink => drink.Description.Contains(descriptionSearch));
                }

                if (minPrice.HasValue)
                {
                    query = query.Where(drink => drink.Price >= minPrice.Value);
                }

                if (maxPrice.HasValue)
                {
                    query = query.Where(drink => drink.Price <= maxPrice.Value);
                }

                if (minQuantity.HasValue)
                {
                    query = query.Where(drink => drink.Quantity >= minQuantity.Value);
                }

                if (maxQuantity.HasValue)
                {
                    query = query.Where(drink => drink.Quantity <= maxQuantity.Value);
                }

                if (minCapacity.HasValue)
                {
                    query = query.Where(drink => drink.Capacity >= minCapacity.Value);
                }

                if (maxCapacity.HasValue)
                {
                    query = query.Where(drink => drink.Capacity <= maxCapacity.Value);
                }

                if (capacityUnit.HasValue)
                {
                    query = query.Where(drink => drink.CapacityUnitId == capacityUnit.Value);
                }

                return query.ToList();
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
                    using var context = new ApplicationDbContext();

                    context.Drinks.AddRange(newDrinks);
                    context.SaveChanges();
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
                    var context = new ApplicationDbContext();

                    var ids = drinksNewValues.Select(drinkNewValues => drinkNewValues.Id).ToList();

                    if (ids.Distinct().Count() != ids.Count)
                    {
                        throw new ArgumentException("One or more drinks submitted are repeated");
                    }

                    var drinksFound = Get(ids);

                    if (ids.Count != drinksFound.Count())
                    {
                        throw new ArgumentException("One or more drinks submitted were not found");
                    }

                    context.Drinks.UpdateRange(drinksNewValues);
                    context.SaveChanges();
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
                var context = new ApplicationDbContext();

                var drinksToDelete = Get(ids);
                context.Drinks.RemoveRange(drinksToDelete);

                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool AllDrinksAreValid(IEnumerable<Models.Drink> drinksToValidate)
        {
            var distinctsProductTypes = drinksToValidate.Select(drink => drink.ProductTypeId).Distinct().ToList();

            var productsTypesFounds = ProductType.Get(distinctsProductTypes);

            if (!productsTypesFounds.Any() || distinctsProductTypes.Count != productsTypesFounds.Count())
            {
                throw new ArgumentException("One or more drinks submitted has a invalid ProductType");
            }

            var distinctsCapacityUnities = drinksToValidate.Select(drinks => drinks.CapacityUnitId).Distinct().ToList();

            var capacityUnitiesFounds = CapacityUnity.Get(distinctsCapacityUnities);

            if (!capacityUnitiesFounds.Any() || distinctsCapacityUnities.Count != capacityUnitiesFounds.Count())
            {
                throw new ArgumentException("One or more drinks submitted has a invalid CapacityUnity");
            }

            return true;
        }
    }
}
