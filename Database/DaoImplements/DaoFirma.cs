using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Kvitteringer.Database.DaoObjects;
using Microsoft.Data.SqlClient;

namespace Kvitteringer.Database.DaoImplements
{
    public class DaoFirma : DaOInterface<Firma>
    {
        hest hest = new hest();


        public void Create(Firma t)
        {
           
        }



        public void Delete(Firma t, int ID)
        {
            throw new NotImplementedException();
        }

        public Firma Get(int ID)
        {
            throw new NotImplementedException();
        }

        public List<Firma> GetAll()
        {
            List<Firma> firms = new List<Firma>();

            try
            {

                string connectionString = hest.GetConnectionString(); 

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM tblFirma";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               
                                Firma firma = new Firma(reader.GetInt32(0), reader.GetString(1));
                                firms.Add(firma);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return firms;
        }



        public void Update(Firma t, string fieldname, string value)
        {
            throw new NotImplementedException();
        }
    }
}
