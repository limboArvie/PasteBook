using PasteBook.Models;
using PasteBookBusinessLogic;
using PasteBookEntityFramework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasteBook.Controllers
{
    public class PasteBookAppController : Controller
    {
        PasteBookViewModel pasteBook = new PasteBookViewModel();
        PasteBookAppBLManager appManager = new PasteBookAppBLManager();
        UserBLManager userManager = new UserBLManager();
        List<LIKE> listOfLikes = new List<LIKE>();


        public ActionResult Index()
        {
            Session["CurrentProfile"] = 0;
            return View(pasteBook);
        }

        public ActionResult FeedPostPartialView()
        {
            List<POST> listOfPosts = new List<POST>();
            USER currentUser = new USER();

            listOfPosts = appManager.RetrieveFeedPosts((int)Session["Userid"]);

            pasteBook.ListOfPost = listOfPosts.ToList();
            return PartialView("FeedPostPartialView", pasteBook);
        }

        public ActionResult UserProfile(string userID)
        {
            USER currentUser = new USER();
            currentUser = appManager.GetUserInfo(userID);
            Session["CurrentProfile"] = currentUser.ID;
            pasteBook.User = currentUser;
            pasteBook.ListOfFriend = appManager.RetrieveFriends((int)Session["Userid"]);

            return View(pasteBook);
        }

        public ActionResult TimeLinePostPartialView(PasteBookViewModel model)
        {
            List<POST> listOfPosts = new List<POST>();
            UserModel currentUser = new UserModel();
            listOfPosts = appManager.RetrieveTimeLinePosts((int)Session["CurrentProfile"]);
            pasteBook.ListOfPost = listOfPosts.ToList();
            return PartialView("TimeLinePostPartialView", pasteBook);
        }

        public ActionResult NotificationPartialView()
        {
            pasteBook.ListOfNotif = appManager.RetrieveNotifications((int)Session["Userid"]);
            return PartialView("NotificationPartialView",pasteBook);
        }

        [ActionName("friends")]
        public ActionResult Friends()
        {
            List<FRIEND> friendList = new List<FRIEND>();
            List<USER> friendsInfo = new List<USER>();
            FriendsViewModel friendsModel = new FriendsViewModel();

            friendList = appManager.RetrieveFriends((int)Session["Userid"]).Where(x => x.REQUEST == "Y").ToList();
            friendsInfo = appManager.RetrieveFriendsInfo(friendList);
            friendsModel.FriendList = friendsInfo;
            return View(friendsModel);
        }

        public ActionResult EditProfilePicture(HttpPostedFileBase file)
        {
            byte[] profilePicture;

            if (file != null)
            {
                USER currentUser = new USER();
                currentUser = appManager.GetUserInfo((int)Session["Userid"]);

                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    profilePicture = ms.GetBuffer();
                }

                currentUser.PROFILE_PIC = profilePicture;
                userManager.EditUser(currentUser);
            }
            
            return RedirectToAction("UserProfile", new { userID = (string)Session["User"] });
        }

        public JsonResult AddFriend(FRIEND friend)
        {
            var result = appManager.AddFriend(friend);
            return Json(new { addFriendResult = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Post(POST post)
        {
            int currentProfile = (int)Session["CurrentProfile"];
            var result = appManager.Post(post, currentProfile);
            return Json(new { postResult = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Like(LIKE like)
        {
            var result = appManager.LikePost(like);
            return Json(new { likeResult = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Comment(COMMENT comment)
        {
            var result = appManager.Comment(comment);
            return Json(new { likeResult = result }, JsonRequestBehavior.AllowGet);
        }

    }
}