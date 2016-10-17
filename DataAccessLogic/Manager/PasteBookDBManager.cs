using PasteBookEntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic
{
    public class PasteBookDBManager
    {
        List<Exception> errorList = new List<Exception>();

        public List<REF_COUNTRY> RetrieveCountryList()
        {
            using (var context = new PASTEBOOKEntities())
            {
                return context.REF_COUNTRY.ToList();
            }
        }
        
        public bool CheckUsernameExists(string username)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return context.USERs.Where(x => x.USER_NAME == username).Count() != 0;
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
                return false;
            }
        }

        public bool CheckEmailExists(string email)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return context.USERs.Where(x => x.EMAIL_ADDRESS == email).Count() != 0;
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
                return false;
            }
        }

        public bool RegisterUser(USER user)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
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

                    context.USERs.Add(user);
                    return context.SaveChanges() > 0;
                }
            }

            catch (Exception ex)
            {
                errorList.Add(ex);
                return false;
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

        public USER Login(USER user)
        {
            try
            {
                bool passwordMatching = false;

                using (var context = new PASTEBOOKEntities())
                {
                    var result = context.USERs.Where(x => x.EMAIL_ADDRESS == user.EMAIL_ADDRESS).SingleOrDefault();
                    if (result == null)
                    {
                        return null;
                    }
                    else
                    {
                        passwordMatching = IsPasswordMatch(user.PASSWORD, result.SALT, result.PASSWORD);
                    }

                    if (passwordMatching)
                    {
                        return result;
                    }

                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
                return null;
            }
        }

        private bool IsPasswordMatch(string password, string salt, string hash)
        {
            HashComputer m_hashComputer = new HashComputer();
            string finalString = password + salt;
            return hash == m_hashComputer.GetPasswordHashAndSalt(finalString);
        }
    }
}
