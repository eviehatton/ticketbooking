using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace ticketbooking
{
    public class Admin
    {
        int _AdminId;
        string _Username;
        string _Password;

        public int AdminId { get => _AdminId; set => _AdminId = value; }
        public string Username { get => _Username; set => _Username = value; }
        public string Password { get => _Password; set => _Password = value; }

        public Admin(DataRow row)
        {
            _AdminId = int.Parse(row["VenueId"].ToString());
            _Username = row["VenueLocation"].ToString();
            _Password = row["VenueName"].ToString();
            
        }

        public Admin()
        {
            _AdminId = -1;
        }
        public void Save()
        {
            SqlConnection Connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
            Connect.Open();
            if (_AdminId == -1) // first time
            {

                string isql = "INSERT INTO AdminLogins (Username,Password) VALUES (?,?)";
                SqlCommand Cmd = new SqlCommand(isql, Connect);
                Cmd.Parameters.AddWithValue("Username", _Username);
                Cmd.Parameters.AddWithValue("Password", _Password);
                

                Cmd.ExecuteNonQuery();
                //get the generated customerid
                Cmd.CommandText = "Select @@Identity";
                _AdminId = (int)Cmd.ExecuteScalar();
            }
            else
            {
                string isql = "UPDATE AdminLogins SET Username=?,Password=? WHERE AdminId=?";
                SqlCommand Cmd = new SqlCommand(isql, Connect);
                Cmd.Parameters.AddWithValue("Username", _Username);
                Cmd.Parameters.AddWithValue("Password", _Password);
                Cmd.Parameters.AddWithValue("AdminId", _AdminId);
                Cmd.ExecuteNonQuery();
            }
        }
    }
}
