namespace ECommerce.Areas.Order.Models
{
    public class OrderModel
    {
        public int OrderID { get; set; }

        public int AddressID { get; set; }

        public int ProductID { get; set; }

        public int UserID { get; set; }

        public bool isCompleted { get; set; }

        public string? OrderStatus { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }


    }
}