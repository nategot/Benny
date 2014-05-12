using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnShowPopup_Click(object sender, EventArgs e)
    {
        string message = "Message from server side";
        ClientScript.RegisterStartupScript(this.GetType(), "gjgh", "ShowPopup('" + message + "');", true);
    }
}