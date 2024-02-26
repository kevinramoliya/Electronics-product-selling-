using ECommerce.Areas.Category.Models;
using ECommerce.BAL;
using ECommerce.DAL.Cart;
using ECommerce.DAL.Category;
using ECommerce.DAL.Order;
using ECommerce.DAL.Product;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ECommerce.Areas.Order.Controllers
{
    [Area("Order")]
    [Route("Order/[controller]/[action]")]
    public class OrderController : Controller
    {
        OrderDAL orderDAL = new OrderDAL();
        CartDAL cartDAL = new CartDAL();

        #region Order List (Pending)
        public IActionResult PendingOrderList()
        {
            DataTable dataTable = orderDAL.PendingOrderSelectAll();
            ViewBag.Complete = TempData["Complete"];
            return View(dataTable);
        }
        #endregion

        #region Order List (Completed)
        public IActionResult CompletedOrderList()
        {
            DataTable dataTable = orderDAL.CompletedOrderSelectAll();
            return View(dataTable);
        }
        #endregion

        #region Order SelectByPK
        public IActionResult OrderSelectByPK(int OrderID)
        {
            DataTable dataTable = orderDAL.OrderSelectByPK(OrderID);
            return View("SingleOrder", dataTable);
        }
        #endregion

        #region Order Complete
        public IActionResult OrderComplete(int OrderID)
        {
            bool isSuccess = orderDAL.OrderComplete(OrderID);
            if (isSuccess)
            {
                TempData["Complete"] = "Order Completed Successfully";
                return RedirectToAction("PendingOrderList");
            }
            return RedirectToAction("PendingOrderList");
        }
		#endregion

		#region Order Insert
		public IActionResult Order_Insert(int UserID, int[] ProductIDs, int AddressID)
		{
			if (ModelState.IsValid)
			{
				if (orderDAL.OrderInsert(UserID, ProductIDs, AddressID))
				{
					bool isSuccess = cartDAL.Update_Order_Status(Convert.ToInt32(CommenVariable.UserID()));
					if (isSuccess)
					{
						DataTable dataTable = cartDAL.CartCount(Convert.ToInt32(CommenVariable.UserID()));
						if (dataTable.Rows.Count > 0)
						{
							foreach (DataRow dataRow in dataTable.Rows)
							{
								HttpContext.Session.SetString("CartCount", dataRow["TotalCartItems"].ToString());
								break;
							}
						}
						return RedirectToAction("ThankYou", "Home");
					}
					else
					{
						return RedirectToAction("ThankYou", "Home");
					}
				}
				else
				{
					return RedirectToAction("ThankYou", "Home");
				}
			}
			return RedirectToAction("ThankYou", "Home");
		}
		#endregion

	}
}