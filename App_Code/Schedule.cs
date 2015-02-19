using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Schedule
/// </summary>
public class Schedule
{
	private String location;
    private int regularPrice, childPrice, soldierPrice, studentPrice, vipPrice, availableTickets, showId;
	private DateTime date;
	private DateTime time;

	public Schedule(String location, int regularPrice, int childPrice, int soldierPrice, int studentPrice, int vipPrice, int availableTickets,  DateTime date, DateTime time)
	{
		//
		// TODO: Add constructor logic here
		//
		this.location = location;
		this.regularPrice = regularPrice;
		this.childPrice = childPrice;
		this.soldierPrice = soldierPrice;
		this.studentPrice = studentPrice;
		this.vipPrice = vipPrice;
		this.time = time;
		this.date = date;
		this.availableTickets = availableTickets;
	}

    public int ShowId
    {
        set
        {
            showId = value;
        }
        get
        {
            return showId;
        }
    }
	public int RegularPrice
	{
		set
		{
			regularPrice = value;
		}
		get
		{
			return regularPrice;
		}
	}

	public int ChildPrice
	{
		set
		{
			childPrice = value;
		}
		get
		{
			return childPrice;
		}
	}
	public int SoldierPrice
	{
		set
		{
			soldierPrice = value;
		}
		get
		{
			return soldierPrice;
		}
	}
	public int StudentPrice
	{
		set
		{
			studentPrice = value;
		}
		get
		{
			return studentPrice;
		}
	}
	public int VipPrice
	{
		set
		{
			vipPrice = value;
		}
		get
		{
			return vipPrice;
		}
	}
	public int AvailableTickets
	{
		set
		{
			availableTickets = value;
		}
		get
		{
			return availableTickets;
		}
	}

	public String Location
	{
		set
		{
			location = value;
		}
		get
		{
			return location;
		}
	}

	public DateTime Date
	{
		set
		{
			date = value;
		}
		get
		{
			return date;
		}
	}
	public DateTime Time
	{
		set
		{
			time = value;
		}
		get
		{
			return time;
		}
	}
}