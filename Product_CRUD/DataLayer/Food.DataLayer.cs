namespace Product_CRUD.DataLayer
{
    public static class Food
    {
        public static IEnumerable<Models.Food> GetFoods(IEnumerable<int>? ids, string? descriptionSearch, decimal? minPrice, decimal? maxPrice, int? minQuantity, int? maxQuantity, decimal? minWeight, decimal? maxWeight, int? weightUnit)
        {
            try
            {
                using var context = new ApplicationDbContext();

                var query = from foods in context.Foods
                            select foods;

                if (ids != null && ids.Any())
                {
                    query = query.Where(food => ids.Contains(food.Id));
                }

                if (descriptionSearch != null)
                {
                    query = query.Where(food => food.Description.Contains(descriptionSearch));
                }

                if (minPrice.HasValue)
                {
                    query = query.Where(food => food.Price >= minPrice.Value);
                }

                if (maxPrice.HasValue)
                {
                    query = query.Where(food => food.Price <= maxPrice.Value);
                }

                if (minQuantity.HasValue)
                {
                    query = query.Where(food => food.Quantity >= minQuantity.Value);
                }

                if (maxQuantity.HasValue)
                {
                    query = query.Where(food => food.Quantity <= maxQuantity.Value);
                }

                if (minWeight.HasValue)
                {
                    query = query.Where(food => food.Weight <= minWeight.Value);
                }

                if (maxWeight.HasValue)
                {
                    query = query.Where(food => food.Weight >= maxWeight.Value);
                }

                if (weightUnit.HasValue)
                {
                    query = query.Where(food => food.WeightUnitId == weightUnit.Value);
                }

                return query.ToList();
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

                var productsTypesFounds = DataLayer.ProductType.GetProductTypes(distinctsProductTypes);

                if (!productsTypesFounds.Any() || distinctsProductTypes.Count != productsTypesFounds.Count())
                {
                    throw new Exception("One or more products submitted has a invalid ProductType");
                }

                var distinctsWeightUnits = newFoods.Select(food => food.WeightUnitId).Distinct().ToList();

                var WeightUnitsFounds = DataLayer.WeightUnity.GetWeightUnities(distinctsWeightUnits);

                if (!WeightUnitsFounds.Any() || distinctsWeightUnits.Count != WeightUnitsFounds.Count())
                {
                    throw new Exception("One or more products submitted has a invalid WeightUnity");
                }

                using var context = new ApplicationDbContext();

                context.Foods.AddRange(newFoods);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}