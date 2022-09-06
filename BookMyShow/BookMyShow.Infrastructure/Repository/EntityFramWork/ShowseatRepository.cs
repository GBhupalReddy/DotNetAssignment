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
        private readonly IDbConnection _dbConnection;
        public ShowSeatRepository(BookMyShowContext bookMyShowContext, IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
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
            var result = (await _dbConnection.QueryAsync<ShowSeat>(query, new { id })).FirstOrDefault();
           // var result = await _dbConnection.QueryFirstAsync<ShowSeat>(query, new { id = id });
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
        public async Task<ShowSeat> UpdateShowSeatAsynce(int id, ShowSeat showSeat)
        {
            var showSeatToBeUpdated = await GetShowSaetAsync(id);
            
            showSeatToBeUpdated.Status = showSeat.Status;
            showSeatToBeUpdated.Price = showSeat.Price;
            showSeatToBeUpdated.CinemaSeatId = showSeat.CinemaSeatId;
            showSeatToBeUpdated.ShowId = showSeat.ShowId;
            showSeatToBeUpdated.BookingId = showSeat.BookingId;

            _bookMyShowContext.ShowSeats.Update(showSeatToBeUpdated);
            await _bookMyShowContext.SaveChangesAsync();
            return showSeatToBeUpdated;

        }

        // delete show seat using id 
        public async Task DeleteShowSeatAsync(int id)
        {
            var showSeat = await GetShowSaetAsync(id);
            _bookMyShowContext.ShowSeats.Remove(showSeat);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
