using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface IShowseatRepository
    {
        Task<ShowSeat> AddShowSeatAsync(ShowSeat showSeat);
        Task DeleteShowSeatAsync(int id);
        Task<ShowSeat> GetShowSaetAsync(int id);
        Task<IEnumerable<ShowSeat>> GetShowSeatsAsync();
        Task<ShowSeat> UpdateShowSeatAsynce(int id, ShowSeat showSeat);
    }
}