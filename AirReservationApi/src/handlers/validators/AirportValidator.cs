using AirReservationApi.Domain;
using FluentValidation;

namespace AirReservationApi.handlers.validators
{
    public class AirportValidator : AbstractValidator<Airport>
    {
        public AirportValidator()
        {
            this.RuleFor(a => a.Name).NotNull();
            this.RuleFor(a => a.Code).NotNull();
            this.RuleFor(a => a.Code.Length).Equal(3);
        }
    }
}