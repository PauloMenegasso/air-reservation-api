using AirReservationApi.Domain;
using AirReservationApi.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace AirReservationApi.Infra.Repositiory
{
    public interface IAirportRepository
    {
        Task<IEnumerable<Airport>> GetAll();
        Task InsertOne(Airport airport);
        Task<bool> Remove(string code);
        Task<bool> Update(string code, Airport airport);
    }
    public class AirportRepository : IAirportRepository
    {
        protected readonly AirReservationDBContext context;

        public AirportRepository(AirReservationDBContext context)
            => this.context = context;

        public async Task<IEnumerable<Airport>> GetAll()
            => (await this.context.Set<Airport>()
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false))
                .ToArray();

        public async Task InsertOne(Airport airport)
        {
            await this.context.Set<Airport>()
                .AddAsync(airport)
                .ConfigureAwait(false);
            await this.context
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }

        public async Task<bool> Remove(string code)
        {
            try
            {
                await this.context.Database.ExecuteSqlRawAsync(@$"
                        DELETE FROM [dbo].[Airports]
                        WHERE Code = '{code}'");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(string code, Airport newAirport)
        {
            try
            {
                await this.context.Database.ExecuteSqlRawAsync(@$"
                        UPDATE [dbo].[Airports]
                        SET Name = '{newAirport.Name}', Code = '{newAirport.Code}'
                        WHERE Code = '{code}'");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}