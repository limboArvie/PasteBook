using PasteBookDataAccessLogic;
using PasteBookEntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PasteBookBusinessLogic
{
    public class PasteBookAppBLManager
    {

        UserDBManager userManager = new UserDBManager();
        PostDBManager postManager = new PostDBManager();
        FriendDBManager friendManager = new FriendDBManager();
        PBGenericDBManager<POST> postGenericManager = new PBGenericDBManager<POST>();
        PBGenericDBManager<FRIEND> friendGenericManager = new PBGenericDBManager<FRIEND>();
        PBGenericDBManager<COMMENT> commentGenericManager = new PBGenericDBManager<COMMENT>();
        PBGenericDBManager<NOTIFICATION> notifGenericManager = new PBGenericDBManager<NOTIFICATION>();
        PBGenericDBManager<LIKE> likeGenericManager = new PBGenericDBManager<LIKE>();

        public USER GetUserInfo(string username)
        {
            return userManager.RetrieveSpecificUser(username);
        }

        public USER GetUserInfo(int userID)
        {
            return userManager.RetrieveSpecificUser(userID);
        }

        public bool Post(POST post, int currentProfile)
        {
            post.CREATED_DATE = DateTime.Now;

            if (currentProfile != 0)
            {
                post.PROFILE_OWNER_ID = currentProfile;
            }
            return postGenericManager.Add(post);
        }

        public List<POST> RetrieveFeedPosts(int userID)
        {
            List<POST> listOfPost = new List<POST>();
            List<int> userIDs = new List<int>();

            var friendList = friendManager.RetrieveFriends(userID).Where(x => x.REQUEST == "Y").ToList();

            foreach (var item in friendList)
            {
                if(item.FRIEND_ID == userID)
                {
                    userIDs.Add(item.USER_ID);
                }

                else
                {
                    userIDs.Add(item.FRIEND_ID);
                }
            }

            userIDs.Add(userID);

            return postManager.RetrieveFeedPosts(userIDs);
        }

        public List<POST> RetrieveTimeLinePosts(int id)
        {
            return postManager.RetrieveTimeLinePosts(id);
        }
        
        public POST RetrieveSpecificPost(int id)
        {
            return postManager.RetrievePost(id);
        }

        public List<FRIEND> RetrieveFriends(int id)
        {
            List<FRIEND> listOfFriends = new List<FRIEND>();

            foreach (var item in friendManager.RetrieveFriends(id))
            {
                listOfFriends.Add(item);
            }

            return listOfFriends;
        }

        public List<USER> RetrieveFriendsInfo(List<FRIEND> friends, int id)
        {
            List<USER> friendsInfo = new List<USER>();

            foreach (var item in friends.Where(x => x.REQUEST == "Y"))
            {
                if (item.FRIEND_ID == id)
                {
                    item.FRIEND_ID = item.USER_ID;
                    item.USER_ID = id;
                }

                friendsInfo.Add(GetUserInfo(item.FRIEND_ID));
            }

            return friendsInfo.OrderBy(x => x.LAST_NAME).ToList();
        }

        public List<USER> RetrieveRequestsInfo(List<FRIEND> friends, int id)
        {
            List<USER> friendsInfo = new List<USER>();

            foreach (var item in friends.Where(x => x.REQUEST == "N" && x.USER_ID != id))
            {
                if (item.FRIEND_ID == id)
                {
                    item.FRIEND_ID = item.USER_ID;
                    item.USER_ID = id;
                }

                friendsInfo.Add(GetUserInfo(item.FRIEND_ID));
            }

            return friendsInfo.OrderBy(x => x.LAST_NAME).ToList();
        }

        public bool AddFriend(FRIEND friend)
        {
            friend.CREATED_DATE = DateTime.Now;
            bool result = friendGenericManager.Add(friend);

            if (result == true)
            {
                NOTIFICATION fRequestNotification = new NOTIFICATION()
                {
                    RECEIVER_ID = friend.FRIEND_ID,
                    NOTIF_TYPE = "F",
                    SENDER_ID = friend.USER_ID,
                    CREATED_DATE = DateTime.Now,
                    COMMENT_ID = null,
                    POST_ID = null,
                    SEEN = "N"
                };

                notifGenericManager.Add(fRequestNotification);
            }
            
            return result;
        }

        public bool AcceptRequest(FRIEND friend)
        {
            return friendGenericManager.Edit(friend);
        }

        public bool DeleteRequest(FRIEND friend)
        {
            return friendGenericManager.Delete(friend);
        }

        public FRIEND RetrieveFriendship(int user, int friend)
        {
            return friendManager.RetrieveFriendship(user, friend);
        }

        public bool LikePost(LIKE like)
        {
            POST post = postManager.RetrieveSpecificPost(like.POST_ID);
            LIKE likeResult = postManager.RetrieveSpecificLike(like.POST_ID, like.LIKED_BY);

            if (likeResult != null)
            {
                NOTIFICATION likeNotif = postManager.RetrieveSpecificLikeNotif(likeResult.LIKED_BY, likeResult.POST_ID);
                notifGenericManager.Delete(likeNotif);
                return likeGenericManager.Delete(likeResult);
            }

            bool result = postManager.LikePost(like);

            if(result == true && post.POSTER_ID != like.LIKED_BY)
            {
                NOTIFICATION likeNotification = new NOTIFICATION()
                {
                    RECEIVER_ID = post.POSTER_ID,
                    NOTIF_TYPE = "L",
                    SENDER_ID = like.LIKED_BY,
                    CREATED_DATE = DateTime.Now,
                    COMMENT_ID = null,
                    POST_ID = like.POST_ID,
                    SEEN = "N"
                };

                notifGenericManager.Add(likeNotification);
            }

            return result;
        }

        public List<LIKE> RetrieveLikes()
        {
            List<LIKE> listOfLike = new List<LIKE>();

            foreach (var item in postManager.RetrieveLikes())
            {
                listOfLike.Add(item);
            }

            return listOfLike;
        }

        public bool Comment(COMMENT comment)
        {
            comment.DATE_CREATED = DateTime.Now;
            bool result = commentGenericManager.Add(comment);

            POST post = postManager.RetrieveSpecificPost(comment.POST_ID);

            if (result == true && comment.POSTER_ID != post.POSTER_ID)
            {
                NOTIFICATION commentNotification = new NOTIFICATION()
                {
                    RECEIVER_ID = post.POSTER_ID,
                    NOTIF_TYPE = "C",
                    SENDER_ID = comment.POSTER_ID,
                    CREATED_DATE = DateTime.Now,
                    COMMENT_ID = comment.ID,
                    POST_ID = comment.POST_ID,
                    SEEN = "N"
                };

                notifGenericManager.Add(commentNotification);
            }
            
            return result;
        }

        public bool ClearNotification(int userID)
        {
            List<NOTIFICATION> unseenNotifList = RetrieveNotifications(userID).Where(x => x.SEEN == "N").ToList();

            foreach (var item in unseenNotifList)
            {
                item.SEEN = "Y";
                var result = notifGenericManager.Edit(item);

                if (result == false)
                {
                    return false;
                }
            }

            return true;
        }

        public List<NOTIFICATION> RetrieveNotifications(int currentUserID)
        {
            return postManager.RetrieveNotifications(currentUserID);
        }

    }
}