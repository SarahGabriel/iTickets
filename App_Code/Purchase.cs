using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Purchase
/// </summary>
public class Purchase
{
	private int id, price, showId, quantity;
	private string clientMail, purchasedWith;
	private DateTime time, date;

	public Purchase(int id, int price, int showId, int quantity, string mail, DateTime date, DateTime time, string purchasedwith)
	{
		this.id = id;
		this.price = price;
		this.showId = showId;
		this.quantity = quantity;
		this.clientMail = mail;
		this.date = date;
		this.time = time;
        this.purchasedWith = purchasedwith;
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

	public int Price
	{
		set
		{
			price = value;
		}
		get
		{
			return price;
		}
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

	public int Quantity
	{
		set
		{
			quantity = value;
		}
		get
		{
			return quantity;
		}
	}

	public String ClientMail
	{
		set
		{
			clientMail = value;
		}
		get
		{
			return clientMail;
		}
	}

    public String PurchasedWith
    {
        set
        {
            purchasedWith = value;
        }
        get
        {
            return purchasedWith;
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