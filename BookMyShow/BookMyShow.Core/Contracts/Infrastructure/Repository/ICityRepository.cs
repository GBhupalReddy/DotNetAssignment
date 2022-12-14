using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface ICityRepository
    {
        Task<City> AddCityAsync(City city);
        Task DeleteCityAsync(City city);
        Task<City> GetCityAsync(int id);
        Task<IEnumerable<CinemaDto>> GetCinemaInCityAsync(string cityName);
        Task<IEnumerable<CityDto>> GetCitysAsync();
        Task<City> UpdateCityAsynce( City city);
        Task<IEnumerable<Movie>> GetMovieInCity(string cityName,string? laguage = null, string? genre = null);
        Task<IEnumerable<MovieDetailes>> GetCityCinemaMovieAsync(string cityName, string? cinemaName = null);
    }
}