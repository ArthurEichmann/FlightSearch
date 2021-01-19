using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FlightSearch;

namespace FlightSearch
{
    class Program
    {
        static void Main(string[] args)
        {

            Airport munich = new Airport("Munich");
            Airport frankfurt = new Airport("Frankfurt");
            Airport amsterdam = new Airport("Amsterdam");
            Airport barcelona = new Airport("Barcelona");
            Airport paris = new Airport("Paris");

            List<Flight> allFlights = new List<Flight>();

            allFlights.Add(new Flight(munich, barcelona, TimeSpan.FromHours(3.5), 50));
            allFlights.Add(new Flight(amsterdam, munich, TimeSpan.FromHours(3), 50));
            allFlights.Add(new Flight(amsterdam, frankfurt, TimeSpan.FromHours(3.8), 50));
            allFlights.Add(new Flight(frankfurt, amsterdam, TimeSpan.FromHours(2), 50));
            allFlights.Add(new Flight(frankfurt, barcelona, TimeSpan.FromHours(2), 50));
            allFlights.Add(new Flight(munich, paris, TimeSpan.FromHours(1.8), 50));



           // allFlights = allFlights.OrderBy(o => o.Duration).ToList();
            List<Flight> fastestFlights = new List<Flight>();
            List<Flight> temp = new List<Flight>();

            fastestFlights = FlightService.moreDestinations(fastestFlights, temp, allFlights, amsterdam, barcelona);

            foreach (Flight flight in fastestFlights)
            { 
                Console.WriteLine(flight.Origin.Name + "   " + flight.Destination.Name);

            }

        }
    }
}
