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
    public class Bookings
    {
        int _BookingId;
        int _CustomerId;
        int _EventId;
        decimal _Price;
        string _seatValue;
        int _status;
        int _seatId;

        

        public int BookingId { get => _BookingId; set => _BookingId = value; }
        public int CustomerId { get => _CustomerId; set => _CustomerId = value; }
        public int EventId { get => _EventId; set => _EventId = value; }
        public decimal Price { get => _Price; set => _Price = value; }
        public string seatValue { get => _seatValue; set => _seatValue = value; }
        public int Status { get => _status; set => _status = value; }
        public int SeatId { get => _seatId; set => _seatId = value; }

        public Bookings(DataRow row)
        {
            _BookingId = int.Parse(row["BookingId"].ToString());
            _CustomerId = int.Parse(row["CustomerId"].ToString());
            _EventId = int.Parse(row["EventId"].ToString());
            _Price = decimal.Parse(row["Price"].ToString());
            _seatValue = row["seatValue"].ToString();
            _status = int.Parse(row["status"].ToString());
            _seatId = int.Parse(row["seatId"].ToString());

        }

        public Bookings()
        {

            _BookingId = -1;

        }
        public void Save()
        {
            SqlConnection Connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
            Connect.Open();
            if (_BookingId == -1) // first time
            {

                string isql = "INSERT INTO Bookings (CustomerId, EventId Price, seatValue, status, seatId) VALUES (?,?,?,?,?,?)";
                SqlCommand Cmd = new SqlCommand(isql, Connect);
                Cmd.Parameters.AddWithValue("CustomerID", _CustomerId);
                Cmd.Parameters.AddWithValue("EventID", _EventId);
                Cmd.Parameters.AddWithValue("Price", _Price);
                Cmd.Parameters.AddWithValue("sealValue", _seatValue);
                Cmd.Parameters.AddWithValue("status", _status);
                Cmd.Parameters.AddWithValue("seatId", _seatId);



                Cmd.ExecuteNonQuery();
                //get the generated bookingid
                Cmd.CommandText = "Select @@Identity";
                _BookingId = (int)Cmd.ExecuteScalar();
            }
            else
            {
                //writing the infomation to the bookings table in database
                string isql = "UPDATE Bookings SET EventID=?,CustomerId=?,Seat=?,Price=?,status=?,seatId=? WHERE BookingID=?";
                SqlCommand Cmd = new SqlCommand(isql, Connect);
                Cmd.Parameters.AddWithValue("EventId", _EventId);
                Cmd.Parameters.AddWithValue("CustomerId", _CustomerId);
                Cmd.Parameters.AddWithValue("seatValue", _seatValue);
                Cmd.Parameters.AddWithValue("Price", _Price);
                Cmd.Parameters.AddWithValue("status", _status);
                Cmd.Parameters.AddWithValue("seatId", _seatId);
                Cmd.Parameters.AddWithValue("BookingID", _BookingId);
                Cmd.ExecuteNonQuery();
            }
        }
    }
}