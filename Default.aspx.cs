using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{

    protected List<Show> shows;
    //private DALshow dalShow;
    protected const int MAX_NUM_OF_SHOWS = 10;

    DateTime from;
    DateTime to;

    protected void Page_Load(object sender, EventArgs e)
    {
        //dalShow = new DALshow();

        shows = GetShows();
        if (shows.Count > MAX_NUM_OF_SHOWS)
              shows.RemoveRange(MAX_NUM_OF_SHOWS, shows.Count - MAX_NUM_OF_SHOWS); 

        if (!IsPostBack)		// if it's the first loading
        {

        }
    }

    protected List<Show> GetShows()
    {
        return BLshow.getShowsByDate(DateTime.Today, DateTime.MaxValue);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        // Response.Redirect(Request.UrlReferrer.ToString());

        if (DropDownGenre.Text.Equals("All"))       //all ganeres (no genre chosen)
        {
            if (checkDatesEmpty())      // only ganeres whitout dates (no genre no dates chosen)
            {
                Response.Redirect("/SearchBar.aspx");
            }
            else if (checkDates())      //  all genres with dates (only dates chosen)
            {
                Response.Redirect("/SearchBar.aspx?Datefrom=" + from.ToString("dd/MM/yyyy") + "&DateTo=" + to.ToString("dd/MM/yyyy") + "&category=Date");
            }
            else // error on dates
            {

            }
        }
        else    // specific genre chosen
        {
            if (checkDatesEmpty())      // only ganere whitout dates (only genre chosen no dates)
            {
                Response.Redirect("/SearchBar.aspx?genreType=" + DropDownGenre.Text + "&category=Genre");
            }
            else if (checkDates())      //  ganere with dates chosen
            {
                Response.Redirect("/SearchBar.aspx?Datefrom=" + from.ToString("dd/MM/yyyy") + "&DateTo=" + to.ToString("dd/MM/yyyy") + "&genreType=" + DropDownGenre.Text + "&category=DateAndGenre");
            }
            else // error on dates
            {

            }



        }

    }


    public bool checkDatesEmpty()
    {

        if (FromDateTextBox.Text.Equals("") && ToDateTextBox.Text.Equals(""))
        {
            return true;
        }

        return false;
    }
    public bool checkDates()
    {

        if (FromDateTextBox.Text.Equals("") || ToDateTextBox.Text.Equals(""))
        {
            return false;
        }

        from = Convert.ToDateTime(FromDateTextBox.Text);
        to = Convert.ToDateTime(ToDateTextBox.Text);

        if (from.Date > to.Date)
        {
            return false;
        }
        return true;
    }
}