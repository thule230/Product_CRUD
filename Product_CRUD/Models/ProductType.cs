namespace Product_CRUD.Models
{
    public class ProductType
    {
        public int Id { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal TypeTax { get; set; }
    }
}
