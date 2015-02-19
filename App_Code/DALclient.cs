using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;

/// <summary>
/// Summary description for DAL
/// </summary>
public class DALclient
{
    private SqlConnection con;   

    public DALclient()
    {
        string str = DAL.STR;
        con = new SqlConnection(str);

        // TODO: Add constructor logic here
        //
    }

    public Client clientLogIn(String mail, String password)
    {
        con.Open();
        String getDetails = "select * from Client where Mail = '" + mail + "' AND Password = '" + password + "'";
        SqlCommand command = new SqlCommand(getDetails, con);
        SqlDataReader reader = command.ExecuteReader();

        reader.Read();

        if (!reader.HasRows)
        {
            con.Close();
            reader.Close();

            return null;
        }

        String fName = reader["FirstName"] + "";
        String lName = reader["LastName"] + "";
        String pNum = reader["PhoneNumber"] + "";
        String addr = reader["Address"] + "";
        String points = reader["Points"] + "";
        int num = Convert.ToInt32(points);
        Client client = new Client(fName, lName, mail, pNum, addr, num);

        con.Close();
        reader.Close();

        return client;
    }

    public bool addNewClient(Client client, String password)
    {
        //add if new
        con.Open();
        String addDetails = "INSERT INTO CLIENT VALUES('" + client.FirstName + "','" + client.LastName + "','" + client.Mail + "','" + client.PhoneNumber + "','" + client.Address + "','" + password + "'," + "0)";
        SqlCommand command = new SqlCommand(addDetails, con);

        if (command.ExecuteNonQuery() > 0)
        {
            con.Close();
            return true;
        }

        con.Close();
        return false;
    }

    public bool isClientExists(Client client)
    {
        //check if mail already exists
        return isClientExists(client.Mail);
    }

    public bool isClientExists(string clientMail)
    {
        //check if mail already exists
        con.Open();
        String getDetails = "select * from Client where Mail = '" + clientMail + "'";

        SqlCommand command = new SqlCommand(getDetails, con);
        SqlDataReader reader = command.ExecuteReader();

        reader.Read();

        if (!reader.HasRows)
        {
            con.Close();
            reader.Close();

            return false;
        }

        con.Close();
        reader.Close();

        return true;
    }


    public bool updateClientDetails(String mail, Client newClient)
    {

        con.Open();
        //	UPDATE Client SET FirstName = 'Guy', LastName = 'Shimoni', Mail = 'bnohad@gmail.com', PhoneNumber = '222', Address = 'israel', Password = 'soos' WHERE Mail = 'bnohad@gmail.com';
        String updateDetails = "UPDATE Client SET FirstName = '" + newClient.FirstName + "', LastName = '" + newClient.LastName + "', Mail = '" + newClient.Mail + "', PhoneNumber = '" + newClient.PhoneNumber + "', Address = '" + newClient.Address + "' WHERE Mail = '" + mail + "'";
        SqlCommand command = new SqlCommand(updateDetails, con);



        if (command.ExecuteNonQuery() > 0)
        {
            con.Close();

            return true;
        }
        con.Close();
        return false;
    } 

    public List<Purchase> getClientsPurchases(string clientMail)
    {
        con.Open();
        List<Purchase> purchases = new List<Purchase>();

        String getPurchases = "SELECT * FROM Purchases WHERE ClientMail = '" + clientMail + "'";
        SqlCommand command = new SqlCommand(getPurchases, con);
        SqlDataReader reader = command.ExecuteReader();

        int id, price, showId, quantity;
        DateTime time, date;
        string purchased;


        while (reader.Read())
        {
            id = Convert.ToInt32(reader["Id"]);
            price = Convert.ToInt32(reader["Price"]);
            quantity = Convert.ToInt32(reader["Quantity"]);
            showId = Convert.ToInt32(reader["ShowId"]);
            time = Convert.ToDateTime(reader["Time"] + "");
            date = Convert.ToDateTime(reader["Date"] + "");

            purchased = reader["PurchasedWith"] + "";

            Purchase purchase = new Purchase(id, price, showId, quantity, clientMail, date, time, purchased);

            purchases.Add(purchase);
        }

        con.Close();
        reader.Close();

        return purchases;
    }

    public List<Purchase> getClientPurchaseFrom(string clientMail, DateTime fromDate)
    {
        con.Open();
        List<Purchase> purchases = new List<Purchase>();

        string sqlFormattedDate = fromDate.ToString("yyyy-MM-dd HH:mm:ss");

        String getPurchases = "SELECT * FROM Purchases WHERE ClientMail = '" + clientMail + "' AND Date >= '" + sqlFormattedDate + "'";
        SqlCommand command = new SqlCommand(getPurchases, con);
        SqlDataReader reader = command.ExecuteReader();

        int id, price, showId, quantity;
        DateTime time, date;

        string purchased;


        while (reader.Read())
        {
            id = Convert.ToInt32(reader["Id"]);
            price = Convert.ToInt32(reader["Price"]);
            quantity = Convert.ToInt32(reader["Quantity"]);
            showId = Convert.ToInt32(reader["ShowId"]);
            time = Convert.ToDateTime(reader["Time"] + "");
            date = Convert.ToDateTime(reader["Date"] + "");

            purchased = reader["PurchasedWith"] + "";

            Purchase purchase = new Purchase(id, price, showId, quantity, clientMail, date, time, purchased);

            purchases.Add(purchase);
        }

        con.Close();
        reader.Close();

        return purchases;

    }

