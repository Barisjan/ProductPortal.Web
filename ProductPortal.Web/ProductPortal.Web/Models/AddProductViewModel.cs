namespace ProductPortal.Web.Models
{
    public class AddProductViewModel
    {
        public required string Name { get; set; }

        public int Price { get; set; }

        public int StockQuantity { get; set; }
    }
}
