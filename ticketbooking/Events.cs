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
    public class Events
    {
        int _EventId;
        int _VenueId;
        string _EventName;
        decimal _EventPrice;
        int _TicketsRemaning;
        DateTime _DateEvent;

        public int EventId { get => _EventId; set => _EventId = value; }
        public int VenueId { get => _VenueId; set => _VenueId = value; }
        public string EventName { get => _EventName; set => _EventName = value; }
        public decimal EventPrice { get => _EventPrice; set => _EventPrice = value; }
        public int TicketsRemaning { get => _TicketsRemaning; set => _TicketsRemaning = value; }
        public DateTime DateEvent { get => _DateEvent; set => _DateEvent = value; }
        public Events(DataRow row)
        {
            _EventId = int.Parse(row["EventId"].ToString());
            _VenueId = int.Parse(row["VenueId"].ToString());
            _EventName = (row["EventName"].ToString());
            _EventPrice = decimal.Parse(row["EventPrice"].ToString());
            _TicketsRemaning = int.Parse(row["TicketsRemaning"].ToString());
            _DateEvent = DateTime.Parse(row["EventDate"].ToString());
        }

        public Events()
        {

            _EventId = -1;

        }
        public void Save()
        {
            SqlConnection Connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
            Connect.Open();
            if (_EventId == -1) // first time
            {

                string isql = "INSERT INTO Events (VenueId, EventName, EventPrice, TicketsRemaning, DateEvent) VALUES (?,?,?,?,?)";
                SqlCommand Cmd = new SqlCommand(isql, Connect);
                Cmd.Parameters.AddWithValue("VenueId", _VenueId);
                Cmd.Parameters.AddWithValue("EventName", _EventName);
                Cmd.Parameters.AddWithValue("EventPrice", _EventPrice);
                Cmd.Parameters.AddWithValue("TicketsRemaning", _TicketsRemaning);
                Cmd.Parameters.AddWithValue("DateEvent", _DateEvent);


                Cmd.ExecuteNonQuery();
                //get the generated eventid
                Cmd.CommandText = "Select @@Identity";
                _EventId = (int)Cmd.ExecuteScalar();
            }
            else
            {
                string isql = "UPDATE Events SET VenueId=?,EventName=?,EventPrice=?,TicketsRemaning=?,DateEvent=? WHERE EventID=?";
                SqlCommand Cmd = new SqlCommand(isql, Connect);
                Cmd.Parameters.AddWithValue("VenueID", _VenueId);
                Cmd.Parameters.AddWithValue("EventName", _EventName);
                Cmd.Parameters.AddWithValue("EventPrice", _EventPrice);
                Cmd.Parameters.AddWithValue("TicketsRemaning", _TicketsRemaning);
                Cmd.Parameters.AddWithValue("DateEvent", DateEvent);
                Cmd.Parameters.AddWithValue("EventId", _EventId);
                Cmd.ExecuteNonQuery();
            }
        }


    }
}

