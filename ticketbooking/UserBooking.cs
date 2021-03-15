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
        public static void bookingEvent(int eventid)
        {
            Seats.DisplaySeats(eventid);
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
                bookingEvent(int.Parse(eventbook));
                
            }
            else
            {
                Program.Menu();
            }
        }
        public static void FinalBooking(int eventid, string seat)
        {

            Console.WriteLine("-----Enter Details-----");
            Console.Write("\nFirst name:");
            string firstname = Console.ReadLine();
            Console.Write("\nLast name:");
            string lastname = Console.ReadLine();
            Console.Write("\nContact Email:");
            string email = Console.ReadLine();


            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
            conn.Open();

            string command = "INSERT INTO Customers (FirstName, SecondName, Email) VALUES( @FirstName, @SecondName, @Email)";
            SqlCommand myCommand = new SqlCommand(command, conn);
            myCommand.Parameters.AddWithValue("@CustomerId", 0);
            myCommand.Parameters.AddWithValue("@FirstName", firstname);
            myCommand.Parameters.AddWithValue("@SecondName", lastname);
            myCommand.Parameters.AddWithValue("@Email", email);
            myCommand.ExecuteNonQuery();



            string priceSQl = "SELECT EventPrice FROM Events where EventId = @eid";
            SqlDataAdapter da = new SqlDataAdapter(priceSQl, conn);
            da.SelectCommand.Parameters.AddWithValue("eid", eventid);
            DataTable _EventPrice = new DataTable();
            da.Fill(_EventPrice);
            List<string> priceList = new List<string>();
            foreach (DataRow dr in _EventPrice.Rows)
            {
                priceList.Add(dr[0].ToString());
            }
            double price = double.Parse(priceList[0]);




            string com = "SELECT CustomerId FROM Customers where FirstName = @firstname";
            SqlDataAdapter da2 = new SqlDataAdapter(com, conn);
            da2.SelectCommand.Parameters.AddWithValue("firstname", firstname);
            DataTable _CustomerId = new DataTable();
            da2.Fill(_CustomerId);
            List<string> CList = new List<string>();
            foreach (DataRow dr in _CustomerId.Rows)
            {
                CList.Add(dr[0].ToString());
            }
            int customerid = int.Parse(CList[0]);

            string con2 = "SELECT seatId FROM Seats where seatValue = @value";
            SqlDataAdapter da3 = new SqlDataAdapter(con2, conn);
            da3.SelectCommand.Parameters.AddWithValue("value", seat);
            DataTable _seatId = new DataTable();
            da3.Fill(_seatId);
            List<string> seatI = new List<string>();
            foreach (DataRow dr in _seatId.Rows)
            {
                seatI.Add(dr[0].ToString());
            }
            int seatid = int.Parse(seatI[0]);

            
            string insBook = "INSERT INTO Bookings (CustomerId ,EventId ,Price ,seatValue ,seatId ,status) VALUES (@CustomerId,@EventId,@Price,@seatValue,@seatId,@status)";

            SqlCommand cmd = new SqlCommand(insBook, conn);
            cmd.Parameters.AddWithValue("@CustomerId", customerid);
            cmd.Parameters.AddWithValue("@EventId", eventid);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@seatValue", seat);
            cmd.Parameters.AddWithValue("@seatId", seatid);
            cmd.Parameters.AddWithValue("@status", 1);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Booking Complete!");
            Console.WriteLine("Would you like to create an account to view order details? (Y) (N)");
            string response = Console.ReadLine().ToUpper();
            if (response == "Y") 
            {
                Console.Clear();
                UserLogin.CreateAccount(email,customerid);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("redirecting to homepage");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                Program.Menu();
            }

            conn.Close();
        }
    }
}

