using AirReservationApi.handlers.validators;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace AirReservationApi.Domain
{
    public class Airport
    {
        private readonly AirportValidator Validator = new();

        public Airport() { }
        public Airport(string name, string code)
        {
            Name = name;
            Code = code.ToUpper();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public bool IsValid => Validator.Validate(this).IsValid;

        public override string ToString()
        {
            return $"Name: {this.Name}, Code: {this.Code}";
        }
    }
}