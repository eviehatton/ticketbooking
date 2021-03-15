using System;

namespace ticketbooking
{
    public class Program
    {
        public static void Menu()
        {
            Console.WriteLine("Ticket Booking");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("1. Book tickets");
            Console.WriteLine("2. View Events");
            Console.WriteLine("3. Login into account");
            Console.WriteLine("4. Cancel Booking");
            Console.WriteLine("4. Admin Login");
            Console.WriteLine("-----------------------------------");
            string MenuChoice = (Console.ReadLine());

            if (MenuChoice == "1")
            {
                UserBooking.BookEvent();
            }

            if (MenuChoice == "2")
            {
                ViewEvents.DisplayEvents();
            }

            if (MenuChoice == "3")
            {
                UserLogin.LoginTo();
            }

            if (MenuChoice == "4")
            {
                UserLogin.LoginTo();
            }

            if (MenuChoice == "5")
            {
                AdminLogin.UserLoginA();
            }
            

        }
        public static void Main(string[] args)
        {
            Menu();
            
        }
    }
    
}
   
