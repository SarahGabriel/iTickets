using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SearchBar : System.Web.UI.Page
{
    public static List<Show> shows;
    public static int ITEMS_PER_PAGE = 6;
    public static int DESCRIPTION_LENGTH = 35;
    public static int TITLE_LENGTH = 20;
   // public static DALshow dalShow;
    public static int showSize, index;

    protected void Page_Load(object sender, EventArgs e)
    {
        //dalShow = new DALshow();
        getShows();

        if (!IsPostBack)
        {

            index = 0;

            showSize = shows.Count;

            //TextBox2.Text = "first load";
            //TestTextBox.Text = "in !IsPostBack";
            if (showSize > ITEMS_PER_PAGE)
            {
                NextButton.Visible = true;
            }
        }
    }
    protected void PrevButton_Click(object sender, EventArgs e)
    {
        index = index - ITEMS_PER_PAGE;

        if (index < 0)
            index = 0;

        showArowButtons();
    }
    protected void NextButton_Click(object sender, EventArgs e)
    {
        index += ITEMS_PER_PAGE;
        //TestTextBox.Text ="index " + index;

        if (index > showSize)
            index = 0;

        showArowButtons();
    }

    public String trimString(String str, int length)
    {
        if (str.Length < length)
        {
            return str;
        }

        str = str.Substring(0, length) + " ...";
        return str;
    }

    public void showArowButtons()
    {
        if (index < ITEMS_PER_PAGE)
        {
            //TestTextBox.Text = "showSize " + showSize + " > index " + index;
            PrevButton.Visible = false;
        }
        else
        {
            //TestTextBox.Text = "in else";
            PrevButton.Visible = true;
        }

        if (showSize < index + ITEMS_PER_PAGE)
        {
            //TestTextBox.Text = "showSize " + showSize + " > index " + index;
            NextButton.Visible = false;
        }
        else
        {
            //TestTextBox.Text = "in else";
            NextButton.Visible = true;
        }
    }

    public void getShows()
    {
        String category = Request.QueryString["category"];
        if (category == null)
        {
            LabelTitle.Text = "All shows";
            shows = BLshow.getShowsByDate(DateTime.Today, DateTime.MaxValue);
        }
        else if (category.Equals("Date"))
        {
            String from = Request.QueryString["Datefrom"];
            String to = Request.QueryString["DateTo"];

            LabelTitle.Text = "Shows from " + from + " to " + to;

            shows = BLshow.getShowsByDate(Convert.ToDateTime(from), Convert.ToDateTime(to));
            changeTitleIfNoShows("Oops no shows available from " + from + " to " + to + " ...");
        }
        else if (category.Equals("Genre"))
        {
            String genre = Request.QueryString["genreType"];
            shows = BLshow.getShowsByGenre(genre);

            LabelTitle.Text = genre + " shows";

            changeTitleIfNoShows("Oops no " + genre + " shows...");
        }
        else if (category.Equals("DateAndGenre"))
        {
            String genre = Request.QueryString["genreType"];
            String from = Request.QueryString["Datefrom"];
            String to = Request.QueryString["DateTo"];

            shows = BLshow.getShowsByDatesAndGenre(Convert.ToDateTime(from), Convert.ToDateTime(to), genre);

            LabelTitle.Text = genre + " shows from " + from + " to " + to;

            changeTitleIfNoShows("Oops no available " + genre + " shows from " + from + " to " + to + " ...");
        }
        else if (category.Equals("Name"))
        {
            String name = Request.QueryString["findName"];

            LabelTitle.Text = "Shows matching '" + name + "'";

            shows = BLshow.getShowsBySearchBarOnlySchedule(name);

            changeTitleIfNoShows("Oops no Shows matching '" + name + "'...");
        }
        else
        {
            changeTitleIfNoShows("No available shows.");
        }

    }

    public void changeTitleIfNoShows(String title)
    {
        if (shows.Count == 0)
        {
            LabelTitle.Text = title;
        }
    }

}