using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Kvitteringer.Database
{
    public class hest
    {
        public string GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "localhost";
            builder.UserID = "sa";
            builder.Password = "123456";
            builder.InitialCatalog = "Kvitteringer";
            builder.TrustServerCertificate = true;

            return builder.ConnectionString;
        }

    }
}
