using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Collections.Specialized;



public partial class Home : System.Web.UI.Page
{
    DataTable dt;
    EventOnAir Ev = new EventOnAir();
    string Eventnum;
    


    protected void Page_Load(object sender, EventArgs e)
    {
        LoadTable();
        EditGridView();
        if (!(Page.IsPostBack))
        {
            CategoryFilter();
        }
        //Join(null, null);

    }

     
    #region


    protected void LoadTable()
    {
        MapPlaceHolder.Visible = false;
        dt = Ev.readTable();
        GridView1.DataSource = dt;
        GridView1.DataBind();

        //load the user age
        if (Session["UserDeatail"] != null)
        {
            DataTable dtUser = (DataTable)HttpContext.Current.Session["UserDeatail"];
            ageTXT.Text = dtUser.Rows[0]["Age"].ToString();
        }
        else
            ageTXT.Text = "0";
    }



    //edit the gridview coulom
    protected void EditGridView()
    {
        string ageRange;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //edit the age range
            ageRange = dt.Rows[i]["MinAge"].ToString();
            ageRange += "-" + dt.Rows[i]["MaxAge"].ToString();
            GridView1.Rows[i].Cells[6].Text = ageRange;

            //hide if private
            CheckBox cb = (CheckBox)GridView1.Rows[i].Cells[10].Controls[0];
            if (cb.Checked)
            { GridView1.Rows[i].Visible = false; }

            //hide EventNumber & Comments & private& lat lng
            for (int r = 8; r < 15; r++)
            {
                GridView1.Rows[i].Cells[r].Visible = false;
                GridView1.HeaderRow.Cells[r].Visible = false;
            }
            AddJoinBtn(i);
            AddNumOfRegister(i);
            Chekdate(i);
        }

