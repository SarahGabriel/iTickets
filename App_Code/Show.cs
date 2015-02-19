using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Show
/// </summary>
public class Show
{
	private int id, duration, points;
	private String name, genre ,description;
	private List<Schedule> schedules;
   

	public Show(int id, int duration, int points, String name, String genre , String description)
	{
		//
		// TODO: Add constructor logic here
		//
        this.description = description;
		this.id = id;
		this.duration = duration;
		this.points = points;
		this.name = name;
		this.genre = genre;
		this.schedules = new List<Schedule>();
	}
	public int Id
	{
		set
		{
			id = value;
		}
		get
		{
			return id;
		}
	}
	public String Name
	{
		set
		{
			name = value;
		}
		get
		{
			return name;
		}
	}
	public String Genre
	{
		set
		{
			genre = value;
		}
		get
		{
			return genre;
		}
	}

    public String Description
    {
        set
        {
            description = value;
        }
        get
        {
            return description;
        }
    }

	public int Duration
	{
		set
		{
			duration = value;
		}
		get
		{
			return duration;
		}
	}
	public int Points
	{
		set
		{
			points = value;
		}
		get
		{
			return points;
		}
	}


}