using PasteBookEntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook.Models
{
    public class UserViewModel
    {
        public List<USER> UserList { get; set; }
        public string SearchKey { get; set; }
    }
}