using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;

namespace RestaurantProject.Services.IServices
{
    public interface ITableService
    {
        Task<IEnumerable<TableShowDTO>> GetAllTablesAsync();
        Task AddTableAsync(TableCreateUpdateDTO tableDto);
        Task<Table> FindTableByIdAsync(int tableId);
        Task UpdateTableAsync(int tableId, TableCreateUpdateDTO tableDto);
        Task DeleteTableAsync(int tableId);
        //Task<Table> FindTableByTableNoAsync(int tableNo);

    }
}
