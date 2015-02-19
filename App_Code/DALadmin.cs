using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;
using System.IO;
using System.Data;


public class DALadmin
{

    private SqlConnection con;

    public DALadmin()
	{
        string str = DAL.STR;
        con = new SqlConnection(str);
	}


    public bool addNewShow(Show show)
    {
        //add if new
        con.Open();
        String addShow = "INSERT INTO Shows VALUES('" + show.Id + "','" + show.Name + "','" + show.Duration + "','" + show.Genre + "','" + show.Points + "','" + show.Description + "')";
        SqlCommand command = new SqlCommand(addShow, con);



        if (command.ExecuteNonQuery() > 0)
        {
            con.Close();
            return true;
        }

        con.Close();
        return false;
    }

    public bool deleteShow(Show show)
    {
        //add if new
        con.Open();

        String delShowSchedules = "DELETE FROM Schedule WHERE Id = '" + show.Id + "'";
        SqlCommand command = new SqlCommand(delShowSchedules, con);

        if (command.ExecuteNonQuery() < 0)
        {
            con.Close();
            return false;
        }

        String delShow = "DELETE FROM Shows WHERE Id = '" + show.Id + "'";
        command = new SqlCommand(delShow, con);

        if (command.ExecuteNonQuery() > 0)
        {
            con.Close();
            return true;
        }
        con.Close();
        return false;
    }

    public bool updateShow(Show show)
    {
        con.Open();
        //	UPDATE Client SET FirstName = 'Guy', LastName = 'Shimoni', Mail = 'bnohad@gmail.com', PhoneNumber = '222', Address = 'israel', Password = 'soos' WHERE Mail = 'bnohad@gmail.com';
        String updateShow = "UPDATE Shows SET Name = '" + show.Name + "', Duration = '" + show.Duration + "', Genre = '" + show.Genre + "', Points = '" + show.Points + "', Description = '" + show.Description + "' WHERE Id = '" + show.Id + "'";
        SqlCommand command = new SqlCommand(updateShow, con);



        if (command.ExecuteNonQuery() > 0)
        {
            con.Close();

            return true;
        }
        con.Close();
        return false;
    }
    public bool addNewScheduleToShow(Schedule schedule, int showId)
    {
        con.Open();

        string sqlFormattedDate = schedule.Date.ToString("yyyy-MM-dd HH:mm:ss");
        string sqlFormattedTime = schedule.Time.ToString("HH:mm:ss");

        //String addSchedule = "INSERT INTO Schedule VALUES('" + showId + "','" + schedule.Date + "','" + schedule.Time + "','" + schedule.Location + "','" + schedule.RegularPrice + "','" + schedule.ChildPrice + "','" + schedule.SoldierPrice + "','" + schedule.StudentPrice + "','" + schedule.VipPrice + "','" + schedule.AvailableTickets + "')";
        String addSchedule = "INSERT INTO Schedule VALUES('" + showId + "','" + sqlFormattedDate + "','" + sqlFormattedTime + "','" + schedule.Location + "','" + schedule.RegularPrice + "','" + schedule.ChildPrice + "','" + schedule.SoldierPrice + "','" + schedule.StudentPrice + "','" + schedule.VipPrice + "','" + schedule.AvailableTickets + "')";

        SqlCommand command = new SqlCommand(addSchedule, con);

        if (command.ExecuteNonQuery() > 0)
        {
            con.Close();
            return true;
        }

        con.Close();
        return false;
    }

    public bool deleteSchedule(Schedule schedule, int showId)
    {

        string sqlFormattedDate = schedule.Date.ToString("yyyy-MM-dd HH:mm:ss");
        string sqlFormattedTime = schedule.Time.ToString("HH:mm:ss");
  
        con.Open();
        String delSchedule = "DELETE FROM Schedule WHERE Id = '" + showId + "' AND Date = '" + sqlFormattedDate + "' AND Time = '" + sqlFormattedTime + "'";
        SqlCommand command = new SqlCommand(delSchedule, con);

        if (command.ExecuteNonQuery() > 0)
        {
            con.Close();
            return true;
        }
        con.Close();
        return false;
    }

    public bool updateSchedule(Schedule schedule, int showId, DateTime date, DateTime time)
    {

        string sqlFormattedDate = date.ToString("yyyy-MM-dd HH:mm:ss");
        string sqlFormattedTime = time.ToString("HH:mm:ss");

        string sqlFormattedScheduleDate = schedule.Date.ToString("yyyy-MM-dd HH:mm:ss");
        string sqlFormattedscheduleTime = schedule.Time.ToString("HH:mm:ss");

        con.Open();
        //	UPDATE Client SET FirstName = 'Guy', LastName = 'Shimoni', Mail = 'bnohad@gmail.com', PhoneNumber = '222', Address = 'israel', Password = 'soos' WHERE Mail = 'bnohad@gmail.com';
        String updateSchedule = "UPDATE Schedule SET Date = '" + sqlFormattedScheduleDate + "', Time = '" + sqlFormattedscheduleTime + "', Location = '" + schedule.Location + "', RegularPrice = '" + schedule.RegularPrice + "', ChildPrice = '" + schedule.ChildPrice + "', SoldierPrice = '" + schedule.SoldierPrice + "', StudentPrice = '" + schedule.StudentPrice + "', VipPrice = '" + schedule.VipPrice + "', AvailableTickets = '" + schedule.AvailableTickets + "' WHERE Id = '" + showId + "' AND Date = '" + sqlFormattedDate + "' AND Time = '" + sqlFormattedTime + "'";
        SqlCommand command = new SqlCommand(updateSchedule, con);



        if (command.ExecuteNonQuery() > 0)
        {
            con.Close();

            return true;
        }
        con.Close();
        return false;
    }

    public int getNextShowId()
    {
        con.Open();
        String getMaxId = "SELECT MAX(Id) As MaxId FROM Shows";
        SqlCommand command = new SqlCommand(getMaxId, con);

        SqlDataReader reader = command.ExecuteReader();

        int id;
        reader.Read();
        if (!reader.IsDBNull(0))
        {
            id = Convert.ToInt32(reader["MaxId"]);
        }
        else
        {
            id = 1000;
        }

        reader.Close();
        con.Close();
        return id + 1;
    }
}