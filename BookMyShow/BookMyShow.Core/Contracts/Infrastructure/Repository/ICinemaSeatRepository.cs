using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface ICinemaSeatRepository
    {
        Task<CinemaSeat> AddCinemaSeatAsync(CinemaSeat cinemaSeat);
        Task DeleteCinemaSeatAsync(CinemaSeat cinemaSeat);
        Task<CinemaSeat> GetCinemaSeatAsync(int id);
        Task<IEnumerable<CinemaSeatDto>> GetCinemaSeatsAsync();
        Task<CinemaSeat> UpdateCinemaSeatAsynce( CinemaSeat cinemaSeat);
    }
}