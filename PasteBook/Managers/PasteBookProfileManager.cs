using DataAccessLogic;
using PasteBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook.Managers
{
    public class PasteBookProfileManager
    {
        PasteBookDBManager dbmanager = new PasteBookDBManager();
        Mapper mapper = new Mapper();

        public UserModel GetUserInfo(string username)
        {
            return mapper.UserToModel(dbmanager.RetrieveSpecificUser(username));
        }

        public bool Post(PostModel post)
        {
            return dbmanager.Post(mapper.PostToDB(post));
        }

        public List<PostModel> RetrieveTimeLinePosts()
        {
            List<PostModel> listOfPost = new List<PostModel>();

            foreach (var item in dbmanager.RetrieveTimeLinePosts())
            {
                listOfPost.Add(mapper.PostToModel(item));
            }

            return listOfPost;
        }

        public bool LikePost(LikeModel like)
        {
            return dbmanager.LikePost(mapper.LikeToDB(like));
        }
    }
}