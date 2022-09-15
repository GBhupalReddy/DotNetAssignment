using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
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
            var result = (await _dbConnection.QueryFirstOrDefaultAsync<City>(query, new { id }));
            return result;
           
        }
        public async Task<IEnumerable<CinemaDto>> GetCinemaCityAsync(string cityName)
        {
            
            var result = await (from cinema in _bookMyShowContext.Cinemas
                                join city in _bookMyShowContext.Cities
                                on cinema.CityId equals city.CityId
                                where city.CityName.ToLower().Contains(cityName.ToLower())  
                                select new CinemaDto
                                {
                                    CinemaId = cinema.CinemaId,
                                    CinemaName = cinema.CinemaName,
                                    TotalCinemaHalls = cinema.TotalCinemaHalls,
                                    CityName = city.CityName,

                                }).ToListAsync();
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
        public async Task<IEnumerable<MovieDto>> GetMovieCity(string cityName)
        {
            var result = (await (from city in _bookMyShowContext.Cities
                                 join cinema in _bookMyShowContext.Cinemas
                                 on city.CityId equals cinema.CityId
                                 join cinemaHall in _bookMyShowContext.CinemaHalls
                                 on cinema.CinemaId equals cinemaHall.CinemaId
                                 join show in _bookMyShowContext.Shows
                                 on cinemaHall.CinemaHallId equals show.CinemaHallId
                                 join movie in _bookMyShowContext.Movies
                                 on show.MovieId equals movie.MovieId
                                 where city.CityName.ToLower().Contains(cityName)
                                 select new MovieDto
                                 {
                                     Tittle = movie.Tittle,
                                     Description = movie.Description,
                                     Duration = movie.Duration,
                                     ReleaseDate = movie.ReleaseDate,
                                     Language = movie.Language,
                                     Genre = movie.Genre,

                                 }).Distinct().ToListAsync());

            return result;

        }
    }
}
