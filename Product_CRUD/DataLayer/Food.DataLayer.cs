namespace Product_CRUD.DataLayer
{
    public static class Food
    {
        public static IEnumerable<Models.Food> GetFoods(IEnumerable<int>? ids = null, string? descriptionSearch = null, decimal? minPrice = null, decimal? maxPrice = null, int? minQuantity = null, int? maxQuantity = null, decimal? minWeight = null, decimal? maxWeight = null, int? weightUnit = null)
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
                    query = query.Where(food => food.Weight >= minWeight.Value);
                }

                if (maxWeight.HasValue)
                {
                    query = query.Where(food => food.Weight <= maxWeight.Value);
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
                if (AllFoodsAreValid(newFoods))
                {
                    using var context = new ApplicationDbContext();

                    context.Foods.AddRange(newFoods);
                    context.SaveChanges();
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
                    var context = new ApplicationDbContext();

                    var ids = foodsUpdated.Select(foodsUpdated => foodsUpdated.Id).ToList();

                    if (ids.Distinct().Count() != ids.Count())
                    {
                        throw new ArgumentException("One or more foods submitted are repeated");
                    }

                    var foodsFound = GetFoods(ids);

                    if (ids.Count != foodsFound.Count())
                    {
                        throw new ArgumentException("One or more foods submitted were not found");
                    }

                    context.Foods.UpdateRange(foodsUpdated);
                    context.SaveChanges();
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
                var context = new ApplicationDbContext();

                var foodsToDetelete = GetFoods(ids);
                context.Foods.RemoveRange(foodsToDetelete);

                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool AllFoodsAreValid(IEnumerable<Models.Food> foodsToValidate)
        {
            var distinctsProductTypes = foodsToValidate.Select(food => food.ProductTypeId).Distinct().ToList();

            var productsTypesFounds = ProductType.GetProductTypes(distinctsProductTypes);

            if (!productsTypesFounds.Any() || distinctsProductTypes.Count != productsTypesFounds.Count())
            {
                throw new ArgumentException("One or more foods submitted has a invalid ProductType");
            }

            var distinctsWeightUnits = foodsToValidate.Select(food => food.WeightUnitId).Distinct().ToList();

            var WeightUnitsFounds = WeightUnity.GetWeightUnities(distinctsWeightUnits);

            if (!WeightUnitsFounds.Any() || distinctsWeightUnits.Count != WeightUnitsFounds.Count())
            {
                throw new ArgumentException("One or more foods submitted has a invalid WeightUnity");
            }

            return true;
        }
    }
}