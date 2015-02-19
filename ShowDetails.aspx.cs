using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteDetails : System.Web.UI.Page
{
    //protected static DALshow dalShow;
    protected static Show show;
    protected static Schedule selectedSchedule;
    private static List<Schedule> showSchedule;
    protected static int price, points, ticketsQuantity;

    protected void Page_Load(object sender, EventArgs e)
    {
        //dalShow = new DALshow();

        string showParam = Request.QueryString["show"];
        int showId = 0;

        if (showParam == null)
            Response.Redirect("/Default.aspx");

        else if (!int.TryParse(showParam, out showId))
        {
            Response.Redirect("/Default.aspx");
        }

        show = BLshow.getShowsById(showId);

        if (show == null)
            Response.Redirect("/Default.aspx");

        showSchedule = BLshow.getShowSchedules(show.Id, DateTime.Today);

        //if no schedule
        if(showSchedule == null || showSchedule.Count == 0)
        {
            Response.Redirect("/Default.aspx");
        }

        if(!this.IsPostBack)
        {
            //show dates
            ShowDates.Items.Clear();
            foreach (Schedule schedule in showSchedule)
            {
                string showDate = schedule.Date.ToString("dd-MM-yyyy");
                string showTime = schedule.Time.ToString("HH:mm");
                string itemText = schedule.Location + " " + showDate + " " + showTime;
                ListItem item = new ListItem(itemText);
                ShowDates.Items.Add(item);
            }

            selectedSchedule = showSchedule[0];
            ShowDates.SelectedIndex = 0;

            DrawDropBoxes();
            DisplayShow();

            DisplayPaymentOptions();
        }

        DisplaySoldOut();
        ShowPrice();
    }

    protected void DisplayPaymentOptions()
    {
        PayWithDropBox.Items.Add("PayPal");
        PayWithDropBox.Items.Add("Visa");
        PayWithDropBox.Items.Add("Points");

        PayWithDropBox.Items[2].Enabled = false;
    }

    protected void DisplayShow()
    {
        ShowImage.ImageUrl = "/Images/" + show.Id + ".jpg";
        ShowTitle.Text = show.Name;
        ShowDescription.Text = show.Description;

        

        //show only available tickets type
        DisableTicketsType();
        EnableValidTickets();
    }

    protected void DisplaySoldOut()
    {
        if (BLshow.getAvailableTicketsAmount(show, selectedSchedule) <= 0)
        {
            SoldOutPic.Visible = true;
            TicketsPanel.Enabled = false;
            PricePanel.Visible = false;
        }
        else
        {
            SoldOutPic.Visible = false;
            TicketsPanel.Enabled = true;
            PricePanel.Visible = true;
        }

        //ShowTitle.Text = dalShow.getAvailableTicketsAmount(show, selectedSchedule) + "";
    }

    private void DisableTicketsType()
    {
        //string noTicketMsg = "Not available";

        RegularPanel.Visible = false;
        VipPanel.Visible = false;
        ChildPanel.Visible = false;
        SoldierPanel.Visible = false;
        StudentPanel.Visible = false;

        RegularAmount.SelectedIndex = 0;
        VipAmount.SelectedIndex = 0;
        ChildAmount.SelectedIndex = 0;
        SoldierAmount.SelectedIndex = 0;
        StudentAmount.SelectedIndex = 0;
    }

    private void EnableValidTickets()
    {
        //show available tickets for this schedule
        int index = ShowDates.SelectedIndex;

        selectedSchedule = showSchedule[index];

        if (selectedSchedule.RegularPrice > 0)
        {
            RegularPanel.Visible = true;
            RegularPrice.Text = selectedSchedule.RegularPrice + "₪";
        }

        if (selectedSchedule.VipPrice > 0)
        {
            VipPanel.Visible = true;
            VIPPrice.Text = selectedSchedule.VipPrice + "₪";
        }

        if (selectedSchedule.ChildPrice > 0)
        {
            ChildPanel.Visible = true;
            ChildPrice.Text = selectedSchedule.ChildPrice + "₪";
        }

        if (selectedSchedule.SoldierPrice > 0)
        {
            SoldierPanel.Visible = true;
            SoldierPrice.Text = selectedSchedule.SoldierPrice + "₪";
        }

        if (selectedSchedule.StudentPrice > 0)
        {
            StudentPanel.Visible = true;
            StudentPrice.Text = selectedSchedule.StudentPrice + "₪";
        }
    }

    protected void ShowDates_SelectedIndexChanged(object sender, EventArgs e)
    {
        EnableValidTickets();
        DisplayShow();
        DisplaySoldOut();
        ShowPrice();
    }

    protected bool CanClientPayWithPoints()
    {
        if (Session["eMail"] == null)
            return false;

        //DALclient dalClient = new DALclient();

        int points = BLclient.getClientDetails(Session["eMail"] + "").Points;

        if (price > points)
        {
            PayWithDropBox.Items[2].Enabled = false;
            PayWithDropBox.SelectedIndex = 0;
            
            PointsLabel.Visible = true;
            PointsTitle.Visible = true;

            return false;
        }

        PayWithDropBox.Items[2].Enabled = true;
        return true;
    }

    protected void DrawDropBoxes()
    {
        for(int i = 0; i < 10; i++)
        {
            ListItem item = new ListItem(i + "", i + "");

            RegularAmount.Items.Add(item);
            VipAmount.Items.Add(item);
            ChildAmount.Items.Add(item);
            SoldierAmount.Items.Add(item);
            StudentAmount.Items.Add(item);
        }
    }

    protected void ShowPrice()
    {
        price = 0;
        points = 0;
        ticketsQuantity = 0;

        if (selectedSchedule != null)
        {

            price = RegularAmount.SelectedIndex * selectedSchedule.RegularPrice;
            ticketsQuantity = RegularAmount.SelectedIndex;
            
            if (VipAmount.SelectedIndex >= 0)
            {
                price += VipAmount.SelectedIndex * selectedSchedule.VipPrice;
                ticketsQuantity += VipAmount.SelectedIndex;
            }
            if (ChildAmount.SelectedIndex >= 0)
            {
                price += ChildAmount.SelectedIndex * selectedSchedule.ChildPrice;
                ticketsQuantity += ChildAmount.SelectedIndex;
            }
            if (SoldierAmount.SelectedIndex >= 0)
            {
                price += SoldierAmount.SelectedIndex * selectedSchedule.SoldierPrice;
                ticketsQuantity += SoldierAmount.SelectedIndex;
            }
            if (StudentAmount.SelectedIndex >= 0)
            {
                price += StudentAmount.SelectedIndex * selectedSchedule.StudentPrice;
                ticketsQuantity += StudentAmount.SelectedIndex;
            }

            points = ticketsQuantity * show.Points;
        }

        Price.Text = price + "₪";
        PointsLabel.Text = points + "";
    }

    protected void AmountDropDown_Changed(object sender, EventArgs e)
    {
        ShowPrice();
        CanClientPayWithPoints();
        //PayWithDropBox_SelectedIndexChanged(sender, e);
    }
    protected void PurchaseButton_Click(object sender, EventArgs e)
    {
        //if not signed in as user
        if(Session["eMail"] == null)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            return;
        }

        //if didnt select amount of tickets
        if(ticketsQuantity == 0)
        {
            return;
        }

        //if user try to buy more tickets than possible
        if(ticketsQuantity > BLshow.getAvailableTicketsAmount(show, selectedSchedule))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openErrorModal();", true);
            return;
        }

        //DALpurchase DalPurchase = new DALpurchase();

        int id = BLpurchase.getNextPurchaseId();

        string purchaseWith = PayWithDropBox.SelectedValue;

        Purchase purchase = new Purchase(id, price, show.Id, ticketsQuantity, Session["eMail"] + "", selectedSchedule.Date, selectedSchedule.Time, purchaseWith);

        if(PayWithDropBox.SelectedIndex == 2) //if payed with Points
        {
            BLpurchase.cancelPointsToClient(Session["eMail"] + "", price); //decrease the points for the client
            BLpurchase.newPurchase(purchase, 0); //no points reward
        }
        else
            BLpurchase.newPurchase(purchase, points);

        Response.Redirect("/Client/MyPurchases.aspx");
    }
    
    protected void PayWithDropBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(PayWithDropBox.SelectedIndex == 2)
        {
            PointsTitle.Visible = false;
            PointsLabel.Visible = false;
        }
        else
        {
            PointsTitle.Visible = true;
            PointsLabel.Visible = true;
        }

    }
}