using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Infrastructure.Service
{
    public class ShowSeatService : IShowSeatService
    {
        private readonly IShowSeatRepository _showSeatRepository;
        public ShowSeatService(IShowSeatRepository showSeatRepository)
        {
            _showSeatRepository = showSeatRepository;
        }
        // Get all show seat seats
        public async Task<IEnumerable<ShowSeatDto>> GetShowSeatsAsync()
        {
            var showSeats = await _showSeatRepository.GetShowSeatsAsync();
            return showSeats;

        }

        //Get show seat using id
        public async Task<ShowSeat> GetShowSaetByIdAsync(int id)
        {
            var showSeat = await _showSeatRepository.GetShowSaetAsync(id);
            return showSeat;
        }

        // add show seat
        public async Task<ShowSeat> AddShowSeatAsync(ShowSeat showSeat)
        {
            var result = await _showSeatRepository.AddShowSeatAsync(showSeat);
            return result;
        }

        // Update show seat using id
        public async Task<ShowSeat> UpdateShowSeatAsynce(int id, ShowSeat showSeat)
        {
            var showSeatToBeUpdated = await GetShowSaetByIdAsync(id);

            showSeatToBeUpdated.Status = showSeat.Status;
            showSeatToBeUpdated.CinemaSeatId = showSeat.CinemaSeatId;
            showSeatToBeUpdated.ShowId = showSeat.ShowId;
            showSeatToBeUpdated.BookingId = showSeat.BookingId;

            var result = await _showSeatRepository.UpdateShowSeatAsynce(showSeatToBeUpdated);
            return result;

        }

        // delete show seat using id 
        public async Task DeleteShowSeatAsync(int id)
        {
            var showSeat = await GetShowSaetByIdAsync(id);
            await _showSeatRepository.DeleteShowSeatAsync(showSeat);
        }
    }
}
