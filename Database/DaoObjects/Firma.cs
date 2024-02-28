using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvitteringer.Database.DaoObjects
{
    public class Firma
    {
        public int firmaID { get; set; }
        public string firmaName { get; set; }

        public Firma(int firmaID, string firmaName)
        {
            this.firmaID = firmaID;
            this.firmaName = firmaName;
        }

    }
}
