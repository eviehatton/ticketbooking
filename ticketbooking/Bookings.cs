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
        int _Seat;
        

        public int BookingId { get => _BookingId; set => _BookingId = value; }
        public int CustomerId { get => _CustomerId; set => _CustomerId = value; }
        public int EventId { get => _EventId; set => _EventId = value; }
        public decimal Price { get => _Price; set => _Price = value; }
        public int Seat { get => _Seat; set => _Seat = value; }
        

        public Bookings(DataRow row)
        {
            _BookingId = int.Parse(row["BookingID"].ToString());
            _CustomerId = int.Parse(row["CustomerID"].ToString());
            _EventId = int.Parse(row["EventID"].ToString());
            _Price = decimal.Parse(row["Price"].ToString());
            _Seat = int.Parse(row["Seat"].ToString());
            
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

                string isql = "INSERT INTO Bookings (CustomerId, EventId Price, Seat) VALUES (?,?,?,?)";
                SqlCommand Cmd = new SqlCommand(isql, Connect);
                Cmd.Parameters.AddWithValue("CustomerID", _CustomerId);
                Cmd.Parameters.AddWithValue("EventID", _EventId);
                Cmd.Parameters.AddWithValue("Price", _Price);
                Cmd.Parameters.AddWithValue("Seat", _Seat);
                


                Cmd.ExecuteNonQuery();
                //get the generated bookingid
                Cmd.CommandText = "Select @@Identity";
                _BookingId = (int)Cmd.ExecuteScalar();
            }
            else
            {
                string isql = "UPDATE Bookings SET EventID=?,CustomerId=?,Seat=?,Price=? WHERE BookingID=?";
                SqlCommand Cmd = new SqlCommand(isql, Connect);
                Cmd.Parameters.AddWithValue("EventID", _EventId);
                Cmd.Parameters.AddWithValue("CustomerID", _CustomerId);
                Cmd.Parameters.AddWithValue("SeatBlock", _Seat);
                ;
                Cmd.Parameters.AddWithValue("Price", _Price);
                Cmd.Parameters.AddWithValue("BookingID", _BookingId);
                Cmd.ExecuteNonQuery();
            }
        }
    }
}