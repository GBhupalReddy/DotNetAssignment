using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface ICinemaService
    {
        Task<Cinema> AddCinemaAsync(Cinema cinema);
        Task DeleteCinemaAsync(int id);
        Task<Cinema> GetCinemaByIdAsync(int id);
        Task<IEnumerable<CinemaDto>> GetCinemasAsync();
        Task<Cinema> UpdateCinemaAsynce(int id, Cinema cinema);
    }
}