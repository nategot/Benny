using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.IO;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]


public class WebService : System.Web.Services.WebService
{
    private DataTable dtUserEvents;

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //gets all events
    public string getEvents()
    {
        EventOnAir ev = new EventOnAir();
        List<EventOnAir> eventsList = new List<EventOnAir>();
        DataTable dt = ev.readTable();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            EventOnAir evTemp = new EventOnAir();
            evTemp.Point = new Point(double.Parse(dt.Rows[i]["Lat"].ToString()), double.Parse(dt.Rows[i]["Lng"].ToString()));
            evTemp.Address = dt.Rows[i]["Address"].ToString();
            evTemp.MaxAge = int.Parse(dt.Rows[i]["MaxAge"].ToString());
            evTemp.MinAge = int.Parse(dt.Rows[i]["MinAge"].ToString());
            evTemp.NumOfParti = int.Parse(dt.Rows[i]["NumOfParticipants"].ToString());
            evTemp.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
            evTemp.AdminID = int.Parse(dt.Rows[0]["AdminId"].ToString());
            evTemp.IsPrivate1 = bool.Parse(dt.Rows[0]["Private"].ToString());
            evTemp.DateTime = DateTime.Parse(dt.Rows[i]["Time"].ToString());
            evTemp.DateTimeStr = (dt.Rows[i]["Time"].ToString());
            evTemp.Description = dt.Rows[i]["Description"].ToString();
            evTemp.Comments = dt.Rows[i]["Comments"].ToString();
            evTemp.EventNum = dt.Rows[i]["EventNumber"].ToString();


            //add the  event to the list
            eventsList.Add(evTemp);
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(eventsList);
        return jsonString;
    }


