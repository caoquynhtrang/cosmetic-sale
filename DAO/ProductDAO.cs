using CSharpAssignment.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CSharpAssignment.DAO
{
    public class ProductDAO
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["CSharpAssignmentConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);

        public bool UpdateQuantityOfProduct(int id, int newQuantity)
        {


            try
            {
                conn.Open();

                SqlCommand com = new SqlCommand("Update Product set Quantity = @newQuantity where ID = @id", conn);
                com.Parameters.Add("@newQuantity", SqlDbType.Int);
                com.Parameters.Add("@id", SqlDbType.Int);
                com.Parameters["@newQuantity"].Value = newQuantity;
                com.Parameters["@id"].Value = id;
                int rowAffect = com.ExecuteNonQuery();
                if (rowAffect > 0)
                {
                    return true;
                }
                conn.Close();


            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return false;
        }
        public decimal GetPriceOfProduct(int id)
        {
            decimal number = -1;
            try
            {

                conn.Open();
                SqlCommand comm = new SqlCommand("Select Price from Product where ID = @id", conn);
                comm.Parameters.Add("@id", SqlDbType.Int);


                comm.Parameters["@id"].Value = id;
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    number = reader.GetDecimal(0);
                }


            }
            catch (Exception e)
            {

            }
            finally
            {

                conn.Close();
            }
            return number;

        }
        public ProductDTO GetProductByID(int id)
        {
            ProductDTO pro = null;
            try
            {

                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT [Quantity],[Price],[Usage],[Name],[StatusID],[CateID],[ImageLink] FROM [dbo].[Product] WHERE ID = @id", conn);
                comm.Parameters.Add("@id", SqlDbType.Int);


                comm.Parameters["@id"].Value = id;
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {

                    int quantity = reader.GetInt32(0);
                    double price = double.Parse(reader.GetDecimal(1).ToString());
                    string usage = reader.GetString(2);
                    string name = reader.GetString(3);
                    int statusID = reader.GetInt32(4);
                    int cateID = reader.GetInt32(5);
                    string img = reader.GetString(6);
                    pro = new ProductDTO(name, quantity, price, usage, cateID, statusID, img);
                    pro.ID = id;
                }


            }
            catch (Exception e)
            {

            }
            finally
            {

                conn.Close();
            }
            return pro;

        }

        public int GetQuantityOfProduct(int id)
        {
            int number = -1;
            try
            {

                conn.Open();
                SqlCommand comm = new SqlCommand("Select Quantity from Product where ID = @id", conn);
                comm.Parameters.Add("@id", SqlDbType.Int);


                comm.Parameters["@id"].Value = id;
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    number = reader.GetInt32(0);
                }


            }
            catch (Exception e)
            {

            }
            finally
            {

                conn.Close();
            }
            return number;

        }
        public bool InsertProduct(ProductDTO dto)
        {
            bool result = false;
            try
            {
                conn.Open();
                String sql = "INSERT INTO Product(Name, Quantity, Price, Usage, CateID, StatusID, ImageLink) " +
                    "VALUES(@name, @quantity, @price, @usage, @cateID, @statusID, @imageLink)";
                SqlCommand com = new SqlCommand(sql, conn);

                com.Parameters.Add("@name", SqlDbType.NVarChar);
                com.Parameters.Add("@quantity", SqlDbType.Int);
                com.Parameters.Add("@price", SqlDbType.Float);
                com.Parameters.Add("@usage", SqlDbType.NVarChar);
                com.Parameters.Add("@cateID", SqlDbType.Int);
                com.Parameters.Add("@statusID", SqlDbType.Int);
                com.Parameters.Add("@imageLink", SqlDbType.VarChar);

                com.Parameters["@name"].Value = dto.Name;
                com.Parameters["@quantity"].Value = dto.Quantity;
                com.Parameters["@price"].Value = dto.Price;
                com.Parameters["@usage"].Value = dto.Usage;
                com.Parameters["@cateID"].Value = dto.CateID;
                com.Parameters["@statusID"].Value = dto.StatusID;
                com.Parameters["@imageLink"].Value = dto.ImageLink;

                if (com.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public bool UpdateProduct(ProductDTO dto)
        {
            bool result = false;
            try
            {
                conn.Open();
                String sql = "UPDATE Product " +
                    "SET Name = @name, Quantity = @quantity, Price = @price, " +
                    "Usage = @usage, CateID = @cateID, StatusID = @statusID, " +
                    "ImageLink = @imageLink " +
                    "WHERE ID = @id";
                SqlCommand com = new SqlCommand(sql, conn);

                com.Parameters.Add("@name", SqlDbType.NVarChar);
                com.Parameters.Add("@quantity", SqlDbType.Int);
                com.Parameters.Add("@price", SqlDbType.Float);
                com.Parameters.Add("@usage", SqlDbType.NVarChar);
                com.Parameters.Add("@cateID", SqlDbType.Int);
                com.Parameters.Add("@statusID", SqlDbType.Int);
                com.Parameters.Add("@imageLink", SqlDbType.VarChar);
                com.Parameters.Add("@id", SqlDbType.Int);

                com.Parameters["@name"].Value = dto.Name;
                com.Parameters["@quantity"].Value = dto.Quantity;
                com.Parameters["@price"].Value = dto.Price;
                com.Parameters["@usage"].Value = dto.Usage;
                com.Parameters["@cateID"].Value = dto.CateID;
                com.Parameters["@statusID"].Value = dto.StatusID;
                com.Parameters["@imageLink"].Value = dto.ImageLink;
                com.Parameters["@id"].Value = dto.ID;

                if (com.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public bool DeleteProduct(int id)
        {
            bool result = false;
            try
            {
                conn.Open();

                string sql = "DELETE FROM Product WHERE ID = @id";
                SqlCommand com = new SqlCommand(sql, conn);
                com.Parameters.Add("@id", SqlDbType.Int);
                com.Parameters["@id"].Value = id;

                if (com.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}