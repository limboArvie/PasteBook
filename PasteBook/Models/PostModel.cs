using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PasteBook.Models
{
    public class PostModel
    {
        public Nullable<int> ID { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string Content { get; set; }
        public int PROFILE_OWNER_ID { get; set; }
        public string PROFILE_OWNER_NAME { get; set; }
        public int POSTER_ID { get; set; }
        public string POSTER_NAME { get; set; }
        public bool LikeIndicator { get; set; }
    }
}