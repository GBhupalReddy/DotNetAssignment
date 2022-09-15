using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using System.Data;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class ShowSeatRepository : IShowSeatRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly ICinemaSeatRepository _cinemaSeatRepository;
        private readonly IDbConnection _dbConnection;

        public ShowSeatRepository(BookMyShowContext bookMyShowContext, ICinemaSeatRepository cinemaSeatRepository, IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
            _cinemaSeatRepository = cinemaSeatRepository;
            _dbConnection = dbConnection;
        }

        // Get all show seat seats
        public async Task<IEnumerable<ShowSeatDto>> GetShowSeatsAsync()
        {
            var query = "select * from ShowSeat";
            var result = await _dbConnection.QueryAsync<ShowSeatDto>(query);
            return result;

        }

        //Get show seat using id
        public async Task<ShowSeat> GetShowSaetAsync(int id)
        {
            var query = "select * from ShowSeat where ShowSeatId = @id";
            var result = (await _dbConnection.QueryFirstOrDefaultAsync<ShowSeat>(query, new { id }));
            return result;
        }

        // add show seat
        public async Task<ShowSeat> AddShowSeatAsync(ShowSeat showSeat)
        {
                _bookMyShowContext.ShowSeats.Add(showSeat);
                await _bookMyShowContext.SaveChangesAsync();

                return showSeat;
            
        }

        // Update show seat using id
        public async Task<ShowSeat> UpdateShowSeatAsynce(ShowSeat showSeat)
        { 

            _bookMyShowContext.ShowSeats.Update(showSeat);
            await _bookMyShowContext.SaveChangesAsync();
            return showSeat;

        }

        // delete show seat using id 
        public async Task DeleteShowSeatAsync(ShowSeat showSeat)
        {
            _bookMyShowContext.ShowSeats.Remove(showSeat);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