    public List<Purchase> getClientPurchaseUp(string clientMail, DateTime upDate)
    {
        con.Open();
        List<Purchase> purchases = new List<Purchase>();

        string sqlFormattedDate = upDate.ToString("yyyy-MM-dd HH:mm:ss");

        String getPurchases = "SELECT * FROM Purchases WHERE ClientMail = '" + clientMail + "' AND Date <= '" + sqlFormattedDate + "'";
        SqlCommand command = new SqlCommand(getPurchases, con);
        SqlDataReader reader = command.ExecuteReader();

        int id, price, showId, quantity;
        DateTime time, date;
        string purchased;

        while (reader.Read())
        {
            id = Convert.ToInt32(reader["Id"]);
            price = Convert.ToInt32(reader["Price"]);
            quantity = Convert.ToInt32(reader["Quantity"]);
            showId = Convert.ToInt32(reader["ShowId"]);
            time = Convert.ToDateTime(reader["Time"] + "");
            date = Convert.ToDateTime(reader["Date"] + "");
            purchased = reader["PurchasedWith"] + "";

            Purchase purchase = new Purchase(id, price, showId, quantity, clientMail, date, time, purchased);

            purchases.Add(purchase);
        }

        con.Close();
        reader.Close();

        return purchases;

    }

    public String getClientLastName(string clientMail)
    {
        con.Open();
        string lastName = "";
        String getLastName = "SELECT LastName FROM Client WHERE Mail = '" + clientMail + "'";
        SqlCommand command = new SqlCommand(getLastName, con);
        SqlDataReader reader = command.ExecuteReader();

        reader.Read();



        if (!reader.HasRows)
        {
            con.Close();
            reader.Close();
        }
        else
        {
            lastName = reader["LastName"] + "";
        }
        con.Close();
        reader.Close();
        return lastName;
    }

    public string getClientPassword(string clientMail)
    {
        con.Open();
        string password = "";
        String getPwd = "SELECT Password FROM Client WHERE Mail = '" + clientMail + "'";
        SqlCommand command = new SqlCommand(getPwd, con);
        SqlDataReader reader = command.ExecuteReader();

        reader.Read();



        if (!reader.HasRows)
        {
            con.Close();
            reader.Close();
        }
        else
        {
            password = reader["Password"] + "";
        }

        con.Close();
        reader.Close();
        return password;
    }
    public Client getClientDetails(string clientMail)
    {
        con.Open();

        Client client = null;
        string first = "", last = "", mail = "", address = "", phone = "";
        int points = 0;

        String getDetails = "SELECT * FROM Client WHERE Mail = '" + clientMail + "'";
        SqlCommand command = new SqlCommand(getDetails, con);
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            first = reader["FirstName"] + "";
            last = reader["LastName"] + "";
            mail = clientMail;
            address = reader["Address"] + "";
            phone = reader["PhoneNumber"] + "";
            points = Convert.ToInt32(reader["Points"]);

            client = new Client(first, last, mail, phone, address, points);
        }

        con.Close();
        reader.Close();

        return client;
    }

    public bool changePassword(String mail, String oldPassword, String newPassword)
    {

        con.Open();
        //	UPDATE Client SET FirstName = 'Guy', LastName = 'Shimoni', Mail = 'bnohad@gmail.com', PhoneNumber = '222', Address = 'israel', Password = 'soos' WHERE Mail = 'bnohad@gmail.com';
        String changePwd = "UPDATE Client SET Password = '" + newPassword + "' WHERE Mail = '" + mail + "' AND Password = '" + oldPassword + "'";
        SqlCommand command = new SqlCommand(changePwd, con);



        if (command.ExecuteNonQuery() > 0)
        {
            con.Close();

            return true;
        }
        con.Close();
        return false;
    }

    public bool deleteClient(string mailClient)
    {
        con.Open();

        String delete = "DELETE FROM Client WHERE Mail = '" + mailClient + "'";
        SqlCommand command = new SqlCommand(delete, con);

        if (command.ExecuteNonQuery() < 0)
        {
            con.Close();
            return false;
        }

        con.Close();
        return true;
    }

    public List<Schedule> getScheduleByPoints(int points)
    {
        con.Open();
        List<Schedule> schedules = new List<Schedule>();

        string sqlFormattedDate = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");


        String getSchedules = "SELECT * FROM Schedule WHERE RegularPrice <= '" + points + "' AND Date >= '" + sqlFormattedDate + "' ORDER BY Date, Time";
        SqlCommand command = new SqlCommand(getSchedules, con);
        SqlDataReader reader = command.ExecuteReader();

        int regularPrice, childPrice, soldierPrice, studentPrice, vipPrice, available;
        string location;
        DateTime time, date;


        while (reader.Read())
        {
            regularPrice = Convert.ToInt32(reader["RegularPrice"]);
            childPrice = Convert.ToInt32(reader["ChildPrice"]);
            soldierPrice = Convert.ToInt32(reader["SoldierPrice"]);
            studentPrice = Convert.ToInt32(reader["StudentPrice"]);
            vipPrice = Convert.ToInt32(reader["VipPrice"]);
            available = Convert.ToInt32(reader["AvailableTickets"]);
            time = Convert.ToDateTime(reader["Time"] + "");
            date = Convert.ToDateTime(reader["Date"] + "");
            location = reader["Location"] + "";

            Schedule schedule = new Schedule(location, regularPrice, childPrice, soldierPrice, studentPrice, vipPrice, available, date, time);
            schedule.ShowId = Convert.ToInt32(reader["Id"]);
            schedules.Add(schedule);
        }

        con.Close();
        reader.Close();

        return schedules;
    }


}