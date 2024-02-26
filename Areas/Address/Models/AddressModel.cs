using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Areas.Address.Models
{
	public class AddressModel
	{
		public int AddressID { get; set; }

		public int UserID { get; set; }

		public string? Address { get; set; }

		public string? Country { get; set; }

		public string? State { get; set; }

		public string? City { get; set; }

		public string? Postal { get; set; }

		public int isDefault { get; set; }

		public DateTime? Created { get; set; }

		public DateTime? Modified { get; set; }


	}
}
