using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface ICinemaRepository
    {
        Task<Cinema> AddCinemaAsync(Cinema cinema);
        Task DeleteCinemaAsync(int id);
        Task<Cinema> GetCinemaAsync(int id);
        Task<IEnumerable<Cinema>> GetCinemasAsync();
        Task<Cinema> UpdateCinemaAsynce(int id, Cinema cinema);
    }
}