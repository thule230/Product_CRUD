using Product_CRUD.Enumerators;

namespace Product_CRUD.Models
{
    public class Clothing : Product
    {
        public ClothingSize Size { get; set; }

        public override decimal TypeTax { get; } = 0.07M;
    }
}
