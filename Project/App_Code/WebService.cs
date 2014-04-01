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

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
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

       [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public string setPOI(double lat, double lng, int nop, int category, bool type, string freuncy,int maxAge,int minAge,string adrees,string time,string comments)
    {
        EventOnAir ev = new EventOnAir();
        ev.Point = new Point(lat,lng);
        ev.Address = adrees;
        ev.MaxAge = maxAge;
        ev.MinAge = minAge;
        ev.NumOfParti = nop;
        ev.Catedory = category;
        ev.IsPrivate1 = bool.Parse(type.ToString());
        ev.DateTimeStr = time;
        ev.Address = adrees;
        ev.Comments = comments;
        //ev.AdminID = int.Parse(dt.Rows[0]["AdminId"].ToString());

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize("ok");
        try
        {
            ev.insert();
            jsonString = js.Serialize("ok");
        }
        catch (Exception ex)
        {
            jsonString = js.Serialize("error in treasure.setPOI --- " + ex.Message);
        }

        return jsonString;
    }

}
