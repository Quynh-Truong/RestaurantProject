using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;

namespace RestaurantProject.Services.IServices
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationsShowDTO>> GetAllReservationsAsync();
        Task MakeReservationAsync(ReservationMakeDTO reservationDto);
        Task<Reservation> FindReservationByIdAsync(int reservationId);
        Task UpdateReservationAsync(int reservationId, ReservationUpdateDTO reservationDto);
        Task DeleteReservationAsync(int  reservationId);
        Task<List<TableShowDTO>> AvailableTablesForReservationAsync(DateTime reservationStart, int noOfPeople);

        Task<List<ReservationsShowDTO>> GetTakenTablesDuringChosenTimeAsync(DateTime reservationStart);
    }
}
