using CSharpAssignment.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSharpAssignment
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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