namespace MavickBackend.Models.Request
{
    public class ProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Amount { get; set; }
        public string Photo { get; set; } = null!;
        public decimal Cost { get; set; }

        public List<SizeProduct> Sizes { get; set; }
        public List<CategoryProduct> Categories { get; set; }

        public ProductRequest()
        {
            Sizes = new List<SizeProduct>();
            Categories = new List<CategoryProduct>();
        }
    }

    public class SizeProduct
    {
        public int Id { get; set; }
    }

    public class CategoryProduct
    {
        public int Id { get; set; }
    }
}
