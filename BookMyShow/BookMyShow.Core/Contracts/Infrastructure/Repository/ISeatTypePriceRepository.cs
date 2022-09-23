using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface ISeatTypePriceRepository
    {
        Task<IEnumerable<SeatTypePrice>> GetSeatTypePrices();
        Task<SeatTypePrice> GetSeatTypePriceBYTypeAsync(int seatType);
        Task<SeatTypePrice> AddSeatTypePriceAsync(SeatTypePrice seatTypePrice);
        Task<SeatTypePrice> UpdateSeatTypePrice(SeatTypePrice seatTypePrice);
        Task DeleteSeatTypePrice(SeatTypePrice seatTypePrice);
    }
}