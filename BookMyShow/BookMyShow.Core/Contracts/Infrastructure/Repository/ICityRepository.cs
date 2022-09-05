﻿using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface ICityRepository
    {
        Task<City> AddCityAsync(City city);
        Task DeleteCityAsync(int id);
        Task<City> GetCityAsync(int id);
        Task<IEnumerable<City>> GetCitysAsync();
        Task<City> UpdateCityAsynce(int id, City city);
    }
}