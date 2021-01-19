using System;

namespace FlightSearch
{
    public class Flight
    {

        public Flight(Airport origin, Airport destination, TimeSpan duration, int availableSeats)
        {
            Origin = origin;
            Destination = destination;
            Duration = duration;
            AvailableSeats = availableSeats;
        }

        public Airport Origin { get; set; }
        public Airport Destination { get; set; }
        public int AvailableSeats { get; set; }
        public TimeSpan Duration { get; set; }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Flight flight = (Flight)obj;
                return (Destination == flight.Destination) && (Origin == flight.Origin) && (AvailableSeats == flight.AvailableSeats);
            }
        }

        public override int GetHashCode()
        {
            return Origin.GetHashCode() ^ Destination.GetHashCode() ^ AvailableSeats.GetHashCode();
        }

        public override string ToString()
        {
            return $"\n\tFrom: {Origin.Name}\n\tTo: {Destination.Name}\n\tAvailableSeats: {AvailableSeats}\n";
        }
    }
}