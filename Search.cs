using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Kvitteringer.Database;
using Kvitteringer.Database.DaoImplements;
using Kvitteringer.Database.DaoObjects;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Kvitteringer
{
    public class Search
    {

        public List<Kvittering> searchForeProductName(string searchTerm, List<Kvittering> firmlist)
        {
            List<Kvittering> filteredList = new List<Kvittering>();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                filteredList = firmlist.Where(k => k.produktNavn.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            return filteredList;
        }


    }
}
