using PasteBookEntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBookDataAccessLogic
{
    public class PBGenericDBManager<T> where T : class
    {
        List<Exception> ErrorList = new List<Exception>();

        public bool Add(T entity)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.Entry(entity).State = System.Data.Entity.EntityState.Added;
                    return context.SaveChanges() > 0;
                }
            }

            catch (Exception ex)
            {
                ErrorList.Add(ex);
                return false;
            }
        }

        public bool Edit(T entity)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    return context.SaveChanges() > 0;
                }
            }

            catch (Exception ex)
            {
                ErrorList.Add(ex);
                return false;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    return context.SaveChanges() > 0;
                }
            }

            catch (Exception ex)
            {
                ErrorList.Add(ex);
                return false;
            }
        }

        //public List<T> RetrieveEntityList()
        //{
        //    try
        //    {
        //        using (var context = new PASTEBOOKEntities())
        //        {
        //            return context.Set<T>().ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorList.Add(ex);
        //        return null;
        //    }
        //}
    }
}
