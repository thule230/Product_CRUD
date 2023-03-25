namespace Product_CRUD.Enumerators
{
    public enum ProductType
    {
        Food,
        Drink,
        Clothing
    }

    static class ProductTypeMethods
    {
        public static int GetId(this ProductType productType)
        {
            try
            {
                return productType switch
                {
                    ProductType.Food => DataLayer.ProductType.GetProductTypes(description: "Food").Single().Id,
                    ProductType.Drink => DataLayer.ProductType.GetProductTypes(description: "Drink").Single().Id,
                    ProductType.Clothing => DataLayer.ProductType.GetProductTypes(description: "Clothing").Single().Id,
                    _ => throw new NotImplementedException()
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Could not find the id for TypeProduct " + productType, ex);
            }
        }
    }
}
