using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Client
/// </summary>
public class Client
{
	private String firstName, lastName, mail, phoneNumber, address;
	private int points;

	public Client(String firstName, String lastName, String mail, String phoneNumber, String address, int points)
	{
		//
		// TODO: Add constructor logic here
		//
		this.firstName = firstName;
		this.lastName = lastName;
		this.mail = mail;
		this.phoneNumber = phoneNumber;
		this.address = address;
		this.points = points;
	}

	public String FirstName
	{
		set
		{
			firstName = value;
		}
		get
		{
			return firstName;
		}
	}
	public String LastName
	{
		set
		{
			lastName = value;
		}
		get
		{
			return lastName;
		}
	}
	public String Mail
	{
		set
		{
			mail = value;
		}
		get
		{
			return mail;
		}
	}
	public String PhoneNumber
	{
		set
		{
			phoneNumber = value;
		}
		get
		{
			return phoneNumber;
		}
	}
	public String Address
	{
		set
		{
			address = value;
		}
		get
		{
			return address;
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
    public String toString()
    {
        String str = "";
        str += FirstName + " " + LastName + " " + Mail + " " + Address + " " + PhoneNumber + " " + points;
        return str;
    }
}