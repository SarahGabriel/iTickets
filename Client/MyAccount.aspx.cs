using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Client_MyAccount : System.Web.UI.Page
{
	//DALclient dalClient;
	Client client;

    protected void Page_Load(object sender, EventArgs e)
    {
		//dalClient = new DALclient();
		string clientMail;

		setLabelVisible();
		setTextBoxNotVisible();
		//LabelPwd.Visible = true;
		LabelMsg.Visible = false;

		if (Session["name"] != null && Session["eMail"] != null)
		{
            clientMail = Session["eMail"].ToString();

			client = BLclient.getClientDetails(clientMail);

			//Page.Title = Session["name"].ToString() + " " + BLclient.getClientLastName(Session["eMail"].ToString()) + " - My Account";
			//clientMail = Session["eMail"].ToString();

            Page.Title = client.FirstName + " " + client.LastName + " - My Account";

			fillAllLabels(client);
		}
		else
		{
			Response.Redirect("/Default.aspx");
		}

    }

	private void fillAllLabels(Client client)
	{
		LabelFirst.Text = client.FirstName;
		LabelLast.Text = client.LastName;
		LabelMail.Text = client.Mail;
		LabelPhone.Text = client.PhoneNumber;
		LabelAddress.Text = client.Address;
		//LabelPoint.Text = client.Points + "";
		LabelPoints.Text = client.Points + "";

		//LabelPwd.Text = dalClient.getClientPassword(client.Mail);
		
	}

	private void clearLabels()
	{
		LabelFirst.Text = "";
		LabelLast.Text = "";
		LabelMail.Text = "";
		LabelPhone.Text = "";
		LabelAddress.Text = "";
		LabelPoint.Text = "";
	}
	protected void EditDetailsButton_Click(object sender, EventArgs e)
	{
		clearLabels();

		setLabelNotVisible();
		setTextBoxVisible();

		LabelMail.Visible = true;
		LabelMail.Text = client.Mail;
		LabelPoint.Text = client.Points + "";
		LabelPoint.Visible = true;

		TextBoxFirst.Text = client.FirstName;
		
		TextBoxLast.Text = client.LastName;
		
		TextBoxAddress.Text = client.Address;
		
		TextBoxPhone.Text = client.PhoneNumber;

		EditDetailsButton.Visible = false;
		SaveDetailsButton.Visible = true;
		CancelDetailButton.Visible = true;

	}
	protected void SaveDetailsButton_Click(object sender, EventArgs e)
	{
		Client newClient;
		string first = "", last = "", address = "", phone = "";

        first = TextBoxFirst.Text.Trim();
        last = TextBoxLast.Text.Trim();
        address = TextBoxAddress.Text.Trim();
        phone = TextBoxPhone.Text.Trim();

        if(first.Equals("") || last.Equals("") || address.Equals("") || phone.Equals(""))
        {
            LabelMsg.Visible = true;
            LabelMsg.ForeColor = System.Drawing.ColorTranslator.FromHtml("red");
            LabelMsg.Text = "Please fill all the fields.";

            EditDetailsButton.Visible = true;
            SaveDetailsButton.Visible = false;
            CancelDetailButton.Visible = false;

            return;
        }

        if (!BLfunctions.isStringValid(first) || !BLfunctions.isStringValid(last))//check if a only letters
        {
            LabelMsg.Visible = true;
            LabelMsg.ForeColor = System.Drawing.ColorTranslator.FromHtml("red");
            LabelMsg.Text = "Name must contain only letters.";

            EditDetailsButton.Visible = true;
            SaveDetailsButton.Visible = false;
            CancelDetailButton.Visible = false;

            return;
        }

        if (!BLfunctions.isPhoneValid(phone))
        {
            LabelMsg.Visible = true;
            LabelMsg.ForeColor = System.Drawing.ColorTranslator.FromHtml("red");
            LabelMsg.Text = "Phone must contain only numbers.";

            EditDetailsButton.Visible = true;
            SaveDetailsButton.Visible = false;
            CancelDetailButton.Visible = false;

            return;
        }

		newClient = new Client(first, last, client.Mail, phone, address, client.Points);

		if (BLclient.updateClientDetails(client.Mail, newClient))
		{
			LabelMsg.Visible = true;
			LabelMsg.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
			LabelMsg.Text = "Your account has been modified successfully";

			SaveDetailsButton.Visible = false;
			EditDetailsButton.Visible = true;

			client = BLclient.getClientDetails(client.Mail);

			fillAllLabels(client);
            Session["name"] = first;
            Response.Redirect(Request.RawUrl);
		}
		else
		{
			LabelMsg.Visible = true;
			LabelMsg.ForeColor = System.Drawing.ColorTranslator.FromHtml("red");
			LabelMsg.Text = "Please fill all the fields.";
		}

	}

	protected void CancelDetailsButton_Click(object sender, EventArgs e)
	{
		SaveDetailsButton.Visible = false;
		EditDetailsButton.Visible = true;
		CancelDetailButton.Visible = false;

		fillAllLabels(client);
	}

	protected void setLabelVisible()
	{
		LabelFirst.Visible = true;
		LabelLast.Visible = true;
		LabelPhone.Visible = true;
		LabelAddress.Visible = true;
		LabelPwd.Visible = true;
	}

	protected void setTextBoxVisible()
	{
		TextBoxFirst.Visible = true;
		TextBoxLast.Visible = true;
		TextBoxAddress.Visible = true;
		TextBoxPhone.Visible = true;

	}

	protected void setLabelNotVisible()
	{
		LabelFirst.Visible = false;
		LabelLast.Visible = false;
		LabelPhone.Visible = false;
		LabelAddress.Visible = false;
	}

	protected void setTextBoxNotVisible()
	{
		TextBoxFirst.Visible = false;
		TextBoxLast.Visible = false;
		TextBoxAddress.Visible = false;
		TextBoxPhone.Visible = false;
	}

	protected void setLabelsPwdNotVisible()
	{
		LabelOldPwd1.Visible = false;
		LabelOldPwd2.Visible = true;

		LabelNewPwd1.Visible = true;
		LabelNewPwd2.Visible = true;

		LabelPwd.Visible = false;

		TextBoxOldPwd.Visible = true;
		TextBoxNewPwd1.Visible = true;
		TextBoxNewPwd2.Visible = true;

		SavePwdButton.Visible = true;
		EditPwdButton.Visible = false;
	}

	protected void setLabelsPwdVisible()
	{
		LabelOldPwd1.Visible = true;
		LabelOldPwd2.Visible = false;

		LabelNewPwd1.Visible = false;
		LabelNewPwd2.Visible = false;

		LabelPwd.Visible = true;

		TextBoxOldPwd.Visible = false;
		TextBoxNewPwd1.Visible = false;
		TextBoxNewPwd2.Visible = false;

		SavePwdButton.Visible = false;
		EditPwdButton.Visible = true;
	}
	protected void EditPasswordButton_Click(object sender, EventArgs e)
	{
		setLabelsPwdNotVisible();
		CancelButton.Visible = true;
	}
	protected void SavePasswordButton_Click(object sender, EventArgs e)
	{
		string oldPwd = TextBoxOldPwd.Text;
		string newPwd1 = TextBoxNewPwd1.Text;
		string newPwd2 = TextBoxNewPwd2.Text;
		//string newPwd = null;

		/* user didn't fill all the fields */
		if (oldPwd.Length == 0 || newPwd1.Length == 0 || newPwd2.Length == 0)
		{
			errorMessage("Fill all the fields");			
			return;
		}

		if (!newPwd1.Equals(newPwd2))
		{
			errorMessage("The passwords must match");
			return;
		}
		if (!BLclient.checkPwd(newPwd1))
		{
			errorMessage("Password cannot be less than " + BLclient.PWD_LENGTH + " characters!");
			return;
		}
		if (BLclient.changePassword(client.Mail, oldPwd, newPwd1))
		{
			setLabelsPwdVisible();
			LabelMsgPwd.Visible = true;
			LabelMsg.Text = "*********";
			LabelMsgPwd.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
			LabelMsgPwd.Text = "Password changed successfully";
		}
		else
		{
			errorMessage("An error occured. Please re-enter the passwords");			
			return;
		}
		CancelButton.Visible = false;
	}

	private void errorMessage(string message)
	{
		LabelMsgPwd.Visible = true;
		LabelMsg.Text = "";
		setLabelsPwdNotVisible();
		LabelMsgPwd.ForeColor = System.Drawing.ColorTranslator.FromHtml("red");
		LabelMsgPwd.Text = message;
		clearTextBoxes();
	}

	private void clearTextBoxes()
	{
		TextBoxOldPwd.Text = "";
		TextBoxNewPwd1.Text = "";
		TextBoxNewPwd2.Text = "";
	}
	protected void LearnMoreButton_Click(object sender, EventArgs e)
	{
		Response.Redirect("/Client/MyPoints.aspx");
	}
	protected void CancelButton_Click(object sender, EventArgs e)
	{
		setLabelsPwdVisible();
		LabelMsg.Text = "*********";
        LabelMsgPwd.Text = "";
		CancelButton.Visible = false;
	}

	protected void YesPopupButton_Click(object sender, EventArgs e)
	{

	}	
}