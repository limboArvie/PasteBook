using PasteBook.Managers;
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

        [Route("")]
        [CustomAuthorize]
        public ActionResult Index()
        {
            Session["CurrentProfile"] = 0;
            return View(pasteBook);
        }

        [CustomAuthorize]
        public ActionResult FeedPostPartialView()
        {
            List<POST> listOfPosts = new List<POST>();
            USER currentUser = new USER();

            listOfPosts = appManager.RetrieveFeedPosts((int)Session["Userid"]);
            pasteBook.ListOfPost = listOfPosts.ToList();
            return PartialView("FeedPostPartialView", pasteBook);
        }

        [Route("{userID:minlength(1)}")]
        [CustomAuthorize]
        public ActionResult UserProfile(string userID)
        {
            USER currentUser = new USER();
            currentUser = appManager.GetUserInfo(userID);
            Session["CurrentProfile"] = currentUser.ID;
            pasteBook.User = currentUser;
            return View(pasteBook);
        }

        [CustomAuthorize]
        public ActionResult TimeLinePostPartialView(PasteBookViewModel model)
        {
            model.ListOfPost = appManager.RetrieveTimeLinePosts((int)Session["CurrentProfile"]);
            return PartialView("TimeLinePostPartialView", model);
        }

        [CustomAuthorize]
        public ActionResult ProfileBannerPartialView(PasteBookViewModel model)
        {
            USER currentUser = new USER();
            currentUser = appManager.GetUserInfo((int)Session["CurrentProfile"]);
            model.User = currentUser;
            model.ListOfFriend = appManager.RetrieveFriends((int)Session["Userid"]);
            return PartialView("ProfileBannerPartialView", model);
        }

        [CustomAuthorize]
        public ActionResult PendingRequestPartialView(FriendsViewModel model)
        {
            List<FRIEND> requestList = new List<FRIEND>();
            List<USER> requestsInfo = new List<USER>();

            requestList = appManager.RetrieveFriends((int)Session["Userid"]);
            model.RequestList = appManager.RetrieveRequestsInfo(requestList, (int)Session["Userid"]);
            return PartialView("PendingRequestPartialView", model);
        }

        [CustomAuthorize]
        public ActionResult NotificationPartialView()
        {
            pasteBook.ListOfNotif = appManager.RetrieveNotifications((int)Session["Userid"]);
            return PartialView("NotificationPartialView",pasteBook);
        }

        [ActionName("friends")]
        [Route("friends")]
        [CustomAuthorize]
        public ActionResult Friends()
        {
            List<FRIEND> friendList = new List<FRIEND>();
            List<USER> friendsInfo = new List<USER>();
            FriendsViewModel friendsModel = new FriendsViewModel();

            friendList = appManager.RetrieveFriends((int)Session["Userid"]);
            friendsInfo = appManager.RetrieveFriendsInfo(friendList, (int)Session["Userid"]);
            friendsModel.FriendList = friendsInfo;         
            return View(friendsModel);
        }

        [CustomAuthorize]
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

        [CustomAuthorize]
        public ActionResult EditAboutMe (PasteBookViewModel model)
        {
            USER currentUser = appManager.GetUserInfo((int)Session["Userid"]);
            currentUser.ABOUT_ME = model.User.ABOUT_ME.Trim();
            userManager.EditUser(currentUser);
            return RedirectToAction("UserProfile", new { userID = (string)Session["User"] });
        }

        [CustomAuthorize]
        public JsonResult AddFriend(FRIEND friend)
        {
            var result = appManager.AddFriend(friend);
            return Json(new { addFriendResult = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AcceptRequest(FRIEND friend)
        {
            FRIEND friendship = appManager.RetrieveFriendship(friend.USER_ID, friend.FRIEND_ID);
            friend.ID = friendship.ID;
            friend.CREATED_DATE = friendship.CREATED_DATE;
            var result = appManager.AcceptRequest(friend);
            return Json(new { acceptRequestResult = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IgnoreRequest(FRIEND friend)
        {
            FRIEND friendship = appManager.RetrieveFriendship(friend.USER_ID, friend.FRIEND_ID);
            friend.ID = friendship.ID;
            friend.CREATED_DATE = friendship.CREATED_DATE;
            var result = appManager.DeleteRequest(friend);
            return Json(new { ignoreRequestResult = result }, JsonRequestBehavior.AllowGet);
        }

        [Route("posts/{postID}")]
        [CustomAuthorize]
        public ActionResult ViewPost(int postID)
        {
            POST post = appManager.RetrieveSpecificPost(postID);
            return View(post);
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

        [Route("notifications")]
        [CustomAuthorize]
        public ActionResult Notifications()
        {
            List<NOTIFICATION> notifList = appManager.RetrieveNotifications((int)Session["Userid"]);
            return View(notifList);
        }

        public JsonResult CountNotifications()
        {
            var result = appManager.RetrieveNotifications((int)Session["Userid"]).Where(x => x.SEEN == "N").Count();
            return Json(new { notifCount = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClearNotification()
        {
            var result = appManager.ClearNotification((int)Session["Userid"]);
            return Json(new { clearNotifResult = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Comment(COMMENT comment)
        {
            var result = appManager.Comment(comment);
            return Json(new { likeResult = result }, JsonRequestBehavior.AllowGet);
        }

        [Route("search")]
        [CustomAuthorize]
        public ActionResult Search(string searchString)
        {
            UserViewModel searchFriends = new UserViewModel();
            searchFriends.SearchKey = searchString;
            searchFriends.UserList = userManager.SearchUser(searchString);
            return View(searchFriends);
        }

    }
}