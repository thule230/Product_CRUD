namespace Product_CRUD.Models
{
    public class Food : Product
    {
        public decimal Weight { get; set; }

        public int WeightUnitId { get; set; }
    }
}