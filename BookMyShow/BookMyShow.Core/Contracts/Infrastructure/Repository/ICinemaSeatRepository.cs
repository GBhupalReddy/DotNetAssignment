using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface ICinemaSeatRepository
    {
        Task<CinemaSeat> AddCinemaSeatAsync(CinemaSeat cinemaSeat);
        Task DeleteCinemaSeatAsync(int id);
        Task<CinemaSeat> GetCinemaSeatAsync(int id);
        Task<IEnumerable<CinemaSeat>> GetCinemaSeatsAsync();
        Task<CinemaSeat> UpdateCinemaSeatAsynce(int id, CinemaSeat cinemaSeat);
    }
}