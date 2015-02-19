using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLpurchase
/// </summary>
public class BLpurchase
{
    private static DALpurchase dal = new DALpurchase();

	public BLpurchase()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool newPurchase(Purchase purchase, int points)
    {
        return dal.newPurchase(purchase, points);
    }

    public static bool cancelPurchase(int purchaseId, String mail, int points)
    {
        return dal.cancelPurchase(purchaseId, mail, points);
    }

    public static bool cancelPurchaseByPoints(int purchaseId, String mail, int points)
    {
        return dal.cancelPurchaseByPoints(purchaseId, mail, points);
    }

    public static bool deleteOldPurchase(int purchaseId)
    {
        return dal.deleteOldPurchase(purchaseId);
    }

    public static int getPurchaseTicketsQuantity(int purchaseId)
    {
        return dal.getPurchaseTicketsQuantity(purchaseId);
    }

    public static string getPurchasePaymentMethod(int purchaseId)
    {
        return dal.getPurchasePaymentMethod(purchaseId);
    }

    public static bool addPointsToClient(String mail, int points)
    {
        return dal.addPointsToClient(mail, points);
    }

    public static bool cancelPointsToClient(String mail, int points)
    {
        //check if client exists
        Client client = BLclient.getClientDetails(mail);

        if (client == null)
            return false;

        //check if after cancel points are less than 0
        if (client.Points - points > 0)
            return dal.cancelPointsToClient(mail, points);

        return dal.cancelPointsToClient(mail, client.Points);
    }

    public static int getNextPurchaseId()
    {
        return dal.getNextPurchaseId();
    }

    public static int getShowTotalSellings(Show show)
    {
        return dal.getShowTotalSellings(show);
    }

    public static Purchase getPurchaseById(int purchaseId)
    {
        return dal.getPurchaseById(purchaseId);
    }
}