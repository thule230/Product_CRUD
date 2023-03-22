namespace Product_CRUD.Models
{
    public class Clothing : Product
    {
        public ClothingSize Size { get; set; } = new ClothingSize();

        public override decimal TypeTax { get; } = 0.07M;
    }
}
