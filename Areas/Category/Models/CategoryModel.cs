using System.ComponentModel.DataAnnotations;

namespace ECommerce.Areas.Category.Models
{
    public class CategoryModel
    {
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }
    public class CategoryDropDownModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
