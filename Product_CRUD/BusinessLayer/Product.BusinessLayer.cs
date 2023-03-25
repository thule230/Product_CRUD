namespace Product_CRUD.BusinessLayer
{
    public static class Product
    {
        public static decimal GetFinalPrice(decimal price, int quantity, decimal tax)
        {
            return price * quantity + (price * tax);
        }

    }
}
