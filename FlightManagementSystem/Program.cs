using FlightManagementSystem.Models;
using System;
using System.Linq;

namespace FlightManagementSystem
{
    internal class Program
    {
        // This is the main context object.
        // It stores all passengers, pilots, aircrafts, flights, and bookings.
        public static FlightContext context = new FlightContext();

        //

 

        
        // ---------------------------------------------------------
        // Main Menu
        // 
        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("---- Flight Management System ----");
                Console.WriteLine("1. Register Passenger");
                Console.WriteLine("2. Add Aircraft");
                Console.WriteLine("3. Register Pilot");
                Console.WriteLine("4. View All Flights");
                Console.WriteLine("5. Schedule Flight");
                Console.WriteLine("6. Book Flight");
                Console.WriteLine("7. Cancel Booking");
                Console.WriteLine("8. Depart Flight");
                Console.WriteLine("9. Cancel Flight");
                Console.WriteLine("10. Passenger Booking History");
                Console.WriteLine("11. Flight Revenue & Load Factor Report");
                Console.WriteLine("0. Exit");

                Console.Write("Enter your choice: ");
                bool validChoice = int.TryParse(Console.ReadLine(), out int choice);

                if (!validChoice)
                {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        break;

                    case 5:
                        break;

                    case 6:
                        break;

                    case 7:
                        break;

                    case 8:
                        break;

                    case 9:
                        break;

                    case 10:
                        break;

                    case 11:
                        break;

                    case 0:
                        running = false;
                        Console.WriteLine("Goodbye.");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}