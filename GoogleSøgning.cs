using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Kvitteringer.Database.DaoObjects;

namespace Kvitteringer
{
    public class GoogleSøgning
    {
        public void findEmail(DataGrid data)
        {
            try
            {
                Kvittering selectedItem = (Kvittering)data.SelectedItem;
                if (selectedItem != null)
                {
                    string emailLink = selectedItem.email;
                    var Result = MessageBox.Show("Are you sure you want to open " + emailLink, "Confirm link", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (Result == MessageBoxResult.Yes)
                    {
                        if (emailLink.StartsWith("https://"))
                        {
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = emailLink,
                                UseShellExecute = true
                            });
                        }
                        else
                        {
                            Debug.WriteLine("You dont have the right link");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Stien blev ikek fundet" + ex.Message);
            }
        }
    }
}
