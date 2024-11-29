using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantProject.Exceptions;
using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;
using RestaurantProject.Services;
using RestaurantProject.Services.IServices;
using ValidationException = RestaurantProject.Exceptions.ValidationException;


namespace RestaurantProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

       
        [HttpGet("getAllReservations")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("getReservation/{reservationId}")]
        public async Task<ActionResult<Reservation>> FindReservationById(int reservationId)
        {
            if (reservationId == null)
            {
                return BadRequest("Input reservation ID, please.");
            }

            var reservation = await _reservationService.FindReservationByIdAsync(reservationId);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

       
        [HttpPut("updateReservation/{reservationId}")]
        public async Task<ActionResult> UpdateReservation(int reservationId, [FromBody] ReservationUpdateDTO reservationDto)
        {
            if (reservationId == null)
            {
                return BadRequest("Input reservation ID, please.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _reservationService.UpdateReservationAsync(reservationId, reservationDto);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in handling request");
            }
        }


     
        [HttpDelete("deleteReservation/{reservationId}")]
        public async Task<ActionResult> DeleteReservation(int reservationId)
        {
            if (reservationId == null)
            {
                return BadRequest("Input reservation ID, please.");
            }

            try
            {
                await _reservationService.DeleteReservationAsync(reservationId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in handling request");
            }
        }

        [HttpGet("getTakenTablesDuringChosenTime")]
        public async Task<ActionResult> GetTakenTables(DateTime reservationStart)
        {
            try
            {
                var takenTables = await _reservationService.GetTakenTablesDuringChosenTimeAsync(reservationStart);
                return Ok(takenTables);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in handling request");
            }
        }

        [HttpGet("getAvailableTablesForReservation")]
        public async Task<ActionResult> AvailableTablesForReservation(DateTime reservationStart, int noOfPeople)
        {
            try
            {
                var availableTables = await _reservationService.AvailableTablesForReservationAsync(reservationStart, noOfPeople);
                return Ok(availableTables);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in handling request");
            }
        }

        [HttpPost("makeReservation")]
        public async Task<ActionResult> MakeReservation([FromBody] ReservationMakeDTO reservationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //check if there is a customerId used
            if (reservationDto.CustomerId == null)
            {
                return BadRequest("No customer ID found");
            }

            try
            {
                await _reservationService.MakeReservationAsync(reservationDto);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in handling request");
            }
        }
    }
}
