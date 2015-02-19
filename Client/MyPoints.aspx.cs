using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Client_MyPoints : System.Web.UI.Page
{
	protected Client client;
    protected List<Schedule> schedules;
	//DALclient dalClient;

    protected void Page_Load(object sender, EventArgs e)
    {
		//dalClient = new DALclient();

		if (Session["name"] != null && Session["eMail"] != null)
		{
			client = BLclient.getClientDetails(Session["eMail"].ToString());
			LabelPoint.Text = client.Points + "";
            schedules = BLclient.getScheduleByPoints(client.Points);
		}
		else
		{
			Response.Redirect("/Default.aspx");
		}
    }
}