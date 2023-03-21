using Product_CRUD.Enumerators;
using System.Transactions;

namespace Product_CRUD.Models
{
    public class Drink : Product
    {
        public decimal Capacity { get; set; }
        public CapacityUnity CapacityUnit { get; set; }

        public override decimal TypeTax { get; } = 0.05M;
    }
}
