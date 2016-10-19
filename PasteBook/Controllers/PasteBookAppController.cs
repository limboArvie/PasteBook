using PasteBook.Managers;
using PasteBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasteBook.Controllers
{
    public class PasteBookAppController : Controller
    {
        PasteBookViewModel pasteBook = new PasteBookViewModel();
        PasteBookAppManager appManager = new PasteBookAppManager();
        List<LikeModel> listOfLikes = new List<LikeModel>();


        public ActionResult Index()
        {
            List<PostModel> listOfPosts = new List<PostModel>();
            UserModel currentUser = new UserModel();

            listOfPosts = appManager.RetrieveFeedPosts();
            listOfLikes = appManager.RetrieveLikes();

            foreach (var item in listOfPosts)
            {
                if(listOfLikes.Any(x=>x.POST_ID == item.ID && x.LIKED_BY == (int)Session["Userid"]))
                {
                    item.LikeIndicator = true;
                }

                currentUser = appManager.GetUserInfo(item.POSTER_ID);
                item.POSTER_NAME = currentUser.FIRST_NAME + " " + currentUser.LAST_NAME;
                currentUser = appManager.GetUserInfo(item.PROFILE_OWNER_ID);
                item.PROFILE_OWNER_NAME = currentUser.FIRST_NAME +" "+ currentUser.LAST_NAME;
                
            }

            pasteBook.ListOfPost = listOfPosts;
            return View(pasteBook);
        }

        public ActionResult UserProfile(string userID)
        {
            
            UserModel currentUser = new UserModel();
            currentUser = appManager.GetUserInfo(userID);
            Session["CurrentProfile"] = currentUser.ID;
            pasteBook.User = currentUser;
            pasteBook.ListOfPost = appManager.RetrieveTimeLinePosts(currentUser.ID);
            pasteBook.ListOfFriend = appManager.RetrieveFriends((int)Session["Userid"]);

            listOfLikes = appManager.RetrieveLikes();

            foreach (var item in pasteBook.ListOfPost)
            {

                if (listOfLikes.Any(x => x.POST_ID == item.ID && x.LIKED_BY == (int)Session["Userid"]))
                {
                    item.LikeIndicator = true;
                }

                currentUser = appManager.GetUserInfo(item.POSTER_ID);
                item.POSTER_NAME = currentUser.FIRST_NAME + " " + currentUser.LAST_NAME;
                currentUser = appManager.GetUserInfo(item.PROFILE_OWNER_ID);
                item.PROFILE_OWNER_NAME = currentUser.FIRST_NAME + " " + currentUser.LAST_NAME;
            }

            currentUser = appManager.GetUserInfo(userID);
            return View(pasteBook);
        }

        public ActionResult TimeLinePostPartialView(PasteBookViewModel model)
        {
            UserModel currentUser = new UserModel();
            pasteBook.ListOfPost = appManager.RetrieveTimeLinePosts((int)Session["CurrentProfile"]);

            listOfLikes = appManager.RetrieveLikes();

            foreach (var item in pasteBook.ListOfPost)
            {

                if (listOfLikes.Any(x => x.POST_ID == item.ID && x.LIKED_BY == (int)Session["Userid"]))
                {
                    item.LikeIndicator = true;
                }

                currentUser = appManager.GetUserInfo(item.POSTER_ID);
                item.POSTER_NAME = currentUser.FIRST_NAME + " " + currentUser.LAST_NAME;
                currentUser = appManager.GetUserInfo(item.PROFILE_OWNER_ID);
                item.PROFILE_OWNER_NAME = currentUser.FIRST_NAME + " " + currentUser.LAST_NAME;
            }

            return PartialView("TimeLinePostPartialView", pasteBook);
        }

        public ActionResult Friends()
        {
            List<FriendModel> friendList = new List<FriendModel>();
            FriendsViewModel friendsModel = new FriendsViewModel();
            FriendModel friend = new FriendModel();
            UserModel currentUser = new UserModel();
            
            friendList = appManager.RetrieveFriends((int)Session["Userid"]).Where(x=>x.REQUEST=="Y").ToList();

            foreach (var item in friendList)
            {
                currentUser = appManager.GetUserInfo(item.FRIEND_ID);
                item.FRIEND_NAME = currentUser.FIRST_NAME + " " + currentUser.LAST_NAME;
                item.FRIEND_PROFILE_PIC = currentUser.PROFILE_PIC;
                item.FRIEND_USER_NAME = currentUser.USERNAME;
            }

            friendsModel.FriendList = friendList;

            return View(friendsModel);
        }

        public JsonResult AddFriend(int CurrentProfile)
        {
            FriendModel friend = new FriendModel();
            friend.USER_ID = (int)Session["Userid"];
            friend.FRIEND_ID = CurrentProfile;
            friend.REQUEST = "N";
            friend.BLOCKED = "N";

            var result = appManager.AddFriend(friend);
            return Json(new { addFriendResult = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Post(string Content)
        {
            PostModel post = new PostModel();
            post.POSTER_ID = (int)Session["Userid"];
            post.PROFILE_OWNER_ID = (int)Session["CurrentProfile"];
            post.Content = Content;
            var result = appManager.Post(post);
            return Json(new { postResult = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FeedPostPartialView()
        {
            List<PostModel> listOfPosts = new List<PostModel>();
            UserModel currentUser = new UserModel();

            listOfPosts = appManager.RetrieveFeedPosts();
            listOfLikes = appManager.RetrieveLikes();

            foreach (var item in listOfPosts)
            {

                if (listOfLikes.Any(x => x.POST_ID == item.ID && x.LIKED_BY == (int)Session["Userid"]))
                {
                    item.LikeIndicator = true;
                }

                currentUser = appManager.GetUserInfo(item.POSTER_ID);
                item.POSTER_NAME = currentUser.FIRST_NAME + " " + currentUser.LAST_NAME;
                currentUser = appManager.GetUserInfo(item.PROFILE_OWNER_ID);
                item.PROFILE_OWNER_NAME = currentUser.FIRST_NAME + " " + currentUser.LAST_NAME;
            }

            pasteBook.ListOfPost = listOfPosts;
            return PartialView("FeedPostPartialView", pasteBook);
        }

        public JsonResult Like(LikeModel like)
        {
            var result = appManager.LikePost(like);
            return Json(new { likeResult = result }, JsonRequestBehavior.AllowGet);
        }
    }
}