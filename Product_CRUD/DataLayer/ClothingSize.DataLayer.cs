namespace Product_CRUD.DataLayer
{
    public class ClothingSize
    {
        public static IEnumerable<Models.ClothingSize> Get(IEnumerable<int>? ids = null, string? description = null)
        {
            try
            {
                var context = new ApplicationDbContext();

                var query = from clothingSizes in context.ClothingSizes
                            select clothingSizes;

                if (ids != null && ids.Any())
                {
                    query = query.Where(clothingSize => ids.Contains(clothingSize.Id));
                }

                if (description != null)
                {
                    query = query.Where(clothingSize => clothingSize.Description == description);
                }

                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IEnumerable<Models.ClothingSize> Add(IEnumerable<Models.ClothingSize> newClothingSizes)
        {
            try
            {
                var context = new ApplicationDbContext();

                context.ClothingSizes.AddRange(newClothingSizes);
                context.SaveChanges();

                return newClothingSizes;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
