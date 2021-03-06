﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;




/// <summary>
/// Summary description for DBservices
/// </summary>
public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;
    public string conectionStr = "bgroup14_test1ConnectionString";

    public DBservices()
    {
    }

    public SqlConnection connect(String conString)
    {
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = CommandSTR;
        cmd.CommandTimeout = 10;
        cmd.CommandType = System.Data.CommandType.Text;
        return cmd;
    }


    //insert event to DB
    public int insert(EventOnAir p)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect(conectionStr);
        }
        catch (Exception )
        {
            return 0; 
            
        }

        String cStr = BuildInsertCommand(p);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            MessageBox.Show("the Event wasnt added"+ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    // update event
    public int update(EventOnAir p)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect(conectionStr);
        }
        catch (Exception)
        {
            return 0;

        }

        String cStr = BuildupdateCommand(p);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            MessageBox.Show("the Event wasnt added" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //build insert command for event
    private String BuildInsertCommand(EventOnAir p)
    {
        String command;
        int isprivate = 0;
        string dateStr = " ";
        StringBuilder sb = new StringBuilder();
        if (p.IsPrivate1)
            isprivate = 1;

        dateStr += p.DateTime.Month.ToString() + "/" + p.DateTime.Day.ToString() + "/" + p.DateTime.Year.ToString() + " " + p.DateTime.Hour.ToString() + ":" + p.DateTime.Minute.ToString() + "0:00";


        sb.AppendFormat("Values({0}, {1} ,{2}, {3},'{4}',{5},{6},'{7}',{8},'{9}','{10}','{11}',0)", p.NumOfParti, p.Catedory, p.Frequency, isprivate, dateStr, p.MinAge, p.MaxAge, p.Comments, p.AdminID, p.Address, p.Point.Lat, p.Point.Lng);
        String prefix = "INSERT INTO EventsOnAir " + "( NumOfParticipants, CategoryId, FrequencyId, [Private],[Time],MinAge,MaxAge,Comments,AdminId,Address,Lat,Lng,NumOfRegister)";
        command = prefix + sb.ToString();

        return command;
    }

    //build insert update for event
    private String BuildupdateCommand(EventOnAir p)
    {
        int isprivate = 0;
        string dateStr = "";
        if (p.IsPrivate1)
            isprivate = 1;
        dateStr += p.DateTime.Month.ToString() + "/" + p.DateTime.Day.ToString() + "/" + p.DateTime.Year.ToString() + " " + p.DateTime.Hour.ToString() + ":" + p.DateTime.Minute.ToString() + "0:00";
        String command;
        StringBuilder sb = new StringBuilder();
        String com = "UPDATE EventsOnAir SET NumOfParticipants=" + p.NumOfParti + ",CategoryId=" + p.Catedory + ",FrequencyId=" + p.Frequency + ", Private=" + isprivate + ", Time='" + dateStr + "',MinAge=" + p.MinAge + ",MaxAge=" + p.MaxAge + ",Comments='" + p.Comments + "',AdminId=" + p.AdminID + ",Address='" + p.Address + "',Lat=" + p.Point.Lat + ",Lng=" + p.Point.Lng + "WHERE EventNumber=" + p.EventNum;
        command = com;

        return command;
    }
    //insert user to useres Table
    public int insert(User u)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect(conectionStr);
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        String cStr = BuildInsertCommand(u);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
        

    }

    //bulid insert comand for NewUser to Users table
    private String BuildInsertCommand(User u)//build insert command for User
    {
        String command;
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Values('{0}', '{1}' ,'{2}', '{3}','{4}',{5},'{6}','{7}',{8})", u.UserName, u.Fname, u.Lname, u.Email, u.UserPassword, u.Age, u.City, u.ImageUrl, u.Rating);
        String prefix = "INSERT INTO Users " + "(UserName, Fname, Lname, Email, UserPassword,[Age],[City],[Picture],[Rating])";

        command = prefix + sb.ToString();

        return command;
    }


    //insert user to Event
    public int InsertToEvent(User u, string eventNum)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect(conectionStr);
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        String cStr = BuildInsertCommand(u, eventNum);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        } 
        catch (SqlException ex)
        {
            return -1;
           
            throw (ex);
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }
       

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //bulid insert commad for user to event
    private String BuildInsertCommand(User u, string eventNum)//build insert command for User
    {
        String command;
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Values({0},'{1}')", eventNum, u.Email);
        String prefix = "INSERT INTO UsersInEvent " + "(EventNumber,Email )";

        command = prefix + sb.ToString();

        return command;
    }

    // Read from the DB into a table (Mtevent)
    public DataTable ReadMyEvent(User u)
    {
        SqlConnection con;
        con = connect(conectionStr);
        DataSet tblGetAdminName = new DataSet();
        SqlDataAdapter adpt1;

        SqlCommand MySPCommand = new SqlCommand("GetMyEvents", con);
        MySPCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter parEmail = new SqlParameter("@userEmail", SqlDbType.NChar);
        parEmail.Value = (u.Email);
        parEmail.Direction = ParameterDirection.Input;
        MySPCommand.Parameters.Add(parEmail);

        adpt1 = new SqlDataAdapter(MySPCommand);

        adpt1.Fill(tblGetAdminName, "T2");
        con.Close();
        return tblGetAdminName.Tables["T2"];

    }


    // Read from the DB into a table (home-event)
    public DBservices ReadFromDataBase(string conString, string tableName)
    {

        DBservices dbS = new DBservices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database/

            String selectStr = "SELECT  [imageUrl], [Description], [NumOfParticipants], [Time], [Frequncy],[Address],[MinAge], [MaxAge],[probability],[EventNumber], [Comments],[Private],[AdminId] ,[Lat] , [Lng], [NumOfRegister] FROM [View_EventsOnAir] ORDER BY Time"; // create the select that will be used by the adapter to select data from the DB

            SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds);                        // Fill the datatable (in the dataset), using the Select command
            DataTable dt = ds.Tables[0];

            // add the datatable and the dataa adapter to the dbS helper class in order to be able to save it to a Session Object
            dbS.dt = dt;
            dbS.da = da;


            return dbS;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    // Read from the DB into a table UserInEVent
    public DBservices ReadUserInEvent(string conString, string eventNm)
    {

        DBservices dbS = new DBservices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database/

            String selectStr = "SELECT  [UserName] ,[Fname] , [Lname] , [Age] ,[Rating] ,[City],[UserId],[Picture] FROM [View_UserInEvent] WHERE EventNumber =" + eventNm; // create the select that will be used by the adapter to select data from the DB
    
            SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds);                        // Fill the datatable (in the dataset), using the Select command
            DataTable dt = ds.Tables[0];

            // add the datatable and the dataa adapter to the dbS helper class in order to be able to save it to a Session Object
            dbS.dt = dt;
            dbS.da = da;


            return dbS;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }


    // Procedures

    //check password
    public DataTable CheckPassword(User u)
    {
        SqlConnection con;
        con = connect("bgroup14_test1ConnectionString");
        DataSet tblpassword = new DataSet();
        SqlDataAdapter adpt1;

        SqlCommand MySPCommand = new SqlCommand("GetUserPassword", con);
        MySPCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter parEmail = new SqlParameter("@Email", SqlDbType.VarChar, 50);
        parEmail.Value = (u.Email);
        parEmail.Direction = ParameterDirection.Input;
        MySPCommand.Parameters.Add(parEmail);

        adpt1 = new SqlDataAdapter(MySPCommand);

        adpt1.Fill(tblpassword, "T1");
        con.Close();
        return tblpassword.Tables["T1"];

    }

    //check admin name
    public DataTable CheckUserName(User u)
    {
        SqlConnection con;
        con = connect(conectionStr);
        DataSet tblGetAdminName = new DataSet();
        SqlDataAdapter adpt1;

        SqlCommand MySPCommand = new SqlCommand("GetAdminName", con);
        MySPCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter parEmail = new SqlParameter("@AdminId", SqlDbType.Int);
        parEmail.Value = (u.UserId);
        parEmail.Direction = ParameterDirection.Input;
        MySPCommand.Parameters.Add(parEmail);

        adpt1 = new SqlDataAdapter(MySPCommand);

        adpt1.Fill(tblGetAdminName, "T2");
        con.Close();
        return tblGetAdminName.Tables["T2"];

    }
    
    //delete user from event
    public int deleteUserFromEvent(User u,string eventnum)
    {
        SqlConnection con;
        con = connect(conectionStr);
        DataSet tblGetAdminName = new DataSet();
        SqlDataAdapter adpt1;
        
        SqlCommand MySPCommand = new SqlCommand("deleteUserFromEvent", con);
        MySPCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter parEventN = new SqlParameter("@EventNumber", SqlDbType.Int);
        parEventN.Value = eventnum;
        parEventN.Direction = ParameterDirection.Input;
        MySPCommand.Parameters.Add(parEventN);

        SqlParameter parEmail = new SqlParameter("@Email", SqlDbType.VarChar);
        parEmail.Value = u.Email;
        parEmail.Direction = ParameterDirection.Input;
        MySPCommand.Parameters.Add(parEmail);

        adpt1 = new SqlDataAdapter(MySPCommand);
        adpt1.Fill(tblGetAdminName, "T2");
        con.Close();


        return 1;

    }


    //RatingDown
    public int RatingDown(User u)
    {
        SqlConnection con;
        con = connect(conectionStr);
        DataSet tblGetAdminName = new DataSet();
        SqlDataAdapter adpt1;

        SqlCommand MySPCommand = new SqlCommand("RatingDown", con);
        MySPCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter parEventN = new SqlParameter("@UserID", SqlDbType.Int);
        parEventN.Value = u.UserId;
        parEventN.Direction = ParameterDirection.Input;
        MySPCommand.Parameters.Add(parEventN);


        adpt1 = new SqlDataAdapter(MySPCommand);
        adpt1.Fill(tblGetAdminName, "T2");
        con.Close();


        return 1;

    }
    //RatingUp
    public int RatingUp(User u)
    {
        SqlConnection con;
        con = connect(conectionStr);
        DataSet tblGetAdminName = new DataSet();
        SqlDataAdapter adpt1;

        SqlCommand MySPCommand = new SqlCommand("RatingUp", con);
        MySPCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter parEventN = new SqlParameter("@UserID", SqlDbType.Int);
        parEventN.Value = u.UserId;
        parEventN.Direction = ParameterDirection.Input;
        MySPCommand.Parameters.Add(parEventN);


        adpt1 = new SqlDataAdapter(MySPCommand);
        adpt1.Fill(tblGetAdminName, "T2");
        con.Close();


        return 1;

    }

    //GetRating get the sum of rating
    public int GetRating(EventOnAir e)
    {
        SqlConnection con;
        con = connect(conectionStr);
        DataSet tblGetAdminName = new DataSet();
        SqlDataAdapter adpt1;

        SqlCommand MySPCommand = new SqlCommand("GetRating", con);
        MySPCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter parEventN = new SqlParameter("@EventNum", SqlDbType.Int);
        parEventN.Value = e.EventNum;
        parEventN.Direction = ParameterDirection.Input;
        MySPCommand.Parameters.Add(parEventN);


        adpt1 = new SqlDataAdapter(MySPCommand);
        adpt1.Fill(tblGetAdminName, "T2");
        con.Close();
        DataTable TTemp = tblGetAdminName.Tables["T2"];

        return int.Parse(TTemp.Rows[0][0].ToString());

    }

    //get all user list
    public DataTable GetAllUsers()
    {
        SqlConnection con;
        con = connect(conectionStr);
        da = new SqlDataAdapter("select Picture ,Fname,Lname,Email from dbo.Users",con);
        DataTable UserT = new DataTable();
        da.Fill(UserT);
        return UserT;
        
    }

    //insert email to group
    public int InsertToGroup(string email,string fname,string Lname, string ImageUrl,string groupname,int userid)
    {
        SqlConnection con;
        con = connect(conectionStr);

        SqlCommand command = new SqlCommand("insert into Groups values(" + userid + ",'" + groupname + "','" + email + "','" + fname + "','" + Lname + "','" + ImageUrl + "')", con);
        return command.ExecuteNonQuery();
      

    }

    

  
}
