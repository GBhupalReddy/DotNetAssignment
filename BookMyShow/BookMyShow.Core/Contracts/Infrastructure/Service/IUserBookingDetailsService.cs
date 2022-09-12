using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface IUserBookingDetailsService
    {
        
        Task<IEnumerable<UserBookingDto>> GetUserBookingDetalisAsync(int id);
        Task<CinemaHallDto> GetUserBookingDetalisAsync();
    }
}