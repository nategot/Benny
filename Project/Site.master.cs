using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fname"] != null)
        {
            LogInLbl.Text = "Hello! " + (Session["Fname"]).ToString();
            DataTable dt = (DataTable)Session["UserDeatail"];
            userImage.ImageUrl = dt.Rows[0]["Picture"].ToString();
        }
    }



}
