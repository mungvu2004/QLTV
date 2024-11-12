using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;

namespace QLTV
{
    internal class Handel
    {
        SqlDataReader dataReader;
        SqlCommand sqlCommand;
        public Handel() { }

        public List<Users> User(string query)
        {
            List<Users> users = new List<Users>();

            using (SqlConnection sql = connection.Connection())
            {
                sql.Open();
                sqlCommand = new SqlCommand(query, sql);
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    users.Add(new Users(dataReader.GetString(1), dataReader.GetString(2)));
                }
                sql.Close();
            }
            return users;
        }

        public void Command(string query)
        {
            using (SqlConnection sql = connection.Connection())
            {
                sql.Open();
                sqlCommand = new SqlCommand(query, sql);
                sqlCommand.ExecuteNonQuery();
                sql.Close();
            }
        }
        public bool CheckEmail(string email)
        {
            using(SqlConnection sql = connection.Connection())
            {
                sql.Open();
                string query = "select COUNT(*) from Users where Email = @email";
                sqlCommand = new SqlCommand(query, sql);
                sqlCommand.Parameters.AddWithValue("@email", email);
                sqlCommand.Connection = sql;
                int userEsxit = (int)sqlCommand.ExecuteScalar();
                sql.Close();
                return userEsxit > 0;
            }
        }

    }
}
