﻿using BookMyShow.Core.Contracts.Infrastructure.Repository;
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
            var result = (await _dbConnection.QueryAsync<City>(query, new { id })).FirstOrDefault();
            //var result = await _dbConnection.QueryFirstAsync<City>(query, new {id= id});
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
            cityToBeUpdated.Name = city.Name;
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
