using CSharpAssignment.DAO;
using CSharpAssignment.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSharpAssignment
{
    public partial class ViewCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                PanelErrMsgg.Visible = false;
                // If the cart does not exist yet
                if (Session["ShoppingCart"] == null)
                {
                    this.CartPanel.Visible = false;
                    this.EmptyPanel.Visible = true;
                    PanelMsgg.Visible = false;
                }
                // If the cart exists
                else
                {
                    PanelMsgg.Visible = false;
                    this.CartPanel.Visible = true;
                    this.EmptyPanel.Visible = false;
                    DataTable dataTable = (DataTable)Session["ShoppingCart"];
                    GridItemCart.DataSource = dataTable;
                    
                    GridItemCart.DataBind();

                }
                if (Session["AccountID"] != null)
                {
                    DiscountPanel.Visible = true;
                    PleaseLoginPanel.Visible = false;
                    SqlDataSource2.SelectCommand += "and (ISNULL((SELECT AccountID FROM Booking where DiscountID = D.ID AND StatusID <> 3), -1)) <> " + Session["AccountID"];
                    DataView view = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
                    DataTable dt = view.ToTable();
                    DataRowCollection rows = dt.Rows;
                    foreach (DataRow row in rows)
                    {
                        string code = row["Code"].ToString();
                        string value = row["Value"].ToString();
                        string id = row["ID"].ToString();
                        DropdownListDiscount.Items.Add(new ListItem(code + " - " + value + " (%)", code));
                    }
                }
                else
                {
                    DiscountPanel.Visible = false;
                    PleaseLoginPanel.Visible = true;
                }


                CalculatePrice();
            }
        }
        private double CalculatePrice()
        {
            double price = 0;
            for (int i = 0; i < GridItemCart.Rows.Count; i++)
            {
                GridViewRow selectedRow = GridItemCart.Rows[i];
                TableCell id = selectedRow.Cells[6];
                TableCell priceText = selectedRow.Cells[5];
                if(priceText.Text.Length > 0)
                {
                    price += Convert.ToDouble(id.Text) * Convert.ToDouble(priceText.Text);
                }
                
            }
            lblTotalAmount.Text = price.ToString();
            return price;
        }
        bool RemoveShoppingCart(string itemID)
        {
            if (Session["ShoppingCart"] == null)
            {
                Literal1.Text = "Sorry, Can not remove item in your shopping cart";
            }
            else
            {
                DataTable dataTable = (DataTable)Session["ShoppingCart"];
                Common common = new Common();
                int indexOfRow = common.IsExistItemInShoppingCart(itemID, dataTable);
                if (indexOfRow != -1)
                {
                    dataTable.Rows.RemoveAt(indexOfRow);
                    Session["ShoppingCart"] = dataTable;
                    GridItemCart.DataSource = dataTable;
                    GridItemCart.DataBind();
                    if(dataTable.Rows.Count == 0)
                    {
                        Session.Remove("ShoppingCart");
                    }
                    return true;
                }
            }
            return false;
        }

        void UpdateShoppingCart(string itemID, string itemName, String itemPrice, string unitPrice, String quantity, int index)
        {
            if (Session["ShoppingCart"] == null)
            {
                Literal1.Text = "Sorry, Can not Update item in your shopping cart";
            }
            else
            {
                DataTable dataTable = (DataTable)Session["ShoppingCart"];

                dataTable.Rows.RemoveAt(index);
                //xoa cai bang kia và add 1 round moi theo cai gia tri nhap
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = itemID;
                dataRow["Name"] = itemName;
                dataRow["Quantity"] = quantity;
                dataRow["Price"] = itemPrice;
                dataRow["UnitPrice"] = unitPrice;
                dataTable.Rows.Add(dataRow);
                // add vô session 
                Session["ShoppingCart"] = dataTable;
                GridItemCart.DataSource = dataTable;
                GridItemCart.DataBind();
            }

        }
        protected void GridItemCart_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int index = Convert.ToInt32(e.NewEditIndex);

            GridViewRow selectedRow = GridItemCart.Rows[index];
            TableCell id = selectedRow.Cells[3];
            TableCell name = selectedRow.Cells[4];
            TableCell quanti = selectedRow.Cells[5];
            TableCell price = selectedRow.Cells[6];
            //int LABAVID = Convert.ToInt32(id.Text);
            string ids = id.Text;
            string itemName = name.Text;
            string itemquantity = quanti.Text;
            string itemprice = price.Text;
            //Literal1.Text = "lấy dc ID: " + ids + itemName + itemquantity + itemprice;
            bool success = RemoveShoppingCart(ids);

            if (success)
            {
                lblTotalAmount.Text = CalculatePrice().ToString();
                if (GridItemCart.Rows.Count == 0)
                {
                    Session.Remove("ShoppingCart");
                }
                Literal1.Text = "Delete Susscess";
            }

        }

        protected void GridItemCart_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = e.RowIndex;

            GridViewRow selectedRow = GridItemCart.Rows[index];
            TableCell id = selectedRow.Cells[3];
            TableCell name = selectedRow.Cells[4];

            TextBox quanti = (TextBox)selectedRow.Cells[2].FindControl("txtQuanity");
            TableCell price = selectedRow.Cells[6];
            TableCell unitPriceEle = selectedRow.Cells[7];
            string ids = id.Text;
            string itemName = name.Text;
            string itemquantity = quanti.Text;
            double itemprice = Convert.ToDouble(unitPriceEle.Text) * Convert.ToInt32(itemquantity);
            double unitPrice = Convert.ToDouble(unitPriceEle.Text);
            UpdateShoppingCart(ids, itemName, itemprice.ToString(), unitPrice.ToString(), itemquantity, index);
            lblTotalAmount.Text = CalculatePrice().ToString();
        }
        protected void GridItemCart_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void BtnApply_Click(object sender, EventArgs e)
        {
            string code = DropdownListDiscount.Text;
            DiscountDAO dao = new DiscountDAO();
            decimal number = dao.CheckDiscountCode(code);
            if (number != -1)
            {
                bool checkAlreadyUsed = dao.CheckAlreadyUsedDiscount(code, 1);
                if (checkAlreadyUsed == false)
                {
                    PriceDiscount.Text = (CalculatePrice() * ((double)number / 100)).ToString();
                    lblTotalAmount.Text = (CalculatePrice() * ((double)(100 - number)) / 100).ToString();

                    litDiscount.Text = "";
                }
                else
                {
                    litDiscount.Text = "Your code is already used ";
                }
            }
            else
            {
                litDiscount.Text = "Your code is not valid";
            }
        }
        public bool CheckUserInfo()
        {

            string cusName = txtCusName.Text.Trim();
            if (cusName.Length <= 3 || cusName.Length > 50)
            {
                litCusName.Text = "Customer name must be from 3 to 50 characters";
                return false;
            }
            string phone = txtPhoneNumber.Text.Trim();
            if (!Regex.IsMatch(phone, @"\d{10,11}"))
            {
                litPhone.Text = "Phone must be from 10 to 11 number";
                return false;
            }
            string address = txtAddress.Text.Trim();
            if (address.Length <= 3 || address.Length > 50)
            {
                litAddress.Text = "Address name must be from 3 to 50 characters";
                return false;
            }
            string contactPerson = txtContactPerson.Text.Trim();
            if (contactPerson.Length <= 3 || contactPerson.Length > 50)
            {
                litContactPerson.Text = "Customer name must be from 3 to 50 characters";
                return false;
            }

            return true;
        }

        public void Clear()
        {
            txtCusName.Text = "";
            txtPhoneNumber.Text = "";
            txtAddress.Text = "";
            txtContactPerson.Text = "";
            txtDescription.Text = "";
        }

        protected void btnCheckOut_Click(object sender, EventArgs e)
        {

        }

        protected void btnCheckOut_Click1(object sender, EventArgs e)
        {
            Literal1.Text = "";
            LblErr.Text = "";
            if (CheckUserInfo())
            {
                bool checkBooking = false;
                string cusName = txtCusName.Text.Trim();
                string phoneNumber = txtPhoneNumber.Text.Trim();
                string address = txtAddress.Text.Trim();
                string contactPerson = txtContactPerson.Text.Trim();
                string description = txtDescription.Text.Trim();
                string payment = chkPayment.Text;
                CheckOutDAO dao = new CheckOutDAO();
                ProductDAO products = new ProductDAO();
                DateTime today = DateTime.Now;
                for (int i = 0; i < GridItemCart.Rows.Count; i++)
                {

                    TableCell productId = GridItemCart.Rows[i].Cells[3];
                    TableCell quanitity = GridItemCart.Rows[i].Cells[5];
                    int proId = Convert.ToInt32(productId.Text.ToString());
                    int quan = Convert.ToInt32(quanitity.Text.ToString());
                    int currentQuantity = products.GetQuantityOfProduct(proId);
                    if (quan <= currentQuantity)
                    {
                        checkBooking = true;
                    }
                    else
                    {
                        string productName = GridItemCart.Rows[i].Cells[4].Text;
                        LblErr.Text += Literal1.Text + "Product " + productName + " only have " + currentQuantity + " left. \n";
                    }
                }
                if (checkBooking)
                {
                    AccountDTO dto = (AccountDTO)Session["USER"];
                    int accountID = -1;
                    if (dto != null)
                    {
                        accountID = dto.AccountID;
                    }
                    var discountID = -1;
                    if (Session["DiscountID"] != null)
                    {
                        discountID = (int)Session["DiscountID"];
                    }
                    bool insertBooking = dao.InsertIntoBooking(today, "ANCHOIBOCODE", address, phoneNumber, payment, contactPerson, description, accountID, discountID, cusName);
                    int bookingId = dao.GetBookingId(today);
                    for (int i = 0; i < GridItemCart.Rows.Count; i++)
                    {
                        TableCell productId = GridItemCart.Rows[i].Cells[3];
                        TableCell quanitity = GridItemCart.Rows[i].Cells[5];
                        int proId = Convert.ToInt32(productId.Text.ToString());
                        int quan = Convert.ToInt32(quanitity.Text.ToString());
                        int currentQuantity = products.GetQuantityOfProduct(proId);

                        bool insertDetails = dao.InsertBookingDetails(bookingId, proId, quan);
                    }
                    if(LblErr.Text.Length == 0)
                    {
                        PanelErrMsgg.Visible = false;
                    }
                    if (insertBooking)
                    {
                        PanelMsgg.Visible = true;
                        Literal1.Text = "Booking successfully !!!";
                        Session.Remove("ShoppingCart");
                        GridItemCart.Visible = false;
                        EmptyPanel.Visible = true;
                        Clear();
                    }
                }


            }
        }
    }
}