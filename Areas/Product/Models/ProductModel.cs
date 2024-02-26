namespace ECommerce.Areas.Product.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }

        public string? ProductName { get; set; }

        public string? Discription { get; set; }

        public double Price { get; set; }

        public string? Code { get; set; }

        public IFormFile? File { get; set; }

        public string? DisplayImage { get; set; }

        public int CategoryID { get; set; }

        public bool isActive { get; set; }

        public int Discount { get; set; }

        public int Rating { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }
    public class ProductFilterModel
    {
        public int? CategoryID { get; set; }
        public string? ProductName { get; set; }
    }
}
