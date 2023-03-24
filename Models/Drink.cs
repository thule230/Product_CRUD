namespace Product_CRUD.Models
{
    public class Drink : Product
    {
        public decimal Capacity { get; set; }

        public int CapacityUnitId { get; set; }
    }
}
