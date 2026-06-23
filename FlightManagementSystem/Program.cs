using FlightManagementSystem.Models;
using System;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace FlightManagementSystem
{
    internal class Program
    {
        // This is the main context object.
        // It stores all passengers, pilots, aircrafts, flights, and bookings.
        public static FlightContext context = new FlightContext();


        // ---------------------------------------------------------
        // case 1
        public static void RegisterPassenger()
        {
            Console.WriteLine("---- Register Passenger ----");

            Console.Write("Enter passenger name: ");
            string name = Console.ReadLine();

            Console.Write("Enter passenger email: ");
            string email = Console.ReadLine();

            Console.Write("Enter passenger phone: ");
            string phone = Console.ReadLine();
        
            Console.Write("Enter passport number: ");
            string passport = Console.ReadLine();

            // Passport number must be unique.
            //linq
            bool passportExists = context.Passengers.Any(p => p.passportNumber == passport);

            if (passportExists)
            {
                Console.WriteLine("This passport already exists.");
                return;
            }

            Console.Write("Enter nationality: ");
            string nationality = Console.ReadLine();

            //Go to the passenger list inside context.
            //Count how many passenger are already saved.
            //Add 1 to create the new passenger ID.

            Passenger passenger = new Passenger
            {
                passengerId = context.Passengers.Count + 1,
                passengerName = name,
                passengerEmail = email,
                passengerPhone = phone,
                passportNumber = passport,
                nationality = nationality
            };

            context.Passengers.Add(passenger);

            Console.WriteLine("Passenger registered successfully.");
            Console.WriteLine("Passenger ID: " + passenger.passengerId);
        }
        //--------------------------------------------------------
        //case 2 
        public static void AddAircraft()
        {
            Console.WriteLine("---- Add Aircraft ----");

            Console.Write("Enter aircraft model: ");
            string model = Console.ReadLine();

            Console.Write("Enter total seats: ");
            int totalSeats = int.Parse(Console.ReadLine());

            if (totalSeats <= 0)
            {
                Console.WriteLine("Invalid seats number.");
                return;
            }


        
            Aircraft aircraft = new Aircraft
            {
                aircraftId = context.Aircrafts.Count + 1,
                model = model,
                totalSeats = totalSeats,
                isOperational = true
            };

            context.Aircrafts.Add(aircraft);

            Console.WriteLine("Aircraft added successfully.");
            Console.WriteLine("Aircraft ID: " + aircraft.aircraftId);
        }
        //---------------------------------------------------------------
        //case 3 
            public static void RegisterPilot()
        {
            Console.WriteLine("---- Register Pilot ----");

            Console.Write("Enter pilot name: ");
            string name = Console.ReadLine();

            Console.Write("Enter pilot phone: ");
            string phone = Console.ReadLine();

            Console.Write("Enter license number: ");
            string license = Console.ReadLine();

            bool licenseExists = context.Pilots.Any(p => p.licenseNumber == license);

            if (licenseExists)
            {
                Console.WriteLine("This license number already exists.");
                return;
            }

           

            Pilot pilot = new Pilot
            {
                pilotId = context.Pilots.Count + 1,
                pilotName = name,
                pilotPhone = phone,
                licenseNumber = license,
                flightHours = 0,
                isAvailable = true                                                                                              
            };

            context.Pilots.Add(pilot);

            Console.WriteLine("Pilot registered successfully.");
            Console.WriteLine("Pilot ID: " + pilot.pilotId);
        }
        

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
                        RegisterPassenger();
                        break;

                    case 2:
                        AddAircraft();
                        break;

                    case 3:
                        RegisterPilot();
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

                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}