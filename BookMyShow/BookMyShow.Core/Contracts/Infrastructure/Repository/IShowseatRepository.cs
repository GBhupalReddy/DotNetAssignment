using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface IShowSeatRepository
    {
        Task<ShowSeat> AddShowSeatAsync(ShowSeat showSeat);
        Task DeleteShowSeatAsync(int id);
        Task<ShowSeat> GetShowSaetAsync(int id);
        Task<IEnumerable<ShowSeatDto>> GetShowSeatsAsync();
        Task<ShowSeat> UpdateShowSeatAsynce(int id, ShowSeat showSeat);
    }
}