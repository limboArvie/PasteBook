using PasteBook.Models;
using PasteBookEntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook
{ 
    public class Mapper
    {
        public Ref_CountryModel REF_COUNTRYtoModel(REF_COUNTRY country)
        {
            return new Ref_CountryModel()
            {
                ID = country.ID,
                COUNTRY = country.COUNTRY
            };
        }

        public USER UserToDB(UserModel user)
        {
            return new USER()
            {
                USER_NAME = user.USERNAME,
                PASSWORD = user.PASSWORD,
                FIRST_NAME = user.FIRST_NAME,
                LAST_NAME = user.LAST_NAME,
                BIRTHDAY = user.BIRTHDAY,
                COUNTRY_ID = user.COUNTRY,
                MOBILE_NO = user.MOBILE_NUMBER,
                GENDER = user.GENDER,
                EMAIL_ADDRESS = user.EMAIL_ADDRESS
            };
        }

        public UserModel UserToModel(USER user)
        {
            return new UserModel()
            {
                ID = user.ID,
                USERNAME = user.USER_NAME,
                FIRST_NAME = user.FIRST_NAME,
                LAST_NAME = user.LAST_NAME,
                BIRTHDAY = user.BIRTHDAY,
                COUNTRY = user.COUNTRY_ID,
                MOBILE_NUMBER = user.MOBILE_NO,
                GENDER = user.GENDER,
                EMAIL_ADDRESS = user.EMAIL_ADDRESS,
                PROFILE_PIC = user.PROFILE_PIC,
                DATE_CREATED = user.DATE_CREATED,
                ABOUT_ME = user.ABOUT_ME
            };
        }

        public POST PostToDB(PostModel post)
        {
            return new POST()
            {
                CONTENT = post.Content,
                PROFILE_OWNER_ID = post.PROFILE_OWNER_ID,
                POSTER_ID = post.POSTER_ID
            };
        }

        public PostModel PostToModel(POST post)
        {
            return new PostModel()
            {
                ID = post.ID,
                CREATED_DATE = post.CREATED_DATE,
                Content = post.CONTENT,
                POSTER_ID = post.POSTER_ID,
                PROFILE_OWNER_ID = post.PROFILE_OWNER_ID
            };
        }

        public FriendModel FriendToModel(FRIEND friend)
        {
            return new FriendModel()
            {
                ID = friend.ID,
                USER_ID = friend.USER_ID,
                FRIEND_ID = friend.FRIEND_ID,
                REQUEST = friend.REQUEST,
                BLOCKED = friend.BLOCKED,
                CREATED_DATE = friend.CREATED_DATE
            };
        }

        public FRIEND FriendToDB(FriendModel friend)
        {
            return new FRIEND()
            {
                USER_ID = friend.USER_ID,
                FRIEND_ID = friend.FRIEND_ID,
                REQUEST = friend.REQUEST,
                BLOCKED = friend.BLOCKED
            };
        }

        public LIKE LikeToDB(LikeModel like)
        {
            return new LIKE()
            {
                POST_ID = like.POST_ID,
                LIKED_BY = like.LIKED_BY
            };
        }

        public LikeModel LikeToModel(LIKE like)
        {
            return new LikeModel()
            {
                ID = like.ID,
                POST_ID = like.POST_ID,
                LIKED_BY = like.LIKED_BY
            };
        }
    }
}