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
                USERNAME = user.USER_NAME,
                PASSWORD = user.PASSWORD,
                FIRST_NAME = user.FIRST_NAME,
                LAST_NAME = user.LAST_NAME,
                BIRTHDAY = user.BIRTHDAY,
                COUNTRY = user.COUNTRY_ID,
                MOBILE_NUMBER = user.MOBILE_NO,
                GENDER = user.GENDER,
                EMAIL_ADDRESS = user.EMAIL_ADDRESS
            };
        }
    }
}