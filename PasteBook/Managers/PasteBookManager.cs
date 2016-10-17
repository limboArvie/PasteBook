using DataAccessLogic;
using PasteBook.Managers;
using PasteBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook.Managers
{
    public class PasteBookManager
    {

        PasteBookDBManager manager = new PasteBookDBManager();
        Mapper mapper = new Mapper();

        public List<Ref_CountryModel> RetrieveCountryList()
        {
            List<Ref_CountryModel> countryList = new List<Ref_CountryModel>();

            foreach (var item in manager.RetrieveCountryList())
            {
                countryList.Add(mapper.REF_COUNTRYtoModel(item));
            }

            return countryList;
        }

        public bool CheckUsername(string username)
        {
            return manager.CheckUsernameExists(username);
        }

        public bool CheckEmail(string email)
        {
            return manager.CheckEmailExists(email);
        }

        public bool RegisterUser(UserModel user)
        {
            return manager.RegisterUser(mapper.UserToDB(user));
        }

        public UserModel LoginUser(UserModel user)
        {
            var userResult = manager.Login(mapper.UserToDB(user));

            if (userResult == null)
            {
                return null;
            }

            return mapper.UserToModel(userResult);
        }

    }
}