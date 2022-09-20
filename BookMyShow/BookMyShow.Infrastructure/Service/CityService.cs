using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Infrastructure.Service
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;

        }
        public async Task<IEnumerable<CityDto>> GetCitysAsync()
        {
            var cities = await _cityRepository.GetCitysAsync();
            return cities;
        }

        // Get city using id
        public async Task<City> GetCityByIdAsync(int id)
        {
            var city = await _cityRepository.GetCityAsync(id);
            return city;

        }
        public async Task<IEnumerable<CinemaDto>> GetCinemaCitysync(string cityName)
        {

            var city = await _cityRepository.GetCinemaCityAsync(cityName);
            return city;
        }

        // Add city
        public async Task<City> AddCityAsync(City city)
        {
            var reusult = await _cityRepository.AddCityAsync(city);
            return reusult;
        }

        // Update city using id
        public async Task<City> UpdateCityAsynce(int id, City city)
        {
            var cityToBeUpdated = await GetCityByIdAsync(id);
            cityToBeUpdated.CityName = city.CityName;
            cityToBeUpdated.State = city.State;
            cityToBeUpdated.ZipCode = city.ZipCode;

            var result = await _cityRepository.UpdateCityAsynce(cityToBeUpdated);
            return result;
        }

        //Delete city using id
        public async Task DeleteCityAsync(int id)
        {
            var city = await GetCityByIdAsync(id);
            await _cityRepository.DeleteCityAsync(city);
        }

        public async Task<IEnumerable<MovieDto>> GetCityMovie(string cityName)
        {
            var result = await _cityRepository.GetCityMovie(cityName);
            return result;
        }
        public async Task<IEnumerable<MovieDetailes>> GetCityCinemaMovieAsync(string cityName, string? cinemaName = null)
        {
            var cityCinemaMovie = await _cityRepository.GetCityCinemaMovieAsync(cityName,cinemaName);
            return cityCinemaMovie;
         }
    }
}
