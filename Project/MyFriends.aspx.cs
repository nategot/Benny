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

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadUserTable();
        
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
            imsel.Attributes.Add("width", "30px");
            imsel.Attributes.Add("hight", "30px");
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < userGride.Rows.Count; i++)
        {
            CheckBox check =(CheckBox)userGride.Rows[i].Cells[3].Controls[0];
            if (check.Checked)
            {
                SendMail(usetT.Rows[i]["Email"].ToString());
            }
        }

    }
   
    //send mail
    protected void SendMail(string email)
    {    
        string eventnum = "185";
        int rownum = 0;

        if (Session["UserDeatail"] == null) return;
        DataTable dtuser = (DataTable)HttpContext.Current.Session["UserDeatail"];
      
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
            Msg.To.Add(email);
            Msg.Subject = "You are invted to a new event by " + dtuser.Rows[0]["Fname"].ToString() + " " + dtuser.Rows[0]["Lname"].ToString();
            // File Upload path
            string mailbody = "<h3 style='color:Navy;font-size:xx-large; font-weight:bold; font-family:Guttman Yad-Brush;'>Hello,You have  been invited to a new event!</h3><br/>";
            mailbody += "<h1 style='color:Navy;font-size:xx-large; font-weight:bold; font-family:Guttman Yad-Brush;'>" + dt.Rows[rownum]["Description"].ToString() + "</h1>";
            mailbody += "<h3 style='Guttman Yad-Brush;'>Participants:  " + dt.Rows[rownum]["NumOfRegister"].ToString() + "/" + dt.Rows[rownum]["NumOfParticipants"].ToString() + "</h3>";
            mailbody += "<h3 style='Guttman Yad-Brush;'>Date & Time: " + dt.Rows[rownum]["Time"].ToString() + "</h3>";
            mailbody += "<h3 style='Guttman Yad-Brush;'>Age Range:  " + dt.Rows[rownum]["MinAge"].ToString() + "-" + dt.Rows[rownum]["MaxAge"].ToString() + "</h3>";
            mailbody += "<h3 style='Guttman Yad-Brush;'>Location:  " + dt.Rows[rownum]["Address"].ToString() + "</h3>";
            mailbody += "<h3 style='Guttman Yad-Brush;'>Admin Comments:  " + dt.Rows[rownum]["Comments"].ToString() + "</h3>";
            mailbody +=  "<a href='http://proj.ruppin.ac.il/bgroup14/prod/tar6/'><h3>to join the event push here!</h3></a>";
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
            Console.WriteLine("{0} Exception caught.", ex);
        }//catch
    }
}