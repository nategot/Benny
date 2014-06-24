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
using System.Net.Mail;



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

        if (diff.Days == 0 && diff.Hours <= 4)//if less then 4 hours to start time
        {
            if (diff.Days == 0 && diff.Hours <= 3)
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
            else//less then 4 hours more then 2
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
               
                SendMail(U1.Email, Eventnum);

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

   //popup func
    protected void ShowPopup(string message) //popup message
    {

        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Popup", "ShowPopup('" + message + "');", true);
    }

    protected void SendMail(string email,string eventnum)
    { int rownum=0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["EventNumber"].ToString() == eventnum)
                rownum = i;
        }
        
        try
        {
            MailMessage Msg = new MailMessage();
            // Sender e-mail address.
            Msg.From = new MailAddress("LetsPlay.ruppin@gmail.com");
            // Recipient e-mail address.
            Msg.To.Add(email);// צריך לעשות דנמי
            Msg.Subject = "You have joined to a new event";
            // File Upload path
            string mailbody = "<h3 style='color:Navy;font-size:xx-large; font-weight:bold; font-family:Guttman Yad-Brush;'>Hello,You have joined a new event!</h3><br/>";

            mailbody += "<h1 style='color:Navy;font-size:xx-large; font-weight:bold; font-family:Guttman Yad-Brush;'>" + dt.Rows[rownum]["Description"].ToString() + "</h1>";
            mailbody += "<h3 style='Guttman Yad-Brush;'>Max Participants:  " + dt.Rows[rownum]["NumOfRegister"].ToString() + "/" + dt.Rows[rownum]["NumOfParticipants"].ToString() + "</h3>";
            mailbody += "<h3 style='Guttman Yad-Brush;'>Date & Time: " + dt.Rows[rownum]["Time"].ToString() + "</h3>";
            mailbody += "<h3 style='Guttman Yad-Brush;'>Age Range:  " + dt.Rows[rownum]["MinAge"].ToString() + "-" + dt.Rows[rownum]["MaxAge"].ToString() + "</h3>";
            mailbody += "<h3 style='Guttman Yad-Brush;'>Location:  " + dt.Rows[rownum]["Address"].ToString() + "</h3>";
            mailbody += "<h3 style='Guttman Yad-Brush;'>Admin Comments:  " + dt.Rows[rownum]["Comments"].ToString() + "</h3>";
            mailbody += "<br /><br /><br />";
            mailbody += "<p><img style='width:100px;' src='http://proj.ruppin.ac.il/bgroup14/prod/tar6/pic/logo_black.png'/></p>";
            mailbody += "<br /><br />";
            mailbody += "<p style='color:blue; font-size:large; font-weight:bold; font-family:Guttman;'>Let's Play </p><p> LetsPlay.ruppin@gmail.com</p>";
           
            // Create HTML view
            AlternateView htmlMail = AlternateView.CreateAlternateViewFromString(mailbody, null, "text/html");
            // Set ContentId property. Value of ContentId property must be the same as
            // the src attribute of image tag in email body. 
            Msg.AlternateViews.Add(htmlMail);
            // your remote SMTP server IP.
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("LetsPlay.ruppin@gmail.com", "bgroup14");
            smtp.EnableSsl = true;
            smtp.Send(Msg);
            //Msg = null;
            //Page.RegisterStartupScript("UserMsg", "<script>alert('Mail sent thank you...');if(alert){ window.location='SendMail.aspx';}</script>");
        }//try
        catch (Exception ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }//catch
    }
    #endregion




}
