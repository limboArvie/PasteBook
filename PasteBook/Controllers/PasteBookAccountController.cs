using PasteBook.Managers;
using PasteBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasteBook.Controllers
{
    public class PasteBookAccountController : Controller
    {
        PasteBookManager manager = new PasteBookManager();

        public ActionResult Index()
        {
            if (Session["CountryList"] == null)
            {
                Session["CountryList"] = new SelectList (manager.RetrieveCountryList(), "ID", "COUNTRY");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            UserModel currentUser = new UserModel();

            if (user.EMAIL_ADDRESS!=null && user.PASSWORD!=null)
            {
                currentUser = manager.LoginUser(user);

                if (currentUser == null)
                {
                    ModelState.AddModelError("EMAIL_ADDRESS", "Email Address or Password is Invalid");
                    return View("Login");
                }

                else
                {
                    Session["User"] = currentUser.USERNAME;
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
            if (Session["CountryList"] == null)
            {
                Session["CountryList"] = new SelectList(manager.RetrieveCountryList(), "ID", "COUNTRY");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            if (manager.CheckUsername(user.USERNAME))
            {
                ModelState.AddModelError("USERNAME", "Username already exists.");
            }

            if (manager.CheckEmail(user.EMAIL_ADDRESS))
            {
                ModelState.AddModelError("EMAIL_ADDRESS", "Email already been used by another account.");
            }

            if (ModelState.IsValid)
            {
                bool result = manager.RegisterUser(user);
                return View("Index");
            }

            return View("Register");
            
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return View("Index");
        }
    }
}