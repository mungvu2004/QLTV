using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QLTV
{
    class connection
    {
        private static string stringConnection = @"Data Source=M-M;Initial Catalog=QLTV;Integrated Security=True;TrustServerCertificate=True";
        public static SqlConnection Connection()
        {
            return new SqlConnection(stringConnection);
        }
    }
}
