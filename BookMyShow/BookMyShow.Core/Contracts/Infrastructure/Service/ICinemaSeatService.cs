using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface ICinemaSeatService
    {
        Task<CinemaSeat> AddCinemaSeatAsync(CinemaSeat cinemaSeat);
        Task DeleteCinemaSeatAsync(int id);
        Task<CinemaSeat> GetCinemaSeatByIdAsync(int id);
        Task<IEnumerable<CinemaSeatDto>> GetCinemaSeatsAsync();
        Task<CinemaSeat> UpdateCinemaSeatAsynce(int id, CinemaSeat cinemaSeat);
    }
}