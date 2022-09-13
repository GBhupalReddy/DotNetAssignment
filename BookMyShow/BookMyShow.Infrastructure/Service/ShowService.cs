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
            return await _showRepository.GetShowsAsync();
        }

        // Get Show using id
        public async Task<Show> GetShowByIdAsync(int id)
        {
            return await _showRepository.GetShowAsync(id);
        }

        // Add show
        public async Task<Show> AddShowAsync(Show show)
        {
            return await _showRepository.AddShowAsync(show);
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

            return await _showRepository.UpdateShowAsynce(showToBeUpdated);

        }

        // Delete show using id
        public async Task DeleteShowAsync(int id)
        {
            var show = await GetShowByIdAsync(id);
            await _showRepository.DeleteShowAsync(show);
        }
    }
}
