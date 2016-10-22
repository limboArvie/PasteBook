using PasteBookEntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook.Models
{
    public class PasteBookViewModel
    {
        public USER User { get; set; }
        public POST Post { get; set; }
        public List<POST> ListOfPost { get; set; }
        public COMMENT Comment { get; set; }
        public List<FRIEND> ListOfFriend { get; set; }
        public List<NOTIFICATION> ListOfNotif { get; set; }
    }
}