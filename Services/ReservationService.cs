using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RestaurantProject.Data.Repos;
using RestaurantProject.Data.Repos.IRepos;
using RestaurantProject.Exceptions;
using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;
using RestaurantProject.Services.IServices;
using ValidationException = RestaurantProject.Exceptions.ValidationException;


namespace RestaurantProject.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITableRepository _tableRepository;

        public ReservationService(IReservationRepository reservationRepository, ICustomerRepository customerRepository, ITableRepository tableRepository)
        {
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
            _tableRepository = tableRepository;
        }
        public async Task DeleteReservationAsync(int reservationId)
        {
            var reservationToDelete = await _reservationRepository.FindReservationByIdAsync(reservationId);

            if (reservationToDelete == null)
            {
                throw new NotFoundException($"Reservation with ID {reservationId} not found.");

            }

            await _reservationRepository.DeleteReservationAsync(reservationToDelete);
        }

        public async Task<Reservation> FindReservationByIdAsync(int reservationId)
        {
            var reservationChosen = await _reservationRepository.FindReservationByIdAsync(reservationId);

            if (reservationChosen == null)
            {
                throw new DirectoryNotFoundException($"Reservation with ID {reservationId} not found.");
            }

            return reservationChosen;
        }

        public async Task<IEnumerable<ReservationDTO>> GetAllReservationsAsync()//fix view in swagger
        {
            var reservations = await _reservationRepository.GetAllReservationsAsync();

            var reservationList = reservations.Select(r => new ReservationDTO
            {
                ReservationId = r.ReservationId,
                CustomerId = r.CustomerId,
                TableId = r.ReservationId,
                ReservationStart = r.ReservationStart,
                ReservationEnd = r.ReservationEnd

            }).ToList();

            return reservationList;
        }

  
        //get by reservbyID shows right no of people, but not getallres
        public async Task UpdateReservationAsync([FromBody] int reservationId, ReservationDTO2 reservationDto)
        {
            var reservationToUpdate = await _reservationRepository.FindReservationByIdAsync(reservationId);

            if (reservationToUpdate == null)
            {
                throw new NotFoundException($"Reservation with ID {reservationId} not found.");
            }

            reservationToUpdate.CustomerId = reservationDto.CustomerId;
            reservationToUpdate.NoOfPeople = reservationDto.NoOfPeople;
            reservationToUpdate.TableId = reservationDto.TableId;
            reservationToUpdate.ReservationStart = reservationDto.ReservationStart;
            reservationToUpdate.ReservationEnd = reservationDto.ReservationEnd;

            await _reservationRepository.UpdateReservationAsync(reservationToUpdate);
        }

 


        public async Task<List<TableDTO>> AvailableTablesForReservationAsync(DateTime reservationTimeStart, int noOfPeople)
        {
            DateTime reservationTimeEnd = reservationTimeStart.AddMinutes(119);

            if (noOfPeople < 1)
            {
                throw new ValidationException("You have to be a company of at least one!");
            }

            

            var reservations = await _reservationRepository.GetAllReservationsAsync();


            var tables = await _tableRepository.GetAllTablesAsync();
  

            

            List<TableDTO> availableTables = new List<TableDTO>();


            //check if table has enough seats, add those that have enough seats(, else skip)
            foreach (var table in tables)
            {
                if (table.NoOfSeats < noOfPeople)
                {
                    continue;
                }

                //looping through all tables' reservations
                var reservationsForTables = reservations.Where(r => r.TableId == table.TableId);



                bool isPossibleToReserve = true;

                foreach (var reservation in reservationsForTables)
                {
                    //checking if reservation starts before existing reservation's end, and if
                    //reservation ends after existing reservation's start => making the time clash with existing res 
                    if (reservationTimeStart < reservation.ReservationEnd && reservationTimeEnd > reservation.ReservationStart)
                    {
                        isPossibleToReserve = false; 
                        break;
                    }
                }

                if (isPossibleToReserve)
                {
                    availableTables.Add(new TableDTO
                    {
                        TableId = table.TableId,
                        NoOfSeats = table.NoOfSeats
                    });
                }

            }

            if (!availableTables.Any())
            {
                await Console.Out.WriteLineAsync("No tables avaiable, please try another time.");
            }

   

            return availableTables;

        }


        public async Task MakeReservationAsync(ReservationDTO2 reservationDto)
        {
            var customer = await _customerRepository.FindCustomerByIdAsync(reservationDto.CustomerId);
            if (reservationDto.CustomerId == null)
            {
                throw new ValidationException("Cannot find customer ID.");
            }


            var availableTables = await _reservationRepository.AvailableTablesForReservationAsync(reservationDto.ReservationStart, reservationDto.NoOfPeople);


            var reservation = new Reservation
            {
                CustomerId = reservationDto.CustomerId,
                TableId = reservationDto.TableId,
                NoOfPeople = reservationDto.NoOfPeople,
                ReservationStart = reservationDto.ReservationStart,
                ReservationEnd = reservationDto.ReservationStart.AddMinutes(119)

            };

            await _reservationRepository.MakeReservationAsync(reservation);

        }
    }
}
