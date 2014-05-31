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


public partial class MyEvents : System.Web.UI.Page
{
    DataTable dt;
    EventOnAir Ev = new EventOnAir();
    string Eventnum;

    protected void Page_Load(object sender, EventArgs e)
    {
        // if the user is not login go to login
        if (Session["Fname"] == null)
        {
            Response.Redirect("MessagePage.aspx?ans=notLogin");
        }

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
        if (Session["UserDeatail"] == null) return;
        DataTable dtuser = (DataTable)HttpContext.Current.Session["UserDeatail"];

        User U1 = new User();
        U1.Email = dtuser.Rows[0]["Email"].ToString();
        dt = U1.ReadMyEvent();
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

            //hide EventNumber & Comments & private& lat lng& Email
            for (int r = 8; r < 16; r++)
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

    //check date if today or tomorrow
    protected void Chekdate(int i)
    {
        DateTime time = DateTime.Parse(dt.Rows[i]["Time"].ToString());
        if (DateTime.Today == time)
        {
            GridView1.Rows[i].Cells[3].Text = "Today!";
        }
        else if (DateTime.Today == DateTime.Today.AddDays(1))
        {
            GridView1.Rows[i].Cells[3].Text = "Tomorrow!";
        }
        else if (DateTime.Today > time )
        {
            Image ImageFUll = new Image();
            ImageFUll.ImageUrl = "pic/Date over.jpg";
            ImageFUll.Width=80;
            ImageFUll.Height = 30;

            GridView1.Rows[i].Cells[7].Controls.Clear();
            GridView1.Rows[i].Cells[7].Controls.Add(ImageFUll);
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

        //if (int.Parse(NumOfParticipants) <= int.Parse(NumOfRegister))//if event is full
        //{
        //    Image ImageFUll = new Image();
        //    ImageFUll.ImageUrl = "Images/Full.png";
        //    GridView1.Rows[i].Cells[7].Controls.Clear();
        //    GridView1.Rows[i].Cells[7].Controls.Add(ImageFUll);
        //}
    }

    //adding the join btn
    protected void AddJoinBtn(int i)
    {
        string idEv = dt.Rows[i]["EventNumber"].ToString();
        GridView1.Rows[i].Cells[7].Text = "<a href='#' class='' data-reveal-id='myModal'  onclick='loadEventDetail(" + idEv + ")'>  View Detail </a>";
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

    //go to join event  and sends the event num    // למחוק???
    protected void JoinBtn_Click(object sender, EventArgs e)
    {
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

            //HttpContext.Current.Session["gridTable"] = GridView1.DataSource;
            //HttpContext.Current.Session["EventNumber"] = Eventnum;
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


    protected void ShowPopup(string message) //popup message
    {
        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Popup", "ShowPopup('" + message + "');", true);
    }

    #endregion




    protected void LeaveBtn_Click(object sender, EventArgs e)
    {
            Eventnum = (eventNumHF.Value);

            if (Session["UserDeatail"] == null) return;
            DataTable dt = (DataTable)HttpContext.Current.Session["UserDeatail"];

            User U1 = new User();
            U1.Email = dt.Rows[0]["Email"].ToString();
            int num = U1.deleteUserFromEvent(Eventnum.ToString());
            //pop register
            if (num >= 1)
            {
                ShowPopup("you have removed from the event Successfully");
            }
            else if (num == 0)
            {
                ShowPopup("Error register faild  please try agin later");
            }
        }
     
    
}
