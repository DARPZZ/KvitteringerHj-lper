using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kvitteringer.Database;
using Kvitteringer.Database.DaoObjects;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Kvitteringer
{
    public class Search
    {
        hest hest = new hest();
        public List<Kvittering> searchForeProduct(string searchTerm)
        {
            List<Kvittering> kvit = new List<Kvittering>();

            try
            {
                string connectionString = hest.GetConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT fldFirmaNavn, fldBuyDate, fldEndDate, fldOrdreNummer, fldEmailLink, fldProduktNavn, fldPris " +
                                 "FROM tblKvit INNER JOIN tblFirma ON tblKvit.fldFirmaId = tblFirma.fldFirmaId " +
                                 "WHERE fldFirmaNavn LIKE @searchTerm";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@searchTerm", searchTerm + "%");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string firmaNavn = reader.GetString(0); 
                                DateOnly købsDato = DateOnly.FromDateTime(reader.GetDateTime(1)); 
                                DateOnly slutDato = DateOnly.FromDateTime(reader.GetDateTime(2)); 
                                int ordreNummer = reader.GetInt32(3); 
                                string email = reader.GetString(4); 
                                string produktNavn = reader.GetString(5); 
                                double produktPris = reader.GetDouble(6);

                                Kvittering kvittering = new Kvittering(firmaNavn, købsDato, slutDato, ordreNummer, email, produktNavn, produktPris);
                                kvit.Add(kvittering);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return kvit;
        }

    }
}
