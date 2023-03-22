namespace Product_CRUD.Models
{
    public abstract class Product
    {
        public int Id { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public abstract decimal TypeTax { get; }

        public decimal FinalPrice
        {
            get
            {
                return Price * Quantity + (Price * TypeTax);
            }
        }
    }
}