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
                using (SqlCommand command = new SqlCommand("SELECT EventName, EventPrice, DateEvent FROM Events", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("-------------------------");
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                
                                Console.WriteLine(reader.GetValue(i));
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
        
    }
}
