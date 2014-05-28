using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

public partial class NewEvent : System.Web.UI.Page
{
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        // if the user is not login go to login
        if (Session["Fname"] == null)
        {
            Response.Redirect("MessagePage.aspx?ans=notLogin");
        }

        if (!Page.IsPostBack)
        {
            //load the age range according to the user age
            dt = (DataTable)HttpContext.Current.Session["UserDeatail"];
            MaxAgeTxt.Text = (int.Parse(dt.Rows[0]["Age"].ToString()) + 5).ToString();
            MinAgeTxt.Text = (int.Parse(dt.Rows[0]["Age"].ToString()) - 5).ToString();

            dateTB.Text = (DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year).ToString();
        }
     

    }

    //insert new event 
    protected void confirmBTN_Click(object sender, EventArgs e)
    {

        string[] latlagArr = new string[2];
        string timedate;
        string message;
        EventOnAir ev = new EventOnAir();

        if (LatLOngHIde.Value != "")
        {
            //take lat lng
            string latlong = LatLOngHIde.Value;
            latlagArr = latlong.Split(',');
            latlagArr[0] = latlagArr[0].Remove(0, 1);
            latlagArr[1] = latlagArr[1].Remove(latlagArr[1].Length - 1, 1);
            string address = CityHIde.Value;

            //insert to the Event class
            ev.Point = new Point(double.Parse(latlagArr[0]), double.Parse(latlagArr[1]));
            ev.Address = address;
        }

        DataTable dt = (DataTable)HttpContext.Current.Session["UserDeatail"];
        ev.AdminID = int.Parse(dt.Rows[0]["UserId"].ToString());

        ev.Catedory = int.Parse(categoryDDL.SelectedValue);
        ev.NumOfParti = int.Parse(NOP.Text);
        timedate = dateTB.Text + " " + timeTB.Text;
        ev.DateTime = DateTime.Parse(timedate);
        ev.MinAge = double.Parse(MinAgeTxt.Text);
        ev.MaxAge = double.Parse(MaxAgeTxt.Text);
        ev.Frequency = int.Parse(FrequRBL.SelectedValue);
        ev.IsPrivate1 = bool.Parse(EventTypeRBL.SelectedValue);
        ev.Comments = commentsTB.Text;


        int numEfect = ev.insert();
        if (numEfect == 0)
        {
            message = "The Event wasnt added!";
            ShowPopup(message);
        }
        else
        {
            message = "The Event was added Successfully!";
            
            ShowPopup(message);
            //Response.Redirect("Home.aspx");//לשנות לMYEVNTS
        }

    }

    protected void ShowPopup(string message)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
    }

}