using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Core.Enums;

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
            var cinemaSeats =await _cinemaSeatRepository.GetCinemaSeatsAsync();
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
            var seatNumber = cinemaSeat.SeatNumber;
            int[] thirdClass = { 1, 2 };
            int[] secondClass = { 3, 4 };
            int[] firstClass = { 5, 6 };
            bool thirdClassReault = Array.Exists(thirdClass, element => element == seatNumber);
            if (thirdClassReault)
            {
                cinemaSeat.Type = (int)CinemaSeatType.ThirdClass;
            }
            bool secondClassResult = Array.Exists(secondClass, element => element == seatNumber);
            if (secondClassResult)
            {
                cinemaSeat.Type = (int)CinemaSeatType.SecondClass;
            }

            bool firstClassresult = Array.Exists(firstClass, element => element == seatNumber);
            if (firstClassresult)
            {
                cinemaSeat.Type = (int)CinemaSeatType.FirstClass;
            }
            var result = await _cinemaSeatRepository.AddCinemaSeatAsync(cinemaSeat);
            return result;
        }

        //Update cinema seat using id
        public async Task<CinemaSeat> UpdateCinemaSeatAsynce(int id, CinemaSeat cinemaSeat)
        {
            var cinemaSeatToBeUpdated = await GetCinemaSeatByIdAsync(id);
            cinemaSeatToBeUpdated.SeatNumber = cinemaSeat.SeatNumber;
            cinemaSeatToBeUpdated.Type = cinemaSeat.Type;
            cinemaSeatToBeUpdated.CinemaHallId = cinemaSeat.CinemaHallId;

            var result = await _cinemaSeatRepository.UpdateCinemaSeatAsynce( cinemaSeatToBeUpdated);
            return result;

        }

        //Delete cinema seat using id
        public async Task DeleteCinemaSeatAsync(int id)
        {
            var cinemaSeat = await GetCinemaSeatByIdAsync(id);
            await _cinemaSeatRepository.DeleteCinemaSeatAsync(cinemaSeat);
        }
    }
}
