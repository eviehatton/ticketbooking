using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace ticketbooking
{
    public class Seats
    {
        public static void DisplaySeats(int eventid)
        {

            int[,] layout = new int[13, 6];
            sbyte[,] layoutchar = new sbyte[13, 6];

            Console.ForegroundColor
            = ConsoleColor.Green;
            Console.WriteLine("0 - Available");
            Console.ForegroundColor
            = ConsoleColor.Red;
            Console.WriteLine("0 - Occupied");
            Console.ForegroundColor
            = ConsoleColor.White;


            List<SeatsClass> seatList = new List<SeatsClass>();
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");

            string seatSQl = "SELECT seatId, seatValue, status FROM Bookings where EventId = @eid";

            SqlDataAdapter da = new SqlDataAdapter(seatSQl, conn);
            da.SelectCommand.Parameters.AddWithValue("eid", eventid);
            DataTable _status = new DataTable();
            da.Fill(_status);
            List<string> bookedseats = new List<string>();
            foreach (DataRow dr in _status.Rows)
            {
                bookedseats.Add(dr[1].ToString());


            }

            foreach (DataRow dr in _status.Rows)
            {

                SeatsClass b = new SeatsClass(dr);
                seatList.Add(b);
            }
            Console.Write(String.Format("{0}\tA\tB\tC\tD\tE\tF\n", " ".PadRight(7)));
            string seatcol = "ABCDEF";
            for (int i = 0; i < 13; i++)
            {
                Console.Write("\n");
                Console.Write(String.Format("Row {0}\t", (i + 1).ToString().PadRight(2)));


                for (int j = 0; j < 6; j++)
                {
                    string thisSeat = seatcol[j] + (i + 1).ToString();
                    bool Status = bookedseats.Contains(thisSeat);

                    if (!Status)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(String.Format("{0}\t", layout[i, j].ToString()));
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(String.Format("{0}\t", layout[i, j].ToString()));
                    }

                }
                conn.Close();
                Console.ForegroundColor = ConsoleColor.White;
            }
            BookSeat(bookedseats, eventid);
        }
        public static void BookSeat(List<string> bookedseats, int eventid)
        {
            Console.WriteLine("\nPick available seat to book in format 'A1':");
            string seatChoice = Console.ReadLine().ToUpper();
            char A = seatChoice[0];
            char B = seatChoice[1];
            bool status = bookedseats.Contains(seatChoice);
            if (A == 'A' || A == 'B' || A == 'C' || A == 'D' || A == 'E' || A == 'F')
            {
                if (B >= 1 || B <= 13)
                {
                    if (status)
                    {
                        Console.WriteLine("seat not available try again");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        DisplaySeats(eventid);

                    }
                    else
                    {
                        UserBooking.FinalBooking();
                    }
                }

            }
        }
    }  
}


