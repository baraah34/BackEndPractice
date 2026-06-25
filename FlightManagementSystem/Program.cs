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
        //case 4
        public static void ViewAllFlights()
        {
            Console.WriteLine("---- View All Flights ----");

            if (!context.Flights.Any())
            {
                Console.WriteLine("No flights found.");
                return;
            }

            foreach (var flight in context.Flights)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Flight Code: " + flight.flightCode);
                Console.WriteLine("From: " + flight.origin);
                Console.WriteLine("To: " + flight.destination);
                Console.WriteLine("Date: " + flight.departureDate);
                Console.WriteLine("Time: " + flight.departureTime);
                Console.WriteLine("Available Seats: " + flight.availableSeats);
                Console.WriteLine("Ticket Price: " + flight.ticketPrice);
                Console.WriteLine("Status: " + flight.status);
            }
        }
        //-------------------------------------------------------------
        //case 5
        public static void ScheduleFlight()
        {
            Console.WriteLine("---- Schedule Flight ----");


            // display all operational aircrafts
            Console.WriteLine("Available Aircrafts:");

            // go through all aircrafts saved in context
            foreach (Aircraft aircraft in context.Aircrafts)
            {
                // show only aircrafts that are operational
                if (aircraft.isOperational == true)
                {
                    Console.WriteLine(aircraft.aircraftId + ". " + aircraft.model + " | Seats: " + aircraft.totalSeats );       
                   
                }
            }

            Console.Write("Choose aircraft ID: ");

            int aircraftId = int.Parse(Console.ReadLine());

            // find the aircraft with the same ID
            Aircraft selectedAircraft = context.Aircrafts.FirstOrDefault(a => a.aircraftId == aircraftId);

            // if aircraft does not exist or is not operational
            if (selectedAircraft == null || selectedAircraft.isOperational == false)
            {
                Console.WriteLine("Aircraft not found or not operational.");
                return;
            }

            // display all available pilots
            Console.WriteLine("Available Pilots:");

            // go through all pilots saved in context
            foreach (Pilot pilot in context.Pilots)
            {
                // show only pilots who  available
                if (pilot.isAvailable == true)
                {
                    Console.WriteLine(  pilot.pilotId + ". " +   pilot.pilotName);
                }
            }

            Console.Write("Choose pilot ID: ");

            int pilotId = int.Parse(Console.ReadLine());

            // find the pilot with   ID
            Pilot selectedPilot = context.Pilots.FirstOrDefault(p => p.pilotId == pilotId);

            // if pilot does not exist or is not available
            if (selectedPilot == null || selectedPilot.isAvailable == false)
            {
                Console.WriteLine("Pilot not found or not available.");
                return;
            }

            Console.Write("Enter origin: ");
            string origin = Console.ReadLine();

            Console.Write("Enter destination: ");
            string destination = Console.ReadLine();

            Console.Write("Enter departure date: ");
            string date = Console.ReadLine();

            Console.Write("Enter departure time: ");
            string time = Console.ReadLine();

            Console.Write("Enter ticket price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            // ticket price must be more than 0
            if (price <= 0)
            {
                Console.WriteLine("Invalid ticket price.");
                return;
            }

            // generate new flight ID
            int newFlightId = context.Flights.Count + 1;

            // Create a new flight object
            Flight flight = new Flight
            {
                flightId = newFlightId,

                // flight code automatically
                flightCode = "OA-" + (200 + newFlightId),
                aircraftId = selectedAircraft.aircraftId,
                pilotId = selectedPilot.pilotId,
                origin = origin,
                destination = destination,
                departureDate = date,
                departureTime = time,
                ticketPrice = price,
                availableSeats = selectedAircraft.totalSeats,   // available seats come from the aircraft total seats
                status = "Scheduled"// new flight starts as Scheduled
            };

            context.Flights.Add(flight);

            // make the pilot unavailable because he is assigned to this flight
            selectedPilot.isAvailable = false;

            Console.WriteLine("Flight scheduled successfully.");
            Console.WriteLine("Flight Code: " + flight.flightCode);
        }
        //--------------------------------------------------------------
        //case 6
        public static void BookFlight()
        {
            Console.WriteLine("---- Book a Flight ----");

            if (context.Passengers.Count == 0)
            {
                Console.WriteLine("No passengers registered.");
                return;
            }

            Console.Write("Enter passenger ID: ");
            int passengerId = int.Parse(Console.ReadLine());

            Passenger passenger = context.Passengers.FirstOrDefault(p => p.passengerId == passengerId);

            if (passenger == null)
            {
                Console.WriteLine("Passenger not found.");
                return;
            }

            Console.Write("Enter destination: ");
            string destination = Console.ReadLine();

            var availableFlights = context.Flights.Where(f => f.destination.ToLower() == destination.ToLower()&& f.status == "Scheduled"&& f.availableSeats > 0).ToList();
            
            if (availableFlights.Count == 0)
            {
                Console.WriteLine("No available flights to this destination.");
                return;
            }

            Console.WriteLine("Available Flights:");

            foreach (Flight flight in availableFlights)
            {
                Console.WriteLine(
                    flight.flightId + ". " +
                    flight.flightCode +
                    " | From: " + flight.origin +
                    " | To: " + flight.destination +
                    " | Seats: " + flight.availableSeats +
                    " | Price: " + flight.ticketPrice
                );
            }

            Console.Write("Choose flight ID: ");
            int flightId = int.Parse(Console.ReadLine());

            Flight selectedFlight = availableFlights.FirstOrDefault(f => f.flightId == flightId);

            if (selectedFlight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            int bookingId = context.Bookings.Count + 1;

            Booking booking = new Booking
            {
                bookingId = bookingId,
                passengerId = passenger.passengerId,
                flightId = selectedFlight.flightId,
                seatNumber = "S" + bookingId,
                bookingDate = DateTime.Now.ToShortDateString(),
                totalPrice = selectedFlight.ticketPrice,
                status = "Confirmed"
            };

            context.Bookings.Add(booking);

            selectedFlight.availableSeats--;

            Console.WriteLine("Booking confirmed successfully.");
            Console.WriteLine("Booking ID: " + booking.bookingId);
            Console.WriteLine("Seat Number: " + booking.seatNumber);
            Console.WriteLine("Total Price: " + booking.totalPrice);
            Console.WriteLine("date: " + booking.bookingDate);


        }
        //------------------------------------------
        //case 7 
        public static void CancelBooking()
        {
            Console.WriteLine("---- Cancel Booking ----");

            Console.Write("Enter booking ID: ");
            int bookingId = int.Parse(Console.ReadLine());

            Booking booking = context.Bookings.FirstOrDefault(b => b.bookingId == bookingId);

            if (booking == null)
            {
                Console.WriteLine("Booking not found.");
                return;
            }

            if (booking.status == "Cancelled")
            {
                Console.WriteLine("Booking is already cancelled.");
                return;
            }

            Flight flight = context.Flights.FirstOrDefault(f => f.flightId == booking.flightId);

            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            if (flight.status == "Departed")
            {
                Console.WriteLine("Cannot cancel booking because flight already departed.");
                return;
            }

            booking.status = "Cancelled"; // change booking status
            flight.availableSeats++;      // return the seat to the flight

            Console.WriteLine("Booking cancelled successfully.");
        }

        // ---------------------------------------------------------
        //case 8 
        public static void DepartFlight()
        {
            Console.WriteLine("---- Depart Flight ----");

            Console.Write("Enter flight ID: ");
            int flightId = int.Parse(Console.ReadLine());

            //search about the same id match the entered id 
            
            Flight flight = context.Flights.FirstOrDefault(f => f.flightId == flightId);

            //flight validation
            if (flight == null)
            {
                Console.WriteLine("flight not found.");
                return;
            }
            //flight status validation
            if (flight.status != "Scheduled")
            {
                Console.WriteLine("only scheduled flights can depart.");
                return;
            }

            Console.Write("enter flight duration in hours: ");
            int duration = int.Parse(Console.ReadLine());

            if (duration <= 0)
            {
                Console.WriteLine("invalid duration.");
                return;
            }

            Pilot pilot = context.Pilots.FirstOrDefault(p => p.pilotId == flight.pilotId);

            flight.status = "Departed";

            //pilot validtion 
            if (pilot != null)
            {
                 //add  flight duration to  pilot total flight hour
                pilot.flightHours = pilot.flightHours + duration;

                // make  pilot available again after the flight departs
                pilot.isAvailable = true;
            }

            Console.WriteLine("flight departed successfully.");
            Console.WriteLine("pilot flight hours updated.");
        }
        //---------------------------------------------------------------
        //case 9
        public static void CancelFlight()
        {
            Console.WriteLine("---- Cancel Flight ----");

            Console.Write("Enter flight ID: ");
            int flightId = int.Parse(Console.ReadLine());

            Flight flight = context.Flights.FirstOrDefault(f => f.flightId == flightId);

            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            if (flight.status != "Scheduled")
            {
                Console.WriteLine("Only scheduled flights can be cancelled.");
                return;
            }


            var confirmedBookings = context.Bookings.Where(b => b.flightId == flight.flightId && b.status == "Confirmed").ToList();

            foreach (Booking booking in confirmedBookings)
            {
                booking.status = "Cancelled";
            }

            flight.status = "Cancelled";

            Pilot pilot = context.Pilots.FirstOrDefault(p => p.pilotId == flight.pilotId);

            if (pilot != null)
            {
                pilot.isAvailable = true;
            }

            Console.WriteLine("Flight cancelled successfully.");
            Console.WriteLine("Affected bookings: " + confirmedBookings.Count);
        }
        //--------------------------------------------------------------------
        //case10

        public static void PassengerBookingHistory()
        {
            Console.WriteLine("---- Passenger Booking History ----");

            Console.Write("Enter passenger ID: ");
            int passengerId = int.Parse(Console.ReadLine());

            Passenger passenger = context.Passengers.FirstOrDefault(p => p.passengerId == passengerId);

            if (passenger == null)
            {
                Console.WriteLine("Passenger not found.");
                return;
            }

            var passengerBookings = context.Bookings
                .Where(b => b.passengerId == passengerId)
                .ToList();

            if (passengerBookings.Count == 0)
            {
                Console.WriteLine("No booking history found.");
                return;
            }

            decimal totalSpent = 0;

            Console.WriteLine("Passenger: " + passenger.passengerName);

            foreach (Booking booking in passengerBookings)
            {
                Flight flight = context.Flights.FirstOrDefault(f => f.flightId == booking.flightId);

                if (flight != null)
                {
                    Console.WriteLine("Flight Code: " + flight.flightCode);
                    Console.WriteLine("Route: " + flight.origin + "---> " + flight.destination);
                    Console.WriteLine("Date: " + flight.departureDate);
                    Console.WriteLine("Seat: " + booking.seatNumber);
                    Console.WriteLine("Price Paid: " + booking.totalPrice);
                    Console.WriteLine("Booking Status: " + booking.status);

                    if (booking.status == "Confirmed")
                    {
                        totalSpent = totalSpent + booking.totalPrice;
                    }
                }
            }
            //if one passenger booked many flight the total spent will count it all 

            Console.WriteLine("Total spent on confirmed bookings: " + totalSpent);
        }
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
                        ViewAllFlights();
                        break;

                    case 5:
                        ScheduleFlight();
                        break;

                    case 6:
                        BookFlight();
                        break;

                    case 7:
                        CancelBooking();
                        break;

                    case 8:
                        DepartFlight();
                        break;

                    case 9:
                        CancelFlight();
                        break;

                    case 10:
                        PassengerBookingHistory();
                        break;

                    case 11:
                        break;

                    case 0:
                        running = false;
                        Console.WriteLine("bye.");
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