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
    public class Seats
    {
        public static void DisplaySeats()
        {
            int seat = 0;
            int[,] layout = new int[13, 6];
            sbyte[,] layoutchar = new sbyte[13, 6];
            

            Console.ForegroundColor
            = ConsoleColor.Green;
            Console.WriteLine("0 - Available");
            Console.ForegroundColor
            = ConsoleColor.Red;
            Console.WriteLine("X - Occupied");
            Console.ForegroundColor
            = ConsoleColor.White;
            Console.Write(String.Format("{0}\tA\tB\tC\tD\tE\tF\n", " ".PadRight(7)));
            for (int i = 0; i < 13; i++)
            {

                Console.Write(String.Format("Row {0}\t", (i + 1).ToString().PadRight(2)));
                for (int j = 0; j < 6; j++)
                {
                    Console.ForegroundColor
            = ConsoleColor.Green;
                    Console.Write(String.Format("{0}\t", layoutchar[i, j]));
                    
                    

                }
                Console.ForegroundColor
            = ConsoleColor.White;
                Console.WriteLine();
            }

            BookSeat(layoutchar);

        }
        public static void BookSeat(sbyte[,] layout)
        {
            Console.WriteLine("Pick available seat to book in format 'A1':");
            string seatChoice = Console.ReadLine().ToUpper();
            char A = seatChoice[0];
            char B = seatChoice[1];
            if (A == 'A' || A == 'B' || A == 'C' || A == 'D' || A == 'E' || A == 'F')
            {
                if (B >= 1 || B <= 13)
                {
                    if (layout[(int)A-65,(int)B-48].ToString() == "0")
                    {
                        Console.WriteLine("seat available");
                        Console.ForegroundColor
            = ConsoleColor.Red;
                        layout[]

                        //layout[(int)A - 65, (int)B - 48)] == 0;
                        Console.Write(layout);



                        Console.ForegroundColor
            = ConsoleColor.White;
                        
                    }
                    else
                    {
                        Console.WriteLine("B");
                    }

                }
            }
        }
    }
}
