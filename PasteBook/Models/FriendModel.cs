using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook.Models
{
    public class FriendModel
    {
        public Nullable<int> ID { get; set; }
        public int USER_ID { get; set; }
        public int FRIEND_ID { get; set; }
        public string FRIEND_NAME { get; set; }
        public string FRIEND_USER_NAME { get; set; }
        public byte[] FRIEND_PROFILE_PIC { get; set; }
        public string REQUEST { get; set; }
        public string BLOCKED { get; set; }
        public DateTime CREATED_DATE { get; set; }
    }
}