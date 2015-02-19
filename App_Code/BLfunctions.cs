using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLfunctions
/// </summary>
public class BLfunctions
{
	public BLfunctions()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string replaceSpecialChars(string str)
    {
        string newStr = str.Trim();
        newStr = str.Replace("'", "''");
        newStr = newStr.Replace('"', '\"');

        return newStr;
    }

    public static bool isStringValid(string str)
    {
        foreach (char c in str)
        {
            if ((c < 'A' || c > 'z') && c != ' ')
            {
                return false;
            }
        }

        return true;
    }

    public static bool isPhoneValid(string phone)
    {
        foreach (char c in phone)
        {
            if (c < '0' || c > '9')
            {
                return false;
            }
        }

        return true;
    }
}