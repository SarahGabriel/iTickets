using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class Registration : System.Web.UI.Page
{
    //private DALclient dal;
    private const int PWD_LENGTH = BLclient.PWD_LENGTH;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["eMail"] != null)
            Response.Redirect("/Client/MyAccount.aspx");

        //dal = new DALclient();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        clearLabels();

        bool isOkay = false;

        checkTextBox();
        checkMail();
        checkPwd();

        isOkay = checkTextBox() && checkMail() && checkPwd();

        if (!isOkay)
        {
            return;
        }

        Client client = new Client(FirstNameTextBox.Text.Trim(), LastNameTextBox.Text.Trim(), MailTextBox.Text.Trim(), PhoneNumberTextBox.Text.Trim(), AddressTextBox.Text.Trim(), 0);

        if (BLclient.isClientExists(client))
        {
            ExistLabel.Text = "User already exists!";
        }
        else
        {
            BLclient.addNewClient(client, PasswordTextBox.Text);
            Session.Add("eMail", client.Mail);
            Session.Add("name", client.FirstName);
            Response.Redirect("/Default.aspx");

        }
    }


    private bool checkPwd()
    {
        string pwd1, pwd2;
        pwd1 = PasswordTextBox.Text;
        pwd2 = RePasswordTextBox.Text;
        if (!pwd1.Equals(pwd2))
        {
            Label7.Text = "Passwords do not match!";
            return false;
        }
        if (pwd1.Length < PWD_LENGTH && pwd1.Length > 0)
        {
            Label6.Text = "Password cannot be less than " + PWD_LENGTH + " characters !";
            return false;
        }
        return true;
    }

    private bool checkMail()
    {
        string mail = MailTextBox.Text;
        RegexUtilities util = new RegexUtilities();
        if (BLclient.isClientExists(mail))
        {
            ExistLabel.Text = "User already exists!";
            return false;
        }
        else if (util.IsValidEmail(mail))
        {
            return true;
        }
        else
        {
            Label3.Text = "Invalid mail address!";
            return false;
        }
    }
    private bool checkTextBox()
    {	// flase if error
        // true if good
        string errorMsg = "Please fill this field!";
        bool check = false;

        if (FirstNameTextBox.Text.Trim().Equals(""))
        {
            Label1.Text = errorMsg;
            check = true;
        }
        else if (!BLfunctions.isStringValid(FirstNameTextBox.Text.Trim()))//check if a only letters
        {

            Label1.Text = "Name must contain only letters!";
            check = true;

        }
        if (LastNameTextBox.Text.Trim().Equals(""))
        {
            Label2.Text = errorMsg;
            check = true;
        }
        else if (!BLfunctions.isStringValid(LastNameTextBox.Text.Trim()))//check if a only letters
        {

            Label2.Text = "Name must contain only letters!";
            check = true;

        }
        if (MailTextBox.Text.Trim().Equals(""))
        {
            Label3.Text = errorMsg;
            check = true;
        }
        if (PhoneNumberTextBox.Text.Trim().Equals(""))
        {
            Label4.Text = errorMsg;
            check = true;
        }
        else if (!BLfunctions.isPhoneValid(PhoneNumberTextBox.Text.Trim()))//check if a valid phone number
        {
            Label4.Text = "Phone must contain only numbers!";
            check = true;

        }
        if (AddressTextBox.Text.Trim().Equals(""))
        {
            Label5.Text = errorMsg;
            check = true;
        }
        if (PasswordTextBox.Text.Trim().Equals(""))
        {
            Label6.Text = errorMsg;
            check = true;
        }
        if (RePasswordTextBox.Text.Trim().Equals(""))
        {
            Label7.Text = errorMsg;
            check = true;
        }
        if (check)
        {
            return false;
        }
        return true;
    }
    private void clearLabels()
    {
        Label1.Text = "";
        Label2.Text = "";
        Label3.Text = "";
        Label4.Text = "";
        Label5.Text = "";
        Label6.Text = "";
        Label7.Text = "";
    }


    /** Check mail */

    public class RegexUtilities
    {
        bool invalid = false;

        public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names. 
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format. 
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}