using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DALGenre
/// </summary>
public class DALGenre
{
    private SqlConnection con;

	public DALGenre()
	{
        string str = DAL.STR;
        con = new SqlConnection(str);
	}

    public bool AddNewGenre(String genre)
    {
        con.Open();
        String addGenre = "INSERT INTO Genre VALUES('" + genre + "')";
        SqlCommand command = new SqlCommand(addGenre, con);

        if (command.ExecuteNonQuery() > 0)
        {
            con.Close();
            return true;
        }

        con.Close();
        return false;
    }

    public bool deleteGenre(String genre)
    {
        con.Open();

        String delete = "DELETE FROM Genre WHERE Genre = '" + genre + "'";
        SqlCommand command = new SqlCommand(delete, con);

        if (command.ExecuteNonQuery() < 0)
        {
            con.Close();
            return false;
        }

        con.Close();
        return true;
    }

    public bool IsGenreExists(String genre)
    {
        con.Open();
        String cmd = "select * from Genre where Genre = '" + genre + "'";

        SqlCommand command = new SqlCommand(cmd, con);
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

    public bool isGenreUsed(String genre)
    {
        con.Open();
        String cmd = "select * from Shows where Genre = '" + genre + "'";

        SqlCommand command = new SqlCommand(cmd, con);
        SqlDataReader reader = command.ExecuteReader();

        reader.Read();

        if (!reader.HasRows)
        {
            con.Close();
            reader.Close();

            return false; //if not used
        }

        con.Close();
        reader.Close();

        return true; //if used
    }
}