using System.Collections.Generic;
using PasteBookDataAccessLogic;
using PasteBookEntityFramework;
using System;

namespace PasteBookBusinessLogic
{
    public class UserBLManager
    {
        PBGenericDBManager<USER> userGenericManager = new PBGenericDBManager<USER>();

        UserDBManager userManager = new UserDBManager();

        public List<REF_COUNTRY> RetrieveCountryList()
        {
            return userManager.RetrieveCountryList();
        }

        public bool CheckUsername(string username)
        {
            return userManager.CheckUsernameExists(username);
        }

        public bool CheckEmail(string email)
        {
            return userManager.CheckEmailExists(email);
        }

        public bool RegisterUser(USER user)
        {
            string salt = null;
            string hash = GeneratePasswordHash(user.PASSWORD, out salt);
            user.SALT = salt;
            user.PASSWORD = hash;
            user.DATE_CREATED = DateTime.Now;

            if (user.GENDER == null)
            {
                user.GENDER = "U";
            }

            return userGenericManager.Add(user);
        }

        public bool EditUser(USER user)
        {
            return userGenericManager.Edit(user);
        }

        public USER LoginUser(USER user)
        {
            USER userResult = new USER();
            bool checkUser = false;
            bool passwordMatching = false;

            checkUser = userManager.CheckEmailExists(user.EMAIL_ADDRESS);
           
            if (checkUser == false)
            {
                return null;
            }

            userResult = userManager.Login(user);
            passwordMatching = IsPasswordMatch(user.PASSWORD, userResult.SALT, userResult.PASSWORD);

            if (passwordMatching)
            {
                return userResult;
            }

            else
            {
                return null;
            }
        }

        private string GeneratePasswordHash(string password, out string salt)
        {
            SaltGenerator saltGen = new SaltGenerator();
            HashComputer m_hashComputer = new HashComputer();
            salt = saltGen.GetSaltString();
            string finalString = password + salt;
            return m_hashComputer.GetPasswordHashAndSalt(finalString);
        }

        private bool IsPasswordMatch(string password, string salt, string hash)
        {
            HashComputer m_hashComputer = new HashComputer();
            string finalString = password + salt;
            return hash == m_hashComputer.GetPasswordHashAndSalt(finalString);
        }

    }
}