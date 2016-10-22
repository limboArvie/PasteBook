using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBookDataAccessLogic
{
    public class DBManager
    {
        public List<Exception> ErrorList { get; set; }

        public DBManager()
        {
            this.ErrorList = new List<Exception>();
        }
    }
}
