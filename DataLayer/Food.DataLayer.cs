using Product_CRUD.Models;

namespace Product_CRUD.DataLayer
{
    public static class Food
    {
        public static IEnumerable<Models.Food> GetFoods(int? id, string? descriptionSearch, decimal? minPrice, decimal? maxPrice, int? minQuantity, int? maxQuantity, decimal? minFinalPrice, decimal? maxFinalPrice, decimal? minWeight, decimal? maxWeight, int? weightUnit)
        {
            using (var context = new ApplicationDbContext())
            {
                var query = from foods in context.Foods
                            select foods;

                if (id.HasValue)
                {
                    query = query.Where(food => food.Id == id.Value);
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

                if (minFinalPrice.HasValue)
                {
                    query = query.Where(food => food.FinalPrice >= minFinalPrice.Value);
                }

                if (maxFinalPrice.HasValue)
                {
                    query = query.Where(food => food.FinalPrice <= maxFinalPrice.Value);
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
                    query = query.Where(food => food.WeightUnit.Id == weightUnit.Value);
                }

                return query.ToList();
            }
        }
    }
}
