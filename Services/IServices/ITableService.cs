using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;

namespace RestaurantProject.Services.IServices
{
    public interface ITableService
    {
        Task<IEnumerable<TableDTO>> GetAllTablesAsync();
        Task AddTableAsync(TableDTO2 tableDto);
        Task<Table> FindTableByIdAsync(int tableId);
        Task UpdateTableAsync(int tableId, TableDTO2 tableDto);
        Task DeleteTableAsync(int tableId);
        //Task<Table> FindTableByTableNoAsync(int tableNo);

    }
}
