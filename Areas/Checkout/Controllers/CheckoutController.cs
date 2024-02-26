using ECommerce.Areas.Address.Models;
using ECommerce.BAL;
using ECommerce.DAL.Checkout;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ECommerce.Areas.Checkout.Controllers
{
    [Area("Checkout")]
    [Route("Checkout/[controller]/[action]")]
    public class CheckoutController : Controller
    {
        CheckoutDAL checkoutDAL = new CheckoutDAL();

        #region Checkout
        public IActionResult Checkout()
        {
            DataTable dataTable1 = checkoutDAL.Checkout(Convert.ToInt32(CommenVariable.UserID()));
            DataTable dataTable2 = checkoutDAL.Address_SelectAll();
            ViewModel viewModel = new ViewModel()
            {
                AddressTable = dataTable2,
                CartTable = dataTable1
            };
            return View(viewModel);
        }
        #endregion

        #region Billing
        public IActionResult Billing()
        {
            return View();
        }
        #endregion

        #region Address Insert
        public IActionResult Address_Insert(AddressModel addressModel, int UserID)
        {
            if (ModelState.IsValid)
            {
                if (checkoutDAL.AddressInsert(addressModel, UserID))
                {
                    return RedirectToAction("Checkout");
                }
                else
                {
                    return RedirectToAction("Checkout");
                }
            }
            return RedirectToAction("Checkout");
        }
        #endregion
    }
}
