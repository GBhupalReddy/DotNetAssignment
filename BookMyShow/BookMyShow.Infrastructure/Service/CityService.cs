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
        public async Task<IEnumerable<CinemaDto>> GetCinemaInCityAsync(string cityName)
        {

            var cityCinema = await _cityRepository.GetCinemaInCityAsync(cityName);
            return cityCinema;
        }

        // Add city
        public async Task<City> AddCityAsync(City city)
        {
            var cityresult = await _cityRepository.AddCityAsync(city);
            return cityresult;
        }

        // Update city using id
        public async Task<City> UpdateCityAsynce(int id, City city)
        {
            var cityToBeUpdated = await GetCityByIdAsync(id);
            cityToBeUpdated.CityName = city.CityName;
            cityToBeUpdated.State = city.State;
            cityToBeUpdated.ZipCode = city.ZipCode;

            var cityresult = await _cityRepository.UpdateCityAsynce(cityToBeUpdated);
            return cityresult;
        }

        //Delete city using id
        public async Task DeleteCityAsync(int id)
        {
            var city = await GetCityByIdAsync(id);
            await _cityRepository.DeleteCityAsync(city);
        }

        public async Task<IEnumerable<movieVDto>> GetMovieInCity(string cityName, string? language = null, string? genre = null)
        {
            var cityMovie = await _cityRepository.GetMovieInCity(cityName,language, genre);
            var moviedata = from movie in cityMovie
                            select new movieVDto()
                            {
                               Tittle=movie.Tittle,
                               Description=movie.Description,
                               Language=movie.Language,
                               Genre=movie.Genre,
                               ImgPath=movie.ImgPath,
                               Country=movie.Country,
                               Duration=movie.Duration,
                               ReleaseDate=movie.ReleaseDate.ToString("yyyy-MM-dd"),
                            };
            return moviedata;
        }
        public async Task<IEnumerable<MovieDetailes>> GetCityCinemaMovieAsync(string cityName, string? cinemaName = null)
        {
            var cityCinemaMovie = await _cityRepository.GetCityCinemaMovieAsync(cityName,cinemaName);
            return cityCinemaMovie;
         }
    }
}
