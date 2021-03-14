using System;
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

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
            conn.Open();

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

            Console.WriteLine("\nTotal Bookings for Event {0} : {1}", eventP, count);

            conn.Close();
            

        }
    }
}
