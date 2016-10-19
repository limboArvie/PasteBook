using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook.Models
{
    public class PasteBookViewModel
    {
        public UserModel User { get; set; }
        public PostModel Post { get; set; }
        public List<PostModel> ListOfPost { get; set; }
        public List<FriendModel> ListOfFriend { get; set; }
    }
}