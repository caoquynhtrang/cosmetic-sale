using CSharpAssignment.DAO;
using CSharpAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSharpAssignment
{
    public partial class ManageProducts : System.Web.UI.Page
    {
        public const int MAXIMUM_FILE_SIZE = 1000000;
        protected void Page_Load(object sender, EventArgs e)
        {
            BtnInsert.Visible = false;
            BtnUpdate.Visible = false;

        }

        protected void GvProducts_SelectedIndexChanged(object sender, EventArgs e)
        {

            int index = GvProducts.SelectedIndex;
            if (index != -1)
            {
                BtnUpdate.Visible = true;
                txtId.Text = GvProducts.Rows[index].Cells[1].Text;
                ProductDTO pro = new ProductDAO().GetProductByID(int.Parse(txtId.Text));
                txtName.Text = pro.Name;
                txtQuantity.Text = pro.Quantity.ToString();
                txtPrice.Text = pro.Price.ToString();
                txtUsage.Text = pro.Price.ToString();
                ddlCategory.SelectedValue = pro.CateID.ToString();
                ddlStatus.SelectedValue = pro.StatusID.ToString();
                string imageLink = pro.ImageLink;
                txtImageLink.Text = imageLink;
                imgPreview.ImageUrl = txtImageLink.Text;
                TxtOk.Text = "OK";

            }
            BtnInsert.Visible = false;
            BtnUpdate.Enabled = true;
            BtnDelete.Enabled = true;
        }

        private void ClearFields()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtQuantity.Text = "";
            txtPrice.Text = "";
            txtUsage.Text = "";
            ddlCategory.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            txtImageLink.Text = "";
            imgPreview.ImageUrl = "";
        }

        protected void BtnNew_Click(object sender, EventArgs e)
        {
            ClearFields();
            BtnInsert.Visible = true;

            lblMessage.Text = "";
        }

        private void ClearErrorMessages()
        {
            lblName.Text = "";
            lblQuantity.Text = "";
            lblPrice.Text = "";
            lblUsage.Text = "";
        }

        private bool ValidateForm()
        {
            bool result = true;
            ClearErrorMessages();

            //Name;
            string name = txtName.Text.Trim();
            if (name.Length == 0)
            {
                lblName.Text = "Product name is required!";
                result = false;
            }

            //Quantity;
            string quantityStr = txtQuantity.Text.Trim();
            int quantity;
            if (quantityStr.Length == 0)
            {
                lblQuantity.Text = "Quantity is required!";
                result = false;
            }
            else
            {
                try
                {
                    quantity = Convert.ToInt32(quantityStr);
                    if (quantity <= 0)
                    {
                        lblQuantity.Text = "Quantity must be positive!";
                        result = false;
                    }
                }
                catch (FormatException)
                {
                    lblQuantity.Text = "Quantity must be a valid integer!";
                    result = false;
                }
            }

            //Price;
            string priceStr = txtPrice.Text.Trim();
            double price;
            if (priceStr.Length == 0)
            {
                lblPrice.Text = "Price is required!";
                result = false;
            }
            else
            {
                try
                {
                    price = Convert.ToDouble(priceStr);
                    if (price <= 0)
                    {
                        lblPrice.Text = "Price must be positive!";
                        result = false;
                    }
                }
                catch (FormatException)
                {
                    lblPrice.Text = "Price must be a valid number!";
                    result = false;
                }
            }

            //Usage;
            string usage = txtUsage.Text.Trim();
            if (usage.Length == 0)
            {
                lblUsage.Text = "Usage is required!";
                result = false;
            }

            return result;
        }

        protected void BtnInsert_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }
            BtnInsert.Visible = true;
            BtnUpdate.Visible = false;

            string name = txtName.Text.Trim();
            int quantity = Convert.ToInt32(txtQuantity.Text.Trim());
            double price = Convert.ToDouble(txtPrice.Text.Trim());
            string usage = txtUsage.Text.Trim();
            int cateID = Convert.ToInt32(ddlCategory.SelectedValue);
            int statusID = Convert.ToInt32(ddlStatus.SelectedValue);
            string imageLink = txtImageLink.Text.Trim();

            ProductDTO dto = new ProductDTO(name, quantity, price, usage, cateID, statusID, imageLink);
            ProductDAO dao = new ProductDAO();
            bool result = dao.InsertProduct(dto);
            if (result)
            {
                lblMessage.Text = "Insert is successful";
                ClearFields();
                GvProducts.DataBind();
            }
            else
            {
                lblMessage.Text = "Insert is failed";
            }
            TxtOk.Text = "OK";
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }
            BtnInsert.Visible = false;
            BtnUpdate.Visible = true;

            int id = Convert.ToInt32(txtId.Text.Trim());
            string name = txtName.Text.Trim();
            int quantity = Convert.ToInt32(txtQuantity.Text.Trim());
            double price = Convert.ToDouble(txtPrice.Text.Trim());
            string usage = txtUsage.Text.Trim();
            int cateID = Convert.ToInt32(ddlCategory.SelectedValue);
            int statusID = Convert.ToInt32(ddlStatus.SelectedValue);
            string imageLink = txtImageLink.Text.Trim();

            ProductDTO dto = new ProductDTO(id, name, quantity, price, usage, cateID, statusID, imageLink);
            ProductDAO dao = new ProductDAO();
            bool result = dao.UpdateProduct(dto);
            if (result)
            {
                lblMessage.Text = "Update is successful";
                GvProducts.DataBind();
            }
            else
            {
                lblMessage.Text = "Update is failed";
            }
            TxtOk.Text = "OK";
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text.Trim());
            ProductDAO dao = new ProductDAO();
            bool result = dao.DeleteProduct(id);
            if (result)
            {
                lblMessage.Text = "Delete is successful";
                ClearFields();
                GvProducts.DataBind();
                BtnInsert.Enabled = false;
                BtnUpdate.Enabled = false;
                BtnDelete.Enabled = false;
            }
            else
            {
                lblMessage.Text = "Delete is failed";
            }
            TxtOk.Text = "OK";
        }

        protected void BtnPreview_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            HttpPostedFile file = fuImage.PostedFile;
            if (fuImage.HasFile)
            {
                if (file.ContentLength > MAXIMUM_FILE_SIZE)
                {
                    lblMessage.Text = "File cannot be greater than 1 MB";
                    return;
                }

                string imagePath = Server.MapPath("~/images/" + fuImage.FileName);
                fuImage.SaveAs(imagePath);
                imgPreview.ImageUrl = "~/images/" + fuImage.FileName;
                txtImageLink.Text = "~/images/" + fuImage.FileName;
            }
            TxtOk.Text = "OK";
        }

        protected void BtnNewProduct_Click(object sender, EventArgs e)
        {
            ClearFields();
            BtnInsert.Visible = true;
            TxtOk.Text = "OK";
        }
    }
}