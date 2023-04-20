using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using EntityLayer;
using System.Data.SqlClient;

namespace DataLayer
{
    public class D_Product
    {
        public bool insert(E_Product product)
        {
            bool success = false;

            var cmd = new SqlCommand("create_product", conn);
            cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = product.Name;
            cmd.Parameters.Add("@description", System.Data.SqlDbType.VarChar).Value = product.Description;
            cmd.Parameters.Add("@price", System.Data.SqlDbType.Int).Value = product.Price;
            cmd.Parameters.Add("@stocks", System.Data.SqlDbType.Int).Value = product.Stocks;
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

        public bool update(E_Product product, bool partial)
        {
            bool success = false;

            var cmd = new SqlCommand(partial ? "modify_product" : "update_product", conn);
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = product.Id;
            cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = product.Name;
            cmd.Parameters.Add("@description", System.Data.SqlDbType.VarChar).Value = product.Description;
            cmd.Parameters.Add("@price", System.Data.SqlDbType.Int).Value = product.Price;
            cmd.Parameters.Add("@stocks", System.Data.SqlDbType.Int).Value = product.Stocks;
            if (!partial)
                cmd.Parameters.Add("@sold_quantity", System.Data.SqlDbType.Int).Value = product.SoldQuantity;    
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                success = cmd.ExecuteNonQuery() != 0;
            }
            finally
            {
                conn.Close();
            }

            return success;
        }

        public E_Product getByName(string name)
        {
            E_Product fetched = null;

            var cmd = new SqlCommand("get_product_by_name", conn);
            cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    fetched = new E_Product();
                    fetched.Id = (int)reader["id"];
                    fetched.Name = reader["name"].ToString();
                    fetched.Description = reader["description"].ToString();
                    fetched.Price = (int)reader["price"];
                    fetched.Stocks = (int)reader["stocks"];
                    fetched.SoldQuantity = (int)reader["sold_quantity"];
                }
            }
            finally
            {
                conn.Close();
            }

            return fetched;
        }

        public bool delete(E_Product product)
        {
            bool success = false;

            var cmd = new SqlCommand("delete_product", conn);
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = product.Id;
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
