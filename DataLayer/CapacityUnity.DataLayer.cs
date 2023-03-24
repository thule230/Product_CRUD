using Product_CRUD.Models;

namespace Product_CRUD.DataLayer
{
    public class CapacityUnity
    {
        public static IEnumerable<Models.CapacityUnity> GetCapacityUnities(IEnumerable<int>? ids = null, string? description = null)
        {
            try
            {
                var context = new ApplicationDbContext();

                var query = from capacityUnities in context.CapacityUnities
                            select capacityUnities;

                if (ids != null && ids.Any())
                {
                    query = query.Where(capacityUnity => ids.Contains(capacityUnity.Id));
                }

                if (description != null)
                {
                    query = query.Where(capacityUnity => capacityUnity.Description == description);
                }

                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IEnumerable<Models.CapacityUnity> AddCapacityUnities(IEnumerable<Models.CapacityUnity> newCapacityUnities)
        {
            try
            {
                var context = new ApplicationDbContext();

                context.CapacityUnities.AddRange(newCapacityUnities);
                context.SaveChanges();

                return newCapacityUnities;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
