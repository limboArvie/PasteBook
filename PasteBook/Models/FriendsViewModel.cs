using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook.Models
{
    public class FriendsViewModel
    {
        public FriendModel Friend { get; set; }
        public List<FriendModel> FriendList { get; set; }
    }
}