using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasteBook.Controllers
{
    public class PasteBookAppController : Controller
    {
        // GET: PasteBookApp
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserProfile(string userID)
        {
            return View();
        }
    }
}