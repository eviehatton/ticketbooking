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
    public class UserBooking
    {
        public static void bookingEvent()
        {
            Seats.DisplaySeats(3);
        }
        public static void BookEvent()
        {
            ViewEvents.DisplayEvents();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Booking");
            Console.WriteLine("Enter event number to book :");
            string eventbook = Console.ReadLine();
            Console.Clear();
            using (SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf'; Integrated Security = True"))
            {
                connection.Open();
                string oString = "SELECT EventName, EventPrice, DateEvent FROM Events WHERE EventId =@eventbook";
                SqlCommand command = new SqlCommand(oString, connection);
                command.Parameters.AddWithValue("@eventbook", eventbook);
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("->");
                            Console.WriteLine("| Event Name: " + (string)reader["EventName"]);
                            Console.WriteLine("| Event Price: " + (double)reader["EventPrice"]);
                            Console.WriteLine("| Event Date: " + (DateTime)reader["DateEvent"]);
                        }

                    }
                }
            }
            
            Console.WriteLine("Confirm this is the event you want to book (Y) (N)");
            string confirmA = (Console.ReadLine());
            if (confirmA.ToUpper() == "Y")
            {
                Console.WriteLine("redirecting to booking page...");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                bookingEvent();
                
            }
            else
            {
                Program.Menu();
            }
        }
        public static void FinalBooking()
        {

        }
    }
}