        GridView1.HeaderRow.Cells[0].Text = "";
        GridView1.HeaderRow.Cells[2].Text = "Participants";
        GridView1.HeaderRow.Cells[4].Text = "Frequency";
        GridView1.HeaderRow.Cells[6].Text = "Age";
        GridView1.HeaderRow.Cells[7].Text = "";
        AddImage();

    }

    //chek date if todat or tomorrow
    protected void Chekdate(int i)
    {  
        DateTime time = DateTime.Parse(dt.Rows[i]["Time"].ToString());
        if (DateTime.Today.Day == time.Day)
        {
            GridView1.Rows[i].Cells[3].Text = "Today!";
            // GridView1.Rows[i].Cells[3].ForeColor = System.Drawing.Color.Red;

        }
        else if (DateTime.Today.Day - time.Day == -1)
        {
            GridView1.Rows[i].Cells[3].Text = "Tomorrow!";
        }
    }

    //onmouse over color 
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='grey'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");
        }
    }

    //load the category
    protected void CategoryFilter()
    {
        NameValueCollection coll = Request.QueryString;
        String ans = coll["ans"];
        catgoryDdl.SelectedValue = ans;
        searchBtn_Click(null, null);
    }

    //adding the number of register player
    protected void AddNumOfRegister(int i)
    {
        string NumOfRegister = dt.Rows[i]["NumOfRegister"].ToString();
        string NumOfParticipants = dt.Rows[i]["NumOfParticipants"].ToString();

        GridView1.Rows[i].Cells[2].Text = NumOfRegister + "/" + NumOfParticipants;

        if (int.Parse(NumOfParticipants) <= int.Parse(NumOfRegister))//if event is full
        {
            //GridView1.Rows[i].BackColor = System.Drawing.Color.Red;
            Image ImageFUll = new Image();
            ImageFUll.ImageUrl = "Images/Full.png";
            GridView1.Rows[i].Cells[7].Controls.Clear();
            GridView1.Rows[i].Cells[7].Controls.Add(ImageFUll);
        }

        //else if ((int.Parse(NumOfParticipants)) - (int.Parse(NumOfRegister)) <= 2)//if event is allmost full
        //{ GridView1.Rows[i].BackColor = System.Drawing.Color.Blue; }

        //else if (int.Parse(NumOfRegister) == 1)//if event is new 
        //{
        //    GridView1.Rows[i].BackColor = System.Drawing.Color.Green;
        //}

    }

    //adding the join btn
    protected void AddJoinBtn(int i)
    {
        Button JoinBtn = new Button();
        JoinBtn.Text = "Join Now";
        JoinBtn.CssClass = "myButton";
        JoinBtn.Style.Add("height", "30px");
        //JoinBtn.Style.Add("visibility","hidden");
        JoinBtn.Click += new EventHandler(JoinBtn_Click);
        JoinBtn.ID = dt.Rows[i]["EventNumber"].ToString();
        GridView1.Rows[i].Cells[7].Controls.Add(JoinBtn);

        //string idEv = dt.Rows[i]["EventNumber"].ToString();
        GridView1.Rows[i].Cells[6].Text = "<a href='#' class='big-link' data-reveal-id='myModal'  onclick='SmallMap()'>  join </a>";
        //  GridView1.Rows[i].Cells[6].Text = "<asp:HyperLink ID='HyperLink1' class='big-link' runat='server'  onclick='SmallMap()'  data-reveal-id='myModal' ><asp:Button ID='" + "88" + "' CssClass='myButton' runat='server' Text='Join Now!' OnClick='joinBTN_Click' /></asp:HyperLink>";

      

    }

    //adding the image
    protected void AddImage()
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Image imsel = new Image();
            imsel.ImageUrl = dt.Rows[i]["imageUrl"].ToString();
            GridView1.Rows[i].Cells[0].Controls.Add(imsel);
        }
    }

    //go to join event page and sends the event num
    protected void JoinBtn_Click(object sender, EventArgs e)
    {// StringBuilder strScript = new StringBuilder();
   //// strScript.Append("$('a[data-reveal-id]').live('click', function(e) {e.preventDefault();var modalLocation = $(this).attr('data-reveal-id')$('#'+modalLocation).reveal($(this).data());}));");
   // strScript.Append("$(document).ready(function(){");
   // strScript.Append("alert('FDSF')"); 
   // strScript.Append("});");
   // ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "function()", strScript.ToString(), true);
   // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
       

   
       
        //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Popup", "ShowPopup();", true); 

        if (Session["Fname"] != null)
        {
            if (eventNumHF.Value != "")
            {
                Eventnum = (eventNumHF.Value);
            }
            else
            {
                Button btn = (Button)sender;
                Eventnum = (btn.ID);
            }
            Join(null, null);
            //HttpContext.Current.Session["gridTable"] = GridView1.DataSource;
            ////HttpContext.Current.Session["EventNumber"] = Eventnum;
            //Response.Redirect("joinEvent.aspx");
        }
        else
        {
            Response.Redirect("MessagePage.aspx?ans=notLogin");
        }
    }

    //changing from map view to table view
    protected void MapviewBTN_Click(object sender, EventArgs e)
    {
        if (GridView1.Visible)
        {
            GridView1.Visible = false;
            searchPholder.Visible = false;
            MapPlaceHolder.Visible = true;
            MapviewBTN.Text = "Table View";


        }
        else
        {
            GridView1.Visible = true;
            searchPholder.Visible = true;
            MapPlaceHolder.Visible = false;
            MapviewBTN.Text = "Map View";

        }
    }

    //sort func
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        //sort by city
        string cityStr = freeSearch.Text;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (!(System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[i]["Address"].ToString(), cityStr)))
            { GridView1.Rows[i].Visible = false; }
            else
            {
                GridView1.Rows[i].Visible = true;
            }
        }

        //sort by catgory 

        string catgory = catgoryDdl.SelectedItem.ToString();
        int num = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (catgory == "All")
            { return; }
            if (catgory != dt.Rows[i][1].ToString())
            { GridView1.Rows[i].Visible = false; num++; }
        }

        //sort by  age
        if (ageTXT.Text != "0")
        {
            int age = int.Parse(ageTXT.Text);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (age < int.Parse(dt.Rows[i]["MinAge"].ToString()) || age > int.Parse(dt.Rows[i]["MaxAge"].ToString()))
                { GridView1.Rows[i].Visible = false; }
            }
        }
    }

    #endregion  //home page



    #region  //join event pop up code

    // load the event detail to show
    protected void Join(object sender, EventArgs e)
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["EventNumber"].ToString() == Eventnum)
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

        //load the users that register to this event


        EventOnAir EV = new EventOnAir();
        DataTable dtUser = EV.ReadUserInEvent(Eventnum);
        int num = dtUser.Rows.Count;

        //adding the num coulm
        DataColumn dc = new DataColumn("num");
        dc.DataType = typeof(int);
        dtUser.Columns.Add(dc);
        dc.SetOrdinal(0);

        for (int i = 0; i < int.Parse(ANS_MaxPlayerLbl.Text) - num; i++)
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




    //adding the user to the event
    protected void joinBTN_Click(object sender, EventArgs e)
    {
        if (Session["UserDeatail"] == null) return;
        DataTable dt = (DataTable)HttpContext.Current.Session["UserDeatail"];

        User U1 = new User();
        U1.Email = dt.Rows[0]["Email"].ToString();
        int num = U1.InsertToEvent(Eventnum);
        Response.Redirect("joinEvent.aspx");
    }
    #endregion



 
}
