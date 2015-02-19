using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;

public class DALshow
{

    private SqlConnection con;

    public DALshow()
    {
        string str = DAL.STR;
        con = new SqlConnection(str);
    }




    public bool checkifShowIsPurchased(int ShowId)
    {
        string sqlFormattedDate = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");

        con.Open();
        String getSched = "SELECT * FROM Purchases WHERE ShowId = '" + ShowId + "' AND Date >= '" + sqlFormattedDate + "'";
        SqlCommand command = new SqlCommand(getSched, con);
        SqlDataReader reader = command.ExecuteReader();



        if (reader.Read() == false)
        {
            reader.Close();
            con.Close();
            return false;
        }

        reader.Close();
        con.Close();
        return true;

    }

    public bool checkifScheduleIsPurchased(int ShowId, DateTime date, DateTime time)
    {

        string sqlFormattedDate = date.ToString("yyyy-MM-dd HH:mm:ss");
        string sqlFormattedTime = time.ToString("HH:mm:ss");

        con.Open();
        String getSched = "SELECT * FROM Purchases WHERE ShowId = '" + ShowId + "' AND Date = '" + sqlFormattedDate + "' AND Time = '" + sqlFormattedTime + "'";
        SqlCommand command = new SqlCommand(getSched, con);
        SqlDataReader reader = command.ExecuteReader();



        if (reader.Read() == false)
        {
            reader.Close();
            con.Close();
            return false;
        }

        reader.Close();
        con.Close();
        return true;

    }


    public List<Show> getShowsBySearchBarOnlySchedule(String name)
    {
        con.Open();
        List<Show> shows = new List<Show>();

        String getShows = "select DISTINCT  Shows.Id , Shows.Name , Shows.Points ,  Shows.Description , Shows.Duration , Shows.Genre from Shows , Schedule where Shows.Id = Schedule.id and  (Shows.Name LIKE '%" + name + "%' or Shows.Description  LIKE  '%" + name + "%')";
        SqlCommand command = new SqlCommand(getShows, con);
        SqlDataReader reader = command.ExecuteReader();

        int showID, pointsShow, durationShow;
        String nameShow, genreShow, description;


        while (reader.Read())
        {
            showID = Convert.ToInt32(reader["Id"]);
            nameShow = reader["Name"] + "";
            durationShow = Convert.ToInt32(reader["Duration"]);
            genreShow = reader["Genre"] + "";
            pointsShow = Convert.ToInt32(reader["Points"]);
            description = reader["Description"] + "";

            Show show = new Show(showID, durationShow, pointsShow, nameShow, genreShow, description);

            shows.Add(show);
        }

        con.Close();
        reader.Close();

        return shows;
    }


    public List<Show> getShowsByGenre(String genre)
    {
        con.Open();
        List<Show> shows = new List<Show>();

        String getShows = "select DISTINCT  Shows.Id , Shows.Name , Shows.Points ,  Shows.Description , Shows.Duration , Shows.Genre from Shows , Schedule where Shows.Id = Schedule.id and Shows.Genre = '" + genre + "'";
        SqlCommand command = new SqlCommand(getShows, con);
        SqlDataReader reader = command.ExecuteReader();

        int showID, pointsShow, durationShow;
        String nameShow, genreShow, description;


        while (reader.Read())
        {
            showID = Convert.ToInt32(reader["Id"]);
            nameShow = reader["Name"] + "";
            durationShow = Convert.ToInt32(reader["Duration"]);
            genreShow = reader["Genre"] + "";
            pointsShow = Convert.ToInt32(reader["Points"]);
            description = reader["Description"] + "";

            Show show = new Show(showID, durationShow, pointsShow, nameShow, genreShow, description);

            shows.Add(show);
        }

        con.Close();
        reader.Close();

        return shows;
    }


