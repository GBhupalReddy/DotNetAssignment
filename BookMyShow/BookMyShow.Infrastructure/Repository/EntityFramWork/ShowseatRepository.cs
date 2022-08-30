using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class ShowseatRepository : IShowseatRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;

        public ShowseatRepository()
        {
            _bookMyShowContext = new BookMyShowContext();
        }


        public async Task<IEnumerable<ShowSeat>> GetShowSeatsAsync()
        {
            return await (from showSeat in _bookMyShowContext.ShowSeats
                          select new ShowSeat
                          {
                              ShowSeatId = showSeat.ShowSeatId,
                              Status = showSeat.Status,
                              Price = showSeat.Price,
                              CinemaSeatId = showSeat.CinemaSeatId,
                              ShowId = showSeat.ShowId,
                              BookingId = showSeat.BookingId
                          }).ToListAsync();

        }

        public async Task<ShowSeat> GetShowSaetAsync(int id)
        {
            return await _bookMyShowContext.ShowSeats.FindAsync(id);
        }

        public async Task<ShowSeat> AddShowSeatAsync(ShowSeat showSeat)
        {
            _bookMyShowContext.ShowSeats.Add(showSeat);
            await _bookMyShowContext.SaveChangesAsync();
            return showSeat;
        }
        public async Task<ShowSeat> UpdateShowSeatAsynce(int id, ShowSeat showSeat)
        {
            var showSeatToBeUpdated = await GetShowSaetAsync(id);
            showSeatToBeUpdated.ShowSeatId = showSeat.ShowSeatId;
            showSeatToBeUpdated.Status = showSeat.Status;
            showSeatToBeUpdated.Price = showSeat.Price;
            showSeatToBeUpdated.CinemaSeatId = showSeat.CinemaSeatId;
            showSeatToBeUpdated.ShowId = showSeat.ShowId;
            showSeatToBeUpdated.BookingId = showSeat.BookingId;

            _bookMyShowContext.ShowSeats.Update(showSeatToBeUpdated);
            await _bookMyShowContext.SaveChangesAsync();
            return showSeatToBeUpdated;

        }

        public async Task DeleteShowSeatAsync(int id)
        {
            var showSeat = await GetShowSaetAsync(id);
            _bookMyShowContext.ShowSeats.Remove(showSeat);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
