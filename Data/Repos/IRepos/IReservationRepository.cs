using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;

namespace RestaurantProject.Data.Repos.IRepos
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<Reservation> FindReservationByIdAsync(int reservationId);
        Task UpdateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(Reservation reservation);

        Task<List<Table>> AvailableTablesForReservationAsync(DateTime reservationStart, int noOfPeople);
        Task MakeReservationAsync(Reservation reservation);
        Task<List<Reservation>> GetTakenTablesDuringChosenTimeAsync(DateTime reservationStart);
    }
}
