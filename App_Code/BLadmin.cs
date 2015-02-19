using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLadmin
/// </summary>
public class BLadmin
{
    private static DALadmin dal = new DALadmin();

	public BLadmin()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool addNewShow(Show show)
    {
        return dal.addNewShow(show);
    }

    public static bool deleteShow(Show show)
    {
        return dal.deleteShow(show);
    }

    public static bool updateShow(Show show)
    {
        return dal.updateShow(show);
    }

    public static bool addNewScheduleToShow(Schedule schedule, int showId)
    {
        return dal.addNewScheduleToShow(schedule, showId);
    }

    public static bool deleteSchedule(Schedule schedule, int showId)
    {
        return dal.deleteSchedule(schedule, showId);
    }

    public static bool updateSchedule(Schedule schedule, int showId, DateTime date, DateTime time)
    {
        return dal.updateSchedule(schedule, showId, date, time);
    }

    public static int getNextShowId()
    {
        return dal.getNextShowId();
    }
}