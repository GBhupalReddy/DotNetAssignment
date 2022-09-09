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
            var query = "select * from Cinema cinema inner join City city on cinema.CityId=city.CityId";
            var result = await _dbConnection.QueryAsync<CinemaDto>(query);
            return result;

        }

        // Get cinema using id
        public async Task<Cinema> GetCinemaAsync(int id)
        {
            var query = "select * from Cinema where CinemaId = @id";
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
        public async Task<Cinema> UpdateCinemaAsynce(int id, Cinema cinema)
        {
            var CinemaToBeUpdated = await GetCinemaAsync(id);
            CinemaToBeUpdated.CinemaName = cinema.CinemaName;
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
