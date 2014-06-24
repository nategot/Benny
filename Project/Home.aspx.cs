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
    int NumOfRegister;
    int NumOfParticipants;
    DateTime time;
    DateTime now;
    

    protected void Page_Load(object sender, EventArgs e)
    {

        LoadTable();
        EditGridView();
        if (!(Page.IsPostBack))
        {
            CategoryFilter();
        }
    }

    #region


    protected void LoadTable()
    {
        MapPlaceHolder.Visible = false;
        dt = Ev.readTable();
        GridView1.DataSource = dt;
        GridView1.DataBind();

        //load the user age
        if (!(Page.IsPostBack))
        {
            if (Session["UserDeatail"] != null)
            {
                DataTable dtUser = (DataTable)HttpContext.Current.Session["UserDeatail"];
                ageTXT.Text = dtUser.Rows[0]["Age"].ToString();
            }
            else
                ageTXT.Text = "0";
        }
    
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
            CheckBox cb = (CheckBox)GridView1.Rows[i].Cells[11].Controls[0];
            if (cb.Checked)
            { GridView1.Rows[i].Visible = false; }

            //hide EventNumber & Comments & private& lat lng
            for (int r = 9; r < 16; r++)
            {
                GridView1.Rows[i].Cells[r].Visible = false;
                GridView1.HeaderRow.Cells[r].Visible = false;
            }
            AddJoinBtn(i);
            AddNumOfRegister(i);
            ProbabilityForGame(i); 
            Chekdate(i);
        }

        GridView1.HeaderRow.Cells[0].Text = "";
        GridView1.HeaderRow.Cells[2].Text = "Participants";
        GridView1.HeaderRow.Cells[4].Text = "Frequency";
        GridView1.HeaderRow.Cells[6].Text = "Age";
        GridView1.HeaderRow.Cells[7].Text = "";
        AddImage();

    }

    //chek date if today or tomorrow
    protected void Chekdate(int i)
    {
        time = DateTime.Parse(dt.Rows[i]["Time"].ToString());
        TimeSpan diff2 = time.Subtract(now);

        if (diff2.Days == 0 && time.Day == now.Day)
        {
            GridView1.Rows[i].Cells[3].Text = "Today!";
        }
        else if (diff2.Days == 1)
        {
            GridView1.Rows[i].Cells[3].Text = "Tomorrow!";
        }
    }


    // Probability calculat 
    protected void ProbabilityForGame(int i)
    {
        if (NumOfRegister == NumOfParticipants)
        {
            GridView1.Rows[i].Cells[8].Text = "99%";
            return;
        }

        int rating;
        double averageRating;
        double prob = 99;
        //calculat by date
        time = DateTime.Parse(dt.Rows[i]["Time"].ToString());
        now = DateTime.Now;
        TimeSpan diff = time.Subtract(now);
        ////by time and num of register
        #region

        if (diff.Days == 0 && diff.Hours <= 3)//if less then 3 hours to start time
        {
            if (diff.Days == 0 && diff.Hours <= 2)
            {
                if (NumOfRegister / NumOfParticipants < 0.5)//if less then 50% has registerd
                {
                    prob = 60;
                }
                else if (NumOfRegister / NumOfParticipants > 0.8)//if more then 80% has registerd
                {
                     prob = 99;
                }
                else //between 50%-80%
                {
                    prob = 70;
                }

            }
            else//less then 3 hours more then 2
            {
                if (NumOfRegister / NumOfParticipants > 0.5)//if more then 50% has registerd
                {
                    if (diff.Days == 0 && diff.Hours < 1.5 && NumOfRegister / NumOfParticipants < 0.8)//if less then  80% has registerd and less then 1.5  hours to start time
                    { prob = 80; }
                }
                else //if less then 50% has registerd less then 3 hours to start time
                {
                    prob = 90;
                }
            }
        }
#endregion  
        //by average rating
       
        Ev.EventNum= dt.Rows[i]["EventNumber"].ToString();
         rating=Ev.GetRating();
         averageRating = rating / NumOfRegister;

         if (averageRating>90)//if average rating is more then 90 add but last then 99 add 20%
         {
             if (prob !=99)
             {
                 prob *= 1.2;
             }
             if (prob >99)
             {prob=99;}
         }
         else if (averageRating > 70)//if average rating is  between 70-90 less 10% for prob
         {
             prob *= 0.9;
         }
         else//if average rating is   less  then 70  less 20% for prob
         {
             prob *= 0.8;
         }
    
        GridView1.Rows[i].Cells[8].Text = prob.ToString() + "%";
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
        NumOfRegister = int.Parse(dt.Rows[i]["NumOfRegister"].ToString());
        NumOfParticipants = int.Parse(dt.Rows[i]["NumOfParticipants"].ToString());

        GridView1.Rows[i].Cells[2].Text = NumOfRegister + "/" + NumOfParticipants;

        if (NumOfParticipants <= NumOfRegister)//if event is full
        {
            GridView1.Rows[i].Cells[7].Controls.Clear();
            GridView1.Rows[i].Cells[7].Text = "<div class='btnFullEvent' >Full!</div>";
        }

    }

    //adding the join btn
    protected void AddJoinBtn(int i)
    {
        string idEv = dt.Rows[i]["EventNumber"].ToString();
        GridView1.Rows[i].Cells[7].Text = "<a href='#' class='' data-reveal-id='myModal'  onclick='loadEventDetail(" + idEv + ")'>  <input class='btnjoinevent' type='button' value='Join!' />  </a>";
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

    // join user to event  
    protected void JoinBtn_Click(object sender, EventArgs e)
    {
        //CategoryFilter();
        if (Session["Fname"] != null)
        {
            Eventnum = (eventNumHF.Value);

            if (Session["UserDeatail"] == null) return;
            DataTable dt = (DataTable)HttpContext.Current.Session["UserDeatail"];

            User U1 = new User();
            U1.Email = dt.Rows[0]["Email"].ToString();
            int num = U1.InsertToEvent(Eventnum);
            //pop register
            if (num >= 1)
            {
                ShowPopup("you have added to the event Successfully");
            }
            else if (num == -1)
            {
                ShowPopup("You Are already register to this event");
            }
            else if (num == 0)
            {
                ShowPopup("Error register faild  please try agin later");
            }
            Response.Redirect("MyEvents.aspx");
        }
        else
        {
            Response.Redirect("MessagePage.aspx?ans=NotLoginME");
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


    protected void ShowPopup(string message) //popup message
    {

        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Popup", "ShowPopup('" + message + "');", true);
    }

    #endregion




}
