using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Collections.Specialized;

public partial class NewEvent : System.Web.UI.Page
{
    DataTable dt;
    string eventnum;
    protected void Page_Load(object sender, EventArgs e)
    {

        // if the user is not login go to login
        if (Session["Fname"] == null)
        {
            Response.Redirect("MessagePage.aspx?ans=notLogin");
        }

        if (!Page.IsPostBack)
        {
            loadUserDetail();
        }

        NameValueCollection QueryString = Request.QueryString;
        String edit = QueryString["edit"];
        if (edit == "Yes")
        {
            confirmBTN.Text = "Edit Event";
            EditEvent();
        }

    }

    //insert new event 
    protected void confirmBTN_Click(object sender, EventArgs e)
    {

        string[] latlagArr = new string[2];
        string timedate;

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

        try
        {
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


        }
        catch (Exception ex)
        {

            ShowPopup(ex.Message);
        }
        //if admin wants to up date
        if (confirmBTN.Text == "Edit Event")
        {
            updateEvent(ev);
        }
        else
        {
            insertEvent(ev);
        }


    }



    //insert event
    protected void insertEvent(EventOnAir ev)
    {
        string message;
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
            Response.Redirect("MyEvents.aspx");
        }

    }

    //updateEvent
    protected void updateEvent(EventOnAir ev)
    {
        string message;
        ev.EventNum = eventnum;
        int numEfect = ev.update();
        if (numEfect == 0)
        {
            message = "The Event wasnt Update!";
            ShowPopup(message);
        }
        else
        {
            message = "The Event was Update Successfully!";

            ShowPopup(message);
            Response.Redirect("MyEvents.aspx");
        }

    }

    //load the age range according to the user age
    protected void loadUserDetail()
    {
        dt = (DataTable)HttpContext.Current.Session["UserDeatail"];
        MaxAgeTxt.Text = (int.Parse(dt.Rows[0]["Age"].ToString()) + 5).ToString();
        MinAgeTxt.Text = (int.Parse(dt.Rows[0]["Age"].ToString()) - 5).ToString();

        dateTB.Text = (DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year).ToString();
    }

    //edit event-load the event detail
    protected void EditEvent()
    {
        if (Session["MyEventsDT"] == null) return;
        DataTable dtEvent = (DataTable)HttpContext.Current.Session["MyEventsDT"];

        if (Session["Eventnum"] == null) return;
         eventnum = (string)HttpContext.Current.Session["Eventnum"];

        for (int i = 0; i < dtEvent.Rows.Count; i++)
        {
            if (dtEvent.Rows[i]["EventNumber"].ToString() == eventnum)
            { 
                NOP.Text = dtEvent.Rows[i]["NumOfParticipants"].ToString();
                MaxAgeTxt.Text = dtEvent.Rows[i]["MaxAge"].ToString();
                MinAgeTxt.Text = dtEvent.Rows[i]["MinAge"].ToString();
                FrequRBL.SelectedValue = checkFrec(dtEvent.Rows[i]["Frequency"].ToString());
                EventTypeRBL.SelectedValue = dtEvent.Rows[i]["Private"].ToString();
                categoryDDL.SelectedValue = CheckCategoryNum(dtEvent.Rows[i]["Description"].ToString());
                commentsTB.Text = dtEvent.Rows[i]["Comments"].ToString();
                string dataStr = dtEvent.Rows[i]["Time"].ToString();
                string[] dataStrArr = new string[2];
                dataStrArr = dataStr.Split(' ');
                dateTB.Text = dataStrArr[0];
                timeTB.Text = dataStrArr[1].Remove(dataStrArr[1].Length - 3, 3);
            }
        }
    }


    //pop up
    protected void ShowPopup(string message)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
    }

    //check the category num 
    protected string CheckCategoryNum(string catName)
    {
        int ans = 0;
        switch (catName)
        {
            case "Soccer":
                ans = 1;
                break;
            case "Basketball":
                ans = 2;
                break;
            case "Tennis":
                ans = 3;
                break;
            case "Running":
                ans = 4;
                break;
            case "Cycling":
                ans = 5;
                break;
            case "Swimming":
                ans = 6;
                break;
            case "Volleyball":
                ans = 7;
                break;
            case "Surfing":
                ans = 8;
                break;
        }
        return ans.ToString(); ;
    }

    //check the frecuncey num 
    protected string checkFrec(string freqtName)
    {
        string ans = "1";
        switch (freqtName)
        {
            case "Once":
                ans = "1";
                break;
            case "Every Week":
                ans = "2";
                break;
            case "Every Month":
                ans = "3";
                break;
        }
        return ans;
    }


}