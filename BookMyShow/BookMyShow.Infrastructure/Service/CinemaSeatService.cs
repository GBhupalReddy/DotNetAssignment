using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Infrastructure.Service
{
    public class CinemaSeatService : ICinemaSeatService
    {
        private readonly ICinemaSeatRepository _cinemaSeatRepository;
        public CinemaSeatService(ICinemaSeatRepository cinemaSeatRepository)
        {
            _cinemaSeatRepository = cinemaSeatRepository;
        }
        // Get all cinema seats
        public async Task<IEnumerable<CinemaSeatDto>> GetCinemaSeatsAsync()
        {
            var cinemaSeats = await _cinemaSeatRepository.GetCinemaSeatsAsync();
            return cinemaSeats;
        }

        // Get cinema seat using id
        public async Task<CinemaSeat> GetCinemaSeatByIdAsync(int id)
        {
            var cinemaseat = await _cinemaSeatRepository.GetCinemaSeatAsync(id);
            return cinemaseat;
        }

        // Add cinema seat
        public async Task<CinemaSeat> AddCinemaSeatAsync(CinemaSeat cinemaSeat)
        {

            var cinemaseatResult = await _cinemaSeatRepository.AddCinemaSeatAsync(cinemaSeat);
            return cinemaseatResult;
        }

        //Update cinema seat using id
        public async Task<CinemaSeat> UpdateCinemaSeatAsynce(int id, CinemaSeat cinemaSeat)
        {
            var cinemaSeatToBeUpdated = await GetCinemaSeatByIdAsync(id);
            cinemaSeatToBeUpdated.SeatNumber = cinemaSeat.SeatNumber;
            cinemaSeatToBeUpdated.SeatType = cinemaSeat.SeatType;
            cinemaSeatToBeUpdated.CinemaHallId = cinemaSeat.CinemaHallId;

            var cinemaseatResult = await _cinemaSeatRepository.UpdateCinemaSeatAsynce(cinemaSeatToBeUpdated);
            return cinemaseatResult;

        }

        //Delete cinema seat using id
        public async Task DeleteCinemaSeatAsync(int id)
        {
            var cinemaSeat = await GetCinemaSeatByIdAsync(id);
            await _cinemaSeatRepository.DeleteCinemaSeatAsync(cinemaSeat);
        }
    }
}
