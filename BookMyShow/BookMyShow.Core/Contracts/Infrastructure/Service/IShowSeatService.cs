using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface IShowSeatService
    {
        Task<ShowSeat> AddShowSeatAsync(ShowSeat showSeat);
        Task DeleteShowSeatAsync(int id);
        Task<ShowSeat> GetShowSaetByIdAsync(int id);
        Task<IEnumerable<ShowSeatDto>> GetShowSeatsAsync();
        Task<ShowSeat> UpdateShowSeatAsynce(int id, ShowSeat showSeat);
    }
}