    //mobile
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //add event
    public string setPOI(double lat, double lng, int nop, int category, string type, int frequecy, int minAge, int maxAge, string address, string time, string comments, int adminId)
    {
        string strtemp;
        string[] dateArr = new string[2];
        string[] dateArrT = new string[3];
        EventOnAir ev = new EventOnAir();
        ev.Point = new Point(lat, lng);
        ev.Address = address;
        ev.MaxAge = maxAge;
        ev.MinAge = minAge;
        ev.NumOfParti = nop;
        ev.Catedory = category;
        ev.IsPrivate1 = bool.Parse(type.ToString());
        dateArr = time.Split(' ');
        dateArrT = dateArr[0].Split('/');
        strtemp = dateArrT[1] + "/" + dateArrT[0] + "/" + dateArrT[2];
        string dateandtime = strtemp + " " + dateArr[1];
        ev.DateTime = DateTime.Parse(dateandtime);
        ev.Comments = comments;
        ev.Frequency = frequecy;
        ev.AdminID = adminId;

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize("ok");
        try
        {     ev.insert();
        jsonString = time + "-----" + "dateandtime";
            //jsonString = ev.insert().ToString();
               
        }
        catch (Exception ex)
        {
            jsonString = js.Serialize("error in setPOI --- " + ex.Message);
        }

        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //read the Myevent table
    public string ReadMyEvent(string UserEmail)
    {
        List<EventOnAir> MyeventsList = new List<EventOnAir>();
        DBservices dbs = new DBservices();
        User U = new User();
        U.Email = UserEmail;

        DataTable dtUserEvents = dbs.ReadMyEvent(U);

        for (int i = 0; i < dtUserEvents.Rows.Count; i++)
        {
            EventOnAir evTemp = new EventOnAir();
            evTemp.Point = new Point(double.Parse(dtUserEvents.Rows[i]["Lat"].ToString()), double.Parse(dtUserEvents.Rows[i]["Lng"].ToString()));
            evTemp.Address = dtUserEvents.Rows[i]["Address"].ToString();
            evTemp.MaxAge = int.Parse(dtUserEvents.Rows[i]["MaxAge"].ToString());
            evTemp.MinAge = int.Parse(dtUserEvents.Rows[i]["MinAge"].ToString());
            evTemp.NumOfParti = int.Parse(dtUserEvents.Rows[i]["NumOfParticipants"].ToString());
            evTemp.ImageUrl = dtUserEvents.Rows[i]["ImageUrl"].ToString();
            evTemp.AdminID = int.Parse(dtUserEvents.Rows[0]["AdminId"].ToString());
            evTemp.IsPrivate1 = bool.Parse(dtUserEvents.Rows[0]["Private"].ToString());
            evTemp.DateTime = DateTime.Parse(dtUserEvents.Rows[i]["Time"].ToString());
            evTemp.DateTimeStr = (dtUserEvents.Rows[i]["Time"].ToString());
            evTemp.Description = dtUserEvents.Rows[i]["Description"].ToString();
            evTemp.Comments = dtUserEvents.Rows[i]["Comments"].ToString();
            evTemp.EventNum = dtUserEvents.Rows[i]["EventNumber"].ToString();


            //add the  event to the list
            MyeventsList.Add(evTemp);
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(MyeventsList);
        return jsonString;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //add user---- log in
    public int Adduser(string UserName, string Password, string FirstName, string LastName, int Age, string City, string Email, string imageUrl)
    {
        User U1 = new User();
        U1.UserName = UserName;
        U1.UserPassword = Password;
        U1.Fname = FirstName;
        U1.Lname = LastName;
        U1.Age = Age;
        U1.City = City;
        if (Email=="undefined")
        {
            Email = FirstName + LastName + "2@gmail.com";
        }
        U1.Email = Email;
        U1.ImageUrl = imageUrl;

        int numEfect = U1.InsertNewUser();
        DataTable dt = U1.CheckPass();
        if (dt.Rows.Count != 0)
        {
            if (dt.Rows[0]["UserPassword"].ToString() == U1.UserPassword)
            {
                HttpContext.Current.Session["Fname"] = dt.Rows[0]["Fname"].ToString();
                HttpContext.Current.Session["UserDeatail"] = dt;
                HttpContext.Current.Session["UserId"] = dt.Rows[0]["UserId"].ToString();
            }
        }
        return numEfect;
    }

    //Log Out
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void LogOut()
    {
        HttpContext.Current.Session["Fname"] = null;
        HttpContext.Current.Session["UserDeatail"] = null;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //add user to event
    public string UserToEvent(string Email, string EventNum)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(" faild");
        try
        {
            User U1 = new User();
            U1.Email = Email;
            int num = U1.InsertToEvent(EventNum);
            if (num > 0)
            {
                jsonString = js.Serialize("Success");
            }
            else
            {
                jsonString = js.Serialize("Event is full!!!");
            }
        }
        catch (Exception ex)
        {
            jsonString = js.Serialize("error in treasure.Login --- " + ex.Message);
        }

        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //get one event for popup  in home
    public string getOneEvent(string eventNum)
    {
        EventOnAir ev = new EventOnAir();
        List<EventOnAir> eventsList = new List<EventOnAir>();
        DataTable dt = ev.readTable();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["EventNumber"].ToString() == eventNum)
            {
                EventOnAir evTemp = new EventOnAir();
                evTemp.Point = new Point(double.Parse(dt.Rows[i]["Lat"].ToString()), double.Parse(dt.Rows[i]["Lng"].ToString()));
                evTemp.Address = dt.Rows[i]["Address"].ToString();
                evTemp.MaxAge = int.Parse(dt.Rows[i]["MaxAge"].ToString());
                evTemp.MinAge = int.Parse(dt.Rows[i]["MinAge"].ToString());
                evTemp.NumOfParti = int.Parse(dt.Rows[i]["NumOfParticipants"].ToString());
                evTemp.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
                evTemp.AdminID = int.Parse(dt.Rows[i]["AdminId"].ToString());
                evTemp.IsPrivate1 = bool.Parse(dt.Rows[i]["Private"].ToString());
                evTemp.DateTime = DateTime.Parse(dt.Rows[i]["Time"].ToString());
                evTemp.DateTimeStr = (dt.Rows[i]["Time"].ToString());
                evTemp.FrequencyStr = (dt.Rows[i]["Frequncy"].ToString());
                evTemp.Description = dt.Rows[i]["Description"].ToString();
                evTemp.Comments = dt.Rows[i]["Comments"].ToString();
                evTemp.EventNum = dt.Rows[i]["EventNumber"].ToString();
                evTemp.NumOfRegis = dt.Rows[i]["NumOfRegister"].ToString();

                User u = new User();
                u.UserId = int.Parse(dt.Rows[i]["AdminId"].ToString());
                DataTable dtName = u.CheckUserName();
                if (dtName.Rows.Count == 1)
                {
                    evTemp.AdminFullName = dtName.Rows[0]["Fname"].ToString() + " " + dtName.Rows[0]["Lname"].ToString();
                }
                else
                {
                    evTemp.AdminFullName = "";
                }

                DataTable dtUS = evTemp.ReadUserInEvent(eventNum);

                for (int r = 0; r < dtUS.Rows.Count; r++)
                {
                    User utemp = new User();
                    utemp.UserName = dtUS.Rows[r]["UserName"].ToString();
                    utemp.UserId = int.Parse(dtUS.Rows[r]["UserId"].ToString());
                    utemp.Fname = dtUS.Rows[r]["Fname"].ToString();
                    utemp.Lname = dtUS.Rows[r]["Lname"].ToString();
                    utemp.Age = int.Parse(dtUS.Rows[r]["Age"].ToString());
                    utemp.Rating = int.Parse(dtUS.Rows[r]["Rating"].ToString());
                    utemp.City = dtUS.Rows[r]["City"].ToString();
                    utemp.ImageUrl = dtUS.Rows[r]["Picture"].ToString();
                    evTemp.PlayerUserList.Add(utemp);
                }

                eventsList.Add(evTemp);   //add the  event to the list

            }
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(eventsList);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //get one event for popup in my event
    public string getOneMyEvent(string eventNum, string UserEmail)
    {
        List<EventOnAir> eventsList = new List<EventOnAir>();
        DBservices dbs = new DBservices();
        User U = new User();
        U.Email = UserEmail;
        DataTable dtUserEvents = dbs.ReadMyEvent(U);

        for (int i = 0; i < dtUserEvents.Rows.Count; i++)
        {
            if (dtUserEvents.Rows[i]["EventNumber"].ToString() == eventNum)
            {
                EventOnAir evTemp = new EventOnAir();
                evTemp.Point = new Point(double.Parse(dtUserEvents.Rows[i]["Lat"].ToString()), double.Parse(dtUserEvents.Rows[i]["Lng"].ToString()));
                evTemp.Address = dtUserEvents.Rows[i]["Address"].ToString();
                evTemp.MaxAge = int.Parse(dtUserEvents.Rows[i]["MaxAge"].ToString());
                evTemp.MinAge = int.Parse(dtUserEvents.Rows[i]["MinAge"].ToString());
                evTemp.NumOfParti = int.Parse(dtUserEvents.Rows[i]["NumOfParticipants"].ToString());
                evTemp.ImageUrl = dtUserEvents.Rows[i]["ImageUrl"].ToString();
                evTemp.AdminID = int.Parse(dtUserEvents.Rows[i]["AdminId"].ToString());
                evTemp.IsPrivate1 = bool.Parse(dtUserEvents.Rows[i]["Private"].ToString());
                evTemp.DateTime = DateTime.Parse(dtUserEvents.Rows[i]["Time"].ToString());
                evTemp.DateTimeStr = (dtUserEvents.Rows[i]["Time"].ToString());
                evTemp.Description = dtUserEvents.Rows[i]["Description"].ToString();
                evTemp.FrequencyStr = dtUserEvents.Rows[i]["Frequency"].ToString();
                evTemp.Comments = dtUserEvents.Rows[i]["Comments"].ToString();
                evTemp.EventNum = dtUserEvents.Rows[i]["EventNumber"].ToString();
                evTemp.NumOfRegis = dtUserEvents.Rows[i]["NumOfRegister"].ToString();


                User u = new User();
                u.UserId = int.Parse(dtUserEvents.Rows[i]["AdminId"].ToString());
                DataTable dtName = u.CheckUserName();
                if (dtName.Rows.Count == 1)
                {
                    evTemp.AdminFullName = dtName.Rows[0]["Fname"].ToString() + " " + dtName.Rows[0]["Lname"].ToString();
                }
                else
                {
                    evTemp.AdminFullName = "";
                }

                DataTable dtUS = evTemp.ReadUserInEvent(eventNum);

                for (int r = 0; r < dtUS.Rows.Count; r++)
                {
                    User utemp = new User();
                    utemp.UserName = dtUS.Rows[r]["UserName"].ToString();
                    utemp.UserId = int.Parse(dtUS.Rows[r]["UserId"].ToString());
                    utemp.Fname = dtUS.Rows[r]["Fname"].ToString();
                    utemp.Lname = dtUS.Rows[r]["Lname"].ToString();
                    utemp.Age = int.Parse(dtUS.Rows[r]["Age"].ToString());
                    utemp.Rating = int.Parse(dtUS.Rows[r]["Rating"].ToString());
                    utemp.City = dtUS.Rows[r]["City"].ToString();
                    utemp.ImageUrl = dtUS.Rows[r]["Picture"].ToString();
                    evTemp.PlayerUserList.Add(utemp);
                }

                eventsList.Add(evTemp);   //add the  event to the list

            }
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(eventsList);
        return jsonString;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //rating Down
    public void RatingDown(string id)
    {

        User u = new User();
        u.UserId = int.Parse(id);
        u.RatingDown();

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //rating up
    public void RatingUp(string id)
    {
        User u = new User();
        u.UserId = int.Parse(id);
        u.RatingUp();

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //login old not facebook
    public string Login(string Email, string Password)
    {
        User u = new User();
        u.UserPassword = Password;
        u.Email = Email;
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize("Worng Email or Password ");
        try
        {
            DataTable dt = u.CheckPass();
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0]["UserPassword"].ToString() == u.UserPassword)
                { jsonString = js.Serialize("ok"); }
            }


        }
        catch (Exception ex)
        {
            jsonString = js.Serialize("error in treasure.Login --- " + ex.Message);
        }
        return jsonString;
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //add user---- log in mobile
    public string AdduserMobile(string UserName, string Password, string FirstName, string LastName, int Age, string City, string Email, string imageUrl)
    {
        User U1 = new User();
        U1.UserName = UserName;
        U1.UserPassword = Password;
        U1.Fname = FirstName;
        U1.Lname = LastName;
        U1.Age = Age;
        U1.City = City;
        U1.Email = Email;
        U1.ImageUrl = imageUrl;
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize("Wrong Email or Password ");

        int numEfect = U1.InsertNewUser();
        DataTable dtt = U1.CheckPass();
        try
        {
            if (dtt.Rows.Count != 0)
            {

                User u = new User();
                u.Fname = dtt.Rows[0]["Fname"].ToString();
                u.Email = dtt.Rows[0]["Email"].ToString();
                u.ImageUrl = dtt.Rows[0]["Picture"].ToString();
                u.UserName = dtt.Rows[0]["UserName"].ToString();
                u.UserId = int.Parse(dtt.Rows[0]["UserId"].ToString());
                jsonString = js.Serialize(u);

            }
        }
        catch (Exception ex)
        {
            jsonString = js.Serialize("Error in: " + ex.Message);
        }
        return jsonString;
    }

    //login mobile
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    //public string LoginMobile(string Email, string Password)
    //{
    //    User u = new User();
    //    u.UserPassword = Password;
    //    u.Email = Email;
    //    JavaScriptSerializer js = new JavaScriptSerializer();
    //    string jsonString = js.Serialize("Wrong Email or Password ");
    //    try
    //    {
    //        DataTable dt = u.CheckPass();
    //        if (dt.Rows.Count != 0)
    //        {

    //            User U1 = new User();
    //            U1.Fname = dt.Rows[0]["Fname"].ToString();
    //            U1.Email = dt.Rows[0]["Email"].ToString();
    //            U1.ImageUrl = dt.Rows[0]["Picture"].ToString();
    //            U1.UserName = dt.Rows[0]["UserName"].ToString();
    //            U1.UserId = int.Parse(dt.Rows[0]["UserId"].ToString());
    //            jsonString = js.Serialize(U1);

    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        jsonString = js.Serialize("Error in: " + ex.Message);
    //    }
    //    return jsonString;
    //}




    //[WebMethod(EnableSession = true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    ////add user---- log in mobile
    //public string AdduserMobile(string UserName, string Password, string FirstName, string LastName, int Age, string City, string Email, string imageUrl)
    //{
    //    User U1 = new User();
    //    U1.UserName = UserName;
    //    U1.UserPassword = Password;
    //    U1.Fname = FirstName;
    //    U1.Lname = LastName;
    //    U1.Age = Age;
    //    U1.City = City;
    //    U1.Email = Email;
    //    U1.ImageUrl = imageUrl;
    //    JavaScriptSerializer js = new JavaScriptSerializer();
    //    string jsonString = js.Serialize("Wrong Email or Password ");

    //    int numEfect = U1.InsertNewUser();
    //    DataTable dtt = U1.CheckPass();
    //    try
    //    {
    //        if (dtt.Rows.Count != 0)
    //        {

    //            User u = new User();
    //            u.Fname = dtt.Rows[0]["Fname"].ToString();
    //            u.Email = dtt.Rows[0]["Email"].ToString();
    //            u.ImageUrl = dtt.Rows[0]["Picture"].ToString();
    //            u.UserName = dtt.Rows[0]["UserName"].ToString();
    //            u.UserId = int.Parse(dtt.Rows[0]["UserId"].ToString());
    //            jsonString = js.Serialize(u);

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        jsonString = js.Serialize("Error in: " + ex.Message);
    //    }
    //    return jsonString;
    //}
}
