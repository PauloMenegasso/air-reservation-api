using AirReservationApi.Domain;
using AirReservationApi.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace AirReservationApi.Infra.Repositiory
{
    public interface IAirReservationRepository
    {
        Task Add<T>(AirReservation entity) where T : class;
        Task<AirReservation> GetOne<T>(int id) where T : class;
        Task<IEnumerable<T>> GetAll<T>() where T : class;
        Task SaveChanges();
        Task<IEnumerable<T>> GetMany<T>(SearchFilter filter);
        Task<bool> RemoveOne(int id);
    }

    public class AirReservationRepository : IAirReservationRepository
    {

        protected readonly AirReservationDBContext context;

        public AirReservationRepository(AirReservationDBContext context)
            => this.context = context;
        public async Task Add<T>(AirReservation entity) where T : class
            => await this.context.Set<AirReservation>()
                .AddAsync(entity)
                .ConfigureAwait(false);

        public async Task<AirReservation> GetOne<T>(int id) where T : class
            => await this.context.Set<AirReservation>().Where(a => a.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<T>> GetAll<T>() where T : class
            => (await this.context.Set<T>()
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false))
                .ToArray();
        public async Task SaveChanges()
            => await this.context
                .SaveChangesAsync()
                .ConfigureAwait(false);

        public Task<IEnumerable<T>> GetMany<T>(SearchFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveOne(int id)
        {
            try
            {
                await this.context.Database.ExecuteSqlRawAsync(@$"
                        DELETE FROM [dbo].[AirReservations]
                        WHERE Id = {id}");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}