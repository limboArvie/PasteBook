using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook.Models
{
    public class CommentModel
    {
        public Nullable<int> MyProperty { get; set; }
        public int POST_ID { get; set; }
        public int POSTER_ID { get; set; }
        public string CONTENT { get; set; }
        public DateTime DATE_CREATED { get; set; }
    }
}