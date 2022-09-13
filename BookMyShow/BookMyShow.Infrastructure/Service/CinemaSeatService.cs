using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Core.Enums;
using System.Data;

namespace BookMyShow.Infrastructure.Service
{
    public class CinemaSeatService : ICinemaSeatService
    {
        private readonly ICinemaSeatRepository _cinemaSeatRepository;
        private readonly IDbConnection _dbConnection;
        public CinemaSeatService(ICinemaSeatRepository cinemaSeatRepository, IDbConnection dbConnection)
        {
            _cinemaSeatRepository = cinemaSeatRepository;
            _dbConnection = dbConnection;
        }
        // Get all cinema seats
        public async Task<IEnumerable<CinemaSeatDto>> GetCinemaSeatsAsync()
        {
            return await _cinemaSeatRepository.GetCinemaSeatsAsync();
        }

        // Get cinema seat using id
        public async Task<CinemaSeat> GetCinemaSeatByIdAsync(int id)
        {
            return await _cinemaSeatRepository.GetCinemaSeatAsync(id);
        }

        // Add cinema seat
        public async Task<CinemaSeat> AddCinemaSeatAsync(CinemaSeat cinemaSeat)
        {
            var result = cinemaSeat.SeatNumber;
            int[] thirdClass = { 1, 2 };
            int[] secondClass = { 3, 4 };
            int[] firstClass = { 5, 6 };
            bool thirdClassReault = Array.Exists(thirdClass, element => element == result);
            if (thirdClassReault)
            {
                cinemaSeat.Type = (int)CinemaSeatType.ThirdClass;
            }
            bool secondClassResult = Array.Exists(secondClass, element => element == result);
            if (secondClassResult)
            {
                cinemaSeat.Type = (int)CinemaSeatType.SecondClass;
            }

            bool firstClassresult = Array.Exists(firstClass, element => element == result);
            if (firstClassresult)
            {
                cinemaSeat.Type = (int)CinemaSeatType.FirstClass;
            }
            return await _cinemaSeatRepository.AddCinemaSeatAsync(cinemaSeat);
        }

        //Update cinema seat using id
        public async Task<CinemaSeat> UpdateCinemaSeatAsynce(int id, CinemaSeat cinemaSeat)
        {
            var cinemaSeatToBeUpdated = await GetCinemaSeatByIdAsync(id);
            cinemaSeatToBeUpdated.SeatNumber = cinemaSeat.SeatNumber;
            cinemaSeatToBeUpdated.Type = cinemaSeat.Type;
            cinemaSeatToBeUpdated.CinemaHallId = cinemaSeat.CinemaHallId;

            return await _cinemaSeatRepository.UpdateCinemaSeatAsynce( cinemaSeatToBeUpdated);

        }

        //Delete cinema seat using id
        public async Task DeleteCinemaSeatAsync(int id)
        {
            var cinemaSeat = await GetCinemaSeatByIdAsync(id);
            await _cinemaSeatRepository.DeleteCinemaSeatAsync(cinemaSeat);
        }
    }
}
