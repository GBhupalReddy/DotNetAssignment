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
            var result = (await _dbConnection.QueryAsync<City>(query, new { id })).FirstOrDefault();
            //var result = await _dbConnection.QueryFirstAsync<City>(query, new {id= id});
            return result;
           
        }
        public async Task<IEnumerable<CinemaDto>> GetCinemasAsync(string cityName)
        {
            //var qury = "  select cin.CinemaId,cin.Name,cin.TotalCinemaHalls,cin.CityId from Cinema cin inner join City cit on cin.CityId=cit.CityId where cit.Name = @cityName;";
            //var result = await _dbConnection.QueryAsync<Cinema>(qury, new { cityName = cityName });
            var result = await (from cinema in _bookMyShowContext.Cinemas
                                join city in _bookMyShowContext.Cities
                                on cinema.CityId equals city.CityId
                                where city.CityName == cityName
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
        public async Task<City> UpdateCityAsynce(int id, City city)
        {
            var cityToBeUpdated = await GetCityAsync(id);
            cityToBeUpdated.CityName = city.CityName;
            cityToBeUpdated.State = city.State;
            cityToBeUpdated.ZipCode = city.ZipCode;
            _bookMyShowContext.Cities.Update(cityToBeUpdated);
            await _bookMyShowContext.SaveChangesAsync();
            return cityToBeUpdated;

        }

        //Delete city using id
        public async Task DeleteCityAsync(int id)
        {
            var city = await GetCityAsync(id);
            _bookMyShowContext.Cities.Remove(city);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
