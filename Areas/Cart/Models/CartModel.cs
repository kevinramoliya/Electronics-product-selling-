namespace ECommerce.Areas.Cart.Models
{
	public class CartModel
	{
		public int CartID { get; set; }

		public int ProductID { get; set; }

		public int UserID { get; set; }

		public int Quantity { get; set; }

		public DateTime Created { get; set; }

		public DateTime Modified { get; set; }
	}
}