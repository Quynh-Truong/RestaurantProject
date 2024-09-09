using RestaurantProject.Models;

namespace RestaurantProject.Data.Repos.IRepos
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<Reservation> FindReservationByIdAsync(int reservationId);
        Task UpdateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(Reservation reservation);

        Task<List<Table>> AvailableTablesForReservationAsync(DateTime reservationTimeStart, int noOfPeople);
        Task MakeReservationAsync(Reservation reservation);
    }
}
