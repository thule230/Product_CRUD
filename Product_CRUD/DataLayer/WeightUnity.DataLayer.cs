namespace Product_CRUD.DataLayer
{
    public class WeightUnity
    {
        public static IEnumerable<Models.WeightUnity> Get(IEnumerable<int>? ids = null, string? description = null)
        {
            try
            {
                var context = new ApplicationDbContext();

                var query = from weightUnities in context.WeightUnities
                            select weightUnities;

                if (ids != null && ids.Any())
                {
                    query = query.Where(weightUnity => ids.Contains(weightUnity.Id));
                }

                if (description != null)
                {
                    query = query.Where(weightUnity => weightUnity.Description == description);
                }

                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IEnumerable<Models.WeightUnity> Add(IEnumerable<Models.WeightUnity> newWeightUnities)
        {
            try
            {
                var context = new ApplicationDbContext();

                context.WeightUnities.AddRange(newWeightUnities);
                context.SaveChanges();

                return newWeightUnities;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
