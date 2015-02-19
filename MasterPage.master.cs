using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["isAdmin"] != null)
        {
            ShowAdminNav();
        }
        else if (Session["name"] != null)
        {
            ShowClientNav();
        }
        else
        {
            ShowGuestNav();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //DALclient dal = new DALclient();
        Client client = BLclient.clientLogIn(UserNameTextBox.Text, PasswordTextBox.Text);

		if (client != null)
		{
			if (client.Mail.Equals("admin@itickets.com"))
			{
				Session.Add("isAdmin", "yes");
			}

			Session.Add("name", client.FirstName);
			Session.Add("eMail", client.Mail);

			//Response.Redirect("/Default.aspx");
            Response.Redirect(Request.RawUrl);
		}
		else
		{
			Label1.Text = "User does not exist! Please Sign Up";
		}
    }

    protected void LogoutClick(object sender, EventArgs e)
    {
        if (Session["name"] != null && Session["eMail"]!=null)
        {
            Session.Clear();
        }

        Response.Redirect("/Default.aspx");
    }

    protected void LoginClick(object sender, EventArgs e)
    {
        
    }

    private void ShowGuestNav()
    {
        logoutButton.Visible = false;
        loginButton.Visible = true;
        signUpButton.Visible = true;
        adminMenu.Visible = false;
        userNameLogin.Text = "Hello Guest";
    }

    private void ShowClientNav()
    {
        logoutButton.Visible = true;
        loginButton.Visible = false;
        signUpButton.Visible = false;
        adminMenu.Visible = false;
        userNameLogin.Text = "<span class='glyphicon glyphicon-user'></span> Hello " + Session["name"] + " <span class='caret'></span>";
    }

    private void ShowAdminNav()
    {
        logoutButton.Visible = true;
        loginButton.Visible = false;
        signUpButton.Visible = false;
        adminMenu.Visible = true;
        userNameLogin.Text = "Hello Admin <span class='caret'></span>";
    }
   protected void SearchButton_Click(object sender, EventArgs e)
    {
        if (SearchTextBox.Text.Equals(""))
        {
            Response.Redirect("/SearchBar.aspx");
        }
        else
        {
            Response.Redirect("/SearchBar.aspx?findName=" + SearchTextBox.Text.Trim() + "&category=Name");
        }
    }
}