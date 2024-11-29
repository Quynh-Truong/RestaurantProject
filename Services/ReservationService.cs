using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RestaurantProject.Data;
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
        private readonly RestaurantContext _context;

        public ReservationService(IReservationRepository reservationRepository, ICustomerRepository customerRepository, ITableRepository tableRepository, RestaurantContext context)
        {
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
            _tableRepository = tableRepository;
            _context = context;
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

        public async Task<Reservation> FindReservationByIdAsync(int reservationId)//fixa!!!
        {
            var reservationChosen = await _reservationRepository.FindReservationByIdAsync(reservationId);

            if (reservationChosen == null)
            {
                throw new DirectoryNotFoundException($"Reservation with ID {reservationId} not found.");
            }

            return reservationChosen;
        }

        public async Task<IEnumerable<ReservationsShowDTO>> GetAllReservationsAsync()
        {
            var reservations = await _reservationRepository.GetAllReservationsAsync();


            var reservationList = reservations.Select(r => new ReservationsShowDTO
            {
                ReservationId = r.ReservationId,
                CustomerId = r.CustomerId,
                FirstName = r.Customer.FirstName,
                LastName = r.Customer.LastName,
                PhoneNo = r.Customer.PhoneNo,
                TableId = r.TableId,
                ReservationStart = r.ReservationStart,
                ReservationEnd = r.ReservationEnd,
                NoOfPeople = r.NoOfPeople

            }).ToList();

            return reservationList;
        }


       
        public async Task UpdateReservationAsync(int reservationId, ReservationUpdateDTO reservationDto)
        {
            var reservationToUpdate = await _reservationRepository.FindReservationByIdAsync(reservationId);

            if (reservationToUpdate == null)
            {
                throw new NotFoundException($"Reservation with ID {reservationId} not found.");
            }

            //reservationToUpdate.CustomerId = reservationDto.CustomerId;

            reservationToUpdate.NoOfPeople = reservationDto.NoOfPeople;
            reservationToUpdate.TableId = reservationDto.TableId;
            reservationToUpdate.ReservationStart = reservationDto.ReservationStart;
            //reservationToUpdate.ReservationEnd = reservationDto.ReservationEnd;

            await _reservationRepository.UpdateReservationAsync(reservationToUpdate);
        }




        public async Task<List<TableShowDTO>> AvailableTablesForReservationAsync(DateTime reservationStart, int noOfPeople)
        {
            DateTime reservationTimeEnd = reservationStart.AddMinutes(120);

            if (noOfPeople < 1)
            {
                throw new ValidationException("You have to be a company of at least one!");
            }



            var takenTables = await _reservationRepository.GetTakenTablesDuringChosenTimeAsync(reservationStart);


            var tables = await _tableRepository.GetAllTablesAsync();



            List<TableShowDTO> availableTables = new List<TableShowDTO>();

            Console.WriteLine("Taken Tables:");
            foreach (var taken in takenTables)
            {
                Console.WriteLine($"TableId: {taken.TableId}, NoOfPeople: {taken.NoOfPeople}");
            }

            //check if table has enough seats, add those that have enough seats(, else skip)
            foreach (var table in tables)
            {
                if (table.NoOfSeats >= noOfPeople)
                {

                    if (!takenTables.Any(t => t.TableId == table.TableId))
                    {
                        availableTables.Add(new TableShowDTO
                        {
                            TableId = table.TableId,
                            NoOfSeats = table.NoOfSeats

                        });
                    }
                    else
                    {
                        Console.WriteLine($"TableId: {table.TableId} is taken.");
                    }
                }

                else
                {
                    Console.WriteLine($"TableId: {table.TableId} does not have enough seats.");
                }
  


            }

         

            Console.WriteLine("Available Tables:");
            foreach (var availableTable in availableTables)
            {
                Console.WriteLine($"TableId: {availableTable.TableId}, NoOfSeats: {availableTable.NoOfSeats}");
            }


            return availableTables;


        }


        public async Task MakeReservationAsync(ReservationMakeDTO reservationDto)
        {
            //check if the phone no already exists = customer already exists
            var existingCustomer = await _customerRepository.FindCustomerByPhoneNoAsync(reservationDto.PhoneNo);
            Customer customer;


            //if customer does not exist, add them into DB
            if (existingCustomer == null)
            {
                var newCustomer = new Customer
                {
                    FirstName = reservationDto.FirstName,
                    LastName = reservationDto.LastName,
                    PhoneNo = reservationDto.PhoneNo
                };
                await _customerRepository.AddCustomerAsync(newCustomer);
                //set the new ID to the new customer
                reservationDto.CustomerId = newCustomer.CustomerId;
            }
            else
            {
                reservationDto.CustomerId = existingCustomer.CustomerId;
            }



            //var customer = await _customerRepository.FindCustomerByIdAsync(reservationDto.CustomerId);
            //if (reservationDto.CustomerId == null)
            //{
            //    throw new ValidationException("Cannot find customer ID.");
            //}


            var availableTables = await _reservationRepository.AvailableTablesForReservationAsync(reservationDto.ReservationStart, reservationDto.NoOfPeople);

            if (!availableTables.Any())
            {
                throw new NotFoundException("No tables found for the chosen time. Please, try another one.");
            }

            var selectedTable = availableTables.FirstOrDefault();

            var reservation = new Reservation
            {
                CustomerId = reservationDto.CustomerId,
                //input found table
                TableId = selectedTable.TableId,
                NoOfPeople = reservationDto.NoOfPeople,
                ReservationStart = reservationDto.ReservationStart,
                ReservationEnd = reservationDto.ReservationStart.AddMinutes(120)

            };

            await _reservationRepository.MakeReservationAsync(reservation);

        }

        //CHECK?
        public async Task<List<ReservationsShowDTO>> GetTakenTablesDuringChosenTimeAsync(DateTime reservationStart)
        {
            DateTime reservationTimeEnd = reservationStart.AddMinutes(120);

            return await _context.Reservations
                //existing res ends when new customer wants to eat/existing res starts before new customer's time ends
                .Where(r => r.ReservationEnd > reservationStart && r.ReservationStart < reservationTimeEnd)
                //.Where(r => r.ReservationStart <= reservationStart.AddMinutes(120) && r.ReservationEnd >= reservationStart)
                .Select(r => new ReservationsShowDTO
                {
                    ReservationId = r.ReservationId,
                    CustomerId = r.CustomerId,
                    NoOfPeople = r.NoOfPeople,
                    TableId = r.TableId,
                    ReservationStart = r.ReservationStart/*,*/
                    //ReservationEnd = r.ReservationEnd
                })
                .ToListAsync();

        }
    }
}
