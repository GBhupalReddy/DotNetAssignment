using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;
        public CinemaRepository(BookMyShowContext bookMyShowContext, IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
            _dbConnection = dbConnection;
        }

        // Get all cinemas
        public async Task<IEnumerable<Cinema>> GetCinemasAsync()
        {
            var query = "select * from Cinema";
            var result = await _dbConnection.QueryAsync<Cinema>(query);
            return result;

        }

        // Get cinema using id
        public async Task<Cinema> GetCinemaAsync(int id)
        {
            var query = "select * from Cinema where CinemaId = @id";
            var result = await _dbConnection.QueryFirstAsync<Cinema>(query, new {id = id});
            return result;

        }

        //Add cinema
        public async Task<Cinema> AddCinemaAsync(Cinema cinema)
        {
            _bookMyShowContext.Cinemas.Add(cinema);
            await _bookMyShowContext.SaveChangesAsync();
            return cinema;
        }

        // update cinema using id
        public async Task<Cinema> UpdateCinemaAsynce(int id, Cinema cinema)
        {
            var CinemaToBeUpdated = await GetCinemaAsync(id);
            CinemaToBeUpdated.Name = cinema.Name;
            CinemaToBeUpdated.TotalCinemaHalls = cinema.TotalCinemaHalls;
            CinemaToBeUpdated.CityId = cinema.CityId;
            _bookMyShowContext.Cinemas.Update(CinemaToBeUpdated);
            await _bookMyShowContext.SaveChangesAsync();
            return CinemaToBeUpdated;

        }

        //deleted cinema using id
        public async Task DeleteCinemaAsync(int id)
        {
            var cinema = await GetCinemaAsync(id);
            _bookMyShowContext.Cinemas.Remove(cinema);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
