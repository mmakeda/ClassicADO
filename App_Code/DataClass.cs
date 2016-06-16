using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Libraries need to talk to database
using System.Data; //
using System.Data.SqlClient;
using System.Configuration;//helps C# code read the web config file

/// <summary>
/// This class will connect to the database
/// It will have methods to list the authors
/// It will also return books
/// Makeda Michael 2016-4-18
/// Classic ADO
/// 
/// </summary>
public class DataClass
{
    SqlConnection connect; 

    public DataClass()
    {
        //ConfigurationManager talks to the config file
        connect = new SqlConnection
            (ConfigurationManager.
            ConnectionStrings["BookReviewConnectionString"].ToString());
    }//end constructor
    public DataTable GetServices()
    {
        DataTable tbl = null;

        //Objects (Connect, command, reader tbl)
        string sql = "Select GrantTypeKey, GrantTypeName from GrantType";
        SqlCommand cmd = new SqlCommand(sql, connect);

        tbl = new DataTable();
        tbl = ReadData(cmd);

        return tbl; 
  
    }
    public DataTable GetGrants(int grantTypeKey)
    {
        DataTable tbl = null;
        string sql = "Select *from GrantRequestDate, GrantRequestExplanation, GrantRequestAmount "
            + "from GrantRequest "
            + "Where GrantTypeKey=@Key";

        SqlCommand cmd = new SqlCommand(sql,connect);
        cmd.Parameters.AddWithValue("@Key", grantTypeKey);

        tbl = ReadData(cmd);
        return tbl; 
    }
    private DataTable ReadData(SqlCommand cmd)
   {
        SqlDataReader reader = null;
        DataTable tbl = new DataTable();

        connect.Open(); // open the connection
        reader = cmd.ExecuteReader(); //Executes the connection
        tbl.Load(reader);
        reader.Close();
        connect.Close();

        return tbl;
    }

}//end class