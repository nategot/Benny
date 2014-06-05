using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Data;


public partial class MessagePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        NameValueCollection coll = Request.QueryString;
        String ans = coll["ans"];

        switch (ans)
        { case "notLogin" :
                massageLBL.Text = "You have to log in in order to join event";
                RegisterBTN.Visible = true;
                BackeBtn.Visible = true;
              break;
            case "MyEvents":
                  massageLBL.Text = "";
                  NoRecords.Visible = true;
                  MyEventsTimer.Enabled = true;
               
              break;

            case "NewEvent":
                  massageLBL.Text = "";
                  NoRecords.Visible = true;
                  NewEventTimer.Enabled = true;
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
    protected void MyEventsTimer_Tick(object sender, EventArgs e)
    {
        MyEventsTimer.Enabled = false;
        Response.Redirect("MyEvents.aspx");
    }

    protected void NewEventTimer_Tick(object sender, EventArgs e)
    {
        NewEventTimer.Enabled = false;
        Response.Redirect("NewEvent.aspx");
    }
    
}