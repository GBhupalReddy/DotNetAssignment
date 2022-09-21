using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Infrastructure.Service
{
    public class ShowService : IShowService
    {
        private readonly IShowRepository _showRepository;
        public ShowService(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }
        public async Task<IEnumerable<ShowDto>> GetShowsAsync()
        {
            var showes = await _showRepository.GetShowsAsync();
            return showes;
        }

        // Get Show using id
        public async Task<Show> GetShowByIdAsync(int id)
        {
            var show = await _showRepository.GetShowAsync(id);
            return show;
        }

        // Add show
        public async Task<Show> AddShowAsync(Show show)
        {
            var result = await _showRepository.AddShowAsync(show);
            return result;  
        }

        // Update show using id
        public async Task<Show> UpdateShowAsynce(int id, Show show)
        {
            var showToBeUpdated = await GetShowByIdAsync(id);
            showToBeUpdated.Date = show.Date;
            showToBeUpdated.StartTime = show.StartTime;
            showToBeUpdated.EndTime = show.EndTime;
            showToBeUpdated.CinemaHallId = show.CinemaHallId;
            showToBeUpdated.MovieId = show.MovieId;
            showToBeUpdated.AvailableSeats = show.AvailableSeats;

            var result = await _showRepository.UpdateShowAsynce(showToBeUpdated);
            return result;

        }

        // Delete show using id
        public async Task DeleteShowAsync(int id)
        {
            var show = await GetShowByIdAsync(id);
            await _showRepository.DeleteShowAsync(show);
        }
    }
}
