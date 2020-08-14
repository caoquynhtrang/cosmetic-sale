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
    public partial class ViewHistory : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter dta;
        DataTable dtb;
        string ConString, CmdString, BookingID;

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BookingID = GridView1.SelectedValue.ToString();
            BindDetailsView(BookingID);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ConString = ConfigurationManager.ConnectionStrings["CSharpAssignmentConnectionString"].ConnectionString;
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridView1.PageIndex = e.NewSelectedIndex;
            BookingID = GridView1.SelectedValue?.ToString();

            if (BookingID != null)
            {
                BindDetailsView(BookingID);
            }
        }

        protected void DetailsView1_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {
            DetailsView1.PageIndex = e.NewPageIndex;
            BookingID = GridView1.SelectedValue.ToString();
            BindDetailsView(BookingID);
        }

        private void BindGridView()
        {
            conn = new SqlConnection(ConString);
            CmdString = "SELECT ID, BookingCode FROM Booking";
            cmd = new SqlCommand(CmdString, conn);
            dta = new SqlDataAdapter(cmd);
            dtb = new DataTable();
            dta.Fill(dtb);
        }

        private void BindDetailsView(string BookingID)
        {
            CmdString = "Select Distinct P.Name, P.Price, BD.Amount, (BD.Amount * P.Price) as TotalPrice From[dbo].[Booking] B, [dbo].[Product] P, [dbo].[BookingDetail] BD Where BD.ProductID = P.ID and BD.BookingID = @BookingID";
            conn = new SqlConnection(ConString);
            cmd = new SqlCommand(CmdString, conn);
            cmd.Parameters.AddWithValue("@BookingID", BookingID);
            dta = new SqlDataAdapter(cmd);
            dtb = new DataTable();
            dta.Fill(dtb);
            DetailsView1.DataSource = dtb.DefaultView;
            DetailsView1.DataBind();
        }
    }
}