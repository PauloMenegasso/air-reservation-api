using System.ComponentModel.DataAnnotations;
using AirReservationApi.handlers.validators;

namespace AirReservationApi.Domain
{
    public class AirReservation
    {
        private readonly AirReservationValidator Validator = new();

        public AirReservation() { }
        public AirReservation(string originAirport, string targetAirport, DateTime departureDatetime, DateTime arrivalDatetime, string airline, int flightNumber, int numberOfAdults, int numberOfKids)
        {
            this.OriginAirport = originAirport;
            this.TargetAirport = targetAirport;
            this.DepartureDatetime = departureDatetime;
            this.ArrivalDatetime = arrivalDatetime;
            this.Airline = airline;
            this.FlightNumber = flightNumber;
            this.NumberOfAdultPassagers = numberOfAdults;
            this.NumberOfChieldPassagers = numberOfKids;
        }

        [Key]
        public int Id { get; set; }
        public string OriginAirport { get; set; }
        public string TargetAirport { get; set; }
        public DateTime DepartureDatetime { get; set; }
        public DateTime ArrivalDatetime { get; set; }
        public string Airline { get; set; }
        public long FlightNumber { get; set; }
        public int NumberOfAdultPassagers { get; set; }
        public int NumberOfChieldPassagers { get; set; }

        public bool IsValid => Validator.Validate(this).IsValid;

        public override string ToString()
        {
            return $"Origin Airport: {this.OriginAirport}, Target Airport: {this.TargetAirport}, Departure Datetime: {this.DepartureDatetime}, Arrival Datetime: {this.ArrivalDatetime}, Airline: {this.Airline}, Flight Number: {this.FlightNumber}, Adults: {this.NumberOfAdultPassagers}, Kids: {this.NumberOfChieldPassagers}";
        }
    }
}