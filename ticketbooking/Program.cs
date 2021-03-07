using System;

namespace ticketbooking
{
    class Program
    {
        static void Main(string[] args)
        {
            static void Menu()
            {
                Console.WriteLine("Ticket Booking");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("1. Book tickets");
                Console.WriteLine("2. View Events");
                Console.WriteLine("3. Login into account");
                Console.WriteLine("4. Admin Login");
                int MenuChoice = int.Parse(Console.ReadLine());

                if (MenuChoice == 1)
                {

                }

                if (MenuChoice == 2)
                {

                }

                if (MenuChoice == 3)
                {

                }

                if (MenuChoice == 4)
                {

                }
                else
                {
                    Console.WriteLine("not a valid input");
                    Menu();
                }

            }


        }
    }
}