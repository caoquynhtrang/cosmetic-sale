using CSharpAssignment.Models;
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
    public partial class ShowProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowDataList();
        }

        private void ShowDataList()
        {
            string name = Request.QueryString["name"];
            string category = Request.QueryString["category"];
            string fromPrice = Request.QueryString["fromPrice"];
            string toPrice = Request.QueryString["toPrice"];

            PanelSearch.Visible = true;
            if (category != null && name == null && fromPrice == null && toPrice == null)
            {
                PanelInfo.Visible = true;
                PanelSearch.Visible = false;
                CategoryName.Text = category;
                CategoryDetail.Text = GetCategoryDisription(category);
            }

            decimal priceTo = -1;
            try
            {
                priceTo = decimal.Parse(toPrice);
            }
            catch (Exception)
            {
            }
            decimal priceFrom = -1;
            try
            {
                priceFrom = decimal.Parse(fromPrice);
            }
            catch (Exception)
            {
            }
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSharpAssignmentConnectionString"].ConnectionString);
            string sql = "SELECT [ID] ,[Quantity] ,[Price] ,[Usage] ,[Name] ,[StatusID] ,[CateID] ,[ImageLink] FROM [Product] Where StatusID = 1 ";
            if (name != null && name.Length > 0)
            {
                sql += "and Name like @name ";
            }
            if (priceTo > 0 && priceTo >= priceFrom)
            {
                sql += "and Price >= @lowerPrice and Price <= @upperPrice ";
            }
            if (category != null && category.Length > 0)
            {
                sql += "and (Select Name FROM Category WHERE ID = CateID) = @cateName ";
            }


            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            if (name != null && name.Length > 0)
            {
                da.SelectCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = "%" + name + "%";
            }
            if (priceFrom > 0 && priceTo >= priceFrom)
            {
                da.SelectCommand.Parameters.Add("@upperPrice", SqlDbType.Decimal).Value = priceTo;
                da.SelectCommand.Parameters.Add("@lowerPrice", SqlDbType.Decimal).Value = priceFrom;
            }
            if (category != null && category.Length > 0)
            {
                da.SelectCommand.Parameters.Add("@cateName", SqlDbType.NVarChar).Value = category;
            }

            DataSet ds = new DataSet();
            da.Fill(ds, "tblProduct");
            DLProduct.DataSourceID = null;
            DLProduct.DataSource = ds;
            DLProduct.DataMember = "tblProduct";
            DLProduct.DataBind();
        }
        public string GetCategoryDisription(string categoryName)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=THE-HIEN;Initial Catalog=CSharpAssignment; Integrated Security=SSPI");
            string des = null;
            try
            {
                DateTime today = DateTime.Today;
                conn.Open();
                SqlCommand comm = new SqlCommand("Select Description from Category where Name = @name", conn);
                comm.Parameters.Add("@name", SqlDbType.NVarChar);

                comm.Parameters["@name"].Value = categoryName;
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    des = reader.GetString(0);
                }


            }
            catch (Exception e)
            {

            }
            finally
            {

                conn.Close();
            }
            return des;

        }

        void AddToShoppingCart(string itemID, string itemName, string itemPrice)
        {
            DataTable dataTable;
            // If this object does not already exist, initialize the DataTable object and 
            // then add the information of the selected product to the DataTable object
            if (Session["ShoppingCart"] == null)
            {
                dataTable = new DataTable();
                dataTable.Columns.Add("ID");
                dataTable.Columns.Add("Name");
                dataTable.Columns.Add("Quantity");
                dataTable.Columns.Add("UnitPrice");
                dataTable.Columns.Add("Price");
                
                
            }
            // If the Shopping Cart object already exists, you declare to convert the data 
            // type of this object into the DataTable object
            else
            {
                dataTable = (DataTable)Session["ShoppingCart"];
            }
            // Check whether the product already exists in ShoppingCart
            Common cls = new Common();
            int indexOfItem = cls.IsExistItemInShoppingCart(itemID, dataTable);
            // If there are already, update add 1 
            if (indexOfItem != -1)
            {
                dataTable.Rows[indexOfItem]["Quantity"] = Convert.ToInt32(dataTable.Rows[indexOfItem]["Quantity"]) + 1;
                dataTable.Rows[indexOfItem]["Price"] = Convert.ToDouble(dataTable.Rows[indexOfItem]["Price"]) + Convert.ToDouble(dataTable.Rows[indexOfItem]["Price"]);
            }
            // If not, add a new product to Shopping Cart
            else
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = itemID;
                dataRow["Name"] = itemName;
                dataRow["Quantity"] = "1";
                dataRow["Price"] = itemPrice;
                dataRow["UnitPrice"] = itemPrice;
                
                dataTable.Rows.Add(dataRow);
            }
            // Assign the DataTable object to the Session object

            Session["ShoppingCart"] = dataTable;

        }
        protected void DLProduct_ItemCommand(object source, DataListCommandEventArgs e)
        {
            

        }

        protected void ButtonAddToCart_Click(object sender, EventArgs e)
        {
            Button btnAddToCart = (Button)sender;
            DataListItem dataListItem = (DataListItem)btnAddToCart.Parent;
            HiddenField hidden = (HiddenField)dataListItem.FindControl("IDLabel");
            Label NameLabel1 = (Label)dataListItem.FindControl("NameLabel1");

            Label PriceLabel1 = (Label)dataListItem.FindControl("PriceLabel1");
            //Literal1.Text = "You just added \"" + labelName.Text + "\" to Shopping cart";
            // Call the method to add to the shopping cart
            AddToShoppingCart(hidden.Value, NameLabel1.Text, PriceLabel1.Text);
        }
    }
}