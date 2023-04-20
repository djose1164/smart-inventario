using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using System.Data.SqlClient;
using System.Configuration;

namespace DataLayer
{
    public class D_User
    {
        public bool createUser(E_User user)
        {
            bool success = false;
            var cmd = new SqlCommand("create_user", conn);
            cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar).Value = user.Email;
            cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar).Value = user.Password;
            cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = user.Name;
            cmd.Parameters.Add("@last_name", System.Data.SqlDbType.VarChar).Value = user.LastName;
            cmd.Parameters.Add("@is_admin", System.Data.SqlDbType.Bit).Value = user.IsAdmin;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                success = cmd.ExecuteNonQuery() != 0;
            }
            catch (SqlException)
            {
                success = false;
            }
            finally
            {
                conn.Close();
            }
            return success;
        }

        public bool verifyUser(E_User user)
        {
            bool success = false;
            var cmd = new SqlCommand("verify_user", conn);
            cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar).Value = user.Email;
            cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar).Value = user.Password;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                    success = reader.GetInt32(0) == 1;
            }
            finally
            {
                conn.Close();
            }
            return success;
        }

        public E_User getByEmail(string email)
        {
            var cmd = new SqlCommand("get_by_email", conn);
            cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar).Value = email;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            E_User user = null;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new E_User();
                    user.Name = reader["name"].ToString();
                    user.LastName = reader["last_name"].ToString();
                    user.Email = email;
                    user.Password = reader["password"].ToString();
                    user.IsAdmin = (bool)reader["is_admin"];
                }
            }
            finally
            {
                conn.Close();
            }

            return user;
        }

        public bool update(E_User user)
        {
            bool success = false;

            var cmd = new SqlCommand("update_user", conn);
            cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = user.Name;
            cmd.Parameters.Add("@last_name", System.Data.SqlDbType.VarChar).Value = user.LastName;
            cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar).Value = user.Email;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                success = true;
            }
            finally
            {
                conn.Close();
            }

            return success;
        }

        private static string uri = ConfigurationManager.ConnectionStrings["connSql"].ConnectionString;
        private SqlConnection conn = new SqlConnection(uri);
    }
}
