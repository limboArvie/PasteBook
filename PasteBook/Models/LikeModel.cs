using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook.Models
{
    public class LikeModel
    {
        public Nullable<int> ID { get; set; }
        public int POST_ID { get; set; }
        public int LIKED_BY { get; set; }
    }
}