using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Infrastructure.Service
{
    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository _cinemaRepository;
        public CinemaService(ICinemaRepository cinemaRepository)
        {
            _cinemaRepository = cinemaRepository;
        }

        // Get all cinemas
        public async Task<IEnumerable<CinemaDto>> GetCinemasAsync()
        {
            var cinemas = await _cinemaRepository.GetCinemasAsync();
            return cinemas;

        }

        // Get cinema using id
        public async Task<Cinema> GetCinemaByIdAsync(int id)
        {
            var cinema = await _cinemaRepository.GetCinemaAsync(id);
            return cinema;
        }

        //Add cinema
        public async Task<Cinema> AddCinemaAsync(Cinema cinema)
        {
            var cinemaResult = await _cinemaRepository.AddCinemaAsync(cinema);
            return cinemaResult;
        }

        // update cinema using id
        public async Task<Cinema> UpdateCinemaAsynce(int id, Cinema cinema)
        {
            var CinemaToBeUpdated = await GetCinemaByIdAsync(id);
            CinemaToBeUpdated.CinemaName = cinema.CinemaName;
            CinemaToBeUpdated.TotalCinemaHalls = cinema.TotalCinemaHalls;
            CinemaToBeUpdated.CityId = cinema.CityId;

            var cinemaResult = await _cinemaRepository.UpdateCinemaAsynce(CinemaToBeUpdated);
            return cinemaResult;

        }

        //deleted cinema using id
        public async Task DeleteCinemaAsync(int id)
        {
            var cinema = await GetCinemaByIdAsync(id);
            await _cinemaRepository.DeleteCinemaAsync(cinema);
        }
    }
}