    public List<Show> getShowsByDatesAndGenre(DateTime from, DateTime to, String genre)
    {
        con.Open();
        List<Show> shows = new List<Show>();

        string sqlFormattedDate = from.ToString("yyyy-MM-dd HH:mm:ss");
        string sqlFormattedDateTo = to.ToString("yyyy-MM-dd HH:mm:ss");

        String getShows = "select DISTINCT  Shows.Id , Shows.Name , Shows.Points ,  Shows.Description , Shows.Duration , Shows.Genre from Shows , Schedule where Shows.Id = Schedule.id and Schedule.Date >= '" + sqlFormattedDate + "' AND  Schedule.Date <= '" + sqlFormattedDateTo + "' and Shows.Genre = '" + genre + "'";
        SqlCommand command = new SqlCommand(getShows, con);
        SqlDataReader reader = command.ExecuteReader();

        int showID, pointsShow, durationShow;
        String nameShow, genreShow, description;


        while (reader.Read())
        {
            showID = Convert.ToInt32(reader["Id"]);
            nameShow = reader["Name"] + "";
            durationShow = Convert.ToInt32(reader["Duration"]);
            genreShow = reader["Genre"] + "";
            pointsShow = Convert.ToInt32(reader["Points"]);
            description = reader["Description"] + "";

            Show show = new Show(showID, durationShow, pointsShow, nameShow, genreShow, description);

            shows.Add(show);
        }

        con.Close();
        reader.Close();

        return shows;
    }

    public List<Schedule> getShowSchedules(int showId, DateTime fromDate)
    {
        con.Open();
        List<Schedule> schedules = new List<Schedule>();
        string sqlFormattedDate = fromDate.ToString("yyyy-MM-dd HH:mm:ss");

        String getSchedules = "SELECT * FROM Schedule WHERE Id = '" + showId + "' AND Date >= '" + sqlFormattedDate + "'" + "ORDER BY Date,Time";
        SqlCommand command = new SqlCommand(getSchedules, con);
        SqlDataReader reader = command.ExecuteReader();

        String location;
        int regularPrice, childPrice, soldierPrice, studentPrice, vipPrice, availableTickets;
        DateTime date, time;
        //String time, date;

        while (reader.Read())
        {
            location = reader["Location"] + "";
            regularPrice = Convert.ToInt32(reader["RegularPrice"]);
            childPrice = Convert.ToInt32(reader["ChildPrice"]);
            soldierPrice = Convert.ToInt32(reader["SoldierPrice"]);
            studentPrice = Convert.ToInt32(reader["StudentPrice"]);
            vipPrice = Convert.ToInt32(reader["VipPrice"]);
            availableTickets = Convert.ToInt32(reader["AvailableTickets"]);
            time = Convert.ToDateTime(reader["Time"] + "");
            date = Convert.ToDateTime(reader["Date"] + "");
            //time = (DateTime)reader["Time"] + "";
            //date = (DateTime)reader["Date"] + "";
            Schedule schedule = new Schedule(location, regularPrice, childPrice, soldierPrice, studentPrice, vipPrice, availableTickets, date, time);

            schedules.Add(schedule);
        }

        con.Close();
        reader.Close();

        return schedules;
    }




