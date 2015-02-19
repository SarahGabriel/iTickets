using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;

public class DALpurchase
{
    private SqlConnection con;

    public DALpurchase()
    {
        string str = DAL.STR;
        con = new SqlConnection(str);
    }


    public bool newPurchase(Purchase purchase, int points)
    {
        con.Open();

        string sqlFormattedDate = purchase.Date.ToString("yyyy-MM-dd HH:mm:ss");
        string sqlFormattedTime = purchase.Time.ToString("HH:mm:ss");

        String addPurchase = "INSERT INTO Purchases VALUES('" + purchase.Id + "','" + purchase.ClientMail + "','" + purchase.Price + "','" + purchase.Quantity + "','" + purchase.ShowId + "','" + sqlFormattedDate + "','" + sqlFormattedTime + "','" + purchase.PurchasedWith + "')";
        SqlCommand command = new SqlCommand(addPurchase, con);

        if (command.ExecuteNonQuery() > 0)
        {
            con.Close();
            addPointsToClient(purchase.ClientMail, points);

            return true;
        }
        con.Close();
        return false;

    }

    public bool cancelPurchase(int purchaseId, String mail, int points)
    {
        con.Open();
        String delPurchase = "DELETE FROM Purchases WHERE Id = '" + purchaseId + "'";
        SqlCommand command = new SqlCommand(delPurchase, con);

        if (command.ExecuteNonQuery() > 0)
        {
            con.Close();

            cancelPointsToClient(mail, points); //cancel points earned with the purchase
            return true;
        }

        con.Close();
        return false;
    }

    public bool cancelPurchaseByPoints(int purchaseId, String mail, int points)
    {
        con.Open();
        String delPurchase = "DELETE FROM Purchases WHERE Id = '" + purchaseId + "'";
        SqlCommand command = new SqlCommand(delPurchase, con);

        if (command.ExecuteNonQuery() > 0)
        {
            con.Close();

            addPointsToClient(mail, points);
            return true;
        }
        con.Close();
        return false;
    }

    public bool deleteOldPurchase(int purchaseId)
    {
        con.Open();
        String delPurchase = "DELETE FROM Purchases WHERE Id = '" + purchaseId + "'";
        SqlCommand command = new SqlCommand(delPurchase, con);

        if (command.ExecuteNonQuery() > 0)
        {
            con.Close();
            return true;
        }
        con.Close();
        return false;
    }

    public int getPurchaseTicketsQuantity(int purchaseId)
    {
        con.Open();

        String getQuantity = "SELECT Quantity FROM Purchases WHERE Id = '" + purchaseId + "'";
        SqlCommand command = new SqlCommand(getQuantity, con);
        SqlDataReader reader = command.ExecuteReader();

        if (reader.Read() == false)
        {
            reader.Close();
            con.Close();
            return 0;
        }

        int quantity = Convert.ToInt32(reader[0]);

        reader.Close();
        con.Close();

        return quantity;
    }

    public string getPurchasePaymentMethod(int purchaseId)
    {
        con.Open();

        String getPayment = "SELECT PurchasedWith FROM Purchases WHERE Id = '" + purchaseId + "'";
        SqlCommand command = new SqlCommand(getPayment, con);
        SqlDataReader reader = command.ExecuteReader();

        if (reader.Read() == false)
        {
            reader.Close();
            con.Close();
            return null;
        }

        string payment = reader[0] + "";

        reader.Close();
        con.Close();

        return payment;
    }

    public bool addPointsToClient(String mail, int points)
    {

        con.Open();
        //	UPDATE Client SET FirstName = 'Guy', LastName = 'Shimoni', Mail = 'bnohad@gmail.com', PhoneNumber = '222', Address = 'israel', Password = 'soos' WHERE Mail = 'bnohad@gmail.com';
        String addPoints = "UPDATE Client SET Points = Points + '" + points + "' WHERE Mail = '" + mail + "'";
        SqlCommand command = new SqlCommand(addPoints, con);



        if (command.ExecuteNonQuery() > 0)
        {
            con.Close();

            return true;
        }

        con.Close();
        return false;
    }

    public bool cancelPointsToClient(String mail, int points)
    {

        con.Open();
        //	UPDATE Client SET FirstName = 'Guy', LastName = 'Shimoni', Mail = 'bnohad@gmail.com', PhoneNumber = '222', Address = 'israel', Password = 'soos' WHERE Mail = 'bnohad@gmail.com';
        String addPoints = "UPDATE Client SET Points = Points - '" + points + "' WHERE Mail = '" + mail + "'";
        SqlCommand command = new SqlCommand(addPoints, con);

        if (command.ExecuteNonQuery() > 0)
        {
            con.Close();

            return true;
        }

        con.Close();
        return false;
    }

    public int getNextPurchaseId()
    {
        con.Open();
        String getMaxId = "SELECT MAX(Id) As MaxId FROM Purchases";
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
            id = 10000;
        }

        reader.Close();
        con.Close();
        return id + 1;
    }

    public int getShowTotalSellings(Show show)
    {
        con.Open();
        String getTotal = "SELECT SUM(Price) As TotalSellings FROM Purchases WHERE ShowId='" + show.Id + "'";
        SqlCommand command = new SqlCommand(getTotal, con);

        SqlDataReader reader = command.ExecuteReader();

        int sum;
        reader.Read();
        if (!reader.IsDBNull(0))
        {
            sum = Convert.ToInt32(reader["TotalSellings"]);
        }
        else
        {
            sum = 0;
        }

        reader.Close();
        con.Close();

        return sum;
    }

    public Purchase getPurchaseById(int purchaseId)
    {
        con.Open();

        //string sqlFormattedDate = upDate.ToString("yyyy-MM-dd HH:mm:ss");

        String getPurchases = "SELECT * FROM Purchases WHERE Id = '" + purchaseId + "'";
        SqlCommand command = new SqlCommand(getPurchases, con);
        SqlDataReader reader = command.ExecuteReader();

        int id, price, showId, quantity;
        DateTime time, date;
        string purchased, clientMail;

        while (reader.Read())
        {
            id = Convert.ToInt32(reader["Id"]);
            price = Convert.ToInt32(reader["Price"]);
            quantity = Convert.ToInt32(reader["Quantity"]);
            showId = Convert.ToInt32(reader["ShowId"]);
            time = Convert.ToDateTime(reader["Time"] + "");
            date = Convert.ToDateTime(reader["Date"] + "");
            purchased = reader["PurchasedWith"] + "";
            clientMail = reader["ClientMail"] + "";

            Purchase purchase = new Purchase(id, price, showId, quantity, clientMail, date, time, purchased);

            con.Close();
            reader.Close();

            return purchase;
        }

        con.Close();
        reader.Close();

        return null;
    }
}