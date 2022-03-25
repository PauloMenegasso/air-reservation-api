using AirReservationApi.handlers.validators;

namespace AirReservationApi.Domain
{
    public class SearchFilter
    {

        public SearchFilter(string originAirport, string targetAirport, DateTime departureDatetime, DateTime arrivalDatetime, string airline)
        {
            this.OriginAirport = originAirport;
            this.TargetAirport = targetAirport;
            this.DepartureDatetime = departureDatetime.Date.Year == 0001 ? null : departureDatetime;
            this.ArrivalDatetime = arrivalDatetime.Date.Year == 0001 ? null : arrivalDatetime;
            this.Airline = airline;
        }

        public string OriginAirport { get; set; }
        public string TargetAirport { get; set; }
        public string Airline { get; set; }
        public DateTime? DepartureDatetime { get; set; }
        public DateTime? ArrivalDatetime { get; set; }
    }
}