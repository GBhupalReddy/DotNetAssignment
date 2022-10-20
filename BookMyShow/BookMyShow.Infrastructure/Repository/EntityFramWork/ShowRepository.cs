using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using System.Data;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class ShowRepository : IShowRepository
    {

        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;

        public ShowRepository(BookMyShowContext bookMyShowContext, IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
            _dbConnection = dbConnection;
        }

        // Get all Shows
        public async Task<IEnumerable<ShowDto>> GetShowsAsync()
        {
            var query = "select * from Show";
            var result = await _dbConnection.QueryAsync<ShowDto>(query);
            return result;

        }

        // Get Show using id
        public async Task<Show> GetShowAsync(int id)
        {
            var query = "select * from Show where ShowId = @id";
            var result = (await _dbConnection.QueryFirstOrDefaultAsync<Show>(query, new { id }));
            return result;
        }

        // Add show
        public async Task<Show> AddShowAsync(Show show)
        {
            _bookMyShowContext.Shows.Add(show);
            await _bookMyShowContext.SaveChangesAsync();
            return show;
        }

        // Update show using id
        public async Task<Show> UpdateShowAsynce(Show show)
        {
            _bookMyShowContext.Shows.Update(show);
            await _bookMyShowContext.SaveChangesAsync();
            return show;

        }

        // Delete show using id
        public async Task DeleteShowAsync(Show show)
        {
            _bookMyShowContext.Shows.Remove(show);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
