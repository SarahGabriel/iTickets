using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;


public partial class Admin_EditShow : System.Web.UI.Page
{
    private List<Show> shows;
    private static Show selectedShow;
    //private DALshow dal;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["isAdmin"] == null)
        {
            Response.Redirect("/Default.aspx");
        }


        //ShowsList.Items.Clear();
            //dal = new DALshow();
            shows = BLshow.getAllShows();

        if (!IsPostBack)
        {
            LoadShowsToList();


            //foreach (Show show in shows)
            {
            //    ShowsList.Items.Add(show.Name);
            }
        }
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        //DALadmin dal = new DALadmin();

        int showID, showDuration = 0, points;
        String showName, showGenre, description;

        bool isOkay = false;

        if (selectedShow == null)
        {
            //SavedLabel.Text = "Show is null";
            return;
        }

        clearLabels();

        checkEmptyBox();

        isOkay = checkEmptyBox();

        if (!isOkay)
        {
            return;
        }

        if (ShowLengthTextBox.Text != "")
        {
            showDuration = Int32.Parse(ShowLengthTextBox.Text);
        }

        showID = selectedShow.Id;
        points = Int32.Parse(ShowPointsTextBox.Text);

        showName = ShowNameTextBox.Text;

        showGenre = DropDownGenre.Text;

        if (checkUploadFile())
        {
            savePicture(showID);
        }

        description = DescriptionTextBox.Text;

        Show show = new Show(showID, showDuration, points, showName, showGenre, description);

        if(BLadmin.updateShow(show))
        {
            //clearTextBoxes();
            clearLabels();
            LoadShowsToList();
            SavedLabel.Text = "Show was successfully updated!";
        }
    }

    private void savePicture(int showID)
    {
        String savePath = @"/Images/";
        String appPath = Request.PhysicalApplicationPath;
        FileUpload1.SaveAs(appPath + savePath + showID + ".jpg");
    }

    private void clearLabels()
    {
        TitleLabel.Text = "";
        PointsLabel.Text = "";
        UploadLabel.Text = "";
        DurationLabel.Text = "";
        SavedLabel.Text = "";
        Imagetest.ImageUrl = "";
    }

    private void clearTextBoxes()
    {
        ShowNameTextBox.Text = "";
        ShowLengthTextBox.Text = "";
        ShowPointsTextBox.Text = "";
        DescriptionTextBox.Text = "";
    }

    private bool checkUploadFile()
    {
        string fileName = FileUpload1.FileName;
        fileName = fileName.Substring(fileName.LastIndexOf(".") + 1);
        fileName = fileName.ToLower();
        if (fileName.Equals("jpg") || fileName.Equals("jpeg"))
        {
            return true;
        }
        else if(!fileName.Equals(""))
        {
            UploadLabel.Text = "Only .jpg/.jpeg files are supported";
        }
        return false;
    }

    private bool checkEmptyBox()
    {

        // flase if error
        // true if good
        string errorMsg = "Please fill this field!";
        string errorNegNum = "The number must be positive!";
        int isNum;

        bool check = false;

        if (ShowNameTextBox.Text.Equals(""))
        {
            TitleLabel.Text = errorMsg;
            check = true;
        }
        int.TryParse(ShowPointsTextBox.Text, out isNum);
      
        if (ShowPointsTextBox.Text.Equals(""))
        {
            PointsLabel.Text = errorMsg;
            check = true;
        }
        else if(isNum < 0)
        {
            PointsLabel.Text = errorNegNum;
            check = true;
        }

        int.TryParse(ShowLengthTextBox.Text, out isNum);
        if (ShowLengthTextBox.Text.Equals(""))
        {
            DurationLabel.Text = errorMsg;
            check = true;
        }
        else if (isNum < 0)
        {
            DurationLabel.Text = errorNegNum;
            check = true;
        }

        if (check)
        {
            return false;
        }

        return true;
    }

    protected void ShowNameTextBox_TextChanged(object sender, EventArgs e)
    {
       
    }

    protected void FilterShowList(object sender, EventArgs e)
    {
        shows = BLshow.getShowsByName(ShowNameTextBox.Text);

        LoadShowsToList();
    }

    private void LoadShowsToList()
    {
        ShowsList.Items.Clear();

        foreach (Show show in shows)
        {
            ShowsList.Items.Add(show.Name);
        }
    }

    private void LoadShowDetails(String name)
    {
        foreach(Show show in shows)
        {
            if(show.Name.Equals(name))
            {
                ShowNameTextBox.Text = name;
                ShowLengthTextBox.Text = show.Duration + "";
                ShowPointsTextBox.Text = show.Points + "";
                DescriptionTextBox.Text = show.Description;
                DropDownGenre.Text = show.Genre;

                Imagetest.ImageUrl = "/Images/" + show.Id + ".jpg";

                selectedShow = new Show(show.Id, show.Duration, show.Points, show.Name, show.Genre, show.Description);



                if (BLshow.checkifShowIsPurchased(show.Id))            ///////////////////////////////////////////////////////////////////
                {
                    DeletePopupButton.Visible = false;
                    CannotDeleteLabel.Visible = true;
                }
                else
                {
                    DeletePopupButton.Visible = true;
                    CannotDeleteLabel.Visible = false;
                }

                break;
                
            }
        }

        return;
    }
    protected void LoadButton_Click(object sender, EventArgs e)
    {
        LoadShowDetails(ShowsList.SelectedValue);
    }
    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        foreach (Show show in shows)
        {
            if (show.Name.Equals(ShowsList.SelectedValue))
            {
                selectedShow = new Show(show.Id, show.Duration, show.Points, show.Name, show.Genre, show.Description);
                
                //DALadmin dalA = new DALadmin();
                BLadmin.deleteShow(selectedShow);

                //delete image file
                File.Delete(Request.PhysicalApplicationPath + "/Images/" + show.Id + ".jpg");
                Imagetest.ImageUrl = "";

                clearLabels();
                clearTextBoxes();

                shows = BLshow.getAllShows();
                LoadShowsToList();
                
                break;

            }
        }
    }
}