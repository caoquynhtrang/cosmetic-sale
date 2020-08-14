using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSharpAssignment
{
    public partial class Coupon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LbInSert_Click(object sender, EventArgs e)
        {
            SqlDataSource1.InsertParameters["Code"].DefaultValue = ((TextBox)GridView1.FooterRow.FindControl("txtCode")).Text;
            SqlDataSource1.InsertParameters["Value"].DefaultValue = ((TextBox)GridView1.FooterRow.FindControl("txtValue")).Text;
            SqlDataSource1.InsertParameters["ImportDate"].DefaultValue = DateTime.Now.ToString();
            SqlDataSource1.InsertParameters["ExpiredDate"].DefaultValue = ((TextBox)GridView1.FooterRow.FindControl("txtExpiredDate")).Text;
            SqlDataSource1.InsertParameters["StatusID"].DefaultValue = ((TextBox)GridView1.FooterRow.FindControl("txtStatusID")).Text;

            SqlDataSource1.Insert();
        }
    }
}