using AirReservationApi.Domain;
using AirReservationApi.Infra.Repositiory;
using AirReservationApi.Infra.Logging;

namespace AirReservationApi.handlers
{
    public interface IAirportHandler
    {
        Task<IEnumerable<Airport>> GetAllAirports();
        Task<string> InsertOneAirport(Airport airport);
        Task<string> RemoveOneAirport(string code);
        Task<string> Update(string code, Airport airport);
    }
    public class AirportHandler : IAirportHandler
    {
        private readonly IAirportRepository airportRepository;
        private readonly AirReservationApi.Infra.Logging.ILogger logger;

        public AirportHandler(IAirportRepository airportRepository, AirReservationApi.Infra.Logging.ILogger logger)
        {
            this.airportRepository = airportRepository;
            this.logger = logger;
        }
        public async Task<IEnumerable<Airport>> GetAllAirports()
        {
            var airports = await this.airportRepository.GetAll();
            this.logger.Information("Retrieved airports sucessfully", airports);
            return airports.OrderBy(a => a.Name);
        }
        public async Task<string> InsertOneAirport(Airport airport)
        {
            if (airport.IsValid)
            {
                try
                {
                    await this.airportRepository.InsertOne(airport);
                    this.logger.Information("Airport inserted successfully");
                    return "Airport inserted successfully";
                }
                catch (Exception ex)
                {
                    this.logger.Error(ex, "There was an unknown error in airport insertion");
                    return "There was an unknown error in airport insertion";
                }
            }
            this.logger.Warning("Invalid airport format", airport);
            return "Invalid airport format";
        }

        public async Task<string> RemoveOneAirport(string code)
        {
            var success = await this.airportRepository.Remove(code);

            if (success)
            {
                this.logger.Information($"Airport removed successfully", code);
                return "Airport deleted successfully";
            }

            this.logger.Error("Error while deleting airport");
            return "Error while deleting airport";
        }

        public async Task<string> Update(string code, Airport airport)
        {
            if (!airport.IsValid)
            {
                this.logger.Warning("Invalid airport format");
                return "Invalid airport format";
            }

            var success = await this.airportRepository.Update(code, airport);

            if (success)
            {
                this.logger.Information("Airport updated successfully", airport);
                return "Airport updated successfully";
            }

            this.logger.Error("Error while updating airport");
            return "Error while updating airport";
        }
    }
}
