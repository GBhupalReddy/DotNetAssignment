using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        public CinemaRepository()
        {
            _bookMyShowContext = new BookMyShowContext();
        }


        public async Task<IEnumerable<Cinema>> GetCinemasAsync()
        {
            return await (from cinema in _bookMyShowContext.Cinemas

                          select new Cinema
                          {
                              CinemaId = cinema.CinemaId,
                              Name = cinema.Name,
                              TotalCinemaHalls = cinema.TotalCinemaHalls,
                              CityId = cinema.CityId,
                          }).ToListAsync();

        }

        public async Task<Cinema> GetCinemaAsync(int id)
        {
            return await _bookMyShowContext.Cinemas.FindAsync(id);
        }

        public async Task<Cinema> AddCinemaAsync(Cinema cinema)
        {
            _bookMyShowContext.Cinemas.Add(cinema);
            await _bookMyShowContext.SaveChangesAsync();
            return cinema;
        }
        public async Task<Cinema> UpdateCinemaAsynce(int id, Cinema cinema)
        {
            var CinemaToBeUpdated = await GetCinemaAsync(id);
            CinemaToBeUpdated.Name = cinema.Name;
            CinemaToBeUpdated.TotalCinemaHalls = cinema.TotalCinemaHalls;
            CinemaToBeUpdated.CityId = cinema.CityId;
            _bookMyShowContext.Cinemas.Update(CinemaToBeUpdated);
            await _bookMyShowContext.SaveChangesAsync();
            return CinemaToBeUpdated;

        }

        public async Task DeleteCinemaAsync(int id)
        {
            var cinema = await GetCinemaAsync(id);
            _bookMyShowContext.Cinemas.Remove(cinema);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
