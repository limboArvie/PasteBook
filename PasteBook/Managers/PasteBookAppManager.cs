using DataAccessLogic;
using PasteBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook.Managers
{
    public class PasteBookAppManager
    {
        PasteBookDBManager dbmanager = new PasteBookDBManager();
        Mapper mapper = new Mapper();

        public UserModel GetUserInfo(string username)
        {
            return mapper.UserToModel(dbmanager.RetrieveSpecificUser(username));
        }

        public UserModel GetUserInfo(int userID)
        {
            return mapper.UserToModel(dbmanager.RetrieveSpecificUser(userID));
        }

        public bool Post(PostModel post)
        {
            return dbmanager.Post(mapper.PostToDB(post));
        }

        public List<PostModel> RetrieveFeedPosts()
        {
            List<PostModel> listOfPost = new List<PostModel>();

            foreach (var item in dbmanager.RetrieveFeedPosts())
            {
                listOfPost.Add(mapper.PostToModel(item));
            }

            return listOfPost;
        }

        public List<PostModel> RetrieveTimeLinePosts(int id)
        {
            List<PostModel> listOfPost = new List<PostModel>();

            foreach (var item in dbmanager.RetrieveTimeLinePosts(id))
            {
                listOfPost.Add(mapper.PostToModel(item));
            }

            return listOfPost;
        }

        public bool LikePost(LikeModel like)
        {
            return dbmanager.LikePost(mapper.LikeToDB(like));
        }

        public List<FriendModel> RetrieveFriends(int id)
        {
            List<FriendModel> listOfFriends = new List<FriendModel>();

            foreach (var item in dbmanager.RetrieveFriends(id))
            {
                if(item.FRIEND_ID == id)
                {
                    item.FRIEND_ID = item.USER_ID;
                    item.USER_ID = id;
                }

                listOfFriends.Add(mapper.FriendToModel(item));
            }

            return listOfFriends;
        }

        public bool AddFriend(FriendModel friend)
        {
            return dbmanager.AddFriend(mapper.FriendToDB(friend));
        }

        public List<LikeModel> RetrieveLikes()
        {
            List<LikeModel> listOfLike = new List<LikeModel>();

            foreach (var item in dbmanager.RetrieveLikes())
            {
                listOfLike.Add(mapper.LikeToModel(item));
            }

            return listOfLike;
        }

        
    }
}