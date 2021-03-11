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
    public class DatabaseLoader
    {
        static void GetCustomers(List<Customers> CustomerList)
        {
            CustomerList = new List<Customers>();
            SqlConnection Connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
            string sql = "SELECT * FROM Customers";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Connect);
            DataTable DataOutcome = new DataTable();
            adapter.Fill(DataOutcome);

            foreach (DataRow row in DataOutcome.Rows)
            {
                Customers Out = new Customers(row);
                CustomerList.Add(Out);
            }

        }
        static void GetBooking(List<Bookings> BookingList)
        {
            BookingList = new List<Bookings>();
            SqlConnection Connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
            string sql = "SELECT * FROM Bookings";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Connect);
            DataTable DataOutcome = new DataTable();
            adapter.Fill(DataOutcome);

            foreach (DataRow row in DataOutcome.Rows)
            {
                Bookings Out = new Bookings(row);
                BookingList.Add(Out);
            }
        }
        static void GetEvent(List<Events> EventList)
        {
            EventList = new List<Events>();
            SqlConnection Connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
            string sql = "SELECT * FROM Events";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Connect);
            DataTable DataOutcome = new DataTable();
            adapter.Fill(DataOutcome);

            foreach (DataRow row in DataOutcome.Rows)
            {
                Events Out = new Events(row);
                EventList.Add(Out);
            }
        }

        static void GetVenue(List<Venues> VenueList)
        {
            VenueList = new List<Venues>();
            SqlConnection Connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
            string sql = "SELECT * FROM Venues";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Connect);
            DataTable DataOutcome = new DataTable();
            adapter.Fill(DataOutcome);

            foreach (DataRow row in DataOutcome.Rows)
            {
                Venues Out = new Venues(row);
                VenueList.Add(Out);
            }
        }
        static void GetSeats(List<SeatsClass> SeatsList)
        {
            SeatsList = new List<SeatsClass>();
            SqlConnection Connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
            string sql = "SELECT * FROM Seats";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Connect);
            DataTable DataOutcome = new DataTable();
            adapter.Fill(DataOutcome);

            foreach (DataRow row in DataOutcome.Rows)
            {
                SeatsClass Out = new SeatsClass(row);
                SeatsList.Add(Out);
            }
        }

    }
}