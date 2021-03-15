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
    class AdminView
    {
        public static void AdminMenu()
        {
            Console.Clear();
            Console.WriteLine("------Admin View------");
            Console.Write("Pick event num to view stats for:");
            int eventP = int.Parse(Console.ReadLine());
            //picking the event id 

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
            conn.Open();
            //opening connection string

            string priceSQl = "SELECT Price FROM Bookings where EventId = @eid";
            SqlDataAdapter da = new SqlDataAdapter(priceSQl, conn);
            da.SelectCommand.Parameters.AddWithValue("eid", eventP);
            DataTable _EventPrice = new DataTable();
            da.Fill(_EventPrice);
            double priceTotal = 0;
            List<string> priceList = new List<string>();
            foreach (DataRow dr in _EventPrice.Rows)
            {
                priceList.Add(dr[0].ToString());
                double price = double.Parse(priceList[0]);
                priceTotal = priceTotal + price;
            }
            //getting price of event selected from database 
            //passing it to a new list and adding up price for each row 

            string command = "SELECT BookingId FROM Bookings where EventId = @eid";
            SqlDataAdapter da2 = new SqlDataAdapter(command, conn);
            da2.SelectCommand.Parameters.AddWithValue("eid", eventP);
            DataTable _BookingId = new DataTable();
            da.Fill(_BookingId);
            double count = 0;
            List<string> BList = new List<string>();
            foreach (DataRow dr in _BookingId.Rows)
            {
                count++;
            }
            //getting bookingid and passing to to list
            //counting rows in the list to calculate how many bookings for one event 

            string command2 = "SELECT EventName FROM Events where EventId = @eid";
            SqlDataAdapter da3 = new SqlDataAdapter(command2, conn);
            da3.SelectCommand.Parameters.AddWithValue("eid", eventP);
            DataTable _EventName = new DataTable();
            da3.Fill(_EventName);
            List<string> nameList = new List<string>();
            foreach (DataRow dr in _EventName.Rows)
            {
                nameList.Add(dr[0].ToString());
                
            }
            string eventname = (nameList[0]);
            //getting event name for the eventid selected
            

            Console.Write("Viewing stats for {0}", eventname);
            Console.WriteLine("\nTotal Bookings for Event {0} : {1}", eventP, count);
            Console.WriteLine("Total Revenue for Event {0} : £{1}", eventP, priceTotal);
            conn.Close();
            Console.WriteLine("press enter to go back to homepage");
            string input = Console.ReadLine();
            if (input == (""))
            {
                Console.Clear();
                Program.Menu();
            }
            //displaying stats for event 


        }
    }
}
