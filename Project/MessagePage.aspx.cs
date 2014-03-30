using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

public partial class MessagePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        NameValueCollection coll = Request.QueryString;
        String ans = coll["ans"];

        switch (ans)
        { case "notLogin" :
                massageLBL.Text = "Only registered users can join event";
                RegisterBTN.Visible = true;
                BackeBtn.Visible = true;
              break;
        }          
            
    }
    protected void RegisterBTN_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");

    }
    protected void BackeBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");

    }
}