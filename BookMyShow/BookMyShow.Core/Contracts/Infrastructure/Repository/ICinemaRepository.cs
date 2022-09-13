using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface ICinemaRepository
    {
        Task<Cinema> AddCinemaAsync(Cinema cinema);
        Task DeleteCinemaAsync(Cinema cinema);
        Task<Cinema> GetCinemaAsync(int id);
        Task<IEnumerable<CinemaDto>> GetCinemasAsync();
        Task<Cinema> UpdateCinemaAsynce( Cinema cinema);
    }
}