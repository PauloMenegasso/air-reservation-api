using AirReservationApi.Domain;
using FluentValidation;

namespace AirReservationApi.handlers.validators
{
    public class AirReservationValidator : AbstractValidator<AirReservation>
    {
        public AirReservationValidator()
        {
            this.RuleFor(ar => ar.ArrivalDatetime)
                .NotNull()
                .GreaterThan(ar => ar.DepartureDatetime);

            this.RuleFor(ar => ar.DepartureDatetime)
                .NotNull();

            this.RuleFor(ar => ar.Airline)
                .NotEmpty();

            this.RuleFor(ar => ar.OriginAirport)
                .NotEmpty()
                .NotEqual(ar => ar.TargetAirport);

            this.RuleFor(ar => ar.TargetAirport)
                .NotEmpty();

            this.RuleFor(ar => ar.FlightNumber)
                .NotEmpty();

            this.RuleFor(ar => ar.NumberOfAdultPassagers)
                .GreaterThan(0);
        }
    }
}