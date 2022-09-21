using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using System.Data;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class CityRepository : ICityRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;
        public CityRepository(BookMyShowContext bookMyShowContext, IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
            _dbConnection = dbConnection;
        }

        // Get all city's
        public async Task<IEnumerable<CityDto>> GetCitysAsync()
        {
            var query = "select * from City";
            var result = await _dbConnection.QueryAsync<CityDto>(query);
            return result;

        }

        // Get city using id
        public async Task<City> GetCityAsync(int id)
        {
            var query = "select * from City where CityId = @id";
            var result = await _dbConnection.QueryFirstOrDefaultAsync<City>(query, new { id });
            return result;
           
        }
        public async Task<IEnumerable<CinemaDto>> GetCityInCinemaAsync(string cityName)
        {
            
            var query = "execute GetCityCinema  @cityName";
            var result = await _dbConnection.QueryAsync<CinemaDto>(query, new { cityName });
            return result;
        }

        // Add city
        public async Task<City> AddCityAsync(City city)
        {
            _bookMyShowContext.Cities.Add(city);
            await _bookMyShowContext.SaveChangesAsync();
            return city;
        }

        // Update city using id
        public async Task<City> UpdateCityAsynce( City city)
        {
          
            await _bookMyShowContext.SaveChangesAsync();
            return city;

        }

        //Delete city using id
        public async Task DeleteCityAsync(City city)
        {
            _bookMyShowContext.Cities.Remove(city);
            await _bookMyShowContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<MovieDto>> GetCityInMovie(string cityName)
        {
            
            var cityMovieQuery = "execute GetCityMovie @cityName";
            var cityMovieresult = await _dbConnection.QueryAsync<MovieDto>(cityMovieQuery,new {cityName});

            return cityMovieresult;

        }
        public async Task<IEnumerable<MovieDetailes>> GetCityCinemaMovieAsync(string cityName, string? cinemaName = null)
        {

            var CityCinemaMovieQuery = "execute GetCityCinemaMovie @cityName , @cinemaName ";

            var CityCinemaMovie = await _dbConnection.QueryAsync<MovieDetailes>(CityCinemaMovieQuery, new { cityName , cinemaName });
            return CityCinemaMovie;
        }
    }
}
