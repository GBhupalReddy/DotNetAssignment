using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using System.Data;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class SeatTypePriceRepository : ISeatTypePriceRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;

        public SeatTypePriceRepository(BookMyShowContext bookMyShowContext, IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<SeatTypePrice>> GetSeatTypePrices()
        {
            var Query = " Select * from SeatTypePrice";
            var seatTypePrices = await _dbConnection.QueryAsync<SeatTypePrice>(Query);
            return seatTypePrices;
        }

        public async Task<SeatTypePrice> GetSeatTypePriceBYTypeAsync(int seatType)
        {
            var Query = "Select * from SeatTypePrice where SeatType = @seatType";
            var seatTypePrice = await _dbConnection.QueryFirstOrDefaultAsync<SeatTypePrice>(Query, new { seatType });
            return seatTypePrice;
        }

        public async Task<SeatTypePrice> AddSeatTypePriceAsync(SeatTypePrice seatTypePrice)
        {
            _bookMyShowContext.Add(seatTypePrice);
            await _bookMyShowContext.SaveChangesAsync();
            return seatTypePrice;
        }

        public async Task<SeatTypePrice> UpdateSeatTypePrice(SeatTypePrice seatTypePrice)
        {
            _bookMyShowContext.Update(seatTypePrice);
            await _bookMyShowContext.SaveChangesAsync();

            return seatTypePrice;
        }

        public async Task DeleteSeatTypePrice(SeatTypePrice seatTypePrice)
        {
            _bookMyShowContext.Remove(seatTypePrice);
            await _bookMyShowContext.SaveChangesAsync();
        }

    }
}
