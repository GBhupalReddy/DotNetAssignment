using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using System.Data;
using Dapper;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class CinemaHallRepository : ICinemaHallRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;
        public CinemaHallRepository(BookMyShowContext bookMyShowContext , IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
            _dbConnection = dbConnection;
        }

        //Get all cinema halls
        public async Task<IEnumerable<CinemaHall>> GetCinemaHallsAsync()
        {
            var query = "select * from CinemaHall";
            var result =await _dbConnection.QueryAsync<CinemaHall>(query);
            return result;
        }

        // Get cinema hall using id
        public async Task<CinemaHall> GetCinemaHallAsync(int id)
        {
            var query = "select * from CinemaHall where CinemaHallId =@id";
            var result= await _dbConnection.QueryFirstAsync<CinemaHall>(query, new {id =id});
            return result;
        }

        // add cinema hall
        public async Task<CinemaHall> AddCinemaHallAsync(CinemaHall cinemaHall)
        {
            _bookMyShowContext.CinemaHalls.Add(cinemaHall);
            await _bookMyShowContext.SaveChangesAsync();
            return cinemaHall;
        }

        // update cinema hall using id
        public async Task<CinemaHall> UpdateCinemaHallAsynce(int id, CinemaHall cinemaHall)
        {
            var cinemaHallToBeUpdated = await GetCinemaHallAsync(id);
            cinemaHallToBeUpdated.Name = cinemaHall.Name;
            cinemaHallToBeUpdated.TotalSeats = cinemaHall.TotalSeats;
            cinemaHallToBeUpdated.CinemaId = cinemaHall.CinemaId;
            _bookMyShowContext.CinemaHalls.Update(cinemaHallToBeUpdated);
            await _bookMyShowContext.SaveChangesAsync();
            return cinemaHallToBeUpdated;

        }

        // delete cinema hall using id
        public async Task DeleteCinemaHallrAsync(int id)
        {
            var cinemaHall = await GetCinemaHallAsync(id);
            _bookMyShowContext.CinemaHalls.Remove(cinemaHall);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
