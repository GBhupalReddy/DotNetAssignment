using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookMyShow.Infrastructure.Service
{
    public class CityinCinemaNameService : ICityinCinemaNameService
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;
        public CityinCinemaNameService(BookMyShowContext bookMyShowContext, IDbConnection dbConnection)
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
    }
}
