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

        public void searchForeProductName(string searchTerm, List<Kvittering> firmlist, DataGrid data)
        {

            if (!string.IsNullOrEmpty(searchTerm))
            {
                firmlist = firmlist.Where(k => k.produktNavn.Contains(searchTerm)).ToList();
                Debug.WriteLine(firmlist);
                data.ItemsSource = firmlist;
            }
        }
    }
}
