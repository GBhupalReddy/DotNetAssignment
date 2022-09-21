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
            var cinemaHalls = await _cinemaHallRepository.GetCinemaHallsAsync();
            return cinemaHalls;
        }

        // Get cinema hall using id
        public async Task<CinemaHall> GetCinemaHallByIdAsync(int id)
        {
            var cinemaHall = await _cinemaHallRepository.GetCinemaHallAsync(id);
            return cinemaHall;

        }

        // add cinema hall
        public async Task<CinemaHall> AddCinemaHallAsync(CinemaHall cinemaHall)
        {
            var result =await _cinemaHallRepository.AddCinemaHallAsync(cinemaHall);
            return result;
        }

        // update cinema hall using id
        public async Task<CinemaHall> UpdateCinemaHallAsynce(int id, CinemaHall cinemaHall)
        {
            var cinemaHallToBeUpdated = await GetCinemaHallByIdAsync(id);
            cinemaHallToBeUpdated.CinemaHallName = cinemaHall.CinemaHallName;
            cinemaHallToBeUpdated.TotalSeats = cinemaHall.TotalSeats;
            cinemaHallToBeUpdated.CinemaId = cinemaHall.CinemaId;

            var result = await _cinemaHallRepository.UpdateCinemaHallAsynce(cinemaHallToBeUpdated);
            return result;

        }

        // delete cinema hall using id
        public async Task DeleteCinemaHallrAsync(int id)
        {
            var cinemaHall = await GetCinemaHallByIdAsync(id);
            await _cinemaHallRepository.DeleteCinemaHallrAsync(cinemaHall);
        }
    }
}
