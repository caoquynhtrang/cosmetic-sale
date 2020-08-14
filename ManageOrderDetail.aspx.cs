using CSharpAssignment.DAO;
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
    public partial class ManageOrderDetail : System.Web.UI.Page
    {
        public const int STATUS_NEW = 6;
        public const int STATUS_ACCEPTED = 3;
        public const int STATUS_CANCELLED = 4;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BtnAccept.Visible = false;
                BtnCancel.Visible = false;
                DropDownStatus.SelectedValue = "6";
                
            }
        }

        protected void GvOrderDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int index = GvOrder.SelectedIndex;
            if (index != -1)
            {
                string discountValueStr = GvOrder.Rows[index].Cells[5].Text;
                double discountValue = 0;
                try
                {
                    discountValue = Convert.ToDouble(discountValueStr);
                    lblDiscountCode.Text = GvOrder.Rows[index].Cells[4].Text;
                    lblDiscountValue.Text = GvOrder.Rows[index].Cells[5].Text;
                }
                catch (Exception)
                {
                    lblDiscountCode.Text = "N/A";
                    lblDiscountValue.Text = "N/A";
                }
                double totalPrice = 0;
                for (int i = 0; i < GvOrderDetail.Rows.Count; i++)
                {
                    totalPrice += Convert.ToDouble(GvOrderDetail.Rows[i].Cells[4].Text);
                }
                lblTotalPrice.Text = (totalPrice * (100 - discountValue) / 100).ToString();
                string status = GvOrder.Rows[index].Cells[8].Text;
                if (status == "New")
                {
                    BtnAccept.Visible = true;
                    BtnCancel.Visible = true;
                }
                else
                {
                    BtnAccept.Visible = false;
                    BtnCancel.Visible = false;
                }
            }
        }
        protected void BtnSelectOrder_Click(object sender, EventArgs e)
        {
            DetailsPanel.Visible = true;
        }
        protected void BtnAccept_Click(object sender, EventArgs e)
        {
            int index = GvOrder.SelectedIndex;
            int id = Convert.ToInt32(GvOrder.Rows[index].Cells[1].Text);

            string connectionString = ConfigurationManager.ConnectionStrings["CSharpAssignmentConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            GridViewRowCollection rows = GvOrderDetail.Rows;

            for (int i = 0; i < GvOrderDetail.Rows.Count; i++)
            {
                ProductDAO products = new ProductDAO();
                TableCell productId = GvOrderDetail.Rows[i].Cells[0];
                TableCell quanitity = GvOrderDetail.Rows[i].Cells[4];
                int proId = Convert.ToInt32(productId.Text.ToString());
                int quan = Convert.ToInt32(quanitity.Text.ToString());
                int currentQuantity = products.GetQuantityOfProduct(proId);
                bool updateQuantity = products.UpdateQuantityOfProduct(proId, currentQuantity - quan);
            }
            try
            {
                conn.Open();
                string sql = "Update Booking SET StatusID = @statusID WHERE ID = @id";
                SqlCommand com = new SqlCommand(sql, conn);
                com.Parameters.AddWithValue("@statusID", STATUS_ACCEPTED);
                com.Parameters.AddWithValue("@id", id);
                if (com.ExecuteNonQuery() > 0)
                {
                    GvOrder.DataBind();
                    BtnAccept.Visible = false;
                    BtnCancel.Visible = false;
                }
            }
            finally
            {
                conn.Close();
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            int index = GvOrder.SelectedIndex;
            int id = Convert.ToInt32(GvOrder.Rows[index].Cells[1].Text);

            string connectionString = ConfigurationManager.ConnectionStrings["CSharpAssignmentConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                string sql = "Update Booking SET StatusID = @statusID WHERE ID = @id";
                SqlCommand com = new SqlCommand(sql, conn);
                com.Parameters.AddWithValue("@statusID", STATUS_CANCELLED);
                com.Parameters.AddWithValue("@id", id);
                if (com.ExecuteNonQuery() > 0)
                {
                    GvOrder.DataBind();
                    BtnAccept.Visible = false;
                    BtnCancel.Visible = false;
                }
            }
            finally
            {
                conn.Close();
            }
        }
    }
}