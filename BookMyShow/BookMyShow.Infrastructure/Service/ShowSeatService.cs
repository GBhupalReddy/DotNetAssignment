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
            return await _showSeatRepository.GetShowSeatsAsync();

        }

        //Get show seat using id
        public async Task<ShowSeat> GetShowSaetByIdAsync(int id)
        {
            return await _showSeatRepository.GetShowSaetAsync(id);
        }

        // add show seat
        public async Task<ShowSeat> AddShowSeatAsync(ShowSeat showSeat)
        {
            return await _showSeatRepository.AddShowSeatAsync(showSeat);
        }

        // Update show seat using id
        public async Task<ShowSeat> UpdateShowSeatAsynce(int id, ShowSeat showSeat)
        {
            var showSeatToBeUpdated = await GetShowSaetByIdAsync(id);

            showSeatToBeUpdated.Status = showSeat.Status;
            showSeatToBeUpdated.CinemaSeatId = showSeat.CinemaSeatId;
            showSeatToBeUpdated.ShowId = showSeat.ShowId;
            showSeatToBeUpdated.BookingId = showSeat.BookingId;

            return await _showSeatRepository.UpdateShowSeatAsynce(showSeatToBeUpdated);

        }

        // delete show seat using id 
        public async Task DeleteShowSeatAsync(int id)
        {
            var showSeat = await GetShowSaetByIdAsync(id);
            await _showSeatRepository.DeleteShowSeatAsync(showSeat);
        }
    }
}
