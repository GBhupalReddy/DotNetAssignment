using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Infrastructure.Service
{
    public class CinemaHallService : ICinemaHallService
    {
        private readonly ICinemaHallRepository _cinemaHallRepository;
        public CinemaHallService(ICinemaHallRepository cinemaHallRepository)
        {
            _cinemaHallRepository = cinemaHallRepository;
        }
        //Get all cinema halls
        public async Task<IEnumerable<CinemaHallDto>> GetCinemaHallsAsync()
        {
            return await _cinemaHallRepository.GetCinemaHallsAsync();
        }

        // Get cinema hall using id
        public async Task<CinemaHall> GetCinemaHallByIdAsync(int id)
        {
            return await _cinemaHallRepository.GetCinemaHallAsync(id);
        }

        // add cinema hall
        public async Task<CinemaHall> AddCinemaHallAsync(CinemaHall cinemaHall)
        {
            return await _cinemaHallRepository.AddCinemaHallAsync(cinemaHall);
        }

        // update cinema hall using id
        public async Task<CinemaHall> UpdateCinemaHallAsynce(int id, CinemaHall cinemaHall)
        {
            var cinemaHallToBeUpdated = await GetCinemaHallByIdAsync(id);
            cinemaHallToBeUpdated.CinemaHallName = cinemaHall.CinemaHallName;
            cinemaHallToBeUpdated.TotalSeats = cinemaHall.TotalSeats;
            cinemaHallToBeUpdated.AvailableSeats = cinemaHall.AvailableSeats;
            cinemaHallToBeUpdated.CinemaId = cinemaHall.CinemaId;

            return await _cinemaHallRepository.UpdateCinemaHallAsynce(cinemaHallToBeUpdated);

        }

        // delete cinema hall using id
        public async Task DeleteCinemaHallrAsync(int id)
        {
            var cinemaHall = await GetCinemaHallByIdAsync(id);
            await _cinemaHallRepository.DeleteCinemaHallrAsync(cinemaHall);
        }
    }
}
