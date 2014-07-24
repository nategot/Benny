using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    private int userId;
    private string email;
    private string lname;
    private string imageUrl;
    private string city;
    private int age;
    private int rating=100;
    private string userName;
    private string fname;
    private string userPassword;
   
    #region //prop
    public int UserId
    {
        get { return userId; }
        set { userId = value; }
    }
   

    public int Age
    {
        get { return age; }
        set { age = value; }
    }
    

    public int Rating
    {
        get { return rating; }
        set { rating = value; }
    }

    public string UserName
    {
        get { return userName; }
        set { userName = value; }
    }
   

    public string Fname
    {
        get { return fname; }
        set { fname = value; }
    }
  
    public string Lname
    {
        get { return lname; }
        set { lname = value; }
    }
   
    public string Email
    {
        get { return email; }
        set { email = value; }
    }
    

    public string UserPassword
    {
        get { return userPassword; }
        set { userPassword = value; }
    }
  

    public string City
    {
        get { return city; }
        set { city = value; }
    }
    
    public string ImageUrl
    {
        get { return imageUrl; }
        set { imageUrl = value; }
    }


#endregion
    public int InsertNewUser()
    {
        DBservices dbs = new DBservices();
        int numAffected = dbs.insert(this);
        return numAffected;
    }

    public DataTable  CheckPass()
    {
        DBservices dbs = new DBservices();
        return dbs.CheckPassword(this);
    }

    public DataTable CheckUserName()
    {
        DBservices dbs = new DBservices();
        return dbs.CheckUserName(this);
    }
    //delete user from event
    public int deleteUserFromEvent(string eventnum)
    {
        DBservices dbs = new DBservices();
        return dbs.deleteUserFromEvent(this,eventnum);
    }
    
    //insert user to event
    public int InsertToEvent(string eventnum)
    {
        DBservices dbs = new DBservices();
        return dbs.InsertToEvent(this,eventnum);
    }


    //read the Myevent table
    public DataTable ReadMyEvent()
    {
        DBservices dbs = new DBservices();
        return dbs.ReadMyEvent(this);
    }

    //rating down 
    public int RatingDown()
    {
        DBservices dbs = new DBservices();
        return dbs.RatingDown(this);
    }
    public int RatingUp()
    {
        DBservices dbs = new DBservices();
        return dbs.RatingUp(this);
    }


    public void BulidGroup(List<string> emailListe, List<string> FnameList, List<string> LnameList, List<string> UrlList, string groupname)
    {
        DBservices db = new DBservices();

        for (int i = 0; i < emailListe.Count; i++)
			{
                db.InsertToGroup(emailListe[i], FnameList[i], LnameList[i], UrlList[i], groupname, this.UserId);
			}
       
    }
    
}