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
    public class ViewEvents
    {
        public static void DisplayEvents()
        {
            Console.WriteLine("Viewing current available events");
            Console.WriteLine("--------------------------------");
            using (SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf'; Integrated Security = True"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT EventId, EventName, EventPrice, DateEvent FROM Events", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("----------------------------------------------");
                            Console.WriteLine("| Event Number: " + (int)reader["EventId"]);
                            Console.WriteLine("| Event Name: " + (string)reader["EventName"]);
                            Console.WriteLine("| Price: " + (double)reader["EventPrice"]);
                            Console.WriteLine("| Date: " + (DateTime)reader["DateEvent"]);
                            Console.WriteLine("----------------------------------------------");
                            Console.WriteLine();

                            
                        }
                    }
                }
                Console.WriteLine("Press enter to return to menu");
                if (Console.ReadKey().Key == ConsoleKey.Enter) {
                    Console.Clear();
                    Program.Menu();
                }

            }
        }
        
    }
}
