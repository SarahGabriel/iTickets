using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLGenre
/// </summary>
public class BLGenre
{
    private static DALGenre dal = new DALGenre();

	public BLGenre()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool IsGenreExists(String genre)
    {
        return dal.IsGenreExists(genre);
    }

    public static bool AddNewGenre(String genre)
    {
        if (IsGenreExists(genre))
        {
            return false;
        }

        return dal.AddNewGenre(genre);
    }

    public static bool deleteGenre(String genre)
    {
        if (isGenreUsed(genre)) //if genre used by a show
            return false;

        return dal.deleteGenre(genre);
    }

    public static bool isGenreUsed(String genre)
    {
        return dal.isGenreUsed(genre);
    }
}