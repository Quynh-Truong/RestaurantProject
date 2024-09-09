using Microsoft.EntityFrameworkCore;
using RestaurantProject.Data.Repos.IRepos;
using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;

namespace RestaurantProject.Data.Repos
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RestaurantContext _context;

        public ReservationRepository(RestaurantContext context)
        {
            _context = context;
        }

        public async Task DeleteReservationAsync(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

        }

        public async Task<Reservation> FindReservationByIdAsync(int reservationId)
        {
            return await _context.Reservations.SingleOrDefaultAsync(r => r.ReservationId == reservationId);
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _context.Reservations.ToListAsync();
        }


        public async Task UpdateReservationAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }


        public async Task<List<Table>> AvailableTablesForReservationAsync(DateTime reservationTimeStart, int noOfPeople)
        {
            return await _context.Tables.ToListAsync();
        }



        public async Task MakeReservationAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

    }
}
