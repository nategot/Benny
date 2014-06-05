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
    DataTable dtMyEvent;
    DataTable dtuser;
    EventOnAir Ev = new EventOnAir();
    string Eventnum;

    protected void Page_Load(object sender, EventArgs e)
    {
        //// if the user is not login go to login
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
        //check if iser log in
        if (Session["UserDeatail"] == null) return;
        dtuser = (DataTable)HttpContext.Current.Session["UserDeatail"];
        
        User U1 = new User();
        U1.Email = dtuser.Rows[0]["Email"].ToString();
        adminIDHIde.Value = dtuser.Rows[0]["UserId"].ToString();
        adminEmailHIde.Value = U1.Email;
        
        dtMyEvent = U1.ReadMyEvent();
        GridView1.DataSource = dtMyEvent;
        GridView1.DataBind();

        //load the user age
        ageTXT.Text = dtuser.Rows[0]["Age"].ToString();
  
    }


    //edit the gridview coulom
    protected void EditGridView()
    {
        string ageRange;
        for (int i = 0; i < dtMyEvent.Rows.Count; i++)
        {
            //edit the age range
            ageRange = dtMyEvent.Rows[i]["MinAge"].ToString();
            ageRange += "-" + dtMyEvent.Rows[i]["MaxAge"].ToString();
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
     

        if (GridView1.Rows.Count == 0)
        {
            ShowPopup("you dont have any events");
        }
        else
        {
            GridView1.HeaderRow.Cells[0].Text = "";
            GridView1.HeaderRow.Cells[2].Text = "Participants";
            GridView1.HeaderRow.Cells[4].Text = "Frequency";
            GridView1.HeaderRow.Cells[6].Text = "Age";
            GridView1.HeaderRow.Cells[7].Text = "";
        }

        AddImage();
    }

    //check date if today or tomorrow
    protected void Chekdate(int i)
    {
        DateTime time = DateTime.Parse(dtMyEvent.Rows[i]["Time"].ToString());
        if (DateTime.Today == time)
        {
            GridView1.Rows[i].Cells[3].Text = "Today!";
        }
        else if (DateTime.Today == DateTime.Today.AddDays(1))
        {
            GridView1.Rows[i].Cells[3].Text = "Tomorrow!";
        }
        else if (DateTime.Today > time)
        {
            Image ImageFUll = new Image();
            ImageFUll.ImageUrl = "pic/Date over.jpg";
            ImageFUll.Width = 80;
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
        string NumOfRegister = dtMyEvent.Rows[i]["NumOfRegister"].ToString();
        string NumOfParticipants = dtMyEvent.Rows[i]["NumOfParticipants"].ToString();
        GridView1.Rows[i].Cells[2].Text = NumOfRegister + "/" + NumOfParticipants;
    }

    //adding the join btn
    protected void AddJoinBtn(int i)
    {
        string idEv = dtMyEvent.Rows[i]["EventNumber"].ToString();
        GridView1.Rows[i].Cells[7].Text = "<a href='#' class='' data-reveal-id='myModal'  onclick='loadEventDetail(" + idEv + ")'>  View Detail </a>";
    }

    //adding the image
    protected void AddImage()
    {
        for (int i = 0; i < dtMyEvent.Rows.Count; i++)
        {
            Image imsel = new Image();
            imsel.ImageUrl = dtMyEvent.Rows[i]["imageUrl"].ToString();
            GridView1.Rows[i].Cells[0].Controls.Add(imsel);
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

        for (int i = 0; i < dtMyEvent.Rows.Count; i++)
        {
            if (!(System.Text.RegularExpressions.Regex.IsMatch(dtMyEvent.Rows[i]["Address"].ToString(), cityStr)))
            { GridView1.Rows[i].Visible = false; }
            else
            {
                GridView1.Rows[i].Visible = true;
            }
        }

        //sort by catgory 

        string catgory = catgoryDdl.SelectedItem.ToString();
        int num = 0;
        for (int i = 0; i < dtMyEvent.Rows.Count; i++)
        {
            if (catgory == "All")
            { return; }
            if (catgory != dtMyEvent.Rows[i][1].ToString())
            { GridView1.Rows[i].Visible = false; num++; }
        }

        //sort by  age
        if (ageTXT.Text != "0")
        {
            int age = int.Parse(ageTXT.Text);

            for (int i = 0; i < dtMyEvent.Rows.Count; i++)
            {
                if (age < int.Parse(dtMyEvent.Rows[i]["MinAge"].ToString()) || age > int.Parse(dtMyEvent.Rows[i]["MaxAge"].ToString()))
                { GridView1.Rows[i].Visible = false; }
            }
        }
    }


    protected void ShowPopup(string message) //popup message
    {
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Popup", "ShowPopup('" + message + "');", true);
    }

    #endregion


    //go to join event  and sends the event num    // למחוק???
    protected void EditEventBTn_Click(object sender, EventArgs e)
    {
        Eventnum = (eventNumHF.Value);
        HttpContext.Current.Session["MyEventsDT"] =dtMyEvent;
        HttpContext.Current.Session["Eventnum"] = Eventnum;
        Response.Redirect("NewEvent.aspx?edit=Yes");
      
    }

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
