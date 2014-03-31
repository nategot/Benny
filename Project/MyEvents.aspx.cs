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
    //DataTable dt;
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    MapPlaceHolder.Visible = false;
    //    string ageRange;
    //    EventOnAir Ev = new EventOnAir();
    //    dt = Ev.readTable();
    //    GridView1.DataSource = dt;
    //    //GridView1 desing
    //    GridView1.AllowPaging = true;
    //    dt.Columns[6].ColumnName = "Age Range";
    //    GridView1.DataBind();




    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {   //edit the age range
    //        ageRange = dt.Rows[i]["Age Range"].ToString();
    //        ageRange += "-" + dt.Rows[i]["MaxAge"].ToString();
    //        GridView1.Rows[i].Cells[6].Text = ageRange;


    //        // adding the join btn
    //        Button JoinBtn = new Button();
    //        JoinBtn.Text = " Join Now";
    //        JoinBtn.Click += new EventHandler(JoinBtn_Click);
    //        JoinBtn.ID = dt.Rows[i]["EventNumber"].ToString();
    //        GridView1.Rows[i].Cells[7].Controls.Add(JoinBtn);

    //        //hide if private
    //        CheckBox cb = (CheckBox)GridView1.Rows[i].Cells[10].Controls[0];
    //        if (cb.Checked)
    //        { GridView1.Rows[i].Visible = false; }

    //        //hide EventNumber & Comments & private
    //        GridView1.Rows[i].Cells[8].Visible = false;
    //        GridView1.Rows[i].Cells[9].Visible = false;
    //        GridView1.Rows[i].Cells[10].Visible = false;


    //    }

    //    GridView1.HeaderRow.Cells[7].Text = "";
    //    GridView1.HeaderRow.Cells[8].Visible = false;
    //    GridView1.HeaderRow.Cells[9].Visible = false;
    //    GridView1.HeaderRow.Cells[10].Visible = false;

    //    //adding the image
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        Image imsel = new Image();
    //        imsel.ImageUrl = dt.Rows[i]["imageUrl"].ToString();
    //        GridView1.Rows[i].Cells[1].Controls.Add(imsel);
    //    }
    //}

    //protected void JoinBtn_Click(object sender, EventArgs e)
    //{
    //    Button btn = (Button)sender;
    //    int Eventnum = int.Parse(btn.ID);

    //    HttpContext.Current.Session["gridTable"] = GridView1.DataSource;
    //    HttpContext.Current.Session["EventNumber"] = Eventnum;
    //    Response.Redirect("joinEvent.aspx");

    //}



    ////changing from map view to table view
    //protected void MapviewBTN_Click(object sender, EventArgs e)
    //{
    //    if (GridView1.Visible)
    //    {
    //        GridView1.Visible = false;
    //        MapPlaceHolder.Visible = true;
    //        MapviewBTN.Text = "Table View";

    //    }
    //    else
    //    {
    //        GridView1.Visible = true;
    //        MapPlaceHolder.Visible = false;
    //        MapviewBTN.Text = "Map View";

    //    }
    //}






    //protected void catgoryDdl_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DropDownList CatagoryDdl = (DropDownList)sender;
    //    string catgory = CatagoryDdl.SelectedItem.ToString();
    //    int num = 0;
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        if (catgory == "All")
    //        { return; }
    //        if (catgory != dt.Rows[i][0].ToString())
    //        { GridView1.Rows[i].Visible = false; num++; }
    //    }


    //}

}