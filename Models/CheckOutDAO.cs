using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CSharpAssignment.Models
{
    public class CheckOutDAO
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSharpAssignmentConnectionString"].ConnectionString);
        public bool InsertIntoBooking(DateTime today, string bookingCode, string address, string phone, string paymentType, string contactPerson, string description, int accountID, int discountID, string cusName)
        {
            bool res = false;


            try
            {
                conn.Open();

                SqlCommand com = new SqlCommand("Insert into Booking(BookingCode,StatusID, CustomerName, Address, Phone, ImportDate, PaymentType, ContactPerson, Description, AccountID, DiscountID) " +
                                            "values (@bookingCode, @status, @customerName,@address,@phone,@importDate,@paymentType,@contactPerson,@description, @accountID, @discountID)", conn);
                com.Parameters.Add("@customerName", SqlDbType.VarChar);
                com.Parameters.Add("@address", SqlDbType.VarChar);
                com.Parameters.Add("@phone", SqlDbType.VarChar);
                com.Parameters.Add("@importDate", SqlDbType.DateTime);
                com.Parameters.Add("@paymentType", SqlDbType.VarChar);
                com.Parameters.Add("@contactPerson", SqlDbType.VarChar);
                com.Parameters.Add("@description", SqlDbType.VarChar);
                com.Parameters.Add("@accountID", SqlDbType.Int);
                com.Parameters.Add("@bookingCode", SqlDbType.VarChar);
                com.Parameters.Add("@status", SqlDbType.Int);
                com.Parameters.Add("@discountID", SqlDbType.Int);

                com.Parameters["@customerName"].Value = cusName;
                com.Parameters["@address"].Value = address;
                com.Parameters["@phone"].Value = phone;
                com.Parameters["@importDate"].Value = today;
                com.Parameters["@paymentType"].Value = paymentType;
                com.Parameters["@contactPerson"].Value = contactPerson;
                com.Parameters["@description"].Value = description;
                if(accountID == -1)
                {
                    com.Parameters["@accountID"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["@accountID"].Value = accountID;
                }
                if(discountID == -1)
                {
                    com.Parameters["@discountID"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["@discountID"].Value = discountID;
                }
                com.Parameters["@status"].Value = 6;
                com.Parameters["@bookingCode"].Value = bookingCode;
                int rowAffect = com.ExecuteNonQuery();
                if (rowAffect > 0)
                {
                    res = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return res;
        }
        public bool InsertBookingDetails(int bookingId, int productId, int amount)
        {
            try
            {
                conn.Open();

                SqlCommand com = new SqlCommand("Insert into BookingDetail(BookingID, ProductID, Amount) values(@bookingID,@productID, @amount)", conn);
                com.Parameters.Add("@bookingID", SqlDbType.Int);
                com.Parameters.Add("@productID", SqlDbType.Int);
                com.Parameters.Add("@amount", SqlDbType.Int);


                com.Parameters["@bookingID"].Value = bookingId;
                com.Parameters["@productID"].Value = productId;
                com.Parameters["@amount"].Value = amount;

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

        public int GetBookingId(DateTime today)
        {
            int number = -1;
            try
            {

                conn.Open();
                SqlCommand comm = new SqlCommand("Select ID from Booking where ImportDate = @today", conn);
                comm.Parameters.Add("@today", SqlDbType.DateTime);


                comm.Parameters["@today"].Value = today;
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
    }
}