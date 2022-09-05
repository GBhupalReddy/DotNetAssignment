using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class ShowRepository : IShowRepository
    {

        private readonly BookMyShowContext _bookMyShowContext;

        public ShowRepository()
        {
            _bookMyShowContext = new BookMyShowContext();
        }


        public async Task<IEnumerable<Show>> GetShowsAsync()
        {
            return await (from show in _bookMyShowContext.Shows
                          select new Show
                          {
                              ShowId = show.ShowId,
                              Date = show.Date,
                              StartTime = show.StartTime,
                              EndTime = show.EndTime,
                              CinemaHallId = show.CinemaHallId,
                              MovieId = show.MovieId
                          }).ToListAsync();

        }

        public async Task<Show> GetShowAsync(int id)
        {
            return await _bookMyShowContext.Shows.FindAsync(id);
        }

        public async Task<Show> AddShowAsync(Show show)
        {
            _bookMyShowContext.Shows.Add(show);
            await _bookMyShowContext.SaveChangesAsync();
            return show;
        }
        public async Task<Show> UpdateShowAsynce(int id, Show show)
        {
            var showToBeUpdated = await GetShowAsync(id);
            showToBeUpdated.Date = show.Date;
            showToBeUpdated.StartTime = show.StartTime;
            showToBeUpdated.EndTime = show.EndTime;
            showToBeUpdated.CinemaHallId = show.CinemaHallId;
            showToBeUpdated.MovieId = show.MovieId;
            _bookMyShowContext.Shows.Update(showToBeUpdated);
            await _bookMyShowContext.SaveChangesAsync();
            return showToBeUpdated;

        }

        public async Task DeleteShowAsync(int id)
        {
            var show = await GetShowAsync(id);
            _bookMyShowContext.Shows.Remove(show);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
