using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;

public partial class Admin_AddShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["isAdmin"] == null)
        {
            Response.Redirect("/Default.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //DALadmin dal = new DALadmin();
        int showID, showDuration = 0, points;
        String showName, showGenre, description;
        bool isOkay = false;

        clearLabels();

        checkEmptyBox();
        checkUploadFile();

        isOkay = checkEmptyBox() && checkUploadFile();

        if (!isOkay)
        {
            return;
        }

        if (ShowLengthTextBox.Text != "")
        {
            showDuration = Int32.Parse(ShowLengthTextBox.Text);
        }

        showID = BLadmin.getNextShowId();
        points = Int32.Parse(ShowPointsTextBox.Text);

        showName = BLfunctions.replaceSpecialChars(ShowNameTextBox.Text);

        showGenre = DropDownGenre.Text;

        savePicture(showID);
        description = BLfunctions.replaceSpecialChars(DescriptionTextBox.Text);

        //Imagetest.ImageUrl = "/Images/" + showID + ".jpg";

        Show show = new Show(showID, showDuration, points, showName, showGenre, description);

        if(BLadmin.addNewShow(show))
        {
            clearTextBoxes();
            clearLabels();
            SavedLabel.Text = "Show was successfuly saved!";
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
        GenreLabelErr.Text = "";
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
        else if (fileName.Equals(""))
        {
            UploadLabel.Text = "Please select a picture";

        }
        else
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

    protected void AddNewGenreButton_Click(object sender, EventArgs e)
    {
        //DALGenre dal = new DALGenre();

        String genre = BLfunctions.replaceSpecialChars(GenreTextBox.Text);

        if(genre.Equals(""))
        {
            return;
        }

        BLGenre.AddNewGenre(genre);
        GenreTextBox.Text = "";
        GenreLabelErr.Text = "";
        DropDownGenre.DataBind();
    }
    protected void DeleteGenreButton_Click(object sender, EventArgs e)
    {
        //DALGenre dal = new DALGenre();

        String genre = DropDownGenre.SelectedValue;

        if (genre.Equals("") || genre.Equals("All"))
        {
            return;
        }

        if(BLGenre.isGenreUsed(genre))
        {
            GenreLabelErr.Text = "Genre cannot be deleted. Used by some shows";
            return;
        }

        GenreLabelErr.Text = "";
        BLGenre.deleteGenre(genre);
        DropDownGenre.DataBind();
    }
}