using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class CinemaSeatRepository : ICinemaSeatRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        public CinemaSeatRepository()
        {
            _bookMyShowContext = new BookMyShowContext();
        }


        public async Task<IEnumerable<CinemaSeat>> GetCinemaSeatsAsync()
        {
            return await (from cinemaSeat in _bookMyShowContext.CinemaSeats

                          select new CinemaSeat
                          {
                              CinemaSeatId = cinemaSeat.CinemaSeatId,
                              SeatNumber = cinemaSeat.SeatNumber,
                              Type = cinemaSeat.Type,
                              CinemaHallId = cinemaSeat.CinemaHallId

                          }).ToListAsync();

        }

        public async Task<CinemaSeat> GetCinemaSeatAsync(int id)
        {
            return await _bookMyShowContext.CinemaSeats.FindAsync(id);
        }

        public async Task<CinemaSeat> AddCinemaSeatAsync(CinemaSeat cinemaSeat)
        {
            _bookMyShowContext.CinemaSeats.Add(cinemaSeat);
            await _bookMyShowContext.SaveChangesAsync();
            return cinemaSeat;
        }
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

        public async Task DeleteCinemaSeatAsync(int id)
        {
            var cinemaSeat = await GetCinemaSeatAsync(id);
            _bookMyShowContext.CinemaSeats.Remove(cinemaSeat);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
