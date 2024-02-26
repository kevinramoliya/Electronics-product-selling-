using ECommerce.Areas.Cart.Models;
using ECommerce.Areas.Category.Models;
using ECommerce.BAL;
using ECommerce.DAL.Cart;
using ECommerce.DAL.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Data;

namespace ECommerce.Areas.Cart.Controllers
{
	[CheckAccess]
	[Area("Cart")]
	[Route("Cart/[controller]/[action]")]
	public class CartController : Controller
	{
		CartDAL cartDAL = new CartDAL();

		#region Cart
		public IActionResult CartList()
		{
			DataTable dataTable = cartDAL.CartSelectAll(Convert.ToInt32(CommenVariable.UserID()));
			return View(dataTable);
		}
		#endregion

		#region Cart Insert
		public IActionResult CartInsert(CartModel cartModel, int ProductID, int UserID)
		{

			if (ModelState.IsValid)
			{
				if (cartDAL.CartInsert(cartModel, ProductID, UserID))
				{
					CartDAL cartDAL = new CartDAL();
					DataTable dataTable = cartDAL.CartCount(Convert.ToInt32(CommenVariable.UserID()));
					if (dataTable.Rows.Count > 0)
					{
						foreach (DataRow dataRow in dataTable.Rows)
						{
							HttpContext.Session.SetString("CartCount", dataRow["TotalCartItems"].ToString());
							break;
						}
					}
					return RedirectToAction("ShoppingProductList", "Product", new { area = "Product" });
				}
				else
				{
					return RedirectToAction("ShoppingProductByID", "Product");
				}
			}
			return View("CategoryAddEdit");
		}
		#endregion

		#region Increment Quantity
		public IActionResult Increment_Quantity(int ProductID)
		{
			bool isSuccess = cartDAL.Increment_Quantity(ProductID);
			if (isSuccess)
			{
				return RedirectToAction("CartList");
			}
			return RedirectToAction("CartList");
		}
		#endregion

		#region Decrement Quantity
		public IActionResult Decrement_Quantity(int ProductID)
		{
			bool isSuccess = cartDAL.Decrement_Quantity(ProductID);
			if (isSuccess)
			{
				return RedirectToAction("CartList");
			}
			return RedirectToAction("CartList");
		}
		#endregion

		#region Remove Cart Product
		public IActionResult Remove_Cart_Product(int ProductID)
		{
			bool isSuccess = cartDAL.Remove_Cart_Product(ProductID);
			if (isSuccess)
			{
				CartDAL cartDAL = new CartDAL();
				DataTable dataTable = cartDAL.CartCount(Convert.ToInt32(CommenVariable.UserID()));
				if (dataTable.Rows.Count > 0)
				{
					foreach (DataRow dataRow in dataTable.Rows)
					{
						HttpContext.Session.SetString("CartCount", dataRow["TotalCartItems"].ToString());
						break;
					}
				}
				return RedirectToAction("CartList");
			}
			return RedirectToAction("CartList");
		}
		#endregion

	}
}