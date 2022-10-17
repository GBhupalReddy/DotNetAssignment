using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using System.Data;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class CinemaHallRepository : ICinemaHallRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;
        public CinemaHallRepository(BookMyShowContext bookMyShowContext, IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
            _dbConnection = dbConnection;
        }

        //Get all cinema halls
        public async Task<IEnumerable<CinemaHallDto>> GetCinemaHallsAsync()
        {
            var query = "select * from CinemaHall";
            var result = await _dbConnection.QueryAsync<CinemaHallDto>(query);
            return result;
        }

        // Get cinema hall using id
        public async Task<CinemaHall> GetCinemaHallAsync(int id)
        {
            var query = "select * from CinemaHall where CinemaId = @id";
            var result = await _dbConnection.QueryFirstOrDefaultAsync<CinemaHall>(query, new { id });

            return result;
        }

        // add cinema hall
        public async Task<CinemaHall> AddCinemaHallAsync(CinemaHall cinemaHall)
        {
            _bookMyShowContext.CinemaHalls.Add(cinemaHall);
            await _bookMyShowContext.SaveChangesAsync();
            return cinemaHall;
        }

        // update cinema hall using id
        public async Task<CinemaHall> UpdateCinemaHallAsynce(CinemaHall cinemaHall)
        {

            _bookMyShowContext.CinemaHalls.Update(cinemaHall);
            await _bookMyShowContext.SaveChangesAsync();
            return cinemaHall;

        }

        // delete cinema hall using id
        public async Task DeleteCinemaHallrAsync(CinemaHall cinemaHall)
        {
            _bookMyShowContext.CinemaHalls.Remove(cinemaHall);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
