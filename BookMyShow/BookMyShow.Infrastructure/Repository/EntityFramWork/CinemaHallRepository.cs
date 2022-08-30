using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BookMyShow.Core.Contracts.Infrastructure.Repository;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    internal class CinemaHallRepository : ICinemaHallRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        public CinemaHallRepository()
        {
            _bookMyShowContext = new BookMyShowContext();
        }


        public async Task<IEnumerable<CinemaHall>> GetCinemaHallsAsync()
        {
            return await (from cinemaHall in _bookMyShowContext.CinemaHalls

                          select new CinemaHall
                          {
                              CinemaHallId = cinemaHall.CinemaHallId,
                              Name = cinemaHall.Name,
                              TotalSeats = cinemaHall.TotalSeats,
                              CinemaId = cinemaHall.CinemaId,
                          }).ToListAsync();

        }

        public async Task<CinemaHall> GetCinemaHallAsync(int id)
        {
            return await _bookMyShowContext.CinemaHalls.FindAsync(id);
        }

        public async Task<CinemaHall> AddCinemaHallAsync(CinemaHall cinemaHall)
        {
            _bookMyShowContext.CinemaHalls.Add(cinemaHall);
            await _bookMyShowContext.SaveChangesAsync();
            return cinemaHall;
        }
        public async Task<CinemaHall> UpdateCinemaHallAsynce(int id, CinemaHall cinemaHall)
        {
            var cinemaHallToBeUpdated = await GetCinemaHallAsync(id);
            cinemaHallToBeUpdated.Name = cinemaHall.Name;
            cinemaHallToBeUpdated.TotalSeats = cinemaHall.TotalSeats;
            cinemaHallToBeUpdated.CinemaId = cinemaHall.CinemaId;
            _bookMyShowContext.CinemaHalls.Update(cinemaHallToBeUpdated);
            await _bookMyShowContext.SaveChangesAsync();
            return cinemaHallToBeUpdated;

        }

        public async Task DeleteCinemaHallrAsync(int id)
        {
            var cinemaHall = await GetCinemaHallAsync(id);
            _bookMyShowContext.CinemaHalls.Remove(cinemaHall);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
