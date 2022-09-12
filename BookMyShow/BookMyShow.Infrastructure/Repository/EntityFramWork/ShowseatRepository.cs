using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class ShowSeatRepository : IShowSeatRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;
        private readonly ICinemaHallRepository _cinemaHallRepository;

        public ShowSeatRepository(BookMyShowContext bookMyShowContext, ICinemaHallRepository cinemaHallRepository, IDbConnection dbConnection)
        {
            _cinemaHallRepository = cinemaHallRepository;
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
            var result = (await _dbConnection.QueryFirstOrDefaultAsync<ShowSeat>(query, new { id }));
            return result;
        }

        // add show seat
        public async Task<ShowSeat> AddShowSeatAsync(ShowSeat showSeat)
        {
            var cinemaHall = await (from cinema in _bookMyShowContext.CinemaHalls
                                    join cinemaSeat in _bookMyShowContext.CinemaSeats
                                    on cinema.CinemaHallId equals cinemaSeat.CinemaHallId
                                    where cinemaSeat.CinemaSeatId == showSeat.CinemaSeatId
                                    select cinema).FirstOrDefaultAsync();
            if (cinemaHall.AvailableSeats >= 0)
            {

                int cinemaSeatId = showSeat.CinemaSeatId;
                var query = "select SeatNumber from CinemaSeat where cinemaSeatId = @cinemaSeatId";
                var result = (await _dbConnection.QueryFirstOrDefaultAsync<int>(query, new { cinemaSeatId }));

                int[] thirdClass = { 1, 2 };
                int[] secondClass = { 3, 4 };
                int[] firstClass = { 5, 6 };
                bool thirdClassReault = Array.Exists(thirdClass, element => element == result);
                if (thirdClassReault)
                {
                    showSeat.Price = 200;
                }
                bool secondClassResult = Array.Exists(secondClass, element => element == result);
                if (secondClassResult)
                {
                    showSeat.Price = 300;
                }

                bool firstClassresult = Array.Exists(firstClass, element => element == result);
                if (firstClassresult)
                {
                    showSeat.Price = 400;
                }

                _bookMyShowContext.ShowSeats.Add(showSeat);
                await _bookMyShowContext.SaveChangesAsync();
                
                return showSeat;
            }
            return null;
        }

        // Update show seat using id
        public async Task<ShowSeat> UpdateShowSeatAsynce(int id, ShowSeat showSeat)
        {
            var showSeatToBeUpdated = await GetShowSaetAsync(id);
            
            showSeatToBeUpdated.Status = showSeat.Status;
            showSeatToBeUpdated.CinemaSeatId = showSeat.CinemaSeatId;
            showSeatToBeUpdated.ShowId = showSeat.ShowId;
            showSeatToBeUpdated.BookingId = showSeat.BookingId;
            int cinemaSeatId = showSeat.CinemaSeatId;
            var query = "select SeatNumber from ShowSeat where ShowSeatId = @cinemaSeatId";
            var result = (await _dbConnection.QueryFirstOrDefaultAsync<int>(query, new { cinemaSeatId }));
            int[] thirdClass = { 1, 2 };
            int[] secondClass = { 3, 4 };
            int[] firstClass = { 5, 6 };
            bool thirdClassReault = Array.Exists(thirdClass, element => element == result);
            if (thirdClassReault)
            {
                showSeatToBeUpdated.Price = 200;
            }
            bool secondClassResult = Array.Exists(secondClass, element => element == result);
            if (secondClassResult)
            {
                showSeatToBeUpdated.Price = 300;
            }

            bool firstClassresult = Array.Exists(firstClass, element => element == result);
            if (firstClassresult)
            {
                showSeatToBeUpdated.Price = 400;
            }

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
