using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Statistics : System.Web.UI.Page
{
    //protected DALshow dalShow;
    protected List<Show> shows;
    protected int totalSellings;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["isAdmin"] == null)
            Response.Redirect("/Default.aspx");
        
        //dalShow = new DALshow();

        shows = BLshow.getAllShows();

        totalSellings = 0;

        AllShows.Controls.Add(DrawAllShows());

        totalSellingsLabel.Text = "Total Sellings: " + totalSellings + " ₪";
    }

    protected Panel DrawShowStatistics(Show show)
    {
        //thumbnail div
        Panel showPanel = new Panel();
        showPanel.CssClass = "thumbnail panel panel-carousel";

        //image
        Image showImg = new Image();
        showImg.ImageUrl = "/Images/" + show.Id + ".jpg";
        showImg.Attributes.Add("data-src", "holder.js/300x300");
        showImg.CssClass = "carousel-img img-rounded";
        showImg.Height = 160;


        //thumbnail body
        Panel showBody = new Panel();
        showBody.CssClass = "caption panel-body";

        //show's title
        showBody.Controls.Add(new Literal { Text = "<h4 class='white-text no-margin'><span class='glyphicon glyphicon-calendar white-text'></span>&nbsp " + show.Name + "</h4>" });
        showBody.Controls.Add(new Literal { Text = "<hr class='white-text no-margin'><br>" });

        //draw schedules
        List<Schedule> showSchedules = BLshow.getShowSchedules(show.Id, DateTime.Today);

        foreach (Schedule schedule in showSchedules)
        {
            Panel rowPanel = new Panel();
            rowPanel.CssClass = "row";

            Panel scheduleStringPanel = new Panel { CssClass = "col-md-7" };
            Panel scheduleBarPanel = new Panel { CssClass = "col-md-5" };
            rowPanel.Controls.Add(scheduleStringPanel);
            rowPanel.Controls.Add(scheduleBarPanel);

            //schedule date & time
            Label scheduleLabel = new Label();

            string showDate = schedule.Date.ToString("dd-MM-yyyy");
            string showTime = schedule.Time.ToString("HH:mm");
            string itemText = showDate + " " + showTime;

            scheduleLabel.Text = itemText;
            scheduleLabel.CssClass = "white-text";

            //scedule progress bar
            scheduleStringPanel.Controls.Add(scheduleLabel);
            scheduleBarPanel.Controls.Add(DrawShowProgressBar(show, schedule));

            showBody.Controls.Add(rowPanel);
            //showBody.Controls.Add(new Literal { Text="<hr class='no-margin'>" });
        }

        showBody.Controls.Add(DrawShowSellings(show));

        showPanel.Controls.Add(showImg);
        showPanel.Controls.Add(showBody);

        return showPanel;
    }

    protected Panel DrawShowSellings(Show show)
    {
        Panel sellsPanel = new Panel();
        sellsPanel.CssClass = "show-sellings label label-danger pull-right default-curser";

        //get sellings
        //DALpurchase dalPurchase = new DALpurchase();

        Label sellings = new Label();

        int showSellings = BLpurchase.getShowTotalSellings(show);
        totalSellings += showSellings;

        sellings.Text = "Total: " + showSellings + " ₪";

        sellsPanel.Controls.Add(sellings);

        return sellsPanel;
    }

    protected Panel DrawShowProgressBar(Show show, Schedule schedule)
    {
        Panel progressBarPanel = new Panel();
        progressBarPanel.CssClass = "progress";
        //string ticketsString = "";

        int availableTickets = BLshow.getAvailableTicketsAmount(show, schedule);
        int precentToShow = 0;

        if (availableTickets > 0)
            precentToShow = (availableTickets * 100) / schedule.AvailableTickets;

        progressBarPanel.ToolTip = "Total Tickets: " + schedule.AvailableTickets;

        if (availableTickets > 0) //tickets left
        {
            Panel greenBar = new Panel { CssClass = "progress-bar progress-bar-success progress-bar-striped active" };
            greenBar.Attributes.Add("role", "progressbar");
            greenBar.Attributes.Add("aria-valuenow", "'" + availableTickets + "'");
            greenBar.Attributes.Add("aria-valuemin", "0");
            greenBar.Attributes.Add("aria-valuemax", "'" + schedule.AvailableTickets + "'");
            //greenBar.Width = precentToShow;
            greenBar.Attributes.Add("style", "width: " + precentToShow + "%");

            progressBarPanel.Controls.Add(greenBar);

        }

        //tickets sold
        Panel redBar = new Panel { CssClass = "progress-bar progress-bar-danger progress-bar-striped active" };
        //redBar.Width = 100 - precentToShow;
        redBar.Attributes.Add("style", "width: " + (100 - precentToShow) + "%");

        progressBarPanel.Controls.Add(redBar);

        //tickets left
        Literal badge = new Literal();
        redBar.Controls.Add(badge);
        badge.Text = "<span class='badge pull-right default-curser' title='Tickets Left: " + availableTickets + "'>" + availableTickets + "</span>";

        return progressBarPanel;
    }

    protected Panel DrawAllShows()
    {
        Panel showsPanel = new Panel();
        showsPanel.CssClass = "showsStatistics";

        Panel rowPanel = new Panel();
        rowPanel.CssClass = "row";
        showsPanel.Controls.Add(rowPanel);

        int counter = 0;

        foreach (Show show in shows)
        {
            if (counter == 4)
            {
                rowPanel = new Panel();
                showsPanel.Controls.Add(rowPanel);
                rowPanel.CssClass = "row";
                counter = 0;
            }

            Panel showThumbnail = new Panel();
            showThumbnail.CssClass = "col-md-3";

            showThumbnail.Controls.Add(DrawShowStatistics(show));
            rowPanel.Controls.Add(showThumbnail);

            counter++;
        }

        return showsPanel;
    }
}