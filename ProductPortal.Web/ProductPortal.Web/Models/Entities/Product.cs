namespace ProductPortal.Web.Models.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public int StockQuantity { get; set; }
    }
}
