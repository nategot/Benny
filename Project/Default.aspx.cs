using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       

    }

    protected void CategoryFilter(object sender, EventArgs e)
    {
        ImageButton ansBTN = (ImageButton)sender;
        string ans = ansBTN.ID;
        Response.Redirect("Home.aspx?ans=" + ans);
    }

  

   
}