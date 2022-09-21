using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface IShowSeatRepository
    {
        Task<ShowSeat> AddShowSeatAsync(ShowSeat showSeat);
        Task DeleteShowSeatAsync(ShowSeat showSeat);
        Task<ShowSeat> GetShowSaetAsync(int id);
        Task<IEnumerable<ShowSeatDto>> GetShowSeatsAsync();
        Task<ShowSeat> UpdateShowSeatAsynce( ShowSeat showSeat);
    }
}