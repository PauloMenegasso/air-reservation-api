using AirReservationApi.Domain;
using AirReservationApi.handlers;
using Microsoft.AspNetCore.Mvc;

namespace AirReservationApi.Controllers
{
    [Route("[controller]")]
    public class AirportController : Controller
    {
        private readonly IAirportHandler airportHandler;
        public AirportController(IAirportHandler airportHandler)
        {
            this.airportHandler = airportHandler;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Airport>> GetAll()
            => await airportHandler.GetAllAirports();

        [HttpPost("Insert")]
        public async Task<ReturnMessage> Insert([FromQuery] string name, string code)
        {
            var airport = new Airport(name, code);
            var message = await airportHandler.InsertOneAirport(airport);

            return new ReturnMessage(message);
        }

        [HttpDelete("Remove")]
        public async Task<ReturnMessage> Remove([FromQuery] string code)
        {
            var message = await airportHandler.RemoveOneAirport(code.ToUpper());

            return new ReturnMessage(message);
        }

        [HttpPut("Update")]
        public async Task<ReturnMessage> Update([FromQuery] string code, string newName, string newCode)
        {
            var airport = new Airport(newName, newCode);

            var message = await airportHandler.Update(code.ToUpper(), airport);

            return new ReturnMessage(message);
        }

    }
}