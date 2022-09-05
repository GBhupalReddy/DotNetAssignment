using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{

     public interface  ICinemaHallRepository
    {
        Task<CinemaHall> AddCinemaHallAsync(CinemaHall cinemaHall);
        Task DeleteCinemaHallrAsync(int id);
        Task<CinemaHall> GetCinemaHallAsync(int id);
        Task<IEnumerable<CinemaHall>> GetCinemaHallsAsync();
        Task<CinemaHall> UpdateCinemaHallAsynce(int id, CinemaHall cinemaHall);
    }
}