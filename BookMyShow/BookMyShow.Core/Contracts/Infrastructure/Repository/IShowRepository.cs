using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface IShowRepository
    {
        Task<Show> AddShowAsync(Show show);
        Task DeleteShowAsync(Show show);
        Task<Show> GetShowAsync(int id);
        Task<IEnumerable<ShowDto>> GetShowsAsync();
        Task<Show> UpdateShowAsynce(Show show);
    }
}