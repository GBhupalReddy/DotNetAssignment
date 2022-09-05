using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class ShowRepository : IShowRepository
    {

        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;

        public ShowRepository(BookMyShowContext bookMyShowContext, IDbConnection dbConnection)
        {
            _bookMyShowContext =bookMyShowContext;
            _dbConnection = dbConnection;
        }

        // Get all Shows
        public async Task<IEnumerable<Show>> GetShowsAsync()
        {
            var query = "select * from Show";
            var result = await _dbConnection.QueryAsync<Show>(query);
            return result;

        }

        // Get Show using id
        public async Task<Show> GetShowAsync(int id)
        {
            var query = "select * from Show where ShowId = @id";
            var result = await _dbConnection.QueryFirstAsync<Show>(query, new { id = id });
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
        public async Task<Show> UpdateShowAsynce(int id, Show show)
        {
            var showToBeUpdated = await GetShowAsync(id);
            showToBeUpdated.Date = show.Date;
            showToBeUpdated.StartTime = show.StartTime;
            showToBeUpdated.EndTime = show.EndTime;
            showToBeUpdated.CinemaHallId = show.CinemaHallId;
            showToBeUpdated.MovieId = show.MovieId;
            _bookMyShowContext.Shows.Update(showToBeUpdated);
            await _bookMyShowContext.SaveChangesAsync();
            return showToBeUpdated;

        }

        // Delete show using id
        public async Task DeleteShowAsync(int id)
        {
            var show = await GetShowAsync(id);
            _bookMyShowContext.Shows.Remove(show);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
