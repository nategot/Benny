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
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadTable();
        EditGridView();
    }

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
        }

        GridView1.HeaderRow.Cells[0].Text = "";
        GridView1.HeaderRow.Cells[2].Text = "Max Partic.";
        GridView1.HeaderRow.Cells[6].Text = "Age Range";
        GridView1.HeaderRow.Cells[7].Text = "";
        AddImage();
        

        
    }

    //adding the number pf register player
    protected void AddNumOfRegister( int i )
    {
        string NumOfRegister = dt.Rows[i]["NumOfRegister"].ToString();

        GridView1.Rows[i].Cells[2].Text = NumOfRegister+  "/" + GridView1.Rows[i].Cells[2].Text ;
    }



    // adding the join btn
    protected void AddJoinBtn(int i)
    {
        Button JoinBtn = new Button();
        JoinBtn.Text = "Join Now";
        JoinBtn.CssClass = "myButton";
        JoinBtn.Style.Add("height", "30px");
        JoinBtn.Click += new EventHandler(JoinBtn_Click);
        JoinBtn.ID = dt.Rows[i]["EventNumber"].ToString();
        GridView1.Rows[i].Cells[7].Controls.Add(JoinBtn);
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
    {
        int Eventnum;

        if (Session["Fname"] != null)
        {
            if (eventNumHF.Value != "")
            {
                Eventnum = int.Parse(eventNumHF.Value);
            }
            else
            {
                Button btn = (Button)sender;
                Eventnum = int.Parse(btn.ID);
            }

            HttpContext.Current.Session["gridTable"] = GridView1.DataSource;
            HttpContext.Current.Session["EventNumber"] = Eventnum;
            Response.Redirect("joinEvent.aspx");
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
    {   //sort by city
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

}