using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Client_ViewPurchases : System.Web.UI.Page
{
	protected Client client;
    protected List<Purchase> purchase;
	//DALclient dalClient;
	//DALpurchase dalPurchase;
	//DALshow dalShow;

    protected int PurchaseId;

	protected void Page_Load(object sender, EventArgs e)
	{
		//dalClient = new DALclient();
		//dalShow = new DALshow();

		string clientMail;
		LabelIsDeleted.Visible = false;

		if (Session["name"] != null && Session["eMail"] != null)
		{
			clientMail = Session["eMail"].ToString();
			client = BLclient.getClientDetails(clientMail);

			Page.Title = client.FirstName + " " + client.LastName + " - Purchases";

			Label2.Text = "next";		// show the purchases from today

			NextButton.Visible = false;
			AllButton.Visible = true;
			PreviousButton.Visible = true;
		}
        else
        {
            Response.Redirect("/Default.aspx");
        }
	}

	
	protected void NextButton_Click(object sender, EventArgs e)
	{
		//Label2.Visible = true;
		NextButton.Visible = false;
		AllButton.Visible = true;
		PreviousButton.Visible = true;
		string str = "";
		if (client != null)
		{
			purchase = BLclient.getClientPurchaseFrom(client.Mail, DateTime.Today);
			if (purchase != null)
			{
				foreach (Purchase p in purchase)
				{
					str += p.ShowId + " ";
				}
				Label2.Text = "next";
			}
			else
			{
				Label2.Text = "NEXT";
			}
		}
		else
		{
			Label2.Text = "client error next";

		}

	}
	protected void AllButton_Click(object sender, EventArgs e)
	{
		//Label2.Visible = true;
		NextButton.Visible = true;
		AllButton.Visible = false;
		PreviousButton.Visible = true;

		string str = "";
		if (client != null)
		{
			purchase = BLclient.getClientsPurchases(client.Mail);
			if (purchase != null)
			{
				foreach (Purchase p in purchase)
				{
					str += p.ShowId + " ";
				}
				Label2.Text = "all";
			}
			else
			{
				Label2.Text = "ALL";
			}
		}
		else
		{
			Label2.Text = "client error all";

		}
	}
	protected void PreviousButton_Click(object sender, EventArgs e)
	{
		//Label2.Visible = true;
		NextButton.Visible = true;
		AllButton.Visible = true;
		PreviousButton.Visible = false;

		string str = "";
		if (client != null)
		{
			purchase = BLclient.getClientPurchaseUp(client.Mail, DateTime.Today);
			if (purchase != null)
			{
				foreach (Purchase p in purchase)
				{
					str += p.ShowId + " ";
				}
				Label2.Text = "prev";
			}
			else
			{
				Label2.Text = "PREV";
			}
		}
		else
		{
			Label2.Text = "client error prev";

		}
	}

	protected void YesOldPopupButton_Click(object sender, EventArgs e)
	{
		

		purchase = BLclient.getClientsPurchases(client.Mail);

		foreach (Purchase p in purchase)
		{
			if (LabelName.Text.Equals(p.Id + ""))
			{
                if (BLpurchase.deleteOldPurchase(p.Id))
				{
					LabelIsDeleted.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
					LabelIsDeleted.Visible = true;
					LabelIsDeleted.Text = "Your purchase has been deleted successfully";
				}
				else
				{
					LabelIsDeleted.ForeColor = System.Drawing.ColorTranslator.FromHtml("red");
					LabelIsDeleted.Visible = true;
					LabelIsDeleted.Text = "An error occured in deleting your purchase. Please retry.";
				}
			}
		}
	}
}