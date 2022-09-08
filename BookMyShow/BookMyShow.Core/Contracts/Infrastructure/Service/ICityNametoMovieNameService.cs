using BookMyShow.Core.Dto;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface ICityinCinemaNameService
    {
        Task<IEnumerable<CinemaDto>> GetCinemasAsync(string cityName);
    }
}