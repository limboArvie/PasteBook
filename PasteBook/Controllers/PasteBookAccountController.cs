using PasteBook.Models;
using PasteBookBusinessLogic;
using PasteBookEntityFramework;
using System.Web.Mvc;

namespace PasteBook.Controllers
{
    public class PasteBookAccountController : Controller
    {
        UserBLManager userManager = new UserBLManager();
        PasteBookAppBLManager pasteBookManager = new PasteBookAppBLManager();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(USER user)
        {
            USER currentUser = new USER();

            if (user.EMAIL_ADDRESS!=null && user.PASSWORD!=null)
            {
                currentUser = userManager.LoginUser(user);

                if (currentUser == null)
                {
                    ViewBag.ErrorLogin = true;
                    return View("Login");
                }

                else
                {
                    Session["User"] = currentUser.USER_NAME;
                    Session["Userid"] = currentUser.ID;
                    return RedirectToAction("Index", "PasteBookApp");
                }
            }

            else
            {
                return View("Login");
            }
            
        }

        public JsonResult CheckUsername(string username)
        {
            var result = userManager.CheckUsername(username);
            return Json(new { usernameExist = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckEmail(string email)
        {
            var result = userManager.CheckEmail(email);
            return Json(new { emailExist = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Register()
        {
            ViewBag.Country = new SelectList(userManager.RetrieveCountryList(), "ID", "COUNTRY");
            return View();
        }

        [HttpPost]
        public ActionResult Register(USER user, string ConfirmPassword)
        {
            if (userManager.CheckUsername(user.USER_NAME))
            {
                ModelState.AddModelError("USER_NAME", "Username already exists.");
            }

            if (userManager.CheckEmail(user.EMAIL_ADDRESS))
            {
                ModelState.AddModelError("EMAIL_ADDRESS", "Email already been used by another account.");
            }

            if (ConfirmPassword == "")
            {
                ModelState.AddModelError("ConfirmPassword", "Confirm password field is required.");
            }

            else if(user.PASSWORD != ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Password must match.");
            }

            if (ModelState.IsValid)
            {
                bool result = userManager.RegisterUser(user);

                if (result == true)
                {
                    Session["User"] = user.USER_NAME;
                    Session["Userid"] = user.ID;
                    return RedirectToAction("Index", "PasteBookApp");
                }

                return View("Register");
            }

            ViewBag.Country = new SelectList(userManager.RetrieveCountryList(), "ID", "COUNTRY");
            return View("Register");
        }

        public ActionResult ProfileSettings()
        {
            USER currentUser = pasteBookManager.GetUserInfo((int)Session["Userid"]);
            ViewBag.Country = new SelectList(userManager.RetrieveCountryList(), "ID", "COUNTRY", currentUser.REF_COUNTRY.COUNTRY);
            return View("ProfileSettings", currentUser);
        }

        [HttpPost]
        public ActionResult ProfileSettings(USER user)
        {
            USER currentUser = pasteBookManager.GetUserInfo((int)Session["Userid"]);
            ViewBag.Country = new SelectList(userManager.RetrieveCountryList(), "ID", "COUNTRY", currentUser.REF_COUNTRY.COUNTRY);
            user = userManager.UserMapper(user);
            bool result = userManager.EditUser(user);
            if (result)
            {
                Session["User"] = user.USER_NAME;
            }
            return View("ProfileSettings", user);
        }

        public ActionResult EmailSettings()
        {
            USER currentUser = pasteBookManager.GetUserInfo((int)Session["Userid"]);
            currentUser.PASSWORD = "";
            return View("EmailSettings", currentUser);
        }

        [HttpPost]
        public ActionResult EmailSettings(USER user, string newEmail)
        {
            bool result = false;
            if (userManager.CheckEmail(newEmail))
            {
                ModelState.AddModelError("newEmail", "Email already exists.");
            }

            else
            {
                result = userManager.EditEmail(user, newEmail);
            }

            if (result == false)
            {
                ModelState.AddModelError("PASSWORD", "Incorrect password.");
            }

            user = pasteBookManager.GetUserInfo(user.ID);
            user.PASSWORD = "";

            return View("EmailSettings", user);
        }

        public ActionResult PasswordSettings()
        {
            USER currentUser = pasteBookManager.GetUserInfo((int)Session["Userid"]);
            currentUser.PASSWORD = "";
            return View("PasswordSettings", currentUser);
        }

        [HttpPost]
        public ActionResult PasswordSettings(USER user, string newPassword, string reTypePassword)
        {
            USER currentUser = pasteBookManager.GetUserInfo(user.ID);

            if (newPassword != reTypePassword)
            {
                ModelState.AddModelError("reTypePassword", "Password must match.");
                return View("PasswordSettings", user);
            }

            if(userManager.EditPassword(user, newPassword) == false)
            {
                ModelState.AddModelError("PASSWORD", "Incorrect password.");
            }

            user = pasteBookManager.GetUserInfo(user.ID);
            user.PASSWORD = "";
            
            return View("PasswordSettings", user);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return View("Login");
        }
    }
}