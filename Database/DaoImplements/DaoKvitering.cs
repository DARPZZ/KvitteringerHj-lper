using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kvitteringer.Database.DaoObjects;
using Microsoft.Data.SqlClient;

namespace Kvitteringer.Database.DaoImplements
{
    public class DaoKvitering : DaOInterface<Kvittering>
    {
        hest hest = new hest();
        public void Create(Kvittering t)
        {
            string connectionString = hest.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string checkFirmaSql = "IF EXISTS (SELECT 1 FROM tblFirma WHERE fldFirmaNavn = @fldFirmaNavn) " +
                                       "SELECT fldFirmaId FROM tblFirma WHERE fldFirmaNavn = @fldFirmaNavn " +
                                       "ELSE " +
                                       "SELECT -1";

                SqlCommand checkFirmaCommand = new SqlCommand(checkFirmaSql, connection);
                checkFirmaCommand.Parameters.AddWithValue("@fldFirmaNavn", t.firmaNavn);

                int firmaId = Convert.ToInt32(checkFirmaCommand.ExecuteScalar());

                if (firmaId != -1)
                {
                    insertMedId(connection, t, firmaId);
                }
                else
                {
                    // not in db
                    string insertFirmaSql = "INSERT INTO tblFirma (fldFirmaNavn) VALUES (@fldFirmaNavn); SELECT SCOPE_IDENTITY();";
                    SqlCommand insertFirmaCommand = new SqlCommand(insertFirmaSql, connection);
                    insertFirmaCommand.Parameters.AddWithValue("@fldFirmaNavn", t.firmaNavn);

                    firmaId = Convert.ToInt32(insertFirmaCommand.ExecuteScalar());

                    insertMedId(connection, t, firmaId);
                }
            }
        }


        private void insertMedId(SqlConnection connection, Kvittering t, int firmaId)
        {
            string sql = "INSERT INTO tblKvit (fldBuyDate, fldEndDate, fldOrdreNummer, fldFirmaId, fldEmailLink,fldProduktNavn,fldPris) " +
                         "VALUES (@købsDato, @slutDato, @ordreNummer,@firmaId, @email, @produktNavn,@pris)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@købsDato", t.købsDato);
            command.Parameters.AddWithValue("@slutDato", t.slutDato);
            command.Parameters.AddWithValue("@ordreNummer", t.ordreNummer);
            command.Parameters.AddWithValue("@firmaId", firmaId);
            command.Parameters.AddWithValue("@email", t.email);
            command.Parameters.AddWithValue("@ProduktNavn", t.produktNavn);
            command.Parameters.AddWithValue("@pris", t.produktPris);

            command.ExecuteNonQuery();
        }



        public void Delete(Kvittering t, int ID)
        {
            throw new NotImplementedException();
        }

        public Kvittering Get(int ID)
        {
            throw new NotImplementedException();
        }

        public List<Kvittering> GetAll()
        {
            List<Kvittering> kvit = new List<Kvittering>();

            try
            {

                string connectionString = hest.GetConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "select fldFirmaNavn, fldBuyDate,fldEndDate,fldOrdreNummer,fldEmailLink,fldProduktNavn,fldPris from tblKvit inner join tblFirma on tblKvit.fldFirmaId = tblFirma.fldFirmaId";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
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


        public void Update(Kvittering t, string fieldname, string value)
        {
            throw new NotImplementedException();
        }
    }
}
