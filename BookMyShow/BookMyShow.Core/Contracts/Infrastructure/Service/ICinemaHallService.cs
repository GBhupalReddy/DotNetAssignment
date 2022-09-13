using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface ICinemaHallService
    {
        Task<CinemaHall> AddCinemaHallAsync(CinemaHall cinemaHall);
        Task DeleteCinemaHallrAsync(int id);
        Task<CinemaHall> GetCinemaHallByIdAsync(int id);
        Task<IEnumerable<CinemaHallDto>> GetCinemaHallsAsync();
        Task<CinemaHall> UpdateCinemaHallAsynce(int id, CinemaHall cinemaHall);
    }
}