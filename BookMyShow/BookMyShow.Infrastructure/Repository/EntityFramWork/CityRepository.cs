using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class CityRepository : ICityRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        public CityRepository()
        {
            _bookMyShowContext = new BookMyShowContext();
        }


        public async Task<IEnumerable<City>> GetCitysAsync()
        {
            return await (from city in _bookMyShowContext.Cities

                          select new City
                          {
                              CityId = city.CityId,
                              Name = city.Name,
                              State = city.State,
                              ZipCode = city.ZipCode,
                          }).ToListAsync();

        }

        public async Task<City> GetCityAsync(int id)
        {
            return await _bookMyShowContext.Cities.FindAsync(id);
        }

        public async Task<City> AddCityAsync(City city)
        {
            _bookMyShowContext.Cities.Add(city);
            await _bookMyShowContext.SaveChangesAsync();
            return city;
        }
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

        public async Task DeleteCityAsync(int id)
        {
            var city = await GetCityAsync(id);
            _bookMyShowContext.Cities.Remove(city);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
