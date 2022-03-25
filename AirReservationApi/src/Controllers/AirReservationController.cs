using AirReservationApi.Domain;
using AirReservationApi.handlers;
using Microsoft.AspNetCore.Mvc;

namespace AirReservationApi.Controllers
{
    [Route("[controller]")]
    public class AirReservationController : Controller
    {
        private readonly IAirReservationHandler airReservationHandler;

        public AirReservationController(IAirReservationHandler airReservationHandler)
        {
            this.airReservationHandler = airReservationHandler;
        }

        [HttpPost("Insert")]
        public async Task<ReturnMessage> SaveAirReservation([FromQuery]
            string originAirport,
            string targetAirport,
            DateTime departureDatetime,
            DateTime arrivalDatetime,
            string airline,
            int flightNumber,
            int numberOfAdults,
            int numberOfKids)
        {
            var airReservation = new AirReservation(
                originAirport,
                targetAirport,
                departureDatetime,
                arrivalDatetime,
                airline,
                flightNumber,
                numberOfAdults,
                numberOfKids);

            var response = await this.airReservationHandler.InsertAirReservation(airReservation);

            return new ReturnMessage(response);
        }

        [HttpGet("GetOne/{id}")]
        public async Task<AirReservation> GetOne([FromRoute] int id)
            => await this.airReservationHandler.GetAnAirReservation(id);

        [HttpGet("GetWithFilter/")]
        public async Task<IEnumerable<AirReservation>> GetMany([FromQuery]
            string originAirport,
            string targetAirport,
            DateTime departureDatetime,
            DateTime arrivalDatetime,
            string airline)
        {

            var filter = new SearchFilter(originAirport, targetAirport, departureDatetime, arrivalDatetime, airline);

            return await this.airReservationHandler.GetManyAirReservation(filter);
        }

        [HttpGet("GetAll/")]
        public async Task<JsonResult> GetAll()
        {
            var airReservation = await this.airReservationHandler.GetAllAirReservation();
            return new JsonResult(airReservation);
        }

        [HttpDelete("Remove")]
        public async Task<ReturnMessage> Remove([FromQuery] int id)
        {
            var message = await airReservationHandler.RemoveReservation(id);

            return new ReturnMessage(message);
        }
    }
}