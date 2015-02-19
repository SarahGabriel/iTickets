using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EditSchedule : System.Web.UI.Page
{

    private List<Show> shows;
    private static Show selectedShow;
    private static List<Schedule> schedules;
    private static Schedule selectedSchedule;
    //private DALshow dalShow;
    private static bool fromBegining = false;


    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["isAdmin"] == null)
        {
            Response.Redirect("/Default.aspx");
        }



        //dalShow = new DALshow();
        shows = BLshow.getAllShows();

        if (!IsPostBack)
        {
            selectedShow = null;
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

    private void LoadSchedulesToList(bool begining)
    {
        DateTime date;
        if (begining == true)
        {
            date = DateTime.MinValue;
        }
        else
        {
            date = DateTime.Today;
        }
        schedules = BLshow.getShowSchedules(selectedShow.Id, date);

        String temp, schedInfo;
        SchedulesListSc.Items.Clear();

        foreach (Schedule s in schedules)
        {
            schedInfo = "";
            temp = s.Date.ToString("dd/MM/yyyy");
            schedInfo = s.Location + " " + temp;

            temp = s.Time.ToString("H:mm");
            schedInfo += " " + temp;


            SchedulesListSc.Items.Add(schedInfo);
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

        if (ShowsListSc.SelectedIndex == -1)
        {
            return;
        }
        clearTextBoxes();
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

        LoadSchedulesToList(fromBegining);

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

        /*
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
        */
        Schedule sched = new Schedule(location, regularPrice, childPrice, soldierPrice, studentprice, vipPrice, availableTickets, date, time);

        if (BLshow.checkIfScheduleExist(sched, selectedShow.Id))
        {
            if (BLadmin.updateSchedule(sched, selectedShow.Id, selectedSchedule.Date, selectedSchedule.Time))
            {
                clearLabels();
                LoadShowsToList();
                clearTextBoxes();
                ScheduleExistsErrLabel.Text = "";
                AddLabel.Text = "The schedule was successfully updated!";

            }
        }
        else
        {
            AddLabel.Text = "";
            ScheduleExistsErrLabel.Text = "The schedule in this date and time does not exists!";
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
    }

    private void clearTextBoxes()
    {
        SelectedShowTextBox.Text = "";
        DateTextBox.Text = "";
        TimeTextBox.Text = "";
        LocationTextBox.Text = "";
        RegularPriceTextBox.Text = "0";
        ChildPriceTextBox.Text = "0";
        SoldierPriceTextBox.Text = "0";
        StudentPriceTextBox.Text = "0";
        VipPriceTextBox.Text = "0";
        AvailableTicketsTextBox.Text = "";
    }
    protected void SelectSCheduleSc_Click(object sender, EventArgs e)
    {
        if (SchedulesListSc.SelectedIndex == -1 || schedules == null)
        {
            return;
        }

        DeletePopupButton.Visible = true;                   ///////////////

        DateTime date, time;
        String[] splt;
        splt = SchedulesListSc.SelectedValue.Split(' ');

        date = Convert.ToDateTime(splt[splt.Length - 2]);
        time = Convert.ToDateTime(splt[splt.Length - 1]);

        foreach (Schedule s in schedules)
        {

            if (s.Date == date && s.Time == time)
            {
                selectedSchedule = s;
                break;
            }

        }

        TestTextBox.Text = selectedSchedule.Date + "\n" + selectedSchedule.Time;

        if (selectedSchedule.Date.Date > DateTime.Today.Date)                   /////////////////////////////////
        {
            if (BLshow.checkifScheduleIsPurchased(selectedShow.Id, selectedSchedule.Date, selectedSchedule.Time))      ///////////////////
            {
                DeletePopupButton.Visible = false;
                CannotDeleteLabel.Visible = true;
            }
            else
            {
                DeletePopupButton.Visible = true;
                CannotDeleteLabel.Visible = false;
            }
        }


        loadSchedualInfoToTextBoxes();
    }

    public void loadSchedualInfoToTextBoxes()
    {
        DateTextBox.Text = selectedSchedule.Date.ToString("yyyy-MM-dd");
        TimeTextBox.Text = selectedSchedule.Time.ToString("HH:mm:ss");
        LocationTextBox.Text = selectedSchedule.Location;
        RegularPriceTextBox.Text = selectedSchedule.RegularPrice.ToString();
        ChildPriceTextBox.Text = selectedSchedule.ChildPrice.ToString();
        SoldierPriceTextBox.Text = selectedSchedule.SoldierPrice.ToString();
        StudentPriceTextBox.Text = selectedSchedule.StudentPrice.ToString();
        VipPriceTextBox.Text = selectedSchedule.VipPrice.ToString();
        AvailableTicketsTextBox.Text = selectedSchedule.AvailableTickets.ToString();
        
    }

    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        if (SchedulesListSc.SelectedIndex == -1)
        {
            return;
        }

        DateTime date, time;
        String[] splt;
        splt = SchedulesListSc.SelectedValue.Split(' ');

        date = Convert.ToDateTime(splt[splt.Length - 2]);
        time = Convert.ToDateTime(splt[splt.Length - 1]);

        foreach (Schedule s in schedules)
        {

            if (s.Date == date && s.Time == time)
            {
                selectedSchedule = s;
                break;
            }

        }

        TestTextBox.Text = selectedSchedule.Date + "\n" + selectedSchedule.Time;

        //DALadmin dalAdmin = new DALadmin();

        BLadmin.deleteSchedule(selectedSchedule, selectedShow.Id);


        clearTextBoxes();
        ShowNameTextBoxSc.Text = ShowsListSc.SelectedValue;
        LoadSchedulesToList(fromBegining);

    }
    protected void SelectFromBegining_Click(object sender, EventArgs e)
    {
        if (fromBegining == false)
        {
            fromBegining = true;
            SchedualsFromLabel.Text = "*Showing Schedules from begining of time";
            SelectFromBeginingButton.Text = "Show Schedules from today onwards";
        }
        else
        {
            fromBegining = false;
            SchedualsFromLabel.Text = "*Showing Schedules from today onwards";
            SelectFromBeginingButton.Text = "Show Schedules from begining of time";
        }

        if (selectedShow != null)
            LoadSchedulesToList(fromBegining);
    }
}