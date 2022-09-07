using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Infrastructure.Service
{
    public class CityNametoMovieNameService : ICityNametoMovieNameService
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;
        public CityNametoMovieNameService(BookMyShowContext bookMyShowContext, IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<CinemaDto>> GetCinemasAsync(string cityName)
        {
            //var qury = "  select cin.CinemaId,cin.Name,cin.TotalCinemaHalls,cin.CityId from Cinema cin inner join City cit on cin.CityId=cit.CityId where cit.Name = @cityName;";
            //var result = await _dbConnection.QueryAsync<Cinema>(qury, new { cityName = cityName });
            var result = await (from cinema in _bookMyShowContext.Cinemas
                                join city in _bookMyShowContext.Cities
                                on cinema.CityId equals city.CityId
                                where city.Name == cityName
                                select new CinemaDto
                                {
                                    CinemaId = cinema.CinemaId,
                                    Name = cinema.Name,
                                    TotalCinemaHalls = cinema.TotalCinemaHalls,
                                    CityId = cinema.CityId,
                                   

                                }).ToListAsync();
            return result;
        }
    }
}
