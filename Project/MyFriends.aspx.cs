using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

public partial class MyFriends : System.Web.UI.Page
{
    List<string> emailList = new List<string>();
    DataTable usetT;
    DataTable dt;
    string eventnum;

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadUserTable();
        //HttpContext.Current.Session["UserId"] = 10;// בדיקה להוריד כשיש לוג אין פייס  
        LoadNewUserTalbe();



        if (Session["Eventnum"] == null) return;
        eventnum = HttpContext.Current.Session["Eventnum"].ToString();



    }

    public void LoadNewUserTalbe()
    {
        addtogroupGV.DataSource = usetT;
        addtogroupGV.DataBind();

        for (int i = 0; i < addtogroupGV.Rows.Count; i++)
        {
            Image imsel = new Image();
            imsel.ImageUrl = usetT.Rows[i]["Picture"].ToString();
            imsel.CssClass = "imgFrinds";
            addtogroupGV.Rows[i].Cells[0].Controls.Add(imsel);
            CheckBox check2 = new CheckBox();
            addtogroupGV.Rows[i].Cells[3].Controls.Add(check2);
        }

    }

    public void LoadUserTable()
    {
        DBservices db = new DBservices();
        usetT = db.GetAllUsers();
        userGride.DataSource = usetT;
        userGride.DataBind();
        AddImage();
        AddCheckBox();

        //load events
        EventOnAir ev = new EventOnAir();
        dt = ev.readTable();
    }

    //add  thr img
    public void AddImage()
    {
        for (int i = 0; i < userGride.Rows.Count; i++)
        {
            Image imsel = new Image();
            imsel.ImageUrl = usetT.Rows[i]["Picture"].ToString();
            imsel.CssClass = "imgFrinds";
            userGride.Rows[i].Cells[0].Controls.Add(imsel);
        }

    }

    //add check box
    public void AddCheckBox()
    {
        for (int i = 0; i < userGride.Rows.Count; i++)
        {
            CheckBox check = new CheckBox();
            userGride.Rows[i].Cells[3].Controls.Add(check);
        }

    }

    //send email from gride
    protected void Button1_Click(object sender, EventArgs e)
    {
        int rownum = 0;

        //find the row num of the event
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["EventNumber"].ToString() == eventnum)
                rownum = i;
        }

        //send from grid
        for (int i = 0; i < userGride.Rows.Count; i++)
        {
            CheckBox check = (CheckBox)userGride.Rows[i].Cells[3].Controls[0];
            if (check.Checked)
            {
                SendMail(usetT.Rows[i]["Email"].ToString(), rownum);
            }
        }

        //send from list
        for (int i = 0; i < userBuletListe.Items.Count; i++)
        {
            SendMail(userBuletListe.Items[i].Text, rownum);
        }

    }

    //add email to send list
    protected void AddBtn_Click(object sender, EventArgs e)
    {
        if (emailTb.Text != "")
        {
            ListItem stelt = new ListItem(emailTb.Text);
            stelt.Selected = true;
            userBuletListe.Items.Add(stelt);
        }


    }

    //send list to send mail 
    protected void SendBTn_Click(object sender, EventArgs e)
    {
        int rownum = 0;
        //find the row num of the event
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["EventNumber"].ToString() == eventnum)
                rownum = i;
        }

        for (int i = 0; i < userBuletListe.Items.Count; i++)
        {
            SendMail(userBuletListe.Items[i].Text, rownum);
        }
    }

    //add to checkbox list
    protected void Button1_Click1(object sender, EventArgs e)
    {
        //ListItem cheitem = new ListItem(emailaddtb.Text);
        //cheitem.Selected = true;
        //CheckBoxList1.Items.Add(cheitem);

    }

    //create new group
    protected void creategroupBtn_Click(object sender, EventArgs e)
    {
        List<string> EmailList = new List<string>();
        List<string> FnameList = new List<string>();
        List<string> LnameList = new List<string>();
        List<string> UrlList = new List<string>();

        //insert all to a lists.

        for (int i = 0; i < userGride.Rows.Count; i++)
        {
            CheckBox check = (CheckBox)userGride.Rows[i].Cells[3].Controls[0];
            if (check.Checked)
            {
                EmailList.Add(userGride.Rows[i].Cells[3].Text);//save  Mail
                LnameList.Add(userGride.Rows[i].Cells[2].Text);//save Lname
                FnameList.Add(userGride.Rows[i].Cells[1].Text);//save  fname
                UrlList.Add(userGride.Rows[i].Cells[0].Text);//save Url
            }
        }



        DataTable dtUser = (DataTable)HttpContext.Current.Session["UserDeatail"];
        User U1 = new User();
        U1.UserId = int.Parse(dtUser.Rows[0]["UserID"].ToString());

        U1.BulidGroup(EmailList, FnameList, LnameList, UrlList, groupnameTb.Text);
        groupnameDDL.DataBind();
        Page_Load(null, null);
    }

    //send mail func
    protected void SendMail(string email, int rownum)
    {
        if (Session["UserDeatail"] == null) return;
        DataTable dtuser = (DataTable)HttpContext.Current.Session["UserDeatail"];

        try
        {
            MailMessage Msg = new MailMessage();
            // Sender e-mail address.
            Msg.From = new MailAddress("LetsPlay.ruppin@gmail.com");
            // Recipient e-mail address.
            Msg.To.Add(email);
            Msg.Subject = "You are invted to a new event by " + dtuser.Rows[0]["Fname"].ToString() + " " + dtuser.Rows[0]["Lname"].ToString();
            // File Upload path
            string mailbody = "<h3 style='color:Navy;font-size:xx-large; font-weight:bold; font-family:Guttman Yad-Brush;'>Hello,You have  been invited to a new event!</h3>";
            mailbody += "<h1 style='color:Navy;font-size:xx-large; font-weight:bold; font-family:Guttman Yad-Brush;'>" + dt.Rows[rownum]["Description"].ToString() + "</h1>";
            mailbody += "<h3 style='Guttman Yad-Brush;'>Participants:  " + dt.Rows[rownum]["NumOfRegister"].ToString() + "/" + dt.Rows[rownum]["NumOfParticipants"].ToString() + "</h3>";
            mailbody += "<h3 style='Guttman Yad-Brush;'>Date & Time: " + dt.Rows[rownum]["Time"].ToString() + "</h3>";
            mailbody += "<h3 style='Guttman Yad-Brush;'>Age Range:  " + dt.Rows[rownum]["MinAge"].ToString() + "-" + dt.Rows[rownum]["MaxAge"].ToString() + "</h3>";
            mailbody += "<h3 style='Guttman Yad-Brush;'>Location:  " + dt.Rows[rownum]["Address"].ToString() + "</h3>";
            mailbody += "<h3 style='Guttman Yad-Brush;'>Admin Comments:  " + dt.Rows[rownum]["Comments"].ToString() + "</h3>";
            mailbody += "<a href='http://proj.ruppin.ac.il/bgroup14/prod/tar6/Home.aspx?ans=" + dt.Rows[rownum]["Description"].ToString() + "'><h3>to join the event push here!</h3></a>";
            mailbody += "<br />";
            mailbody += "<p><img style='width:200px;' src='http://proj.ruppin.ac.il/bgroup14/prod/tar6/pic/logo_black.png'/></p>";
            mailbody += "<br/>";
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

        }//try
        catch (Exception ex)
        {
            ShowPopup(ex.Message);
        }
    }

    //view the user in group (dropdownlist)
    protected void groupnameDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        userIngroupGv.DataBind();
    }


    //invite group
    protected void Unnamed2_Click(object sender, EventArgs e)
    {
        int rownum = 0;

        //find the row num of the event
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["EventNumber"].ToString() == eventnum)
                rownum = i;
        }

        //send from grid
        for (int i = 0; i < userIngroupGv.Rows.Count; i++)
        {
            SendMail(userIngroupGv.Rows[i].Cells[4].Text, rownum);
        }
    }

    //insert new email to group
    protected void addNewTogroup_Click(object sender, EventArgs e)
    {
        List<string> NewEmailList = new List<string>();
        List<string> NewFnameList = new List<string>();
        List<string> NewLnameList = new List<string>();
        List<string> NewUrlList = new List<string>();

        //insert all to a lists.
        for (int i = 0; i < addtogroupGV.Rows.Count; i++)
        {

            CheckBox check2 = (CheckBox)addtogroupGV.Rows[i].Cells[3].Controls[0];
            if (check2.Checked)
            {
                NewEmailList.Add(userGride.Rows[i].Cells[3].Text);//save  Mail
                NewFnameList.Add(userGride.Rows[i].Cells[2].Text);//save Lname
                NewLnameList.Add(userGride.Rows[i].Cells[1].Text);//save  fname
                NewUrlList.Add(userGride.Rows[i].Cells[0].Text);//save Url
            }
        }

        DataTable dtUser = (DataTable)HttpContext.Current.Session["UserDeatail"];
        User U1 = new User();
        U1.UserId = int.Parse(dtUser.Rows[0]["UserID"].ToString());

        U1.BulidGroup(NewEmailList, NewFnameList, NewLnameList, NewUrlList, groupnameDDL.SelectedItem.Text);
        addtogroupGV.Visible = false;
        addNewTogroup.Visible = false;
        Page_Load(null, null);
    }

    //CHANGE VIEW   
    protected void changeBtn_Click(object sender, EventArgs e)
    {
        if (buildgroupPH.Visible)
        {
            buildgroupPH.Visible = false;
            invitPH.Visible = true;
            changeBtn.Text = "build new group";

        }
        else
        {
            buildgroupPH.Visible = true;
            invitPH.Visible = false;
            changeBtn.Text = "Invite from list";

        }


    }

    //add new to group open the gride view
    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        addtogroupGV.Visible = true;
        addNewTogroup.Visible = true;
    }

    //pop up
    protected void ShowPopup(string message)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
    }
}