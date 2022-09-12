﻿using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Core.Enums;
using BookMyShow.Infrastructure.Data;
using Dapper;
using System.Data;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class CinemaSeatRepository : ICinemaSeatRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;
        public CinemaSeatRepository(BookMyShowContext bookMyShowContext,IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
            _dbConnection = dbConnection;
        }

        // Get all cinema seats
        public async Task<IEnumerable<CinemaSeatDto>> GetCinemaSeatsAsync()
        {
            var query = "select * from CinemaSeat";
            var result = await _dbConnection.QueryAsync<CinemaSeatDto>(query);
            return result;
        }

        // Get cinema seat using id
        public async Task<CinemaSeat> GetCinemaSeatAsync(int id)
        {
            var query = "select * from CinemaSeat where CinemaSeatId = @id";
            var result = (await _dbConnection.QueryFirstOrDefaultAsync<CinemaSeat>(query, new { id }));
            return result;
        }

        // Add cinema seat
        public async Task<CinemaSeat> AddCinemaSeatAsync(CinemaSeat cinemaSeat)
        {
            var result = cinemaSeat.SeatNumber;
            int[] thirdClass = { 1, 2 };
            int[] secondClass = { 3, 4 };
            int[] firstClass = { 5, 6 };
            bool thirdClassReault = Array.Exists(thirdClass, element => element == result);
            if (thirdClassReault)
            {
                cinemaSeat.Type = (int)CinemaSeatType.ThirdClass;
            }
            bool secondClassResult = Array.Exists(secondClass, element => element == result);
            if (secondClassResult)
            {
                cinemaSeat.Type = (int)CinemaSeatType.SecondClass;
            }

            bool firstClassresult = Array.Exists(firstClass, element => element == result);
            if (firstClassresult)
            {
                cinemaSeat.Type = (int)CinemaSeatType.FirstClass;
            }
            _bookMyShowContext.CinemaSeats.Add(cinemaSeat);
            await _bookMyShowContext.SaveChangesAsync();
            return cinemaSeat;
        }

        //Update cinema seat using id
        public async Task<CinemaSeat> UpdateCinemaSeatAsynce(int id, CinemaSeat cinemaSeat)
        {
            var cinemaSeatToBeUpdated = await GetCinemaSeatAsync(id);
            cinemaSeatToBeUpdated.SeatNumber = cinemaSeat.SeatNumber;
            cinemaSeatToBeUpdated.Type = cinemaSeat.Type;
            cinemaSeatToBeUpdated.CinemaHallId = cinemaSeat.CinemaHallId;
            _bookMyShowContext.CinemaSeats.Update(cinemaSeatToBeUpdated);
            await _bookMyShowContext.SaveChangesAsync();
            return cinemaSeatToBeUpdated;

        }

        //Delete cinema seat using id
        public async Task DeleteCinemaSeatAsync(int id)
        {
            var cinemaSeat = await GetCinemaSeatAsync(id);
            _bookMyShowContext.CinemaSeats.Remove(cinemaSeat);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
