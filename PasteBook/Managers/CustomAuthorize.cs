using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasteBook.Managers
{
    //https://vivekcek.wordpress.com/2013/06/29/custom-form-authentication-in-mvc-4-with-custom-authorize-attribute-and-session-variable/
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Now check the session:
            var sessionCheck = httpContext.Session["User"];

            if (sessionCheck == null)
            {
                // the session has expired
                return false;
            }

            return true;
        }
    }
}