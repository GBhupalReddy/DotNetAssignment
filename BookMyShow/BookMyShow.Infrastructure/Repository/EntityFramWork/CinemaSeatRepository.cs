using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using System.Data;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class CinemaSeatRepository : ICinemaSeatRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;
        public CinemaSeatRepository(BookMyShowContext bookMyShowContext, IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
            _dbConnection = dbConnection;
        }

        // Get all cinema seats
        public async Task<IEnumerable<CinemaSeatDto>> GetCinemaSeatsAsync()
        {
            var query = "select * from CinemaSeat";
            var result = await _dbConnection.QueryAsync<CinemaSeatDto>(query);
            return result;
        }

        // Get cinema seat using id
        public async Task<CinemaSeat> GetCinemaSeatAsync(int id)
        {
            var query = "select * from CinemaSeat where CinemaSeatId = @id";
            var result = (await _dbConnection.QueryFirstOrDefaultAsync<CinemaSeat>(query, new { id }));
            return result;
        }

        // Add cinema seat
        public async Task<CinemaSeat> AddCinemaSeatAsync(CinemaSeat cinemaSeat)
        {

            _bookMyShowContext.CinemaSeats.Add(cinemaSeat);
            await _bookMyShowContext.SaveChangesAsync();
            return cinemaSeat;
        }

        //Update cinema seat using id
        public async Task<CinemaSeat> UpdateCinemaSeatAsynce(CinemaSeat cinemaSeat)
        {
            _bookMyShowContext.CinemaSeats.Update(cinemaSeat);
            await _bookMyShowContext.SaveChangesAsync();
            return cinemaSeat;

        }

        //Delete cinema seat using id
        public async Task DeleteCinemaSeatAsync(CinemaSeat cinemaSeat)
        {
            _bookMyShowContext.CinemaSeats.Remove(cinemaSeat);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
