using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlightSearch
{
    public class FlightService
    {
        private IEnumerable<Flight> availableFlights;

        public static FlightService Of(IEnumerable<Flight> availableFlights)
        {
            return new FlightService(availableFlights);
        }

        private FlightService(IEnumerable<Flight> availableFlights)
        {
            this.availableFlights = availableFlights;
        }

        public IEnumerable<Flight> GetFullFlights()
        {
            List<Flight> bookedUpFlights = new List<Flight>();
            foreach (Flight flight in availableFlights)
            {
                if (flight.AvailableSeats <= 0)
                    bookedUpFlights.Add(flight);
            }

            return bookedUpFlights;
        }

        public IEnumerable<Flight> GetFlightsForDestination(Airport destination)
        {
            List<Flight> destinationFlights = new List<Flight>();
            foreach (Flight flight in availableFlights)
            {
                if (flight.Destination == destination)
                    destinationFlights.Add(flight);
            }

            return destinationFlights;
        }

        public IEnumerable<Flight> GetFlightsForOrigin(Airport origin)
        {
            List<Flight> originFlights = new List<Flight>();
            foreach (Flight flight in availableFlights)
            {
                if (flight.Origin == origin)
                    originFlights.Add(flight);
            }

            return originFlights;
        }

        public List<Flight> GetShortestFlightByRoute(Airport origin, Airport destination)
        {
            List<Flight> allFlights = new List<Flight>();
            List<Flight> fastestFlightConnections = new List<Flight>();
            List<Flight> temp = new List<Flight>();

            return moreDestinations(fastestFlightConnections, temp, allFlights, origin, destination); ;
        }

        public static List<Flight> moreDestinations(List<Flight> fastestFlight, List<Flight> flightsList, List<Flight> allFlights, Airport origin, Airport destination)
        {
            foreach (Flight flight in allFlights)
            {
                if (flight.AvailableSeats > 0 && flightsList.Count() < 3) 
                {
                    if (flight.Destination == destination)
                    {
                        List<Flight> allFlightsTemp  = allFlights.ToList<Flight>();
                        List<Flight> flightsListTemp = flightsList.ToList<Flight>();

                        flightsListTemp.Insert(0, flight);
                        allFlightsTemp.Remove(flight);

                        if (origin == flight.Origin)
                        {
                            return flightsListTemp;
                        }
                        else
                        {
                            flightsListTemp = moreDestinations(fastestFlight, flightsListTemp, allFlightsTemp, origin, flight.Origin); //origin of flight is new destination

                            if (flightsListTemp.Count() > 0 &&  flightsListTemp[0].Origin == origin)
                            {
                                TimeSpan durationNewFlight = new TimeSpan();
                                foreach (Flight flightResult in flightsListTemp)
                                {
                                    durationNewFlight = durationNewFlight + flightResult.Duration;
                                }

                                TimeSpan duration = new TimeSpan();
                                foreach (Flight flightResult in fastestFlight)
                                {
                                    duration = duration + flightResult.Duration;
                                }

                                if (duration.TotalHours == 0 || duration > durationNewFlight)
                                {
                                    fastestFlight = flightsListTemp.ToList<Flight>();
                                }
                            }
                        }
                    }
                }
            }
            return fastestFlight;
        }
    }
}
