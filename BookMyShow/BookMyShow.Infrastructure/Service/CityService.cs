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
            return await _cityRepository.GetCitysAsync();
        }

        // Get city using id
        public async Task<City> GetCityByIdAsync(int id)
        {
            return await _cityRepository.GetCityAsync(id);

        }
        public async Task<IEnumerable<CinemaDto>> GetCinemaByNameAsync(string cityName)
        {

            return await _cityRepository.GetCinemasAsync(cityName);
        }

        // Add city
        public async Task<City> AddCityAsync(City city)
        {
            return await _cityRepository.AddCityAsync(city);
        }

        // Update city using id
        public async Task<City> UpdateCityAsynce(int id, City city)
        {
            var cityToBeUpdated = await GetCityByIdAsync(id);
            cityToBeUpdated.CityName = city.CityName;
            cityToBeUpdated.State = city.State;
            cityToBeUpdated.ZipCode = city.ZipCode;

            return await _cityRepository.UpdateCityAsynce(cityToBeUpdated);
        }

        //Delete city using id
        public async Task DeleteCityAsync(int id)
        {
            var city = await GetCityByIdAsync(id);
            await _cityRepository.DeleteCityAsync(city);
        }

        public async Task<IEnumerable<MovieDto>> GetMovieCity(string cityName)
        {
            var result = await _cityRepository.GetMovieCity(cityName);
            return result;
        }
    }
}
