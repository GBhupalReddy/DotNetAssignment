using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
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
        public async Task<IEnumerable<CinemaDto>> GetCinemasAsync()
        {
            var query = "execute GetCinemas";
            var result = await _dbConnection.QueryAsync<CinemaDto>(query);
            return result;

        }

        // Get cinema using id
        public async Task<Cinema> GetCinemaAsync(int id)
        {
            var query = "execute GetCinemaById @id";
            var result = (await _dbConnection.QueryFirstOrDefaultAsync<Cinema>(query, new { id }));
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
        public async Task<Cinema> UpdateCinemaAsynce(Cinema cinema)
        {
     
            _bookMyShowContext.Cinemas.Update(cinema);
            await _bookMyShowContext.SaveChangesAsync();
            return cinema;

        }

        //deleted cinema using id
        public async Task DeleteCinemaAsync(Cinema cinema)
        {
            _bookMyShowContext.Cinemas.Remove(cinema);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
