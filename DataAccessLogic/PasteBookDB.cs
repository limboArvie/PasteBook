using PasteBookEntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic
{
    public class PasteBookDB
    {
        public void RegisterUser(USER user)
        {
            using (var context = new PASTEBOOKEntities())
            {
                context.USERs.Add(user);
                context.SaveChanges();
            }
        }
    }
}
