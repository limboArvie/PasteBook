using PasteBook.Models;
using PasteBookBusinessLogic;
using PasteBookEntityFramework;
using System.Web.Mvc;

namespace PasteBook.Controllers
{
    public class PasteBookAccountController : Controller
    {
        UserBLManager userManager = new UserBLManager();

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
                    ModelState.AddModelError("EMAIL_ADDRESS", "Email Address or Password is Invalid");
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

        public ActionResult Register()
        {
            ViewBag.Country = new SelectList(userManager.RetrieveCountryList(), "ID", "COUNTRY");
            return View();
        }

        [HttpPost]
        public ActionResult Register(USER user)
        {
            ViewBag.Country = new SelectList(userManager.RetrieveCountryList(), "ID", "COUNTRY");

            if (userManager.CheckUsername(user.USER_NAME))
            {
                ModelState.AddModelError("USER_NAME", "Username already exists.");
            }

            if (userManager.CheckEmail(user.EMAIL_ADDRESS))
            {
                ModelState.AddModelError("EMAIL_ADDRESS", "Email already been used by another account.");
            }

            if (ModelState.IsValid)
            {
                bool result = userManager.RegisterUser(user);
                return View("Index");
            }

            return View("Register");
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            Session["Userid"] = null;
            return View("Index");
        }
    }
}