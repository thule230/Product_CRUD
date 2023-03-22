namespace Product_CRUD.Models
{
    public class Food : Product
    {
        public decimal Weight { get; set; }

        public WeightUnity WeightUnit { get; set; } = new WeightUnity();

        public override decimal TypeTax { get; } = 0.06M;
    }
}