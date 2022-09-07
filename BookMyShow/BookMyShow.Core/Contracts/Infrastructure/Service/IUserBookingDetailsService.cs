using BookMyShow.Core.Dto;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface IUserBookingDetailsService
    {
        Task<IEnumerable<UserBookingDto>> GetUserBookingDetalisAsync(int id);
    }
}