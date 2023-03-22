namespace Product_CRUD.BusinessLayer
{
    public static class Food
    {
        public static IEnumerable<Models.Food> GetFoods(int? id, string? descriptionSearch, decimal? minPrice, decimal? maxPrice, int? minQuantity, int? maxQuantity, decimal? minFinalPrice, decimal? maxFinalPrice, decimal? minWeight, decimal? maxWeight, int? weightUnit)
        {
            return DataLayer.Food.GetFoods(id, descriptionSearch, minPrice, maxPrice, minQuantity, maxQuantity, minFinalPrice, maxFinalPrice, minWeight, maxWeight, weightUnit);
        }
    }
}
