using ECommerce.Areas.SEC_User.Models;
using ECommerce.DAL.SEC_User;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ECommerce.Areas.SEC_User.Controllers
{
    [Area("SEC_User")]
    [Route("SEC_User/[controller]/[action]")]
    public class SEC_UserController : Controller
    {
        public IActionResult SEC_UserSignIn()
        {
            return View();
        }

        public IActionResult SEC_UserSignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(SEC_UserModel sEC_UserModel)
        {
            if (sEC_UserModel.UserName == null)
            {
                TempData["UserNameError"] = "User Name is Required!";
            }
            if (sEC_UserModel.Password == null)
            {
                TempData["PasswordError"] = "Password is Required!";
            }

            if (TempData["UserNameError"] != null || TempData["PasswordError"] != null)
            {
                return RedirectToAction("SEC_UserSignIn", "SEC_User");
            }
            else
            {

                SEC_UserDAL sEC_UserDAL = new SEC_UserDAL();
                DataTable dataTable = sEC_UserDAL.SEC_User_SelectByUserNamePassword(sEC_UserModel.UserName, sEC_UserModel.Password);
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        HttpContext.Session.SetString("UserID", dataRow["UserID"].ToString());
                        HttpContext.Session.SetString("UserName", dataRow["UserName"].ToString());
                        HttpContext.Session.SetString("Password", dataRow["Password"].ToString());
                        HttpContext.Session.SetString("FirstName", dataRow["FirstName"].ToString());
                        HttpContext.Session.SetString("LastName", dataRow["LastName"].ToString());
                        HttpContext.Session.SetString("EmailAddress", dataRow["EmailAddress"].ToString());
                        HttpContext.Session.SetString("isAdmin", dataRow["isAdmin"].ToString());
                        //HttpContext.Session.SetString("isActive", dataRow["isActive"].ToString());
                        break;
                    }
                }
                else
                {
                    TempData["Error"] = "UserName or Password is invalid!";
                    return RedirectToAction("SEC_UserSignIn");
                }
                if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null && HttpContext.Session.GetString("isAdmin") == "True")
                {
                        return RedirectToAction("SEC_AdminDashboard", "SEC_Admin", new { area = "SEC_Admin" });
                }
                else if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SEC_UserSignIn");
        }

        public IActionResult Register(SEC_UserModel sEC_UserModel)
        {

            if (sEC_UserModel.UserName == null)
            {
                TempData["UserNameError"] = "User Name is Required!";
            }
            if (sEC_UserModel.Password == null)
            {
                TempData["PasswordError"] = "Password is Required!";
            }
            if (sEC_UserModel.FirstName == null)
            {
                TempData["FirstNameError"] = "First  Name is Required!";
            }
            if (sEC_UserModel.LastName == null)
            {
                TempData["LastNameError"] = "Last Name is Required!";
            }
            if (sEC_UserModel.EmailAddress == null)
            {
                TempData["EmailAddressError"] = "Email Address is Required!";
            }

            if (TempData["UserNameError"] != null || TempData["PasswordError"] != null || TempData["FirstNameError"] != null || TempData["LastNameError"] != null || TempData["EmailAddressError"] != null)
            {
				SEC_UserDAL sEC_UserDAL = new SEC_UserDAL();
				bool IsSuccess = sEC_UserDAL.PR_SEC_User_Register(sEC_UserModel.UserName, sEC_UserModel.Password, sEC_UserModel.FirstName, sEC_UserModel.LastName, sEC_UserModel.EmailAddress);
				if (IsSuccess)
				{
					return RedirectToAction("SEC_UserSignIn");
				}
				else
				{
					return RedirectToAction("SEC_UserSignUp");
				}
			}
            else
            {
                SEC_UserDAL sEC_UserDAL = new SEC_UserDAL();
                bool IsSuccess = sEC_UserDAL.PR_SEC_User_Register(sEC_UserModel.UserName, sEC_UserModel.Password, sEC_UserModel.FirstName, sEC_UserModel.LastName, sEC_UserModel.EmailAddress);
                if (IsSuccess)
                {
                    return RedirectToAction("SEC_UserSignIn");
                }
                else
                {
                    return RedirectToAction("SEC_UserSignUp");
                }
            }

        }
        #region User SelectALL
        public IActionResult SEC_UserList()
        {
            SEC_UserDAL sEC_UserDAL = new SEC_UserDAL();
            DataTable dataTable = sEC_UserDAL.SEC_User_SelectALL();
            return View("UserList", dataTable);
        }
        #endregion

        #region User SelectByPK
        public IActionResult SEC_User_SelectByPK(int UserID)
        {
            SEC_UserDAL sEC_UserDAL = new SEC_UserDAL();
            DataTable dataTable = sEC_UserDAL.SEC_User_SelectByPK(UserID);
            return View("SingleUser", dataTable);
        }
        #endregion
    }
}
