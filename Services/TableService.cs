using RestaurantProject.Data.Repos;
using RestaurantProject.Data.Repos.IRepos;
using RestaurantProject.Exceptions;
using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;
using RestaurantProject.Services.IServices;
using System.ComponentModel.DataAnnotations;
using ValidationException = RestaurantProject.Exceptions.ValidationException;

namespace RestaurantProject.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;//repo dep inj

        public TableService(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }
        public async Task AddTableAsync(TableDTO2 tableDto)
        {

            var tableAdded = new Table
            {
                NoOfSeats = tableDto.NoOfSeats,
                Availability = tableDto.Availability
            };

            await _tableRepository.AddTableAsync(tableAdded);
        }

        public async Task DeleteTableAsync(int tableId)
        {
            var table = await _tableRepository.FindTableByIdAsync(tableId);
            
            if (table == null)
            {
                throw new NotFoundException($"Table with ID {tableId} not found.");
            }

            await _tableRepository.DeleteTableAsync(table);
        }

        public async Task<Table> FindTableByIdAsync(int tableId)//CHECJ
        {
            var tableChosen = await _tableRepository.FindTableByIdAsync(tableId);
            
            if ( tableChosen == null )
            {
                throw new NotFoundException($"Table with ID {tableId} not found.");
            }

            return tableChosen;
        }

        //public async Task<Table> FindTableByTableNoAsync(int tableNo)
        //{
        //    var tableChosen = await _tableRepository.FindTableByTableNoAsync(tableNo);

        //    return tableChosen;
        //}

        public async Task<IEnumerable<TableDTO>> GetAllTablesAsync()
        {
            var tables = await _tableRepository.GetAllTablesAsync();

            var tableList = tables.Select(t => new TableDTO
            {
                TableId = t.TableId,
                NoOfSeats = t.NoOfSeats
            }).ToList();

            return tableList;
        }


        public async Task UpdateTableAsync(int tableId, TableDTO2 tableDto)
        {
            var chosenTable = await _tableRepository.FindTableByIdAsync(tableId);

            if (chosenTable == null)
            {
                throw new NotFoundException("Table with ID {tableId} not found.");
            }

            chosenTable.NoOfSeats = tableDto.NoOfSeats;
            chosenTable.Availability = tableDto.Availability;

            await _tableRepository.UpdateTableAsync(chosenTable);
        }
    }
}
