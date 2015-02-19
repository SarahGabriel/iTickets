using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLshow
/// </summary>
public class BLshow
{
    private static DALshow dal = new DALshow();

	public BLshow()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool checkifShowIsPurchased(int ShowId)
    {
        return dal.checkifShowIsPurchased(ShowId);
    }

    public static bool checkifScheduleIsPurchased(int ShowId, DateTime date, DateTime time)
    {
        return dal.checkifScheduleIsPurchased(ShowId, date, time);
    }

    public static List<Show> getShowsBySearchBarOnlySchedule(String name)
    {
        return dal.getShowsBySearchBarOnlySchedule(name);
    }

    public static List<Show> getShowsByGenre(String genre)
    {
        return dal.getShowsByGenre(genre);
    }

    public static List<Show> getShowsByDatesAndGenre(DateTime from, DateTime to, String genre)
    {
        return dal.getShowsByDatesAndGenre(from, to, genre);
    }

    public static List<Schedule> getShowSchedules(int showId, DateTime fromDate)
    {
        return dal.getShowSchedules(showId, fromDate);
    }

    public static List<Show> getShowsByDate(DateTime dateFrom, DateTime dateTo)
    {
        //needs to be edited
        return dal.getShowsByDate(dateFrom, dateTo);
    }

    public static Show getShowsById(int showId)
    {
        return dal.getShowsById(showId);
    }

    public static bool checkIfScheduleExist(Schedule schedule, int Id)
    {
        return dal.checkIfScheduleExist(schedule, Id);
    }

    public static List<Schedule> getScheduleByName(String name)
    {
        //needs to be edited
        return dal.getScheduleByName(name);
    }

    public static List<Show> getAllShows()
    {
        return dal.getAllShows();
    }

    public static List<Show> getShowsByName(String name)
    {
        return dal.getShowsByName(name);
    }

    public static int getTicketsAmount(Show show, Schedule showSchedule)
    {
        return dal.getTicketsAmount(show, showSchedule);
    }

    public static int getSoldTicketsAmount(Show show, Schedule showSchedule)
    {
        return dal.getSoldTicketsAmount(show, showSchedule);
    }

    public static int getAvailableTicketsAmount(Show show, Schedule showSchedule)
    {
        if (showSchedule == null)
        {
            return -1;
        }

        int ticketAmount = getTicketsAmount(show, showSchedule);
        int ticketSold = getSoldTicketsAmount(show, showSchedule);

        int availableTickets = ticketAmount - ticketSold;

        return availableTickets;
    }
}