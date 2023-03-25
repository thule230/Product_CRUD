namespace Product_CRUD.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int ProductTypeId { get; set; }
    }
}