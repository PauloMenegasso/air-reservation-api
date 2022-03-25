using AirReservationApi.Domain;
using AirReservationApi.Infra.Repositiory;

namespace AirReservationApi.handlers
{
    public interface IAirReservationHandler
    {
        Task<string> InsertAirReservation(AirReservation reservation);
        Task<AirReservation> GetAnAirReservation(int reservationId);
        Task<IEnumerable<AirReservation>> GetManyAirReservation(SearchFilter filter);
        Task<IEnumerable<AirReservation>> GetAllAirReservation();
        Task<string> RemoveReservation(int reservationId);
    }

    public class AirReservationHandler : IAirReservationHandler
    {
        private readonly IAirReservationRepository airReservationRepository;
        private readonly AirReservationApi.Infra.Logging.ILogger logger;

        public AirReservationHandler(IAirReservationRepository airReservationRepository, AirReservationApi.Infra.Logging.ILogger logger)
        {
            this.airReservationRepository = airReservationRepository;
            this.logger = logger;
        }
        public async Task<string> InsertAirReservation(AirReservation reservation)
        {
            try
            {
                if (reservation.IsValid)
                {
                    await this.airReservationRepository.Add<AirReservation>(reservation);
                    await this.airReservationRepository.SaveChanges();
                    this.logger.Information("Air reservation inserted!", reservation);
                    return ("Air reservation inserted!");
                }
                this.logger.Warning("Invalid air reservation", reservation);
                return ("Invalid air reservation");
            }
            catch (Exception ex)
            {
                this.logger.Error($"An error has occured", ex);
                return ("An error has occured");
            }
        }

        public async Task<AirReservation> GetAnAirReservation(int reservationId)
        {
            var airReservation = await this.airReservationRepository.GetOne<AirReservation>(reservationId);

            if (airReservation == null)
            {
                this.logger.Warning($"Air reservation not found for this Id: {reservationId}");
            }

            this.logger.Information($"Reserva com Id : {reservationId} encontrada", airReservation);
            return airReservation;
        }

        public async Task<IEnumerable<AirReservation>> GetManyAirReservation(SearchFilter filter)
        {
            var airReservations = await this.airReservationRepository.GetAll<AirReservation>();

            var filteredAirReservations = FilterAirReservations(filter, airReservations);

            return filteredAirReservations;
        }

        public async Task<string> RemoveReservation(int reservationId)
        {
            var success = await this.airReservationRepository.RemoveOne(reservationId);

            if (success)
            {
                this.logger.Information($"Removed with success. reservationId: {reservationId}");
                return "Removed with success";
            }
            this.logger.Error("Error while removing air reservation");
            return "Error while removing air reservation";
        }

        private static IEnumerable<AirReservation> FilterAirReservations(SearchFilter filter, IEnumerable<AirReservation> airReservations)
        {
            var filteredAirReservations = new List<AirReservation>(airReservations);

            if (filter.Airline != null)
            {
                filteredAirReservations.RemoveAll(ar => ar.Airline != filter.Airline);
            }

            if (filter.OriginAirport != null)
            {
                filteredAirReservations.RemoveAll(ar => ar.OriginAirport != filter.OriginAirport);
            }

            if (filter.TargetAirport != null)
            {
                filteredAirReservations.RemoveAll(ar => ar.TargetAirport != filter.TargetAirport);
            }

            if (filter.ArrivalDatetime != null)
            {
                var ArrivalDate = (DateTime)filter.ArrivalDatetime;
                filteredAirReservations.RemoveAll(ar => ar.ArrivalDatetime < ArrivalDate || ar.ArrivalDatetime >= ArrivalDate.AddDays(1));
            }

            if (filter.DepartureDatetime != null)
            {
                var DepartureDate = (DateTime)filter.DepartureDatetime;
                filteredAirReservations.RemoveAll(ar => ar.DepartureDatetime < DepartureDate || ar.DepartureDatetime >= DepartureDate.AddDays(1));
            }

            if (filter.Airline == null & filter.OriginAirport == null & filter.TargetAirport == null & filter.ArrivalDatetime == null & filter.DepartureDatetime == null)
            {
                return new List<AirReservation>();
            }

            return filteredAirReservations;
        }

        public async Task<IEnumerable<AirReservation>> GetAllAirReservation()
        {
            var airReservations = await this.airReservationRepository.GetAll<AirReservation>();
            this.logger.Information("Retrieved airReservations successfully");
            return airReservations.OrderBy(ar => ar.DepartureDatetime);
        }
    }
}
