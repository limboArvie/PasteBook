using PasteBookEntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace PasteBookDataAccessLogic
{
    public class PostDBManager : DBManager
    {
        public List<POST> RetrieveFeedPosts(List<int> userIDs)
        {
            List<POST> postList = new List<POST>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {

                    return context.POSTs.Include("USER")
                                        .Include("USER1")
                                        .Include("COMMENTs.USER")
                                        .Include("LIKEs.USER")
                                        .Where(x=>userIDs.Contains(x.POSTER_ID))
                                        .OrderByDescending(x => x.CREATED_DATE)
                                        .ToList();
                }
            }

            catch (Exception ex)
            {
                ErrorList.Add(ex);
                return null;
            }
        }

        public List<POST> RetrieveTimeLinePosts(int id)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return context.POSTs.Include("USER")
                                        .Include("USER1")
                                        .Include("COMMENTs.USER")
                                        .Include("LIKEs.USER")
                                        .Where(x => x.PROFILE_OWNER_ID == id)
                                        .OrderByDescending(x => x.CREATED_DATE)
                                        .Take(100)
                                        .ToList();
                }
            }

            catch (Exception ex)
            {
                ErrorList.Add(ex);
                return null;
            }
        }

        public bool LikePost(LIKE like)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.LIKEs.Add(like);
                    return context.SaveChanges() > 0;
                }
            }

            catch (Exception ex)
            {
                ErrorList.Add(ex);
                return false;
            }
        }

        public POST RetrieveSpecificPost(int postID)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return context.POSTs.Where(x => x.ID == postID).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                ErrorList.Add(ex);
                return null;
            }
        }

        public List<LIKE> RetrieveLikes()
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return context.LIKEs.ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorList.Add(ex);
                return null;
            }
        }

        public LIKE RetrieveSpecificLike(int post, int user)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return context.LIKEs.Where(x => x.POST_ID == post && x.LIKED_BY == user).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                ErrorList.Add(ex);
                return null;
            }
        }

        public List<NOTIFICATION> RetrieveNotifications(int userID)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return context.NOTIFICATIONs.Include("USER")
                                                .Include("USER1")
                                                .Include("COMMENT")
                                                .Include("POST")
                                                .Where(x => x.RECEIVER_ID == userID)
                                                .OrderByDescending(x => x.CREATED_DATE)
                                                .ToList();
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
