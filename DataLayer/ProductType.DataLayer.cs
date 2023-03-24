namespace Product_CRUD.DataLayer
{
    public static class ProductType
    {
        public static IEnumerable<Models.ProductType> GetProductTypes(IEnumerable<int>? ids = null, string? description = null)
        {
            try
            {
                var context = new ApplicationDbContext();

                var query = from productType in context.ProductTypes
                            select productType;

                if (ids != null && ids.Any())
                {
                    query = query.Where(productType => ids.Contains(productType.Id));
                }

                if (description != null)
                {
                    query = query.Where(productType => productType.Description == description);
                }

                return query.ToList();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public static IEnumerable<Models.ProductType> AddProductType(IEnumerable<Models.ProductType> newProductTypes)
        {
            try
            {
                var context = new ApplicationDbContext();

                context.ProductTypes.AddRange(newProductTypes);
                context.SaveChanges();

                return newProductTypes;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
