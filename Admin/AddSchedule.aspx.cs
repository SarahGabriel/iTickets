using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddSchedule : System.Web.UI.Page
{

    private List<Show> shows;
    private static Show selectedShow;
    //private DALshow dalShow;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["isAdmin"] == null)
        {
            Response.Redirect("/Default.aspx");
        }


        //ShowsList.Items.Clear();
        //dalShow = new DALshow();
        shows = BLshow.getAllShows();

        if (!IsPostBack)		// if it's the first loading
        {
            LoadShowsToList();
        }

    }


    private void LoadShowsToList()
    {
        ShowsListSc.Items.Clear();

        foreach (Show show in shows)
        {
            ShowsListSc.Items.Add(show.Name);
        }
    }
    protected void ShowsListSc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void FilterShowList(object sender, EventArgs e)
    {
        shows = BLshow.getShowsByName(ShowNameTextBoxSc.Text);

        LoadShowsToList();
    }
    protected void SelectShowSc_Click(object sender, EventArgs e)
    {
        SelectedShowTextBox.Text = ShowsListSc.SelectedValue;

        if (ShowsListSc.SelectedValue != null)
        {
            foreach (Show s in shows)
            {
                if (s.Name.Equals(ShowsListSc.SelectedValue))
                {
                    selectedShow = s;
                    Imagetest.ImageUrl = "/Images/" + selectedShow.Id + ".jpg";
                    break;
                }
            }

        }

    }
    protected void AddScheduleButton_Click(object sender, EventArgs e)
    {
        String location;
        int regularPrice, childPrice, soldierPrice, studentprice, vipPrice, availableTickets;
        DateTime date, time;
        //DALadmin dalAdmin = new DALadmin();

        clearLabels();

        if (!checkEmptyBox())
        {
            return;
        }


        location = BLfunctions.replaceSpecialChars(LocationTextBox.Text);
        int.TryParse(RegularPriceTextBox.Text, out regularPrice);
        int.TryParse(ChildPriceTextBox.Text, out childPrice);
        int.TryParse(SoldierPriceTextBox.Text, out soldierPrice);
        int.TryParse(StudentPriceTextBox.Text, out studentprice);
        int.TryParse(VipPriceTextBox.Text, out vipPrice);
        int.TryParse(AvailableTicketsTextBox.Text, out availableTickets);

        // date = Convert.ToDateTime("1987/11/11");
        // time = Convert.ToDateTime("11:30:00");
        date = Convert.ToDateTime(DateTextBox.Text);
        time = Convert.ToDateTime(TimeTextBox.Text);

        if (selectedShow == null)
            return;

        //for testing
        TestTextBox.Text = "show Id : " + selectedShow.Id + "\n"
            + "location : " + LocationTextBox.Text + "\n"
            + "regular Price : " + RegularPriceTextBox.Text + "\n"
            + "child Price : " + ChildPriceTextBox.Text + "\n"
            + "soldier Price : " + SoldierPriceTextBox.Text + "\n"
            + "student price : " + StudentPriceTextBox.Text + "\n"
            + "vip Price : " + VipPriceTextBox.Text + "\n"
            + "available Tickets : " + AvailableTicketsTextBox.Text + "\n"
            + "date : " + date + "\n"
            + "time : " + time + "\n";

        Schedule sched = new Schedule(location, regularPrice, childPrice, soldierPrice, studentprice, vipPrice, availableTickets, date, time);

        if (!BLshow.checkIfScheduleExist(sched, selectedShow.Id))
        {
            if (BLadmin.addNewScheduleToShow(sched, selectedShow.Id))
            {
                clearLabels();
                LoadShowsToList();
                clearTextBoxes();
                ScheduleExistsErrLabel.Text = "";
                AddLabel.Text = "The schedule was successfully added!";

            }
        }
        else
        {
            AddLabel.Text = "";
            ScheduleExistsErrLabel.Text = "The schedule in this date and time already exists!";
        }
    }

    private bool checkEmptyBox()
    {

        // flase if error
        // true if good
        string errorSelectMsg = "Please select Show!";
        string errorMsg = "Please fill this field!";
        string errorNegNum = "All numbers must be positive!";
        int isNum;

        bool check = false;

        if (SelectedShowTextBox.Text.Equals(""))
        {
            SelectedShowErrLabel.Text = errorSelectMsg;
            check = true;
        }

        if (DateTextBox.Text.Equals(""))
        {
            DateErrLabel.Text = errorMsg;
            check = true;
        }

        if (TimeTextBox.Text.Equals(""))
        {
            TimeErrLabel.Text = errorMsg;
            check = true;
        }

        if (LocationTextBox.Text.Equals(""))
        {
            LocationErrLabel.Text = errorMsg;
            check = true;
        }


        int.TryParse(RegularPriceTextBox.Text, out isNum);

        if (RegularPriceTextBox.Text.Equals(""))
        {
            RegularPriceErrLabel.Text = errorMsg;
            check = true;
        }
        else if (isNum < 0)
        {
            RegularPriceErrLabel.Text = errorNegNum;
            check = true;
        }

        int.TryParse(ChildPriceTextBox.Text, out isNum);
        if (isNum < 0)
        {
            PriceErrLabel.Text = errorNegNum;
            check = true;
        }

        int.TryParse(SoldierPriceTextBox.Text, out isNum);
        if (isNum < 0)
        {
            PriceErrLabel.Text = errorNegNum;
            check = true;
        }

        int.TryParse(StudentPriceTextBox.Text, out isNum);
        if (isNum < 0)
        {
            PriceErrLabel.Text = errorNegNum;
            check = true;
        }

        int.TryParse(VipPriceTextBox.Text, out isNum);
        if (isNum < 0)
        {
            PriceErrLabel.Text = errorNegNum;
            check = true;
        }

        int.TryParse(AvailableTicketsTextBox.Text, out isNum);
        if (AvailableTicketsTextBox.Text.Equals(""))
        {
            AvailibleTicketsErrLabel.Text = errorMsg;
            check = true;
        }
        else if (isNum < 0)
        {
            AvailibleTicketsErrLabel.Text = errorNegNum;
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
        SelectedShowErrLabel.Text = "";
        DateErrLabel.Text = "";
        TimeErrLabel.Text = "";
        LocationErrLabel.Text = "";
        RegularPriceErrLabel.Text = "";
        PriceErrLabel.Text = "";
        AvailibleTicketsErrLabel.Text = "";
        Imagetest.ImageUrl = "";
    }

    private void clearTextBoxes()
    {
        SelectedShowTextBox.Text = "";
        DateTextBox.Text = "";
        TimeTextBox.Text = "";
        LocationTextBox.Text = "";
        RegularPriceTextBox.Text = "";
        ChildPriceTextBox.Text = "0";
        SoldierPriceTextBox.Text = "0";
        StudentPriceTextBox.Text = "0";
        VipPriceTextBox.Text = "0";
        AvailableTicketsTextBox.Text = "";
    }
}