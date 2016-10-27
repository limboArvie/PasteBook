using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasteBook.Controllers
{
    public class ErrorController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult GlobalError()
        {
            return View();
        }
    }
}