    public List<Show> getShowsByDate(DateTime dateFrom, DateTime dateTo)
    {
        con.Open();
        List<Show> shows = new List<Show>();
        List<int> ids = new List<int>();

        string sqlFormattedDate = dateFrom.ToString("yyyy-MM-dd HH:mm:ss");
        string sqlFormattedDateTo = dateTo.ToString("yyyy-MM-dd HH:mm:ss");

        String getSchedules = "SELECT Id FROM Schedule WHERE Date >= '" + sqlFormattedDate + "' AND Date <= '" + sqlFormattedDateTo + "'";
        SqlCommand command = new SqlCommand(getSchedules, con);
        SqlDataReader reader = command.ExecuteReader();

        int id;

        while (reader.Read())
        {
            id = Convert.ToInt32(reader["Id"]);
            if (!ids.Contains(id))
            {
                ids.Add(id);
            }
        }
        reader.Close();

        String getShows;

        int pointsShow, durationShow;
        String nameShow, genreShow, description;

        foreach (int i in ids)
        {
            getShows = "SELECT * FROM Shows WHERE Id = '" + i + "'";
            command = new SqlCommand(getShows, con);
            SqlDataReader reader1 = command.ExecuteReader();


            while (reader1.Read())
            {
                nameShow = reader1["Name"] + "";
                durationShow = Convert.ToInt32(reader1["Duration"]);
                genreShow = reader1["Genre"] + "";
                pointsShow = Convert.ToInt32(reader1["Points"]);
                description = reader1["Description"] + "";
                Show show = new Show(i, durationShow, pointsShow, nameShow, genreShow, description);

                shows.Add(show);
            }
            reader1.Close();
        }
        con.Close();

        reader.Close();

        return shows;
    }

    public Show getShowsById(int showId)
    {
        con.Open();
        Show show;

        String getShow = "SELECT * FROM Shows WHERE Id = '" + showId + "'";
        SqlCommand command = new SqlCommand(getShow, con);
        SqlDataReader reader = command.ExecuteReader();

        if (reader.Read() == false)
        {
            reader.Close();
            con.Close();
            return null;
        }

        int pointsShow, durationShow;
        String nameShow, genreShow, description;

        nameShow = reader["Name"] + "";
        durationShow = Convert.ToInt32(reader["Duration"]);
        genreShow = reader["Genre"] + "";
        pointsShow = Convert.ToInt32(reader["Points"]);
        description = reader["Description"] + "";
        show = new Show(showId, durationShow, pointsShow, nameShow, genreShow, description);

        con.Close();
        reader.Close();

        return show;
    }


    public bool checkIfScheduleExist(Schedule schedule, int Id)
    {

        string sqlFormattedDate = schedule.Date.ToString("yyyy-MM-dd HH:mm:ss");
        string sqlFormattedTime = schedule.Time.ToString("HH:mm:ss");

        con.Open();
        String getSched = "SELECT * FROM Schedule WHERE Id = '" + Id + "' AND Date = '" + sqlFormattedDate + "' AND Time = '" + sqlFormattedTime + "'";
        SqlCommand command = new SqlCommand(getSched, con);
        SqlDataReader reader = command.ExecuteReader();

        if (reader.Read() == false)
        {
            reader.Close();
            con.Close();
            return false;
        }

        reader.Close();
        con.Close();
        return true;

    }

    public List<Schedule> getScheduleByName(String name)
    {
        con.Open();
        List<Schedule> schedules = new List<Schedule>();
        //List<int> ids = new List<int>();
        String getIds = "SELECT Id FROM Shows WHERE Name = '" + name + "'";
        SqlCommand command = new SqlCommand(getIds, con);
        SqlDataReader reader = command.ExecuteReader();

        reader.Read();
        int id = Convert.ToInt32(reader["Id"]);

        reader.Close();

        String getSchedules = "SELECT * FROM Schedule WHERE Id = '" + id + "'";
        command = new SqlCommand(getSchedules, con);
        reader = command.ExecuteReader();

        String location;
        int regularPrice, childPrice, soldierPrice, studentPrice, vipPrice, availableTickets;
        DateTime date, time;


        while (reader.Read())
        {
            location = reader["Location"] + "";
            regularPrice = Convert.ToInt32(reader["RegularPrice"]);
            childPrice = Convert.ToInt32(reader["ChildPrice"]);
            soldierPrice = Convert.ToInt32(reader["SoldierPrice"]);
            studentPrice = Convert.ToInt32(reader["StudentPrice"]);
            vipPrice = Convert.ToInt32(reader["VipPrice"]);
            availableTickets = Convert.ToInt32(reader["AvailableTickets"]);
            time = Convert.ToDateTime(reader["Time"] + "");
            date = Convert.ToDateTime(reader["Date"] + "");

            Schedule schedule = new Schedule(location, regularPrice, childPrice, soldierPrice, studentPrice, vipPrice, availableTickets, date, time);

            schedules.Add(schedule);
        }

        con.Close();
        reader.Close();

        return schedules;
    }

