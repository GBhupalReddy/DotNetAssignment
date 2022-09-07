using BookMyShow.Core.Dto;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface ICinemaHallinCinemaService
    {
        Task<IEnumerable<CinemaHallDto>> GetCinemainCinemaHall(string cinemaName, string cityName);
    }
}