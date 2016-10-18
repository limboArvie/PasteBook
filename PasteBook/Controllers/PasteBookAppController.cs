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
        PasteBookProfileManager profileManager = new PasteBookProfileManager();

        public ActionResult Index()
        {
            List<PostModel> listOfPosts = new List<PostModel>();
            listOfPosts = profileManager.RetrieveTimeLinePosts();
            pasteBook.ListOfPost = listOfPosts;
            return View(pasteBook);
        }

        [HttpPost]
        public ActionResult Index(PasteBookViewModel pasteBookModel)
        {
            PostModel post = new PostModel();
            UserModel user = new UserModel();

            user.USERNAME = (string)Session["User"];
            pasteBookModel.User = user;
            post = pasteBookModel.Post;
            post.POSTER_ID = (int)Session["Userid"];
            post.PROFILE_OWNER_ID = (int)Session["Userid"];
            profileManager.Post(post);

            return Index();
        }

        public ActionResult UserProfile(string userID)
        {
            UserModel currentUser = new UserModel();
            currentUser = profileManager.GetUserInfo(userID);
            pasteBook.User = currentUser;
            return View(pasteBook);
        }

        public JsonResult Like(LikeModel like)
        {
            var result = profileManager.LikePost(like);
            return Json(new { likeResult = result }, JsonRequestBehavior.AllowGet);
        }
    }
}