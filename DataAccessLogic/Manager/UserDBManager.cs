using PasteBookEntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBookDataAccessLogic
{
    public class UserDBManager : DBManager
    {
        
        public List<REF_COUNTRY> RetrieveCountryList()
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return context.REF_COUNTRY.ToList();
                }
            }

            catch (Exception ex)
            {
                ErrorList.Add(ex);
                return null;
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
                ErrorList.Add(ex);
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
                ErrorList.Add(ex);
                return false;
            }
        }

        public USER Login(USER user)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return context.USERs.Where(x => x.EMAIL_ADDRESS == user.EMAIL_ADDRESS).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                ErrorList.Add(ex);
                return null;
            }
        }

        public USER RetrieveSpecificUser(string username)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return context.USERs.Include("REF_COUNTRY").Where(x => x.USER_NAME == username).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                ErrorList.Add(ex);
                return null;
            }
        }

        public USER RetrieveSpecificUser(int userID)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return context.USERs.Include("REF_COUNTRY").Where(x => x.ID == userID).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                ErrorList.Add(ex);
                return null;
            }
        }

        public List<USER> SearchUser(string searchString)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return context.USERs.Include("REF_COUNTRY").Where(x => x.FIRST_NAME == searchString || x.LAST_NAME == searchString).ToList();
                    //return context.USERs.Include("REF_COUNTRY").Where(x => x.FIRST_NAME.Contains(searchString) || x.LAST_NAME.Contains(searchString)).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorList.Add(ex);
                return null;
            }
        }
    }
}
