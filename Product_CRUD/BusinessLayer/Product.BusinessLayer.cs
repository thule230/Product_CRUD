using Product_CRUD.Enumerators;

namespace Product_CRUD.BusinessLayer
{
    public static class Product
    {
        public static void Delete(IEnumerable<Models.Product> products)
        {
            var productsTypes = products.Select(product => product.ProductTypeId).Distinct().ToList();

            var productsTypesFound = DataLayer.ProductType.Get(productsTypes);

            if (!productsTypesFound.Any() || productsTypes.Count != productsTypesFound.Count())
            {
                throw new ArgumentException("One or more products submitted has an invalid ProductType");
            }

            var drinkTypeId = ProductType.Drink.GetId();
            var drinksToDelete = products
                .Where(product => product.ProductTypeId == drinkTypeId)
                .Select(product => product.Id)
                .ToList();

            var foodTypeId = ProductType.Food.GetId();
            var foodsToDelete = products
                .Where(product => product.ProductTypeId == foodTypeId)
                .Select(product => product.Id)
                .ToList();

            var clothingTypeId = ProductType.Clothing.GetId();
            var clothingsToDelete = products
                .Where(product => product.ProductTypeId == clothingTypeId)
                .Select(product => product.Id)
                .ToList();

            Drink.Delete(drinksToDelete);
            Food.Delete(foodsToDelete);
            Clothing.Delete(clothingsToDelete);
        }

        public static decimal GetFinalPrice(decimal price, int quantity, decimal tax)
        {
            return price * quantity + (price * tax);
        }

    }
}
