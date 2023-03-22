namespace Product_CRUD.Models
{
    public class Drink : Product
    {
        public decimal Capacity { get; set; }

        public CapacityUnity CapacityUnit { get; set; } = new CapacityUnity();

        public override decimal TypeTax { get; } = 0.05M;
    }
}
