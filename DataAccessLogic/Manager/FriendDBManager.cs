using PasteBookEntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBookDataAccessLogic
{
    public class FriendDBManager : DBManager
    {

        public List<FRIEND> RetrieveFriends(int id)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return context.FRIENDs.Where(x => x.USER_ID == id || x.FRIEND_ID == id).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorList.Add(ex);
                return null;
            }
        }

        public bool AddFriend(FRIEND friend)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    friend.CREATED_DATE = DateTime.Now;
                    context.FRIENDs.Add(friend);
                    return context.SaveChanges() > 0;
                }
            }

            catch (Exception ex)
            {
                ErrorList.Add(ex);
                return false;
            }
        }

        public FRIEND RetrieveFriendship(int user, int friend)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return context.FRIENDs.Where(x => (x.USER_ID == user && x.FRIEND_ID == friend) || (x.USER_ID == friend && x.FRIEND_ID == user)).SingleOrDefault();
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
