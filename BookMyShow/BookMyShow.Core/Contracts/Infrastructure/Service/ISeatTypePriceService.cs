using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface ISeatTypePriceService
    {
        Task<IEnumerable<SeatTypePrice>> GetSeatTypePrices();
        Task<SeatTypePrice> GetSeatTypePriceBYTypeAsync(int seatType);
        Task<SeatTypePrice> AddSeatTypePriceAsync(SeatTypePrice seatTypePrice);
        Task<SeatTypePrice> UpdateSeatTypePrice(int seatType,SeatTypePrice seatTypePrice);
        Task DeleteSeatTypePrice(int seatType);
    }
}
