using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CSharpAssignment.Models
{
    public class DiscountDAO
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSharpAssignmentConnectionString"].ConnectionString);


        public decimal CheckDiscountCode(string code)
        {
            decimal number = -1;
            try
            {
                DateTime today = DateTime.Today;
                conn.Open();
                SqlCommand comm = new SqlCommand("Select Value from Discount where ExpiredDate >= @today and StatusID = 1 and Code = @code", conn);
                comm.Parameters.Add("@today", SqlDbType.DateTime);
                comm.Parameters.Add("@code", SqlDbType.VarChar);
                comm.Parameters["@today"].Value = today;
                comm.Parameters["@code"].Value = code;
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
        public int GetDiscountID(string code)
        {
            int number = -1;
            try
            {
                DateTime today = DateTime.Today;
                conn.Open();
                SqlCommand comm = new SqlCommand("Select ID from Discount where Code = @code", conn);
                comm.Parameters.Add("@today", SqlDbType.DateTime);
                comm.Parameters.Add("@code", SqlDbType.VarChar);

                comm.Parameters["@today"].Value = today;
                comm.Parameters["@code"].Value = code;
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

        public bool CheckAlreadyUsedDiscount(string code, int accountId)
        {
            try
            {

                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT D.Code FROM dbo.Booking B, dbo.Discount D WHERE D.StatusID <> 3 and B.DiscountID = D.ID AND B.AccountID = @accountId and d.Code = @code", conn);
                comm.Parameters.Add("@accountId", SqlDbType.Int);
                comm.Parameters.Add("@code", SqlDbType.VarChar);

                comm.Parameters["@accountId"].Value = accountId;
                comm.Parameters["@code"].Value = code;
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                conn.Close();


            }
            catch
            {
                throw;
            }
            return false;
        }
    }
}