    public List<Show> getAllShows()
    {
        con.Open();
        List<Show> shows = new List<Show>();

        String getShows = "SELECT * FROM Shows ORDER BY Name";
        SqlCommand command = new SqlCommand(getShows, con);
        SqlDataReader reader = command.ExecuteReader();

        int showID, pointsShow, durationShow;
        String nameShow, genreShow, description;


        while (reader.Read())
        {
            showID = Convert.ToInt32(reader["Id"]);
            nameShow = reader["Name"] + "";
            durationShow = Convert.ToInt32(reader["Duration"]);
            genreShow = reader["Genre"] + "";
            pointsShow = Convert.ToInt32(reader["Points"]);
            description = reader["Description"] + "";

            Show show = new Show(showID, durationShow, pointsShow, nameShow, genreShow, description);

            shows.Add(show);
        }

        con.Close();
        reader.Close();

        return shows;
    }

    public List<Show> getShowsByName(String name)
    {
        con.Open();
        List<Show> shows = new List<Show>();

        String getShows = "SELECT * FROM Shows WHERE Name LIKE '%" + name + "%'";
        SqlCommand command = new SqlCommand(getShows, con);
        SqlDataReader reader = command.ExecuteReader();

        int showID, pointsShow, durationShow;
        String nameShow, genreShow, description;


        while (reader.Read())
        {
            showID = Convert.ToInt32(reader["Id"]);
            nameShow = reader["Name"] + "";
            durationShow = Convert.ToInt32(reader["Duration"]);
            genreShow = reader["Genre"] + "";
            pointsShow = Convert.ToInt32(reader["Points"]);
            description = reader["Description"] + "";

            Show show = new Show(showID, durationShow, pointsShow, nameShow, genreShow, description);

            shows.Add(show);
        }

        con.Close();
        reader.Close();

        return shows;
    }

    public int getTicketsAmount(Show show, Schedule showSchedule)
    {
        con.Open();

        if (showSchedule == null)
        {
            con.Close();
            return -1;
        }

        string sqlFormattedDate = showSchedule.Date.ToString("yyyy-MM-dd HH:mm:ss");
        string sqlFormattedTime = showSchedule.Time.ToString("HH:mm:ss");

        String getAmount = "SELECT AvailableTickets FROM Schedule WHERE Id='" + show.Id + "' AND Date='" + sqlFormattedDate + "' AND Time='" + sqlFormattedTime + "'";
        SqlCommand command = new SqlCommand(getAmount, con);
        SqlDataReader reader = command.ExecuteReader();

        int ticketAmount = -1;


        if (reader.Read())
        {
            ticketAmount = Convert.ToInt32(reader["AvailableTickets"]);
        }

        con.Close();
        reader.Close();

        return ticketAmount;
    }

    public int getSoldTicketsAmount(Show show, Schedule showSchedule)
    {

        con.Open();

        int soldTickets = 0;

        string sqlFormattedDate = showSchedule.Date.ToString("yyyy-MM-dd HH:mm:ss");
        string sqlFormattedTime = showSchedule.Time.ToString("HH:mm:ss");

        String getAmount = "SELECT SUM(Quantity) FROM Purchases WHERE ShowId='" + show.Id + "' AND Date='" + sqlFormattedDate + "' AND Time='" + sqlFormattedTime + "'";
        SqlCommand command = new SqlCommand(getAmount, con);
        SqlDataReader reader = command.ExecuteReader();


        if (reader.Read())
        {
            if (reader[0] != DBNull.Value)
                soldTickets = Convert.ToInt32(reader[0]);
        }

        con.Close();
        reader.Close();

        return soldTickets;
    }


}