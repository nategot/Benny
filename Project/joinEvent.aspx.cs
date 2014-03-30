using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Text;
public partial class joinEvent : System.Web.UI.Page
{
    string eventNum;
    protected void Page_Load(object sender, EventArgs e)
    {   //load the event detail 
        if (Session["EventNumber"] == null) return;
        eventNum = (Session["EventNUmber"]).ToString();

        if (Session["gridTable"] == null) return;
        DataTable dt = (DataTable)HttpContext.Current.Session["gridTable"];


        // load the event detail to show
        #region   
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["EventNumber"].ToString() == eventNum)
            {
                latHF.Value = dt.Rows[i]["Lat"].ToString();
                lngHF.Value = dt.Rows[i]["Lng"].ToString();
                ANS_MaxPlayerLbl.Text = dt.Rows[i]["NumOfParticipants"].ToString();
                iconImg.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
                ANS_datatimelbl.Text = dt.Rows[i]["Time"].ToString();
                ANS_locationLbl.Text = dt.Rows[i]["Address"].ToString();
                ANS_commentLbl.Text = dt.Rows[i]["Comments"].ToString();
                ANS_Frequency.Text = dt.Rows[i]["Frequncy"].ToString();
                ANS_AgeLbl.Text = dt.Rows[i]["MinAge"].ToString() + "-" + dt.Rows[i]["MaxAge"].ToString();
                EventNameLbl.Text = dt.Rows[i]["Description"].ToString();
                User u = new User();
                u.UserId = int.Parse(dt.Rows[i]["AdminId"].ToString());
                DataTable dtName = u.CheckUserName();
                ANS_AdminLbl.Text = dtName.Rows[0]["Fname"].ToString() + " " + dtName.Rows[0]["Lname"].ToString();
                bool ansTemp = (bool)dt.Rows[0]["Private"];
                string temp = "Public";
                if (ansTemp)
                    temp = "Private";
                ANS_EventTypelbl.Text = temp;
            }
        }
        #endregion 

        //loag the users that register to this event
        #region
      
        EventOnAir EV = new EventOnAir();
        DataTable dtUser = EV.ReadUserInEvent(eventNum);

        //adding the num coulm
        DataColumn dc = new DataColumn("num");
        dc.DataType = typeof(int);
        dtUser.Columns.Add(dc);
        dc.SetOrdinal(0);

        for (int i = 0; i < int.Parse(ANS_MaxPlayerLbl.Text); i++)
         {
             DataRow NewRow = dtUser.NewRow();
             dtUser.Rows.Add(NewRow); 
        }

        playerTableGrv.DataSource = dtUser;
        playerTableGrv.DataBind();
        playerTableGrv.HeaderRow.Cells[0].Text = "";
       
        //add the num of row like the num of players
        for (int i = 0; i < playerTableGrv.Rows.Count; i++)
        {
            playerTableGrv.Rows[i].Cells[0].Text = (i + 1).ToString();
        }
  
    }
    #endregion


     // adding the user to the event
    protected void joinBTN_Click(object sender, EventArgs e)
    { 
        if (Session["UserDeatail"] == null) return;
        DataTable dt = (DataTable)HttpContext.Current.Session["UserDeatail"];
        User Newuser = new User();
        User U1 = new User();
        U1.Email = dt.Rows[0]["Email"].ToString();
        int num = U1.InsertToEvent(eventNum);
        Response.Redirect("MyEvents.aspx");
    }


}