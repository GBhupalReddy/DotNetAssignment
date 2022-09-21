using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface IShowService
    {
        Task<Show> AddShowAsync(Show show);
        Task DeleteShowAsync(int id);
        Task<Show> GetShowByIdAsync(int id);
        Task<IEnumerable<ShowDto>> GetShowsAsync();
        Task<Show> UpdateShowAsynce(int id, Show show);
    }
}