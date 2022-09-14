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
            var showAvailableSeats = await (from show in _bookMyShowContext.Shows
                                    where show.ShowId == showSeat.ShowId
                                    select show.AvailableSeats).FirstAsync();
            var cinemaSeats = await (from cinema in _bookMyShowContext.CinemaSeats
                                    where cinema.CinemaSeatId == showSeat.CinemaSeatId
                                    select cinema).FirstAsync();
            // if (cinemaHall.AvailableSeats >= 0)
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
        public async Task<ShowSeat> UpdateShowSeatAsynce(ShowSeat showSeat)
        { 
            int cinemaSeatId = showSeat.CinemaSeatId;
            var query = "select SeatNumber from ShowSeat where ShowSeatId = @cinemaSeatId";
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
