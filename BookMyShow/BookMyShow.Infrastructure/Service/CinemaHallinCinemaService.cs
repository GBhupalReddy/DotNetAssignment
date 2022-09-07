using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookMyShow.Infrastructure.Service
{
    public class CinemaHallinCinemaService : ICinemaHallinCinemaService
    {
        private readonly BookMyShowContext _bookMyShowContext;
        public CinemaHallinCinemaService(BookMyShowContext bookMyShowContext)
        {
            _bookMyShowContext = bookMyShowContext;
        }

        public async Task<IEnumerable<CinemaHallDto>> GetCinemainCinemaHall(string cinemaName, string cityName)
        {
            var result = await (from cinemaHall in _bookMyShowContext.CinemaHalls
                                join cinema in _bookMyShowContext.Cinemas
                                on cinemaHall.CinemaId equals cinema.CinemaId
                                join city in _bookMyShowContext.Cities
                                on cinema.CityId equals city.CityId
                               where cinema.Name == cinemaName && city.Name== cityName
                                select new CinemaHallDto
                                {
                                    CinemaHallId = cinemaHall.CinemaHallId,
                                    Name = cinemaHall.Name,
                                    TotalSeats = cinemaHall.TotalSeats,
                                    CinemaId = cinemaHall.CinemaId
                                }).ToListAsync();
            return result;
        }
    }
}
