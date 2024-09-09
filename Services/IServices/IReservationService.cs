using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;

namespace RestaurantProject.Services.IServices
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDTO>> GetAllReservationsAsync();
        Task MakeReservationAsync(ReservationDTO2 reservationDto);
        Task<Reservation> FindReservationByIdAsync(int reservationId);
        Task UpdateReservationAsync(int reservationId, ReservationDTO2 reservationDto);
        Task DeleteReservationAsync(int  reservationId);
        Task<List<TableDTO>> AvailableTablesForReservationAsync(DateTime reservationTimeStart, int noOfPeople);
    }
}
