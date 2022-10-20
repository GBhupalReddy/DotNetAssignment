using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Entities;

namespace BookMyShow.Infrastructure.Service
{
    public class SeatTypePriceService : ISeatTypePriceService
    {
        private readonly ISeatTypePriceRepository _seatTypePriceRepository;

        public SeatTypePriceService(ISeatTypePriceRepository seatTypePriceRepository)
        {
            _seatTypePriceRepository = seatTypePriceRepository;
        }

        public async Task<IEnumerable<SeatTypePrice>> GetSeatTypePrices()
        {
            var seatTypePrices = await _seatTypePriceRepository.GetSeatTypePrices();
            return seatTypePrices;
        }

        public async Task<SeatTypePrice> GetSeatTypePriceBYTypeAsync(int seatType)
        {
            var seatTypePrice = await _seatTypePriceRepository.GetSeatTypePriceBYTypeAsync(seatType);
            return seatTypePrice;
        }

        public async Task<SeatTypePrice> AddSeatTypePriceAsync(SeatTypePrice seatTypePrice)
        {
            var seatTypePriceResult = await _seatTypePriceRepository.AddSeatTypePriceAsync(seatTypePrice);
            return seatTypePriceResult;
        }

        public async Task<SeatTypePrice> UpdateSeatTypePrice(int seatType, SeatTypePrice seatTypePrice)
        {
            var seatTypePeiceToBeUpdated = await GetSeatTypePriceBYTypeAsync(seatType);
            seatTypePeiceToBeUpdated.Price = seatTypePrice.Price;
            var seatTypePriceResult = await _seatTypePriceRepository.UpdateSeatTypePrice(seatTypePeiceToBeUpdated);
            return seatTypePriceResult;
        }

        public async Task DeleteSeatTypePrice(int seatType)
        {
            var seatTypePeice = await GetSeatTypePriceBYTypeAsync(seatType);
            await _seatTypePriceRepository.UpdateSeatTypePrice(seatTypePeice);
        }


    }
}
