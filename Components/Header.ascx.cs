using CSharpAssignment.Components;
using CSharpAssignment.Models;
using CSharpAssignment.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSharpAssignment
{
    public partial class Header : System.Web.UI.UserControl
    {
        public string Fullname;
        public string Shortname;
        public string Picture;
        public string RedirectLink { get; set; } = GoogleUtil.GetRedirectLink();
        protected void Page_Load(object sender, EventArgs e)
        {
            AdminPlaceHolder1.Visible = false;
            SearchBar searchBar = (SearchBar)Page.LoadControl("./Components/SearchBar.ascx");
            SearchBarPanel.Controls.Add(searchBar);
            AccountDTO account = (AccountDTO)Session["USER"];
            if (account != null)
            {
                Fullname = account.Fullname;
                Welcome.Text = Fullname;
                bool hasPicture = account.Picture != null;
                if (hasPicture)
                {
                    Picture = account.Picture;
                    PictureBox.ImageUrl = Picture;
                    PictureBox.Visible = true;
                    BtnUser.Visible = false;
                }
                else
                {
                    Shortname = account.Fullname.Substring(0, 1).ToUpper();
                    BtnUser.Text = Shortname;
                    BtnUser.Visible = true;
                    PictureBox.Visible = false;
                }

                if (account.Role.Equals("ADMIN"))
                {
                    AdminPlaceHolder1.Visible = true;
                    BtnViewCartt.Visible = false;
                }
                else
                {
                    AdminPlaceHolder1.Visible = false;
                }
            }
            if(Session["ShoppingCart"] == null)
            {
                PanelQuantity.Visible = false;
            }

            bool isLoggedIn = account != null;
            ContainerAction.Visible = !isLoggedIn;
            ContainerUserInfo.Visible = isLoggedIn;
        }
        private bool CheckLogin(string username, string password)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSharpAssignmentConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand com = new SqlCommand("Select [ID],[Fullname],[StatusID],[Email], [Role] From Account Where Username = @username and Password = @password", conn);
            com.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
            com.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                int id = dr.GetInt32(0);
                string fullname = dr.GetString(1);
                int statusId = dr.GetInt32(2);
                string email = dr.GetString(3);
                AccountDTO userInfo = new AccountDTO(id, username, fullname, statusId, email);
                userInfo.Role = dr.GetString(4);

                Session["USER"] = userInfo;
                Session["AccountID"] = userInfo.AccountID;
                LabelLoginFailed.Visible = false;
                return true;
            }
            LabelLoginFailed.Visible = true;
            return false;
        }

        private int InsertAccount(string username, string password, string fullname, string email)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSharpAssignmentConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand com = new SqlCommand("INSERT INTO [dbo].[Account] ([Username],[Password],[Fullname],[StatusID],[Email],[Role]) OUTPUT Inserted.ID VALUES(@username, @password, @fullname, @statusId, @email, @role)", conn);
            com.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
            com.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
            com.Parameters.Add("@fullname", SqlDbType.VarChar).Value = fullname;
            com.Parameters.Add("@statusId", SqlDbType.Int).Value = 1;
            com.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
            com.Parameters.Add("@role", SqlDbType.VarChar).Value = "USER";

            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                int id = dr.GetInt32(0);
                Console.WriteLine(id);
                return id;
            }
            return -1;
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = TxtUsernameLogin.Text;
            string password = TxtPasswordLogin.Text;
            bool check = CheckLogin(username, password);
            if (check)
            {
                Response.Redirect("HomePage.aspx");
            }
        }

        private void ResetError()
        {
            LblRequiredUsername.Visible = false;

            LblNotValidUsername.Visible = false;

            LblNotValidFullname.Visible = false;

            LblNotValidFullname.Visible = false;


            LblRequiredEmail.Visible = false;

            LblNotValidEmail.Visible = false;

            LblRequiredPassword.Visible = false;


            LblNotValidPassword.Visible = false;

            LblMatchingPassword.Visible = false;
            LblAccept.Visible = false;

        }

        protected void BtnSignUp_Click(object sender, EventArgs e)
        {
            ResetError();
            string username = TxtUsername.Text;
            bool isCorrectInput = true;
            if (username.Length == 0)
            {
                LblRequiredUsername.Visible = true;
                isCorrectInput = false;

            }
            else if (username.Length < 5)
            {
                LblNotValidUsername.Visible = true;
                isCorrectInput = false;

            }
            string fullname = TxtFullname.Text;
            if (fullname.Length == 0)
            {
                LblRequiredFullName.Visible = true;
                isCorrectInput = false;

            }
            else if (fullname.Length < 5)
            {
                LblNotValidFullname.Visible = true;
                isCorrectInput = false;

            }
            string email = TxtEmail.Text;
            if (email.Length == 0)
            {
                LblRequiredEmail.Visible = true;
                isCorrectInput = false;

            }
            else if (!Regex.IsMatch(email, @"^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$"))
            {
                LblNotValidEmail.Visible = true;
                isCorrectInput = false;
            }
            string password = TxtPassword.Text;
            if (password.Length == 0)
            {
                LblRequiredPassword.Visible = true;
                isCorrectInput = false;

            }
            else if (password.Length < 8)
            {
                LblNotValidPassword.Visible = true;
                isCorrectInput = false;

            }
            string confirm = TxtConfirm.Text;
            if (!confirm.Equals(password))
            {
                LblMatchingPassword.Visible = true;
                isCorrectInput = false;
            }
            bool isValid = ChbAccept.Checked;

            if (!isValid)
            {
                LblAccept.Visible = true;
                return;
            }

            if (!isCorrectInput)
            {
                return;
            }

            int idInsert = InsertAccount(username, password, fullname, email);
            if (idInsert != -1)
            {
                LogMeInPanel.Visible = true;
                RegisterPanel.Visible = false;
                AccountDTO account = new AccountDTO(idInsert, username, fullname, 1, email);
                Session["RecentlyRegistered"] = account;
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string name = TxtName.Text;
            string category = CbCategory.Text;
            string fromPrice = TxtFromPrice.Text;
            string toPrice = TxtToPrice.Text;
            string urlRewriting = "ShowProduct.aspx"
                + "?name=" + name
                + "&category=" + category
                + "&fromPrice=" + fromPrice
                + "&toPrice=" + toPrice;

            Response.Redirect(urlRewriting);
        }

        protected void TxtSearchName_TextChanged(object sender, EventArgs e)
        {
            string name = TxtSearchName.Text;
            Response.Redirect("ShowProduct.aspx?name=" + name);
        }

        protected void BtnLogMeIn_Click(object sender, EventArgs e)
        {
            Session["USER"] = Session["RecentlyRegistered"];
            Session["AccountID"] = ((AccountDTO)Session["USER"]).AccountID;
            Session["RecentlyRegistered"] = null;
            Response.Redirect("HomePage.aspx");

        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("USER");
            Session.Remove("AccountID");
            Response.Redirect("HomePage.aspx");
        }

        protected void BtnViewCart_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewCart.aspx");
        }
    }
}