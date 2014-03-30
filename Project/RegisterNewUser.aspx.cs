using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class RegisterNewUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         
    }
 
    protected void CreateUserButton_Click(object sender, EventArgs e)
    {
        User U1 = new User();
        U1.UserName = UserName.Text;
        U1.UserPassword = Password.Text;
        U1.Fname = FirstName.Text;
        U1.Lname = LastName.Text;
        U1.Age = int.Parse(Age.Text);
        U1.City = City.Text;
        U1.Email = Email.Text;
        //שמירת תמונה?
        string path2 = Server.MapPath("." + "/Images/");
        string fname2 = path2 + imageFileUp.FileName;
        imageFileUp.SaveAs(fname2);
        U1.ImageUrl = @"..\Images\" + imageFileUp.FileName;
      

        int numEfect = U1.InsertNewUser();
        if (numEfect == 0)
        {
            MessageBox.Show("The Email is already taken ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        else
        {
            MessageBox.Show("the User was added Successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Response.Redirect("Project/Home.aspx");
        }
    }
}