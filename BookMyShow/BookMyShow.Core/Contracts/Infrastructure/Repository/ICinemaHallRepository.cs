using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{

     public interface  ICinemaHallRepository
    {
        Task<CinemaHall> AddCinemaHallAsync(CinemaHall cinemaHall);
        Task DeleteCinemaHallrAsync(CinemaHall cinemaHall);
        Task<CinemaHall> GetCinemaHallAsync(int id);
        Task<IEnumerable<CinemaHallDto>> GetCinemaHallsAsync();
        Task<CinemaHall> UpdateCinemaHallAsynce( CinemaHall cinemaHall);
    }
}