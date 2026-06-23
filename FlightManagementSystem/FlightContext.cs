using FlightManagementSystem.Models;
using System.Collections.Generic;

namespace FlightManagementSystem
{
    public class FlightContext
    {
        public List<Passenger> Passengers { get; set; } = new List<Passenger>();
        public List<Pilot> Pilots { get; set; } = new List<Pilot>();
        public List<Aircraft> Aircrafts { get; set; } = new List<Aircraft>();
        public List<Flight> Flights { get; set; } = new List<Flight>();
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}