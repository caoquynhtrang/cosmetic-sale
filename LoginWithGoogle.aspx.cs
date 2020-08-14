using CSharpAssignment.Models;
using CSharpAssignment.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSharpAssignment
{
    public partial class LoginWithGoogle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string code = Request.QueryString["code"];
            string accessToken = GoogleUtil.GetAccessToken(code).Result;
            AccountDTO accountDTO = GoogleUtil.GetUserInfo(accessToken).Result;
            if (!CheckLogin(accountDTO.Email))
            {
                int idInsert = InsertAccount(accountDTO.Email, accountDTO.Picture, accountDTO.Fullname);
                if (idInsert != -1)
                {
                    accountDTO.AccountID = idInsert;
                    Session["USER"] = accountDTO;
                    Session["AccountID"] = idInsert;
                    
                }
            }
            Response.Redirect("HomePage.aspx");
        }

        private bool CheckLogin(string email)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSharpAssignmentConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand com = new SqlCommand("Select [ID],[Fullname],[StatusID],[Email],[Picture],[Role] From Account Where Email = @email and StatusID = 1", conn);
            com.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                int id = dr.GetInt32(0);
                string fullname = dr.GetString(1);
                int statusId = dr.GetInt32(2);
                string picture = dr.GetString(4);
                string role = dr.GetString(5);
                AccountDTO userInfo = new AccountDTO(id, fullname, statusId, email, picture);
                userInfo.Role = role;
                Session["USER"] = userInfo;
                Session["AccountID"] = userInfo.AccountID;
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }
        private int InsertAccount(string email, string picture, string fullname)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSharpAssignmentConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand com = new SqlCommand("INSERT INTO [dbo].[Account] ([Email],[Picture],[Fullname],StatusID, Role) OUTPUT Inserted.ID VALUES(@email, @picture, @fullname, 1, 'USER')", conn);
            com.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
            com.Parameters.Add("@picture", SqlDbType.VarChar).Value = picture;
            com.Parameters.Add("@fullname", SqlDbType.NVarChar).Value = fullname;


            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                int id = dr.GetInt32(0);
                conn.Close();
                return id;
            }
            conn.Close();
            return -1;
        }

    }
}