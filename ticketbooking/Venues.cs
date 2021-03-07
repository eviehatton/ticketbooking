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
    public class Venues
    {
        int _VenueId;
        string _VenueLocation;
        string _VenueName;
        int _Capacity;

        public int VenueId { get => _VenueId; set => _VenueId = value; }
        public string VenueLocation { get => _VenueLocation; set => _VenueLocation = value; }
        public string VenueName { get => _VenueName; set => _VenueName = value; }
        public int Capacity { get => _Capacity; set => _Capacity = value; }

        public Venues(DataRow row)
        {
            _VenueId = int.Parse(row["VenueId"].ToString());
            _VenueLocation = row["VenueLocation"].ToString();
            _VenueName = row["VenueName"].ToString();
            _Capacity = int.Parse(row["Capacity"].ToString());
        }

        public Venues()
        {
            _VenueId = -1;
        }
        public void Save()
        {
            SqlConnection Connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
            Connect.Open();
            if (_VenueId == -1) // first time
            {

                string isql = "INSERT INTO Venue (VenueLocation,VenueName,Capacity,) VALUES (?,?,?)";
                SqlCommand Cmd = new SqlCommand(isql, Connect);
                Cmd.Parameters.AddWithValue("VenueLocation", _VenueLocation);
                Cmd.Parameters.AddWithValue("VenueName", _VenueName);
                Cmd.Parameters.AddWithValue("Capcity", _Capacity);

                Cmd.ExecuteNonQuery();
                //get the generated customerid
                Cmd.CommandText = "Select @@Identity";
                _VenueId = (int)Cmd.ExecuteScalar();
            }
            else
            {
                string isql = "UPDATE Venue SET VenueLocation=?,venueName=?,Capacity=? WHERE VenueId=?";
                SqlCommand Cmd = new SqlCommand(isql, Connect);
                Cmd.Parameters.AddWithValue("VenueLocation", _VenueLocation);
                Cmd.Parameters.AddWithValue("VenueName", _VenueName);
                Cmd.Parameters.AddWithValue("Capacity", _Capacity);
                Cmd.Parameters.AddWithValue("VenueId", _VenueId);
                Cmd.ExecuteNonQuery();
            }
        }
    }
}

