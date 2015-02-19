using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLclient
/// </summary>
public class BLclient
{
    private static DALclient dal = new DALclient();
    public const int PWD_LENGTH = 6;

	public BLclient()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static Client clientLogIn(String mail, String password)
    {
        return dal.clientLogIn(mail, password);
    }

    public static bool addNewClient(Client client, String password)
    {
        //check if mail already exists
        if (isClientExists(client.Mail))
            return false;

        return dal.addNewClient(client, password);
    }

    public static bool isClientExists(Client client)
    {
        return isClientExists(client.Mail);
    }

    public static bool isClientExists(string clientMail)
    {
        return dal.isClientExists(clientMail);
    }

    public static bool updateClientDetails(String mail, Client newClient)
    {
        return dal.updateClientDetails(mail, newClient);
    }

    public static List<Purchase> getClientsPurchases(string clientMail)
    {
        return dal.getClientsPurchases(clientMail);
    }

    public static List<Purchase> getClientPurchaseFrom(string clientMail, DateTime fromDate)
    {
        return dal.getClientPurchaseFrom(clientMail, fromDate);
    }

    public static List<Purchase> getClientPurchaseUp(string clientMail, DateTime upDate)
    {
        return dal.getClientPurchaseUp(clientMail, upDate);
    }

    public static String getClientLastName(string clientMail)
    {
        return dal.getClientLastName(clientMail);
    }

    //public string getClientPassword(string clientMail)
    //{
        //turn to check password! not get
    //}

    public static Client getClientDetails(string clientMail)
    {
        return dal.getClientDetails(clientMail);
    }

    public static bool changePassword(String mail, String oldPassword, String newPassword)
    {
        return dal.changePassword(mail, oldPassword, newPassword);
    }

    public static bool checkPwd(string pwd)
    {
        //check if a valid password
        if (pwd.Length < PWD_LENGTH && pwd.Length > 0)
        {
            // "Password cannot be less than " + PWD_LENGTH + " characters !";
            return false;
        }
        return true;
    }

    public static bool deleteClient(string mailClient)
    {
        return dal.deleteClient(mailClient);
    }

    public static List<Schedule> getScheduleByPoints(int points)
    {
        return dal.getScheduleByPoints(points);
    }

}